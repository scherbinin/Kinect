using System.Globalization;

namespace BvhConverter.DataStructures
{
    /// <summary>
    /// Эйлеровы углы для вектора
    /// </summary>
    public class PozitionPoint
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public PozitionPoint(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public PozitionPoint(double x, double y, double z)
        {
            _x = (float)x;
            _y = (float)y;
            _z = (float)z;
        }

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float Z
        {
            get { return _z; }
        }

        public override string ToString()
        {
            return _z.ToString("0.00", CultureInfo.InvariantCulture) + " " +
                   _x.ToString("0.00", CultureInfo.InvariantCulture) + " " +
                   _y.ToString("0.00", CultureInfo.InvariantCulture) + " ";
        }
    }
}
