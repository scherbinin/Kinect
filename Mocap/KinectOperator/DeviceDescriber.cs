using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Kinect;

namespace KinectOperator
{
    public class DeviceDescriber : IDeviceDescriber
    {
        private readonly KinectSensor _kinect;//Само устройство
        private byte[] _videoColorPixels;//Буфер кадра с кинекта
        private Bitmap _bitmapColorVideo;//Сюда рисуем из буфера кадра
        private VideoFlowHandler _videoFlowHandler;
        private SkeletonFlowHandler _skeletonFlowHandler;

        private bool _needRecord;
        private List<Skeleton> _skeletonsRecordBuffer = null; //Сюда пишем поток, до момента когда нужно возвратить

        private bool _deviceInited = false;

        public DeviceDescriber(KinectSensor kinect)
        {
            this._kinect = kinect;
            NeedRecord = false;
        }

        public KinectStatus GetDeviceStatus()
        {
            return _kinect.Status;
        }

        public bool NeedRecord {
            get { return _needRecord; }
            set { _needRecord = value; }
        }

        public List<Skeleton> SkeletonsRecordBuffer
        {
            get { return _skeletonsRecordBuffer; }
        }

        public KinectSensor Kinect
        {
            get { return _kinect; }
        }

        #region Снятие видопотока
        
        public void ActivateVideoFlow(VideoFlowHandler videoShowerHandler)
        {
            if (!_deviceInited)
                InitDevice();

            _videoFlowHandler = videoShowerHandler;

            //Активируем поток RGB видео
            _kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            //Подпишемся на событие видео потока
            _kinect.ColorFrameReady += SensorColorFrameReady;

            //Захватим память под буфер c видео
            _videoColorPixels = new byte[_kinect.ColorStream.FramePixelDataLength];
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    //640*480, 4 байта на 1 пиксель
                    colorFrame.CopyPixelDataTo(_videoColorPixels);

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
            if (!_deviceInited)
                InitDevice();

            _skeletonFlowHandler = skeletonFlowHandler;

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
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    var skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    if (_needRecord)
                        AddFrameToRecord(skeletons);

                    if (_skeletonFlowHandler != null)
                        _skeletonFlowHandler.Invoke(skeletons);
                    
                }
            }
        }

        private void InitDevice()
        {
            if (_kinect == null)
                throw new Exception("Сущность устройства инициализирована с ошибкой");

            _bitmapColorVideo = new Bitmap(640, 480, PixelFormat.Format32bppRgb);
            _kinect.Start();

            _deviceInited = true;
        }

        #endregion

        private void AddFrameToRecord(Skeleton[] skeletons)
        {
            if (_skeletonsRecordBuffer == null)
                _skeletonsRecordBuffer = new List<Skeleton>();

            foreach (Skeleton skeleton in skeletons)
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    _skeletonsRecordBuffer.Add(skeleton);
                    return;
                }
        }
    }
}
