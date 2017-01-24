using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KinectPong.Kinect
{
    public class KinectHelper : IDisposable
    {
        #region attr

        private const int MapDepthToByte = 8000 / 256; // Map depth range to byte range

        private KinectSensor _kinectSensor = null;

        private DepthFrameReader _depthFrameReader = null;

        private FrameDescription _depthFrameDescription = null;

        private Bitmap _depthBitmap = null;

        private readonly Dictionary<int, int> _whitePixels = new Dictionary<int, int>();

        #endregion

        public event EventHandler<PaddlePositionEventArgs> PositionDetermined;

        private void OnPositionDetermined(PaddlePositionEventArgs args)
        {
            if (PositionDetermined != null)
                PositionDetermined(this, args);
        }

        public KinectHelper()
        {
            _kinectSensor = KinectSensor.GetDefault();
            _kinectSensor.IsAvailableChanged += Sensor_IsAvailableChanged;

            _depthFrameReader = _kinectSensor.DepthFrameSource.OpenReader();
            _depthFrameReader.FrameArrived += Reader_FrameArrived;
            _depthFrameDescription = _kinectSensor.DepthFrameSource.FrameDescription;

            _depthBitmap = new Bitmap(_depthFrameDescription.Width, _depthFrameDescription.Height);
        }

        public void Start()
        {
            _kinectSensor.Open();
        }

        private void Reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            var depthFrameProcessed = false;
            using (var depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame != null)
                {
                    using (var depthBuffer = depthFrame.LockImageBuffer())
                    {
                        if (((_depthFrameDescription.Width * _depthFrameDescription.Height) == (depthBuffer.Size / _depthFrameDescription.BytesPerPixel)) &&
                            (_depthFrameDescription.Width == _depthBitmap.Width) && (_depthFrameDescription.Height == _depthBitmap.Height))
                        {
                            _whitePixels.Clear();
                            ProcessDepthFrameData(depthBuffer.UnderlyingBuffer, depthBuffer.Size, minDepth: 1000, maxDepth: 1100);
                            depthFrameProcessed = true;
                        }
                    }
                }
            }

            if (depthFrameProcessed)
            {
                var left = _whitePixels.Where(p => p.Key < _depthFrameDescription.Width / 2).OrderByDescending(p => p.Key);
                if (left.Any())
                {
                    var leftPos = left.First();
                    OnPositionDetermined(new PaddlePositionEventArgs(1, leftPos.Key, leftPos.Value / _depthFrameDescription.Height * 100));
                }

                if (!Program.IsTwoPlayerMode)
                    return;

                var right = _whitePixels.Where(p => p.Key > _depthFrameDescription.Width / 2).OrderByDescending(p => p.Key);
                if (right.Any())
                {
                    var rightPos = right.First();
                    OnPositionDetermined(new PaddlePositionEventArgs(2, rightPos.Key, rightPos.Value / _depthFrameDescription.Height * 100));
                }
            }
        }

        private unsafe void ProcessDepthFrameData(IntPtr depthFrameData, uint depthFrameDataSize, ushort minDepth, ushort maxDepth)
        {
            ushort* frameData = (ushort*)depthFrameData;

            for (int i = 0; i < (int)(depthFrameDataSize / _depthFrameDescription.BytesPerPixel); ++i)
            {
                ushort pixelDepth = frameData[i];

                var detected = pixelDepth >= minDepth && pixelDepth <= maxDepth;
                if (detected)
                    _whitePixels.Add(i % _depthFrameDescription.Width, i / _depthFrameDescription.Height);
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
        }

        public void Dispose()
        {
            if(_depthFrameReader != null)
            {
                _depthFrameReader.Dispose();
                _depthFrameReader = null;
            }
            if (_kinectSensor != null)
            {
                _kinectSensor.Close();
                _kinectSensor = null;
            }
        }
    }
}
