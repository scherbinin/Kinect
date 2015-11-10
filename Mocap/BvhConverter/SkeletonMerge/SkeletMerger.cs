using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BvhConverter.Stuff;
using Main.Common;
using Microsoft.Kinect;

namespace BvhConverter.SkeletonMerge
{
    /// <summary>
    /// Класс первичной обработки входных данных с кинекта
    /// 1. Перенос данных со второй камеры в глобальную систему координат
    /// 2. Объединение данных с 2-х камер в одну стрктуру данных
    /// </summary>
    public static class SkeletMerger
    {
        //Матрица поворота для второй камеры
        private static Matrix4 _rotateY90degrees = new Matrix4()
            {
                //Строка 1: (0 0 1 0)
                M11 = 0, //cos 90
                M12 = 0,
                M13 = 1, //sin 90
                M14 = 0,

                //Строка 2: (0 1 0 0)
                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = 0,

                //Строка 3: ( -1 0 0 0)
                M31 = -1, // - cos 90
                M32 = 0,
                M33 = 0, // sin 90
                M34 = 0,

                //Строка 4: ( 0 0 0 1)
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };

        public static void MergeJointPointsData(List<Skeleton> skeletons1, List<Skeleton> skeletons2 = null)
        {
            //1. Перенос данных со второй камеры в глобальную систему координат
            if (skeletons2 == null)
                return;

            //Получить смещения и вычесть из второй камеры
            var offsetX = OffsetCameraBuilder.GetXOffset();
            var offsetZ = OffsetCameraBuilder.GetZOffset();

            foreach (var skeleton in skeletons2)
            {
                //Прбежать по енуму Joint'в
                for (int i = 0; i < 20; i++)
                {
                    //Если трекается, то сделать пересчет
                    if (skeleton.Joints[(JointType)i].TrackingState == JointTrackingState.Tracked)
                    {
                        var joint = skeleton.Joints[(JointType)i];
                        
                        //Сделаем пересчет на вторую камеру: оси X и Z меняются (двумерный простой поворот) + смещения
                        joint.Position = new SkeletonPoint()
                            {
                                //При повороте камеры на 90 градусов ось X и Z меняется местами
                                X = -joint.Position.Z + offsetZ,
                                Y = joint.Position.Y,
                                Z = joint.Position.X + offsetX

                            };
                    }
                }
            }

            //2. Объединение данных с 2-х камер в одну стрктуру данных (Усредненое значение)
            for (int j = 0; j < skeletons1.Count; j++)
            {
                //Смержить позиции джойнтов
                PositionMerge(skeletons1[j], skeletons2[j]);

                //Смержить углы костей
                AnglesMerge(skeletons1[j], skeletons2[j]);
            }
        }

        /// <summary>
        /// Преобразование матрицы поворота, что бы она показывала поворт со второй камеры, поворт в глобальной системе координат
        /// Для этого развернем все матрицы поворота костей вокруг оси Y на 90 градусов
        /// </summary>
        /// <param name="skeleton1"></param>
        /// <param name="skeleton2"></param>
        private static void AnglesMerge(Skeleton skeleton1, Skeleton skeleton2)
        {
            var rotateMatrix = MathHelper.GetRotationMatrixY(Math.PI/2.0);

            foreach (BoneOrientation bone in skeleton2.BoneOrientations)
            {
                //Усреднить углы
                var angleAbs =
                    MathHelper.GetElersAnglesZXY(MathHelper.Multiplication(bone.AbsoluteRotation.Matrix,
                                                                           _rotateY90degrees));

                var angleHier =
                    MathHelper.GetElersAnglesZXY(MathHelper.Multiplication(bone.HierarchicalRotation.Matrix,
                                                                           _rotateY90degrees));


                bone.AbsoluteRotation.Matrix = MathHelper.Multiplication(bone.AbsoluteRotation.Matrix, _rotateY90degrees);
                bone.HierarchicalRotation.Matrix = MathHelper.Multiplication(bone.HierarchicalRotation.Matrix, _rotateY90degrees);
            }
        }

        /// <summary>
        /// Мерж точек в декартовой системе координат для позиций джойнтов
        /// </summary>
        /// <param name="skeleton1"></param>
        /// <param name="skeleton2"></param>
        private static void PositionMerge(Skeleton skeleton1, Skeleton skeleton2)
        {
            //Прбежать по енуму Joint'в
            for (int i = 0; i < 20; i++)
            {
                //Если трекается оба скелета, то сделать пересчет как усреднение
                if (skeleton1.Joints[(JointType)i].TrackingState == JointTrackingState.Tracked &&
                    skeleton2.Joints[(JointType)i].TrackingState == JointTrackingState.Tracked)
                {
                    var joint1 = skeleton1.Joints[(JointType)i];
                    var joint2 = skeleton2.Joints[(JointType)i];
                    joint1.Position = new SkeletonPoint()
                    {
                        X = (joint1.Position.X + joint2.Position.X) / 2,
                        Y = (joint1.Position.Y + joint2.Position.Y) / 2,
                        Z = (joint1.Position.Z + joint2.Position.Z) / 2
                    };
                }
                else
                {
                    //Если второй трекается, первый нет, просто присвоить второе первому, если наоборот - не трогаем
                    if (skeleton1.Joints[(JointType)i].TrackingState != JointTrackingState.Tracked &&
                        skeleton2.Joints[(JointType)i].TrackingState == JointTrackingState.Tracked)
                    {
                        var joint1 = skeleton1.Joints[(JointType)i];
                        var joint2 = skeleton2.Joints[(JointType)i];

                        joint1.Position = new SkeletonPoint()
                        {
                            X = joint2.Position.X,
                            Y = joint2.Position.Y,
                            Z = joint2.Position.Z
                        };
                    }
                }
            }
        }
    }
}
