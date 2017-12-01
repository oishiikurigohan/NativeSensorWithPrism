using System;

namespace NativeSensorWithPrism
{
    // センサの値が更新されたときのイベントのパラメータ
    public class SensorEventArgs : EventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Interval { get; set; }
    }

    // センサの値が更新されたときのイベントハンドラ
    public delegate void AccelerometerEventHandler(object sender, SensorEventArgs args);
    public delegate void GyroscopeEventHandler(object sender, SensorEventArgs args);

    public interface INativeSensor
    {
        string MobileDeviceName { get; }
        event AccelerometerEventHandler AccelerationReceived;
        event GyroscopeEventHandler AngularVelocityReceived;
        void Start();
        void Stop();
    }
}
