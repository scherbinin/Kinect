using System;
using System.IO;

namespace Main
{
    public static class Logger
    {
        private static bool _isSessionIninited = false;
        private const string _filePath = "D:\\BvhLog.txt";

        private static void InitNewLogSession()
        {
            File.WriteAllText(_filePath, "======Start New Log Session======" + Environment.NewLine);
            _isSessionIninited = true;
        }

        public static void PushMessage(string msg)
        {
            if(!_isSessionIninited)
                InitNewLogSession();

            var sw = File.AppendText(_filePath);
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }
    }
}
