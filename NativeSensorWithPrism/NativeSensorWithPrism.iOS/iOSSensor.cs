using CoreMotion;
using Foundation;
using UIKit;

namespace NativeSensorWithPrism.iOS
{
    public class iOSSensor : INativeSensor
    {
        private CMMotionManager motionManager;
        private double prevAccelTimeStamp;
        private double prevGyroTimeStamp;

        public string MobileDeviceName { get; private set; }
        public event AccelerometerEventHandler AccelerationReceived;
        public event GyroscopeEventHandler AngularVelocityReceived;

        public iOSSensor()
        {
            motionManager = new CMMotionManager();
            MobileDeviceName = UIDevice.CurrentDevice.SystemName + " " + UIDevice.CurrentDevice.SystemVersion + " " + UIDevice.CurrentDevice.Model;
        }

        public void Start()
        {
            // 加速度センサ
            if (motionManager.AccelerometerAvailable)
            {
                motionManager.AccelerometerUpdateInterval = 0.1;
                motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    // 加速度センサの取得間隔を取得(sec)
                    // CMAccelerometerData.Timestamp : デバイスが起動してからの秒単位の時間
                    double nowAccelTimeStamp = data.Timestamp;
                    double accelInterval = (prevAccelTimeStamp != 0) ? (nowAccelTimeStamp - prevAccelTimeStamp) : 0;
                    prevAccelTimeStamp = nowAccelTimeStamp;

                    AccelerationReceived(this, new SensorEventArgs
                    {
                        X = data.Acceleration.X,
                        Y = data.Acceleration.Y,
                        Z = data.Acceleration.Z,
                        Interval = accelInterval
                    });
                });
            }

            // ジャイロスコープ
            if (motionManager.GyroAvailable)
            {
                motionManager.GyroUpdateInterval = 0.1;
                motionManager.StartGyroUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    // ジャイロの取得間隔を取得(sec)
                    // CMGyroData.Timestamp : デバイスが起動してからの秒単位の時間
                    double nowGyroTimeStamp = data.Timestamp;
                    double gyroInterval = (prevGyroTimeStamp != 0) ? (nowGyroTimeStamp - prevGyroTimeStamp) : 0;
                    prevGyroTimeStamp = nowGyroTimeStamp;

                    AngularVelocityReceived(this, new SensorEventArgs
                    {
                        X = data.RotationRate.x,
                        Y = data.RotationRate.y,
                        Z = data.RotationRate.z,
                        Interval = gyroInterval
                    });
                });
            }
        }

        public void Stop()
        {
            motionManager.StopAccelerometerUpdates();
            motionManager.StopGyroUpdates();
            prevAccelTimeStamp = 0;
            prevGyroTimeStamp = 0;
        }
    }
}