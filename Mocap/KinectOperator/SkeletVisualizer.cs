using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectOperator
{
    /// <summary>
    /// Визуализатор скелета
    /// </summary>
    static public class SkeletVisualizer
    {
        private static CoordinateMapper _coordinateMapper;

        public static Image VisualSkelet(Skeleton[] skeletons, KinectSensor kinect)
        {
            if (skeletons.Length == 0)
                return null;

            _coordinateMapper = kinect.CoordinateMapper;
            var bitmap = new Bitmap(640, 480, PixelFormat.Format32bppRgb);

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                foreach (Skeleton skeleton in skeletons)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        DrawBonesAndJoints(skeleton, gr);
                        return bitmap;
                    }
                    else
                        if (skeleton.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            var pen = new Pen(Brushes.Blue);

                            gr.DrawEllipse(pen,
                                           (int)SkeletonPointToScreen(skeleton.Position).X,
                                           (int)SkeletonPointToScreen(skeleton.Position).Y,
                                           10,
                                           10);

                            return bitmap;
                        }

                    
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private static void DrawBonesAndJoints(Skeleton skeleton, Graphics drawingContext)
        {
            // Render Torso
            DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = Brushes.Green;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = Brushes.Gray;
                }

                if (drawBrush != null)
                {
                    drawingContext.FillEllipse(drawBrush,
                        (int)SkeletonPointToScreen(joint.Position).X,
                        (int)SkeletonPointToScreen(joint.Position).Y,
                        10,
                        10);
                }
            }
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private static void DrawBone(Skeleton skeleton, Graphics drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = new Pen(Brushes.Gray, 1);

            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = new Pen(Brushes.Green, 6);
            }

            drawingContext.DrawLine(drawPen,
                (int)SkeletonPointToScreen(joint0.Position).X,
                (int)SkeletonPointToScreen(joint0.Position).Y,
                (int)SkeletonPointToScreen(joint1.Position).X,
                (int)SkeletonPointToScreen(joint1.Position).Y);
        }

        private static Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = _coordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
    }
}
