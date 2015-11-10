using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BvhConverter.Stuff;
using Microsoft.Kinect;

namespace BvhConverter_new.Mappers
{
    /// <summary>
    /// Осуществляет мепинг из костей структуры BVH в кости структуры Kinect'а
    /// </summary>
    public static class BoneMapperBvhToKinect
    {
        public static Matrix4 GetBoneMatrixByBvhName(Skeleton skeleton, HierarchyNode node)
        {
            //Итого 20 костей + 1 для поворота рутового креста: 20*3 + 3 = 63 угла + 3 декартовы координаты для положения рутового креса

            /* Правила для левой ноги
             * 0. bvh:Hips(рутовый крест) == kineck: (0,0,0)
             * 1. bhv: LeftCross-LeftHips(половина нижней части таза) == kinect: HipCenter-HipRight
             * 2. bvh:LeftHip-LeftKnee(Тазовая кость) == kinect:HipRight-KneeRight
             * 3. bvh:LeftKnee-LeftAnkle(Голень) == kinect:KneeRight-AnkleRight
             * 4. bvh:LeftAnkle-EndSite(ступня) == kinect:AnkleRight-FootRight
             * 
             * Правила для правой ноги
             * 5. bvh:RightCross-RightHips(половина нижней части таза) == kinect: HipCenter-HipLeft
             * 6. bvh:RightHip-RightKnee(Тазовая кость) == kinect:HipLeft-KneeLeft
             * 7. bvh:RightKnee-RighAnkle(Голень) == kinect:KneeLeft-AnkleLeft
             * 8. bvh:RightAnkle-EndSite(ступня) == kinect:AnkleLeft-FootLeft
             * 
             * Корпус
             * 9. bvh:UpCross-Chest(верхняя часть таза) == kinect:HipCenter-Spine
             * 10. bvh:Chest(спина) == kinect:Spine-ShoulderCenter
             * 
             * Правила для левой руки
             * 11. bvh:LeftCollar-LeftShoulder(лева ключица) == kineck:ShoulderCenter-ShoulderRight
             * 12. bvh:LeftShoulder-LeftElbow(Трицепс) == kinect:ShoulderRight-ElbowRight
             * 13. bvh:LeftElbow-LeftWrist(Предплечье) == kinect:ElbowRight-WristRight
             * 14. bvh:LeftWrist-EndSite(Кисть) == kinect: WristRight-HandRight
             * 
             * Правила для правой руки
             * 15. bvh:RightCollar-RightShoulder(лева ключица) == kineck:ShoulderCenter-ShoulderLeft
             * 16. bvh:RightShoulder-RightElbow(Трицепс) == kinect:ShoulderLeft-ElbowLeft
             * 17. bvh:RightElbow-RighttWrist(Предплечье) == kinect:ElbowLeft-WristLeft
             * 18.bvh:RightWrist-EndSite(Кисть) == kinect: WristLeft-HandLeft
             * 
             * 
             * Правила для Шея и голова
             * 19. bvh:Neck-Head(шея) == kinect:ShoulderCenter-Head
             * 20. bhv:Head-EndSite(верхняя часть головы) == kinect: (x,y,z)=(0,0,0)
             */

            //0
            //if (node.GetTypeNode() == ConstBodyPoints.Hips)
            //    return GetZeroBoneOrientation(JointType.HipCenter);
            if (node.GetTypeNode() == ConstBodyPoints.Hips)
                //return GetHipBoneOrientation(skeleton.Joints).HierarchicalRotation.Matrix;
                return FindBone(JointType.HipCenter, JointType.HipCenter, skeleton.BoneOrientations).AbsoluteRotation.Matrix;

            //1
            if (node.GetTypeNode() == ConstBodyPoints.LeftCross)
                return FindBone(JointType.HipCenter, JointType.HipLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //2
            if (node.GetTypeNode() == ConstBodyPoints.LeftHip)
            {
                return FindBone(JointType.HipLeft, JointType.KneeLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;
                //var mat = FindBone(JointType.HipRight, JointType.KneeRight, skeleton.BoneOrientations).AbsoluteRotation.Matrix;
                //mat.M32 += (float)0.463;
                //return mat;
            }

            //3
            if (node.GetTypeNode() == ConstBodyPoints.LeftKnee)
                return FindBone(JointType.KneeLeft, JointType.AnkleLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //4
            if (node.GetTypeNode() == ConstBodyPoints.LeftAnkle)
                return FindBone(JointType.AnkleLeft, JointType.FootLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //5
            if (node.GetTypeNode() == ConstBodyPoints.RightCross)
                return FindBone(JointType.HipCenter, JointType.HipRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //6
            if (node.GetTypeNode() == ConstBodyPoints.RightHip)
            {
                return FindBone(JointType.HipRight, JointType.KneeRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;
                //var mat = FindBone(JointType.HipLeft, JointType.KneeLeft, skeleton.BoneOrientations).AbsoluteRotation.Matrix;
                //mat.M32 += (float)0.416;
                //return mat;
            }

            //7
            if (node.GetTypeNode() == ConstBodyPoints.RightKnee)
                return FindBone(JointType.KneeRight, JointType.AnkleRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //8
            if (node.GetTypeNode() == ConstBodyPoints.RightAnkle)
                return FindBone(JointType.AnkleRight, JointType.FootRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //9
            if (node.GetTypeNode() == ConstBodyPoints.UpCross)
                return FindBone(JointType.HipCenter, JointType.Spine, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //10
            if (node.GetTypeNode() == ConstBodyPoints.Chest)
                return FindBone(JointType.Spine, JointType.ShoulderCenter, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //11
            if (node.GetTypeNode() == ConstBodyPoints.LeftCollar)
                return FindBone(JointType.ShoulderCenter, JointType.ShoulderLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //12
            if (node.GetTypeNode() == ConstBodyPoints.LeftShoulder)
                return FindBone(JointType.ShoulderLeft, JointType.ElbowLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //13
            if (node.GetTypeNode() == ConstBodyPoints.LeftElbow)
                return FindBone(JointType.ElbowLeft, JointType.WristLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //14
            if (node.GetTypeNode() == ConstBodyPoints.LeftWrist)
                return FindBone(JointType.WristLeft, JointType.HandLeft, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //15
            if (node.GetTypeNode() == ConstBodyPoints.RightCollar)
                return FindBone(JointType.ShoulderCenter, JointType.ShoulderRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //16
            if (node.GetTypeNode() == ConstBodyPoints.RightShoulder)
                return FindBone(JointType.ShoulderRight, JointType.ElbowRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //17
            if (node.GetTypeNode() == ConstBodyPoints.RightElbow)
                return FindBone(JointType.ElbowRight, JointType.WristRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //18
            if (node.GetTypeNode() == ConstBodyPoints.RightWrist)
                return FindBone(JointType.WristRight, JointType.HandRight, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //19
            if (node.GetTypeNode() == ConstBodyPoints.Neck)
                return FindBone(JointType.ShoulderCenter, JointType.Head, skeleton.BoneOrientations).HierarchicalRotation.Matrix;

            //20
            if (node.GetTypeNode() == ConstBodyPoints.Head)
                return GetZeroBoneOrientation(JointType.ShoulderCenter).HierarchicalRotation.Matrix;

            throw new Exception("Кость " + node.GetTypeNode() + " не определена");
        }

        private static BoneOrientation FindBone(JointType start, JointType end, BoneOrientationCollection collection)
        {
            foreach (BoneOrientation bone in collection)
            {
                if (bone.StartJoint == start && bone.EndJoint == end)
                    return bone;
            }

            throw new Exception("Кость " + start.ToString() + "-" + end.ToString() + " не найдена в коллекции");
        }

        private static BoneOrientation GetZeroBoneOrientation(JointType joint)
        {
            return new BoneOrientation(joint);
        }

        //private static BoneOrientation GetHipBoneOrientation(JointCollection Joints)
        //{
        //    var bone = new BoneOrientation(JointType.HipCenter);
        //    bone.HierarchicalRotation.Matrix = FillRootRotateMatrix(Joints);
        //    bone.AbsoluteRotation.Matrix = FillRootRotateMatrix(Joints);

        //    return bone;
        //}

        //     private static Matrix4 FillRootRotateMatrix(JointCollection Joints)
        //   {

        /*
           Center.X = (HipRight.X + HipLeft.X)/2;
           Center.Y = (HipRight.Y + HipLeft.Y)/2;
           Center.Z = (HipRight.Z + HipLeft.Z)/2;
           e1 = [HipRight.X - HipLeft.X; HipRight.Y - HipLeft.Y; HipRight.Z - HipLeft.Z];
           e2 = [Spine.X - Center.X; Spine.Y - Center.Y; Spine.Z - Center.Z];
           e1 = e1/norm(e1); % Нормируем
           e2 = e2/norm(e2);
           e3 = cross(e1, e2); % Векторное произведение
           Root.RotationAbsolute = [e1, e2, e3]; % Матрица, сформированная столбцами e1, e2, e3
         */

        //var center = new SkeletonPoint();

        //center.X = (Joints.GetJointCoordByName(JointType.HipRight).X + Joints.GetJointCoordByName(JointType.HipLeft).X)/2;

        //center.Y = (Joints.GetJointCoordByName(JointType.HipRight).Y + Joints.GetJointCoordByName(JointType.HipLeft).Y) / 2;

        //center.Z = (Joints.GetJointCoordByName(JointType.HipRight).Z + Joints.GetJointCoordByName(JointType.HipLeft).Z) / 2;

        //var e1 = GetNorm(new Vector4()
        //             {
        //                 X = Joints.GetJointCoordByName(JointType.HipRight).X - Joints.GetJointCoordByName(JointType.HipLeft).X,
        //                 Y = Joints.GetJointCoordByName(JointType.HipRight).Y - Joints.GetJointCoordByName(JointType.HipLeft).Y,
        //                 Z = Joints.GetJointCoordByName(JointType.HipRight).Z - Joints.GetJointCoordByName(JointType.HipLeft).Z,
        //                 W = 0
        //             });

        //var e2 = GetNorm(new Vector4()
        //            {
        //                X = Joints.GetJointCoordByName(JointType.Spine).X - center.X,
        //                Y = Joints.GetJointCoordByName(JointType.Spine).Y - center.Y,
        //                Z = Joints.GetJointCoordByName(JointType.Spine).Z - center.Z,
        //                W = 0
        //            });

        //var e3 = Cross(e1,e2);

        //return new Matrix4()
        //           {
        //               M11 = e1.X,
        //               M12 = e2.X,
        //               M13 = e3.X,
        //               M14 = 0,
        //               M21 = e1.Y,
        //               M22 = e2.Y,
        //               M23 = e3.Y,
        //               M24 = 0,
        //               M31 = e1.Z,
        //               M32 = e2.Z,
        //               M33 = e3.Z,
        //               M34 = 0,
        //               M41 = e1.W,
        //               M42 = e2.W,
        //               M43 = e3.W,
        //               M44 = 0
        //           };

        //}

        /// <summary>
        /// Нормирование вектора
        /// </summary>
        /// <param name="vector4"></param>
        /// <returns></returns>
        private static Vector4 GetNorm(Vector4 v)
        {
            var norma = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);

            return new Vector4()
                       {
                           X = v.X / norma,
                           Y = v.Y / norma,
                           Z = v.Z / norma,
                           W = 0
                       };
        }

        /// <summary>
        /// Векторное произведение 2-х векторов
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        private static Vector4 Cross(Vector4 e1, Vector4 e2)
        {
            return new Vector4()
                       {
                           X = e1.Y * e2.Z - e1.Z * e2.Y,
                           Y = e1.Z * e2.X - e1.X * e2.Z,
                           Z = e1.X * e2.Y - e1.Y * e2.X,
                           W = 0
                       };
        }

        private static SkeletonPoint GetJointCoordByName(this JointCollection Joints, JointType name)
        {
            return Joints.First(joint => joint.JointType == name).Position;
        }
    }
}
