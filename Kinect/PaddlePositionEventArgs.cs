using System;

namespace KinectPong.Kinect
{
    public class PaddlePositionEventArgs : EventArgs
    {
        public int XPercentage { get; private set; }
        public int YPercentage { get; private set; }
        public int PlayerNumber { get; private set; }

        public PaddlePositionEventArgs(int playerNumber, int xPercentage, int yPercentage)
        {
            PlayerNumber = playerNumber;
            XPercentage = xPercentage;
            YPercentage = yPercentage;
        }
    }
}
