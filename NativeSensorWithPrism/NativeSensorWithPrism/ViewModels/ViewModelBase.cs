using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace NativeSensorWithPrism.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        private INativeSensor NativeSensor { get; }

        private string mobileDeviceName;
        public string MobileDeviceName
        {
            get { return mobileDeviceName; }
            set { SetProperty(ref mobileDeviceName, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string accelX;
        public string AccelX
        {
            get { return accelX; }
            set { SetProperty(ref accelX, value); }
        }

        private string accelY;
        public string AccelY
        {
            get { return accelY; }
            set { SetProperty(ref accelY, value); }
        }

        private string accelZ;
        public string AccelZ
        {
            get { return accelZ; }
            set { SetProperty(ref accelZ, value); }
        }

        private string accelInterval;
        public string AccelInterval
        {
            get { return accelInterval; }
            set { SetProperty(ref accelInterval, value); }
        }

        private string gyroX;
        public string GyroX
        {
            get { return gyroX; }
            set { SetProperty(ref gyroX, value); }
        }

        private string gyroY;
        public string GyroY
        {
            get { return gyroY; }
            set { SetProperty(ref gyroY, value); }
        }

        private string gyroZ;
        public string GyroZ
        {
            get { return gyroZ; }
            set { SetProperty(ref gyroZ, value); }
        }

        private string gyroInterval;
        public string GyroInterval
        {
            get { return gyroInterval; }
            set { SetProperty(ref gyroInterval, value); }
        }

        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }

        public ViewModelBase(INavigationService navigationService, INativeSensor nativeSensor)
        {
            NavigationService = navigationService;
            this.NativeSensor = nativeSensor;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }
        public virtual void OnNavigatedTo(NavigationParameters parameters) { }
        public virtual void OnNavigatingTo(NavigationParameters parameters) { }
        public virtual void Destroy() { }
    }
}
