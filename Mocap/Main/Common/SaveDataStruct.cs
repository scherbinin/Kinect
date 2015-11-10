using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Main.Common
{
    /// <summary>
    /// Структура данных для сохранения данных с камер, как единый объект
    /// </summary>
    [Serializable]
    public class SaveDataStruct
    {
        public int OffsetX;
        public int OffsetZ;

        public List<Skeleton> Kinect1;
        public List<Skeleton> Kinect2;
    }
}
