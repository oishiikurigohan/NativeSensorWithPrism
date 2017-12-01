using Prism.Commands;
using Prism.Navigation;

namespace NativeSensorWithPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, INativeSensor nativeSensor) : base(navigationService, nativeSensor)
        {
            Title = "Prism Test.";
            MobileDeviceName = nativeSensor.MobileDeviceName;
            StartCommand = new DelegateCommand(() => nativeSensor.Start());
            StopCommand = new DelegateCommand(() => nativeSensor.Stop());
            nativeSensor.AccelerationReceived += (sender, e) =>
            {
                AccelX = e.X.ToString();
                AccelY = e.Y.ToString();
                AccelZ = e.Z.ToString();
                AccelInterval = e.Interval.ToString();
            };
            nativeSensor.AngularVelocityReceived += (sender, e) =>
            {
                GyroX = e.X.ToString();
                GyroY = e.Y.ToString();
                GyroZ = e.Z.ToString();
                GyroInterval = e.Interval.ToString();
            };
        }
    }
}
