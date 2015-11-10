using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KinectOperator;
using Microsoft.Kinect;

namespace Main.View
{
    public partial class FlowView : UserControl
    {
        public FlowView()
        {
            InitializeComponent();

            IsRecording = false;
        }

        public bool IsRecording { get; private set; }

        /// <summary>
        /// Для PictureBox видеопотока
        /// </summary>
        public PictureBox VideFlowPicture
        {
            get { return VideoPicBox; }
        }

        /// <summary>
        /// Для PictureBox потока скелетизации
        /// </summary>
        public PictureBox SkeleteFlowPicture
        {
            get { return SkiletPicBox; }
        }

        public IDeviceDescriber Device { get; set; }

        /// <summary>
        /// Остановка вывода потоков
        /// </summary>
        public void StopFlows()
        {
            Device.Kinect.Stop();
        }

        /// <summary>
        /// Старт вывода потоков в данный контрол
        /// </summary>
        public void StartFlows()
        {
            if (Device.GetDeviceStatus() == KinectStatus.Connected)
            {
                Device.ActivateVideoFlow(image => VideFlowPicture.Image = image);

                Device.ActivateSkeletFlow(skeletons =>
                {
                    SkeleteFlowPicture.Image = SkeletVisualizer.VisualSkelet(skeletons,
                                                  Device.Kinect);
                },SmoothMotionBuilder.GetSmoothParameters());

                Device.NeedRecord = true;
            }
            else
                throw new Exception("Устройство не готово к старту, ИД устройства: " + Device.Kinect.UniqueKinectId + " Текущий статус устройства: " + Device.GetDeviceStatus());
        }

        public void StartRecord()
        {
            if (Device == null)
                throw new Exception("Невозможно использовать запись, если DeviceDescriber не инициализирован");

            IsRecording = true;

            Device.NeedRecord = true;
        }

        public List<Skeleton> StopRecord()
        {
            if (Device == null)
                return null;

            IsRecording = false;

            Device.NeedRecord = false;

            return Device.SkeletonsRecordBuffer;
        }
    }
}
