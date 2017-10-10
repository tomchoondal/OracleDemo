namespace OracleCommunication_Demo
{
    public class RemoteSupportViewModel : BaseViewModel
    {
        private bool isAudioPaused;
        private bool isDrawAllowed;
        private bool isVideoMinimized;
        private bool isSharingAllowed;
        private bool isVideoConnected;
        private VideoFallBackType videoFallBackStates;
        private bool isAudioConnected = true;
        private bool hasLoudSpeaker;

        public bool IsAudioPaused
        {
            get { return isAudioPaused; }
            set
            {
                isAudioPaused = value;
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

        public bool IsAudioConnected
        {
            get { return isAudioConnected; }
            set
            {
                isAudioConnected = value;
                RaisePropertyChanged();
            }
        }

        public bool HasLoudSpeaker
        {
            get { return hasLoudSpeaker; }
            set
            {
                hasLoudSpeaker = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDrawAllowed
        {
            get { return isDrawAllowed; }
            set
            {
                isDrawAllowed = value;
                RaisePropertyChanged();
            }
        }


        public bool IsVideoMinimized
        {
            get { return isVideoMinimized; }
            set
            {
                isVideoMinimized = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsVideoShown");
            }
        }

        public bool InverseIsSharingAllowed
        {
            get
            {
                return !isSharingAllowed;
            }
        }

        public bool IsSharingAllowed
        {
            get { return isSharingAllowed; }
            set
            {
                isSharingAllowed = value;
                RaisePropertyChanged();
                RaisePropertyChanged("InverseIsSharingAllowed");
            }
        }

        public bool IsVideoShown
        {
            get
            {
                return IsVideoConnected && !IsVideoMinimized;
            }
        }

        public bool IsVideoConnected
        {
            get { return isVideoConnected; }
            set
            {
                isVideoConnected = value;

                RaisePropertyChanged();
                if (value)
                {
                    MainVM.StartVideoCapture();
                }
                else
                {
                    MainVM.StopVideoPreview();
                }
                RaisePropertyChanged("IsVideoShown");
            }
        }

        public VideoFallBackType VideoFallBackStates
        {
            get { return videoFallBackStates; }
            set
            {
                videoFallBackStates = value;
                RaisePropertyChanged();
            }
        }

        public void StopVideo()
        {
            IsDrawAllowed = false;
            VideoFallBackStates = VideoFallBackType.None;
            IsVideoConnected = false;
            IsSharingAllowed = false;
            IsVideoMinimized = false;

        }

        public void Reset()
        {
            StopVideo();
            HasLoudSpeaker = false;
            IsAudioConnected = true;
            IsAudioPaused = false;
        }
    }
}
