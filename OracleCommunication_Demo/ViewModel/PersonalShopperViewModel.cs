namespace OracleCommunication_Demo
{
    public class PersonalShopperViewModel : BaseViewModel
    {
        private bool isVideoMaximized;
        private bool isRecordingStarted;
        private bool isAudioPaused;
        private bool isVideoConnected = true;

        public bool IsVideoMaximized
        {
            get { return isVideoMaximized; }
            set
            {
                isVideoMaximized = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRecordingStarted
        {
            get { return isRecordingStarted; }
            set
            {
                isRecordingStarted = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAudioPaused
        {
            get { return isAudioPaused; }
            set
            {
                isAudioPaused = value;
                if (value)
                {
                    MainVM.ToastVM.ShowToast("Your communication with the Agent is on Hold. You can resume your communication any time by tapping Resume");
                    MainVM.TimerVM.PauseTimer();
                }
                else
                {
                    MainVM.TimerVM.StartTimer();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsVideoConnected
        {
            get { return isVideoConnected; }
            set
            {
                isVideoConnected = value;
                RaisePropertyChanged();
            }
        }


        public void StopService()
        {
            IsVideoMaximized = IsRecordingStarted = IsAudioPaused = false;
            IsVideoConnected = true;
        }
    }
}
