using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectOperator
{
    /// <summary>
    /// Для видео потока
    /// </summary>
    /// <param name="image"></param>
    public delegate void VideoFlowHandler(Image image);

    /// <summary>
    /// Для потока трекинга тела
    /// </summary>
    /// <param name="skeletons"></param>
    public delegate void SkeletonFlowHandler(Skeleton[] skeletons);

    public static class Consts
    {

        
    }
}
