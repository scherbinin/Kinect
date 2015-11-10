using System;
using System.Collections.Generic;

namespace BvhConverter.DataStructures
{
    public class FrameContainer
    {
        private List<CoordinateFrame> _frameContainer = new List<CoordinateFrame>();

        public void Add(CoordinateFrame frame)
        {
            _frameContainer.Add(frame);
        }
        
        public void Compensate()
        {
            for (int i = 0; i < _frameContainer.Count; i++)
            {
                if (i == 0) continue;//Первый пропустили

                _frameContainer[i].Diff(_frameContainer[0]);
            }
        }

        public override string ToString()
        {
            string frameList = "";

            foreach (var frame in _frameContainer)
                frameList += frame.ToString() + Environment.NewLine;

            return frameList;
        }

    }
}
