using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace Main
{
    public partial class SmoothMotionOptionsForm : Form
    {
        private readonly string _labelDefaultText = "Наведите курсор на поле ввода для получения справки";

        public SmoothMotionOptionsForm()
        {
            InitializeComponent();
        }

        private void SmoothMotionOptionsForm_Load(object sender, EventArgs e)
        {
            SetValuesToForms();
        }

        private void SetValuesToForms()
        {
            var parameters = SmoothMotionBuilder.GetSmoothParameters();

            parameters = AplayRangingUp(parameters);

            txbCorrection.Text = parameters.Correction.ToString();
            txbJitRadius.Text = parameters.JitterRadius.ToString();
            txbMaxRadius.Text = parameters.MaxDeviationRadius.ToString();
            txbPrediction.Text = parameters.Prediction.ToString();
            txbSmooth.Text = parameters.Smoothing.ToString();
        }

        private void SendValuesToSingletone()
        {
            float correction = Convert.ToSingle(txbCorrection.Text);
            float jitRadius = Convert.ToSingle(txbJitRadius.Text);
            float maxJitRadius = Convert.ToSingle(txbMaxRadius.Text);
            float prediction = Convert.ToSingle(txbPrediction.Text);
            float smooth = Convert.ToSingle(txbSmooth.Text);

            var outParam = new TransformSmoothParameters()
                               {
                                   Correction = correction,
                                   JitterRadius = jitRadius,
                                   MaxDeviationRadius = maxJitRadius,
                                   Prediction = prediction,
                                   Smoothing = smooth
                               };

            SmoothMotionBuilder.SetSmoothParameters(outParam);
        }

        #region Масштабирование

        private TransformSmoothParameters AplayRangingUp(TransformSmoothParameters parameters)
        {
            //Применить восходящее масштабирование (умножение на 100 и отброска дробей)
            parameters.Correction = Math.Abs(parameters.Correction * 100);
            parameters.JitterRadius = Math.Abs(parameters.JitterRadius * 100);
            parameters.MaxDeviationRadius = Math.Abs(parameters.MaxDeviationRadius * 100);
            parameters.Prediction = Math.Abs(parameters.Prediction * 100);
            parameters.Smoothing = Math.Abs(parameters.Smoothing * 100);

            return parameters;
        }

        private TransformSmoothParameters AplayRangingDown(TransformSmoothParameters parameters)
        {
            //Применить нисходящее масштабирование (умножение на 100 и отброска дробей)
            parameters.Correction = (float)(parameters.Correction / 100.0);
            parameters.JitterRadius = (float)(parameters.JitterRadius / 100.0);
            parameters.MaxDeviationRadius = (float)(parameters.MaxDeviationRadius / 100.0);
            parameters.Prediction = (float)(parameters.Prediction / 100.0);
            parameters.Smoothing = (float)(parameters.Smoothing / 100.0);

            return parameters;
        }

        #endregion

        #region Справка в величинам сглаживания

        private void txbCorrection_MouseEnter(object sender, EventArgs e)
        {
            lblDescription.Text = "Изменяется в диапазоне [0,99]. Более высокое значение корректирует в сторону исходных данных быстрее, при низком значении исправляет более медленно, движение более гладкое. По умолчанию в SDK 50см";
        }

        private void txbCorrection_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Text = _labelDefaultText;
        }

        private void txbJitRadius_MouseEnter(object sender, EventArgs e)
        {
            lblDescription.Text = "Больше или равен нулю. Задает радиус дрожания. Определяет, насколько агрессивно, будут удаляться дрожания из исходных данных. По умолчанию в SDK 5см";
        }

        private void txbJitRadius_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Text = _labelDefaultText;
        }

        private void txbMaxRadius_MouseEnter(object sender, EventArgs e)
        {
            lblDescription.Text = "Больше или равен нулю. Задает максимальный радиус отклонения входной точки. Определяет, максимальный радиус, на который может отклоняться отфильтрованная позиция точки относительно сырых входных данных. Значение, которое превышает этот радиус зажимается в рамки этого радиуса, с соблюдением направления. По умолчанию в SDK 4см";
        }

        private void txbMaxRadius_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Text = _labelDefaultText;
        }

        private void txbPrediction_MouseEnter(object sender, EventArgs e)
        {
            lblDescription.Text = "Больше или равен нулю. Задает кадров для интерполяции (прогнозирования в будующем) в которых будет применено сглаживание резко изменившейся велечины уже поступвшей во входных данных.  По умолчанию в SDK 0 кадров";
        }

        private void txbPrediction_MouseLeave(object sender, EventArgs e)
        {
                lblDescription.Text = _labelDefaultText;
        }

        private void txbSmooth_MouseEnter(object sender, EventArgs e)
        {
            lblDescription.Text = "Изменяется в диапазоне [0,99]. Более высокое значение соответствует более сильному сглаживанию, при нуле - входные данные не обрабатываются. По умолчанию в SDK 50см";
        }

        private void txbSmooth_MouseLeave(object sender, EventArgs e)
        {
            lblDescription.Text = _labelDefaultText;
        }

        #endregion

        #region Кнопки выставления параметров

        private void btnSmoothHard_Click(object sender, EventArgs e)
        {
            SmoothMotionBuilder.SetHardSmoothParameters();
            SetValuesToForms();
        }

        private void btnSmothLight_Click(object sender, EventArgs e)
        {
            SmoothMotionBuilder.SetLightSmoothParameters();
            SetValuesToForms();
        } 
        #endregion

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
