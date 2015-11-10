using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Kinect;

namespace KinectOperator
{
    /// <summary>
    /// Активация устройства и работа с потоками от него
    /// </summary>
    public class KinectFlowWorker
    {
        KinectSensor _kinect;//Само устройство
        private byte[] _videoColorPixels;//Буфер кадра с кинекта
        private Bitmap _bitmapColorVideo;//Сюда рисуем из буфера кадра
        private VideoFlowHandler _videoFlowHandler;
        private SkeletonFlowHandler _skeletonFlowHandler;

        private bool _skeletRecordFlowEnable = false;//Запись потока скелета начата
        private List<Skeleton> _skeletonsRecordBuffer = null; //Сюда пишем поток, до момента когда нужно возвратить

        public bool ActivateFirstFreeDevice()
        {
            _bitmapColorVideo = new Bitmap(640, 480, PixelFormat.Format32bppRgb);
            _kinect = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();

            if (_kinect != null)
                _kinect.Start();
            else
                return false;

            return true;
        }

        public bool IsHaveActiveDevice
        {
            get
            {
                if (_kinect != null) return true;
                else return false;
            }
        }

        /// <summary>
        /// Получить активное устройство
        /// </summary>
        public KinectSensor GetActiveDevice
        {
            get { return _kinect; }
        }

        #region Снятие видео потока

        public void ActivateVideoFlow(VideoFlowHandler videoShowerHandler)
        {
            if (_kinect == null)
                throw new Exception("Нет инициализированных устройств");

            _videoFlowHandler = videoShowerHandler;

            //Активируем поток RGB видео
            _kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            //Подпишемся на событие видео потока
            _kinect.ColorFrameReady += SensorColorFrameReady;

            //Захватим память под буфер c видео
            this._videoColorPixels = new byte[_kinect.ColorStream.FramePixelDataLength];
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    //640*480, 4 байта на 1 пиксель
                    colorFrame.CopyPixelDataTo(this._videoColorPixels);

                    var bitmapData = _bitmapColorVideo.LockBits(new Rectangle(0, 0, 640, 480), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    var ptrOnStart = bitmapData.Scan0;

                    Marshal.Copy(_videoColorPixels, 0, ptrOnStart, _videoColorPixels.Length);

                    _bitmapColorVideo.UnlockBits(bitmapData);

                    if (_videoFlowHandler != null)
                        _videoFlowHandler.Invoke(_bitmapColorVideo);

                }
            }
        }
        #endregion

        #region Снятие потока скилетезации

        public void ActivateSkeletFlow(SkeletonFlowHandler skeletonFlowHandler, TransformSmoothParameters? smoothingParam = null)
        {
            if(_kinect == null)
                throw new Exception("Нет инициализированных устройств");

            _skeletonFlowHandler = skeletonFlowHandler;

            //var smoothingParam = new TransformSmoothParameters();
            //{
            //    smoothingParam.Smoothing = 0.7f;
            //    smoothingParam.Correction = 0.3f;
            //    smoothingParam.Prediction = 1.0f;
            //    smoothingParam.JitterRadius = 1.0f;
            //    smoothingParam.MaxDeviationRadius = 1.0f;
            //};

            if (smoothingParam.HasValue)
                _kinect.SkeletonStream.Enable((TransformSmoothParameters)smoothingParam);
            else
                _kinect.SkeletonStream.Enable();

            //Сидячий режим
            //_kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
            _kinect.SkeletonFrameReady += this.SensorSkeletonFrameReady;
        }

        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            var skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                    
                    if (_skeletRecordFlowEnable)
                        RecordSkeletFrame(skeletons);

                    if (_skeletonFlowHandler != null)
                        _skeletonFlowHandler.Invoke(skeletons);
                    
                }
            }
        }

        #endregion

        public void StartSkeletonFlowRecord()
        {
            if (!_kinect.SkeletonStream.IsEnabled)
                throw new Exception("Активация записи до активации потока скелета невозможна");

            _skeletRecordFlowEnable = true;
            _skeletonsRecordBuffer = new List<Skeleton>();
        }

        public List<Skeleton> StopSkeletonFlowRecord()
        {
            _skeletRecordFlowEnable = false;

            return _skeletonsRecordBuffer;
        }

        private void RecordSkeletFrame(Skeleton[] skeletons)
        {
            foreach (Skeleton skeleton in skeletons)
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    _skeletonsRecordBuffer.Add(skeleton);
        }
    }




}
