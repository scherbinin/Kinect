using System.Collections.Generic;
using BvhConverter.Stuff;
using BvhConverter_new.Stuff;
using Microsoft.Kinect;

namespace BvhConverter.DataStructures
{
    public class CoordinateFrame
    {
        private List<EilerAngle> _AngleContainer = new List<EilerAngle>();
        private PozitionPoint _pozition;

        public List<EilerAngle> GetCollection
        {
            get { return _AngleContainer; }
        }

        public void AddPozitionPoint(SkeletonPoint point)
        {
            _pozition = new PozitionPoint(point.X, point.Y, point.Z);
        }

        public void AddAnglePoint(EilerAngle point)
        {
            _AngleContainer.Add(point);
        }

        public void Diff(CoordinateFrame diff)
        {
            if (diff != null && diff.GetCollection.Count > 0)
                for (int i = 0; i < diff.GetCollection.Count; i++)
                {
                    float x = _AngleContainer[i].X - diff.GetCollection[i].X;
                    float y = _AngleContainer[i].Y - diff.GetCollection[i].Y;
                    float z = _AngleContainer[i].Z - diff.GetCollection[i].Z;

                    _AngleContainer[i] = new EilerAngle(x, y, z);
                }
        }
        
        public override string ToString()
        {
            string frameTxt = _pozition.ToString();

            foreach (var pointDecriptor in _AngleContainer)
                frameTxt += pointDecriptor.ToString();

            return frameTxt;
        }
    }
}
