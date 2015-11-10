using System;
using BvhConverter_new;
using Microsoft.Kinect;
using JointType = BvhConverter_new.Stuff.JointType;

namespace BvhConverter.Stuff
{
    public static class CoordinateConversion
    {
        private static float _multiNamberX = 30;
        private static float _multiNamberY = 30;
        private static float _multiNamberZ = 0;

        /// <summary>
        /// Пересчет абсолютных координат на иерархически зависимые смещения
        /// </summary>
        public static void ApllyCoordinateConversion(this HierarchyNode root)
        {
            var translatePoint = root.Point;

            //1. Перенос скелета в точку (0,0)
            //2. Растяжение (ось Z занулим, сделаем скелет плоским)
            foreach (var node in root)
            {
                node.Point = new SkeletonPoint()
                    {
                        X = (node.Point.X - translatePoint.X) * _multiNamberX,
                        Y = (node.Point.Y - translatePoint.Y) * _multiNamberY,
                        Z = (node.Point.Z - translatePoint.Z) * _multiNamberZ
                    };
            }

            GetHierachyOffset(root);
        }
        
        /// <summary>
        /// Будем выполнять следующее преобразование точки в 3д пространстве:
        /// Будем вычислять длинну кости и записывать ее в Y, это даст начальную постановку
        /// когда все кости направлены вверх
        /// Для этого сделаем: 
        /// 1. Вычислим эвклидово расстояние от текущей точки до прошлой
        /// 2. создатим точку (x,y,z) = (0,эвкл. раст, 0) и вернем ее
        /// </summary>
        /// <param name="jointCollection"></param>
        /// <param name="jointType"></param>
        /// <returns></returns>
        private static SkeletonPoint FillCoordinateForCurrentJoint(SkeletonPoint prevPoint, SkeletonPoint currPoint)
        {
            //Поймем декартово расстояние между точками: это будет длинна кости
            float distance = -1;

            var diffX = currPoint.X - prevPoint.X;
            var diffY = currPoint.Y - prevPoint.Y;
            var diffZ = currPoint.Z - prevPoint.Z;

            distance = (float)Math.Sqrt(diffX*diffX + diffY*diffY + diffZ*diffZ);

            return new SkeletonPoint() {X = 0, Y = distance, Z = 0};
        }

        /// <summary>
        /// 1. Вычисляет смещения каждой следующей точки относительно предыдущей
        /// 2. Вызываем FillCoordinateForCurrentJoint
        /// </summary>
        /// <param name="root"></param>
        private static void GetHierachyOffset(HierarchyNode root)
        {
            var hierarchyEnumerator = root.GetEnumerator() as HierarchyEnumerator;
            HierarchyNode currentPoint = null;
            HierarchyNode parentForCurrentPoint = null;

            if (hierarchyEnumerator != null)
            {
                //сам энумератор дает корректных родителей ()

                hierarchyEnumerator.MoveNext();
                hierarchyEnumerator.MoveNext();

                do
                {
                    currentPoint = hierarchyEnumerator.Current;
                    parentForCurrentPoint = hierarchyEnumerator.ParentForCurrentNode;

                    SkeletonPoint point;

                    if (parentForCurrentPoint.GetJointType == JointType.HipCenter)
                        point = hierarchyEnumerator.ParentForCurrentNode.Point;
                    else
                        point = hierarchyEnumerator.ParentForCurrentNode.LastPointValue;
                    
                    //Делаем пересчет
                    //currentPoint.Point = new SkeletonPoint()
                    //{
                    //    X = currentPoint.Point.X - point.X,
                    //    Y = currentPoint.Point.Y - point.Y,
                    //    Z = currentPoint.Point.Z - point.Z
                    //};

                    currentPoint.Point = FillCoordinateForCurrentJoint(point, currentPoint.Point);

                } while (hierarchyEnumerator.MoveNext());
            }
        }
    }
}
