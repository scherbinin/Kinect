using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Common
{
    /// <summary>
    /// Мера длинны
    /// </summary>
    public enum DistanceType
    {
        Millimeters = 1000,
        Centimeter = 100,
        Meters = 1
    }

    /// <summary>
    /// Синглтон для хранения смещения второй камеры относительно первой
    /// </summary>
    public class OffsetCameraBuilder
    {
        private int _offsetX;
        private int _offsetZ;
        private DistanceType _distanceType;

        private static OffsetCameraBuilder classObject = null;

        /// <summary>
        /// Инициализировать синглтон
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetZ"></param>
        /// /// <param name="meters"></param>
        public static void InitOffsetBuilder(int offsetX, int offsetZ, DistanceType meters)
        {
            classObject = new OffsetCameraBuilder { _offsetX = offsetX, _offsetZ = offsetZ, _distanceType = meters};
        }

        /// <summary>
        /// Получить значения смещений в метрах
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetZ"></param>
        public static float GetXOffset()
        {
            if (classObject == null)
                return 0;

            return ((float)classObject._offsetX) / (float)classObject._distanceType;
        }

        /// <summary>
        /// Получить значения смещений в метрах
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetZ"></param>
        public static float GetZOffset()
        {
            if (classObject == null)
                return 0;

            return ((float)classObject._offsetZ) / (float)classObject._distanceType;
        }

        public static bool isExist
        {
            get { return classObject != null; }
        }

    }
}
