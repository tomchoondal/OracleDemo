namespace OracleCommunication_Demo
{
    public class BasicGuidanceViewModel : BaseViewModel
    {
        private bool isChatOpen;
        private bool isChatMinimized;
        private bool isControlOpened;
        private bool hasAudio;
        private bool isAudioPaused;
        private bool isChatFullScreen;

        public bool IsChatOpen
        {
            get { return isChatOpen; }
            set
            {
                isChatOpen = value;
                RaisePropertyChanged();
            }
        }

        public bool IsChatMinimized
        {
            get { return isChatMinimized; }
            set
            {
                isChatMinimized = value;
                RaisePropertyChanged();
            }
        }

        public bool IsControlOpened
        {
            get { return isControlOpened; }
            set
            {
                isControlOpened = value;
                RaisePropertyChanged();
            }
        }

        public bool HasAudio
        {
            get { return hasAudio; }
            set
            {
                hasAudio = value;
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
                    MainViewModel.Instance.ToastVM.ShowToast("Your communication with the Agent is on Hold. You can resume your communication any time by tapping Resume");
                    MainVM.TimerVM.PauseTimer();
                }
                else
                {
                    MainVM.TimerVM.StartTimer();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsChatFullScreen
        {
            get { return isChatFullScreen; }
            set
            {
                isChatFullScreen = value;
                RaisePropertyChanged();
            }
        }

        public void StopService()
        {
            IsControlOpened = false;
            IsChatOpen = false;
            IsChatMinimized = false;
            HasAudio = false;
            IsAudioPaused = false;
            IsChatFullScreen = false;
            MainVM.TimerVM.StopTimer();
        }
    }
}
