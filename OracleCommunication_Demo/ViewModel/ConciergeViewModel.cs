namespace OracleCommunication_Demo
{
    public class ConciergeViewModel : BaseViewModel
    {
        private bool isFullScreen;
        private bool isVideoPaused;
        private bool hasControl;

        public bool IsFullScreen
        {
            get { return isFullScreen; }
            set
            {
                isFullScreen = value;
                RaisePropertyChanged();
            }
        }

        public bool IsVideoPaused
        {
            get { return isVideoPaused; }
            set
            {
                isVideoPaused = value;
                if (value)
                {
                    MainVM.TimerVM.PauseTimer();
                    MainVM.ToastVM.ShowToast("Your communication with the Agent is on Hold.You can resume your communication any time by tapping Resume");
                }
                else
                {
                    MainVM.TimerVM.StartTimer();
                }
                RaisePropertyChanged();
            }
        }

        public bool HasControl
        {
            get { return hasControl; }
            set
            {
                hasControl = value;
                RaisePropertyChanged();
            }
        }

        public void StopService()
        {
            IsVideoPaused = HasControl = IsFullScreen = false;
        }
    }
}
