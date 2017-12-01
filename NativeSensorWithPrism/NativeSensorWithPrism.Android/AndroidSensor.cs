using Android.Hardware;
using Android.Content;
using Xamarin.Forms;

namespace NativeSensorWithPrism.Droid
{
    public class AndroidSensor : Java.Lang.Object, INativeSensor, ISensorEventListener
    {
        private static readonly object syncLock = new object();
        private SensorManager sensorManager;
        private double prevAccelTimeStamp;
        private double prevGyroTimeStamp;

        public string MobileDeviceName { get; private set; }
        public event AccelerometerEventHandler AccelerationReceived;
        public event GyroscopeEventHandler AngularVelocityReceived;

        public AndroidSensor()
        {
            sensorManager = (SensorManager)Forms.Context.GetSystemService(Context.SensorService);
            MobileDeviceName = Android.OS.Build.Manufacturer + " " + Android.OS.Build.Model;
        }

        // 計測開始
        public void Start()
        {
            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);
            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Normal);
        }

        // 計測終了
        public void Stop()
        {
            // 必要のない時はセンサーを無効にしないとバッテリーが消耗するので注意。
            sensorManager.UnregisterListener(this);
            prevGyroTimeStamp = 0;
            prevAccelTimeStamp = 0;
        }

        // センサの値を取得したとき
        public void OnSensorChanged(SensorEvent e)
        {
            switch (e.Sensor.Type)
            {
                case SensorType.Accelerometer:

                    // 加速度センサの取得間隔を取得(sec)
                    double nowAccelTimeStamp = e.Timestamp;
                    double accelInterval = (prevAccelTimeStamp != 0) ? (nowAccelTimeStamp - prevAccelTimeStamp) / 1000000000 : 0;
                    prevAccelTimeStamp = nowAccelTimeStamp;

                    // 加速度センサの値をセットする
                    AccelerationReceived(this, new SensorEventArgs
                    {
                        X = e.Values[0],
                        Y = e.Values[1],
                        Z = e.Values[2],
                        Interval = accelInterval
                    });
                    break;

                case SensorType.Gyroscope:

                    // ジャイロスコープの取得間隔を取得(sec)
                    double nowGyroTimeStamp = e.Timestamp;
                    double gyroInterval = (prevGyroTimeStamp != 0) ? (nowGyroTimeStamp - prevGyroTimeStamp) / 1000000000 : 0;
                    prevGyroTimeStamp = nowGyroTimeStamp;

                    // ジャイロスコープの値をセットしイベントを投げる
                    AngularVelocityReceived(this, new SensorEventArgs
                    {
                        X = e.Values[0],
                        Y = e.Values[1],
                        Z = e.Values[2],
                        Interval = gyroInterval
                    });
                    break;

                default:
                    break;
            }
        }

        // センサの値を取得する頻度が変わったとき
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy) { }
    }
}