using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace BvhConverter_new.Stuff
{
    public static class Helper
    {
        /// <summary>
        /// Печать координат типа SkeletonPoint в виде: "X.XX Y.YY Z.ZZ"
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string PrintCoordinate(this SkeletonPoint point)
        {
            return point.X.ToString("0.00", CultureInfo.InvariantCulture) + " " +
                    point.Y.ToString("0.00", CultureInfo.InvariantCulture) + " " +
                    point.Z.ToString("0.00", CultureInfo.InvariantCulture) + " ";
        }
    }
}
