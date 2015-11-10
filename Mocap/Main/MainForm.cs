using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BvhConverter;
using KinectOperator;
using Main.Common;
using Microsoft.Kinect;

namespace Main
{
    public partial class MainForm : Form
    {
        private KinectFlowWorker _kinectFlowWorker;

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            

            if (DeviceManager.Devices().Count < 1)
                MessageBox.Show("Нет доступных устройств со свободным статусом", "Ошибка");
            else
            {
                flowViewer1.Device = DeviceManager.Devices()[0];
                flowViewer1.StartFlows();

                if (DeviceManager.Devices().Count > 1)
                {
                    //Инициализировать объект описывающий смещения второй камеры
                    new CameraOffsetOptionsForm().ShowDialog();

                    flowViewer2.Device = DeviceManager.Devices()[1];
                    flowViewer2.StartFlows();
                }
            }
        }

        /// <summary>
        /// Загрузка предыдущего движения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadRecord_Click(object sender, EventArgs e)
        {
            var skeletonFlowsObj = DataWorker.Load();

            if (skeletonFlowsObj != null)
                if (skeletonFlowsObj.Kinect1 != null)
                {
                    OffsetCameraBuilder.InitOffsetBuilder(skeletonFlowsObj.OffsetX, skeletonFlowsObj.OffsetZ, DistanceType.Centimeter);
                    DataWorker.SaveBvhContent(BvhCreater.GetBvhFile(skeletonFlowsObj.Kinect1, skeletonFlowsObj.Kinect2));
                }
                else
                    MessageBox.Show("Отсутствует файл с сохранеными данными");
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (flowViewer1.IsRecording)
            {
                var skeletonFlowBuffer1 = flowViewer1.StopRecord();
                var skeletonFlowBuffer2 = flowViewer2.StopRecord();

                //Сделаем сохранение BVH
                btnRecord.Text = "Запись";//Поменяем название кнопки
                DataWorker.SaveBvhContent(BvhCreater.GetBvhFile(skeletonFlowBuffer1, skeletonFlowBuffer2));

                //Сохраним поток скелетизации для последующей отладки
                DataWorker.SaveMoovement(new SaveDataStruct()
                {
                    Kinect1 = skeletonFlowBuffer1,
                    Kinect2 = skeletonFlowBuffer2,
                    OffsetX = (int)OffsetCameraBuilder.GetXOffset(),
                    OffsetZ = (int)OffsetCameraBuilder.GetZOffset()
                });

                if(OffsetCameraBuilder.isExist)
                {
                    
                }
            }
            else
            {
                btnRecord.Text = "Стоп";//Поменяем название кнопки
                flowViewer1.StartRecord();
            }
        }

        /// <summary>
        /// Событие закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flowViewer1.Device != null)
                flowViewer1.StopFlows();

            if (flowViewer2.Device != null)
                flowViewer2.StopFlows();
        }

        /// <summary>
        /// Открыть настройки сглаживания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemSmoothOptions_Click(object sender, EventArgs e)
        {
            var dlg = new SmoothMotionOptionsForm();
            dlg.ShowDialog();
        }

        private void смещенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new CameraOffsetOptionsForm();
            dlg.ShowDialog();
        }
    }
}
