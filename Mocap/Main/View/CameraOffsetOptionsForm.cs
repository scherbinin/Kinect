using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Main.Common;
using Microsoft.Kinect;

namespace Main
{
    public partial class CameraOffsetOptionsForm : Form
    {
        public CameraOffsetOptionsForm()
        {
            InitializeComponent();
        }

        private void SmoothMotionOptionsForm_Load(object sender, EventArgs e)
        {
            SetValuesToForms();
        }

        private void SetValuesToForms()
        {
            if (OffsetCameraBuilder.isExist)
            {
                txbOffsetX.Text = OffsetCameraBuilder.GetXOffset().ToString();
                txbOffsetZ.Text = OffsetCameraBuilder.GetZOffset().ToString();
            }
        }

        private void SendValuesToSingletone()
        {
            int offsetX = Convert.ToInt32(txbOffsetX.Text);
            int offsteZ = Convert.ToInt32(txbOffsetZ.Text);

            OffsetCameraBuilder.InitOffsetBuilder(offsetX, offsteZ, DistanceType.Centimeter);
        }


        /// <summary>
        /// Кнопка ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            SendValuesToSingletone();

            Close();
        }

        /// <summary>
        /// Кнопка Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
