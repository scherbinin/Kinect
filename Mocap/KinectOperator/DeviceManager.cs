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
    public static class DeviceManager
    {
        private static List<DeviceDescriber> _deviceContainer;

        static DeviceManager()
        {
            _deviceContainer = KinectSensor.KinectSensors.Where(kinect => kinect.Status == KinectStatus.Connected).
                Select(kinect => new DeviceDescriber(kinect)).ToList();
        }

        public static List<DeviceDescriber> Devices()
        {
            return _deviceContainer ?? (_deviceContainer = new List<DeviceDescriber>());
        }
    }
}
