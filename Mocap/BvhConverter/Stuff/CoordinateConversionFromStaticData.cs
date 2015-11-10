using System;
using BvhConverter_new;
using Microsoft.Kinect;

namespace BvhConverter.Stuff
{
    public static class CoordinateConversionFromKinect
    {
        private static float _multiNamberX = 30;
        private static float _multiNamberY = 30;
        private static float _multiNamberZ = 0;

        /// <summary>
        /// Вставить константные значения
        /// </summary>
        /// <param name="root"></param>
        public static void ApllyConstCoord(this HierarchyNode root)
        {
            /*
               1. Hips:	  0.00   0.00  0.00
               2. LeftCross   0.00   0.00  0.00
               3. LeftHip :	  3.43   0.00  0.00
               4. LeftKnee:	  0.00  -18.46 0.00
               5. LeftAnkle:     0.00  -17.95 0.00
               6. LeftFoot:      0.00  -3.11  0.00
               7. LeftCross:     0.00   0.00  0.00
               8. RightHip:     -3.43   0.00  0.00
               9. RightKnee:     0.00  -18.80 0.00
               10. RightAnkle:    0.00  -17.57 0.00
               11. RightFoot:     0.00  -3.25  0.00
               12. UpCross:       0.00   0.00  0.00
               13. Spine:        0.00   4.57  0.00   
               14. LeftCollar:   1.06   15.33 1.76
               15. LeftShoulder: 5.81   0.00  0.00
               16. LeftElbow:    0.00  -12.08 0.00
               17. LeftWrist:    0.00  -9.82  0.00
               18. LeftHand:     0.00  -7.36  0.00
               19. RightCollar: -1.06   15.33 1.76
               20. RightShouldr:-6.06   0.00  0.00
               21. RightElbow:   0.00  -11.90 0.00
               22. RightWrist:   0.00  -9.52  0.00
               23. RightHand:    0.00  -7.14  0.00
               24. Neck:         0.00   17.62 0.00
               25. Head:         0.00   5.19  0.00
               26. Макушка:      0.00   4.14  0.00
             */
            var enumerator = root.GetEnumerator();
            enumerator.Reset();
            var node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = 0, Z = 0 }; enumerator.MoveNext();//HipCenter
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = 0, Z = 0 }; enumerator.MoveNext();//LeftCross
            node = enumerator.Current;
            node.Point = new SkeletonPoint() {X = (float) 3.43, Y = 0, Z = 0};  enumerator.MoveNext();//HipLeft
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-18.46, Z = 0 }; enumerator.MoveNext();//LeftKnee
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-17.95, Z = 0 }; enumerator.MoveNext();//LeftAnkle
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-2.75, Z = 0 }; enumerator.MoveNext();//LeftFoot
            node = enumerator.Current;

            node.Point = new SkeletonPoint() { X = 0, Y = 0, Z = 0 }; enumerator.MoveNext();//RightCross
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = (float)-3.43, Y = 0, Z = 0 }; enumerator.MoveNext();//RightHip
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-18.80, Z = 0 }; enumerator.MoveNext();//RightKnee
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-17.57, Z = 0 }; enumerator.MoveNext();//RightAnkle
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-2.75, Z = 0 }; enumerator.MoveNext();//RightFoot
            node = enumerator.Current;

            node.Point = new SkeletonPoint() { X = 0, Y = 0, Z = 0 }; enumerator.MoveNext();//UpCross
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)4.57, Z = 0 }; enumerator.MoveNext();//Chest
            node = enumerator.Current;

            node.Point = new SkeletonPoint() { X = 0, Y = (float)17.62, Z = 0 }; enumerator.MoveNext();//LeftCollar->Neck
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = (float)5.81, Y = 0, Z = 0 }; enumerator.MoveNext();//LeftShoulder
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-12.08, Z = 0 }; enumerator.MoveNext();//LeftElbow
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-9.82, Z = 0 }; enumerator.MoveNext();//LeftWrist
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-2.36, Z = 0 }; enumerator.MoveNext();//LeftHand
            node = enumerator.Current;

            node.Point = new SkeletonPoint() { X = 0, Y = (float)17.62, Z = 0 }; enumerator.MoveNext();//RightCollar->Neck
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = (float)-6.06, Y = 0, Z = 0 }; enumerator.MoveNext();//RightShoulder
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-11.9, Z = 0 }; enumerator.MoveNext();//RightElbow
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-9.52, Z = 0 }; enumerator.MoveNext();//RightWrist
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)-2.36, Z = 0 }; enumerator.MoveNext();//RightHand
            node = enumerator.Current;

            node.Point = new SkeletonPoint() { X = 0, Y = (float)17.62, Z = 0 }; enumerator.MoveNext();//ShoulderCenter->Neck
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)5.19, Z = 0 }; enumerator.MoveNext();//Head
            node = enumerator.Current;
            node.Point = new SkeletonPoint() { X = 0, Y = (float)0, Z = 0 }; enumerator.MoveNext();//Макушка->Head
            node = enumerator.Current;

            //Теперь сделает пересчет в: (x,y,z) = (0,декартово расстояние,0)
            GetHierachyOffset(root);
        }

        /// <summary>
        /// 1. Вычисляет смещения каждой следующей точки относительно предыдущей
        /// 2. Вызываем FillCoordinateForCurrentJoint
        /// </summary>
        /// <param name="root"></param>
        private static void GetHierachyOffset(HierarchyNode root)
        {
            var hierarchyEnumerator = root.GetEnumerator() as HierarchyEnumerator;

            if (hierarchyEnumerator != null)
            {
                //сам энумератор дает корректных родителей ()

                hierarchyEnumerator.MoveNext();
                hierarchyEnumerator.MoveNext();

                do
                {
                    HierarchyNode currentPoint = hierarchyEnumerator.Current;

                    //Пропускаем Корневой крест, иначе его сложит в линию по Y. Т.к. матриц поворота для него нет, то крест не развернется
                    if (currentPoint.GetTypeNode() == ConstBodyPoints.LeftHip || currentPoint.GetTypeNode() == ConstBodyPoints.RightHip)
                        continue;

                    currentPoint.Point = FillCoordinateForCurrentJoint(hierarchyEnumerator.Current.Point);

                } while (hierarchyEnumerator.MoveNext());
            }
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
        private static SkeletonPoint FillCoordinateForCurrentJoint(SkeletonPoint currPoint)
        {
            //Поймем декартово расстояние между точками: это будет длинна кости
            float distance = -1;

            distance = (float)Math.Sqrt(currPoint.X * currPoint.X + currPoint.Y * currPoint.Y + currPoint.Z * currPoint.Z);

            return new SkeletonPoint() { X = 0, Y = distance, Z = 0 };
        }
    }
}
