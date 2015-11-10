using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Main.Common;
using Microsoft.Kinect;

namespace Main
{
    public static class DataWorker
    {
        private const string FileName = "LastMoovement.rec";

        public static void SaveBvhContent(string bvhContent)
        {
            if (string.IsNullOrEmpty(bvhContent))
            {
                MessageBox.Show("Нет данных для записи");
                return;
            }

            var saveDialog = new SaveFileDialog()
            {
                FileName = "record.bvh",
                DefaultExt = "bvh"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveDialog.FileName, bvhContent);
        }

        public static void SaveMoovement(SaveDataStruct saveObj)
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();

            bf.Serialize(ms, saveObj);

            File.WriteAllBytes(FileName, ms.GetBuffer());
        }

        public static SaveDataStruct Load()
        {
            if (!File.Exists(FileName))
                return null;

            var bf = new BinaryFormatter();
            var ms = new MemoryStream(File.ReadAllBytes(FileName));
            object obj = bf.Deserialize(ms);
            ms.Close();

            return (SaveDataStruct)obj;
        }

        public static string Print(bool isPointNeed, bool isMatrixNeed, Skeleton skelet)
        {
            string rez = "======= Данные по одному фрейму ======" + Environment.NewLine;

            if (isMatrixNeed)
                rez += DoMatrixPrint(skelet.BoneOrientations);

            if (isPointNeed)
                rez += DoPointsPrint(skelet.Joints);

           

            return rez;
        }

        private static string DoPointsPrint(JointCollection jointCollection)
        {
            string rez = "";

            foreach (Joint joint in jointCollection)
            {
                rez += joint.JointType.ToString() + ": " +
                       "X=" + joint.Position.X.ToString("0.00", CultureInfo.InvariantCulture) +
                       "  Y=" + joint.Position.Y.ToString("0.00", CultureInfo.InvariantCulture) +
                       "  Z=" + joint.Position.Z.ToString("0.00", CultureInfo.InvariantCulture)
                       + Environment.NewLine;
            }

            rez += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;

            return rez;
        }

        private static string DoMatrixPrint(BoneOrientationCollection boneOrientationCollection)
        {
            string rez = "+++++++++++Данные по матрицам+++++++++" + Environment.NewLine;

            foreach (BoneOrientation bone in boneOrientationCollection)
            {
                rez += "Кость: " + bone.StartJoint + "-" + bone.EndJoint + Environment.NewLine;

                rez += "===AbsoluteRotation===" + PrintMatrix(bone.AbsoluteRotation.Matrix);
                rez += "===HierarchicalRotation===" + PrintMatrix(bone.HierarchicalRotation.Matrix);

                rez += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
            }

            return rez;
        }

        private static string PrintMatrix(Matrix4 mat)
        {
            string str = Environment.NewLine;

            str += "M11=" + mat.M11 + Environment.NewLine;
            str += "M12=" + mat.M12 + Environment.NewLine;
            str += "M13=" + mat.M13 + Environment.NewLine;
            str += "M21=" + mat.M21 + Environment.NewLine;
            str += "M22=" + mat.M22 + Environment.NewLine;
            str += "M23=" + mat.M23 + Environment.NewLine;
            str += "M31=" + mat.M31 + Environment.NewLine;
            str += "M32=" + mat.M32 + Environment.NewLine;
            str += "M33=" + mat.M33 + Environment.NewLine;

            return str;
        }
    }
}
