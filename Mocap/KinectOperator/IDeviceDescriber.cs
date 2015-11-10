using System.Collections.Generic;
using Microsoft.Kinect;

namespace KinectOperator
{
    public interface IDeviceDescriber
    {
        KinectSensor Kinect { get; }

        /// <summary>
        /// Активировать запись
        /// </summary>
        bool NeedRecord { get; set; }

        /// <summary>
        /// Активировать поток скелетизации
        /// </summary>
        /// <param name="skeletonFlowHandler"></param>
        /// <param name="smoothingParam"></param>
        void ActivateSkeletFlow(SkeletonFlowHandler skeletonFlowHandler,
                                TransformSmoothParameters? smoothingParam = null);

        /// <summary>
        /// Активировать поток видео
        /// </summary>
        /// <param name="videoShowerHandler"></param>
        void ActivateVideoFlow(VideoFlowHandler videoShowerHandler);

        /// <summary>
        /// Получить статус устройства
        /// </summary>
        /// <returns></returns>
        KinectStatus GetDeviceStatus();

        /// <summary>
        /// Получить запись потока скелетизации
        /// </summary>
        List<Skeleton> SkeletonsRecordBuffer { get; }
    }
}
