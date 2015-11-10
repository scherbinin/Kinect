using System;
using System.Collections.Generic;
using System.Globalization;
using BvhConverter.SkeletonMerge;
using BvhConverter.Stuff;
using BvhConverter_new;
using BvhConverter_new.Mappers;
using BvhConverter_new.Stuff;
using Microsoft.Kinect;
using JointType = BvhConverter_new.Stuff.JointType;

namespace BvhConverter
{
    public static class BvhCreater
    {
        public static string GetBvhFile(List<Skeleton> skeletons1, List<Skeleton> skeletons2 = null)
        {
            string bvhFile = "";

            //Выполним мерж
            SkeletMerger.MergeJointPointsData(skeletons1, skeletons2);

            if (skeletons1.Count > 2)
            {
                //Построим иерархию файла
                var treeRoot = BuildTree(skeletons1[0].Joints);

                //Сначала выполним все необходимые преобразования координат в дереве
                //treeRoot.ApllyCoordinateConversion();
                treeRoot.ApllyConstCoord();//Прибить константные координаты

                //Получим текст иерархической части файла
                bvhFile = GetHierarchyPart(treeRoot);
                skeletons1.RemoveAt(0);

                //Текст технических данных
                bvhFile += GetMiddlePart(skeletons1.Count - 1);

                //Координатные фреймы

                //Если второе устройство также работало
                if (skeletons2 != null)
                {
                    var enumerator1 = skeletons1.GetEnumerator();
                    var enumerator2 = skeletons2.GetEnumerator();

                    while (enumerator1.MoveNext())
                    {
                        enumerator2.MoveNext();

                        var skelet1 = (Skeleton)enumerator1.Current;
                        var skelet2 = (Skeleton)enumerator2.Current;

                        if (skelet1 != null && skelet2 != null)
                        {
                            if (skelet1.TrackingState != SkeletonTrackingState.Tracked)
                                bvhFile += GetCoordinateFrame(skelet2, treeRoot);

                            if (skelet2.TrackingState != SkeletonTrackingState.Tracked)
                                bvhFile += GetCoordinateFrame(skelet1, treeRoot);

                            if (skelet1.TrackingState != SkeletonTrackingState.Tracked &&
                                skelet2.TrackingState != SkeletonTrackingState.Tracked)
                                bvhFile += GetCoordinateFrame(skelet1, treeRoot, skelet2);
                        }
                    }

                }
                else
                {//Если второе устройство не работало, то все просто
                    
                    foreach (var skelet in skeletons1)
                        if (skelet.TrackingState == SkeletonTrackingState.Tracked)
                            bvhFile += GetCoordinateFrame(skelet, treeRoot);
                }


            }

            return bvhFile;
        }

        /// <summary>
        /// Печать одной строки координат - фрейма
        /// </summary>
        /// <param name="skelet"></param>
        /// <param name="root"></param>
        /// <param name="skelet2"></param>
        /// <returns></returns>
        private static string GetCoordinateFrame(Skeleton skelet, HierarchyNode root, Skeleton skelet2 = null)
        {
            string str = "";
            var enumerator = root.GetEnumerator();
            enumerator.Reset();

            //Для рутовой точки получим позицию
            str += root.Point.PrintCoordinate();

            do
            {
                if (enumerator.Current.GetTypeNode() == ConstBodyPoints.Site)
                    continue;

                if (skelet2 != null)
                    str +=MathHelper.GetElersAnglesZXY(
                            BoneMapperBvhToKinect.GetBoneMatrixByBvhName(skelet, enumerator.Current),
                            BoneMapperBvhToKinect.GetBoneMatrixByBvhName(skelet2, enumerator.Current));
                else
                    str +=MathHelper.GetElersAnglesZXY(BoneMapperBvhToKinect.GetBoneMatrixByBvhName(skelet,
                                                                                                  enumerator.Current));

            } while (enumerator.MoveNext());


            return (str += Environment.NewLine);

        }

        /// <summary>
        /// Сформировать иерархическую часть файла
        /// </summary>
        /// <returns></returns>
        private static string GetHierarchyPart(HierarchyNode root)
        {
            var str = ConstsKeyWords.Hierarchy + Environment.NewLine;

            //Обойдем его с помощью стандартного енумератора
            foreach (var node in root)
                str += node.ToString();

            str += ConstsKeyWords.RightBrace + Environment.NewLine;

            return str;
        }

        /// <summary>
        /// Сформировать промежуточную часть с настройками
        /// </summary>
        /// <param name="framesValue"></param>
        /// <returns></returns>
        private static string GetMiddlePart(int framesValue)
        {
            var str = "";

            str += ConstsKeyWords.Motion + Environment.NewLine;
            str += ConstsKeyWords.Frames + " " + framesValue.ToString(CultureInfo.InvariantCulture) + Environment.NewLine;
            str += ConstsKeyWords.FrameTime + Environment.NewLine;

            return str;
        }

        /// <summary>
        /// Описание иерархии
        /// </summary>
        /// <param name="joints"></param>
        /// <returns></returns>
        private static HierarchyNode BuildTree(JointCollection joints)
        {
            var root = new HierarchyNode(new SkeletonPoint(), JointType.HipCenter, ConstBodyPoints.Hips);
            HierarchyNode node = null;

            //Ветка: Корень - Левая часть рутового креста - Левое бедро - левая коленка - левая лодыжка - Энд сайт(Также пусть будет лодыжка)
            node = root.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.CrossLeft, ConstBodyPoints.LeftCross));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.HipLeft, ConstBodyPoints.LeftHip));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.KneeLeft, ConstBodyPoints.LeftKnee));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.AnkleLeft, ConstBodyPoints.LeftAnkle));
            node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.FootLeft, ConstBodyPoints.Site));

            //Ветка: Корень - Правое бедро - правая коленка - правая лодыжка - Энд сайт
            node = root.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.CrossRight, ConstBodyPoints.RightCross));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.HipRight, ConstBodyPoints.RightHip));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.KneeRight, ConstBodyPoints.RightKnee));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.AnkleRight, ConstBodyPoints.RightAnkle));
            node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.FootRight, ConstBodyPoints.Site));

            //Ветка: Корень - Верхняя часть рутового креста - Поясница - Левая ключица - Левое плечо - Левый локоть - Левая кисть - Энд сайт
            node = root.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.CrossUp, ConstBodyPoints.UpCross));
            var chestNode = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.Spine, ConstBodyPoints.Chest));

            node = chestNode.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ShoulderCenter, ConstBodyPoints.LeftCollar));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ShoulderLeft, ConstBodyPoints.LeftShoulder));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ElbowLeft, ConstBodyPoints.LeftElbow));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.WristLeft, ConstBodyPoints.LeftWrist));
            node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.HandLeft, ConstBodyPoints.Site));

            //Ветка:Поясница - Левая ключица - Левое плечо - Левый локоть - Левая кисть - Энд сайт
            node = chestNode.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ShoulderCenter, ConstBodyPoints.RightCollar));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ShoulderRight, ConstBodyPoints.RightShoulder));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ElbowRight, ConstBodyPoints.RightElbow));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.WristRight, ConstBodyPoints.RightWrist));
            node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.HandRight, ConstBodyPoints.Site));

            //Ветка: Шея - Средняя точка головы - Энд Сайт
            node = chestNode.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.ShoulderCenter, ConstBodyPoints.Neck));
            node = node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.Head, ConstBodyPoints.Head));
            node.AddChild(new HierarchyNode(new SkeletonPoint(), JointType.Head, ConstBodyPoints.Site));

            return root;
        }
    }
}
