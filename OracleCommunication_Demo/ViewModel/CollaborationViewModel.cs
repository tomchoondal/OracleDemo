namespace OracleCommunication_Demo
{
    public class CollaborationViewModel : BaseViewModel
    {
        private bool isServicePaused;
        private bool isSharingAllowed;
        private bool isVideoConnected;
        private bool isVideoShown;
        private bool isVideoPresentingAllowed;
        private bool isVideoMinimized;
        private CollaborationStates collaborationState = CollaborationStates.VideoExpanded;
        private bool isUserScreenShared;
        private bool canUserDraw;
        private bool canAgentDraw;
        private bool canPresentScreen = true;
        private bool isCalenderVisible;
        private bool isUserPreviewShown = true;
        private bool isAgentPreviewShown = true;
        private bool isVideoTurnedOff;

        public bool IsServicePaused
        {
            get { return isServicePaused; }
            set
            {
                isServicePaused = value;
                if (value)
                {
                    MainViewModel.Instance.TimerVM.PauseTimer();
                    MainViewModel.Instance.ToastVM.ShowToast("Your communication with the Agent is on Hold. You can resume your communication any time by tapping Resume");
                }
                else
                {
                    MainViewModel.Instance.TimerVM.StartTimer();
                }
                RaisePropertyChanged();
            }
        }

        public bool CanUserDraw
        {
            get { return canUserDraw; }
            set
            {
                canUserDraw = value;
                RaisePropertyChanged();
            }
        }

        public bool CanAgentDraw
        {
            get { return canAgentDraw; }
            set
            {
                canAgentDraw = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCalenderVisible
        {
            get { return isCalenderVisible; }
            set
            {
                isCalenderVisible = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSharingAllowed
        {
            get { return isSharingAllowed; }
            set
            {
                isSharingAllowed = value;
                RaisePropertyChanged();
                RaisePropertyChanged("CanPresentScreen");
            }
        }

        public bool IsUserPreviewShown
        {
            get { return isUserPreviewShown; }
            set
            {
                isUserPreviewShown = value;
                RaisePropertyChanged();
                RaisePropertyChanged("HasUserPreviewOnPresenting");
                RaisePropertyChanged("IsVideoTurnedOff");
            }
        }

        public bool HasUserPreviewOnPresenting
        {
            get { return IsUserPreviewShown && InverseIsVideoPresentingAllowed; }
        }


        public bool IsAgentPreviewShown
        {
            get { return isAgentPreviewShown; }
            set
            {
                isAgentPreviewShown = value;
                RaisePropertyChanged();
            }
        }

        public bool IsUserScreenShared
        {
            get { return isUserScreenShared; }
            set
            {
                isUserScreenShared = value;
                if (!value)
                {
                    MainVM.ToastVM.ShowToast("You have stopped screen sharing to the Representative.");
                    CanUserDraw = false;
                }
                RaisePropertyChanged();
                RaisePropertyChanged("CanPresentScreen");
            }
        }

        public bool CanPresentScreen
        {
            get
            {
                return canPresentScreen && IsSharingAllowed && !IsUserScreenShared;
            }
            set
            {
                canPresentScreen = value;
                RaisePropertyChanged();
            }
        }

        public bool IsVideoPresentingAllowed
        {
            get { return isVideoPresentingAllowed; }
            set
            {
                isVideoPresentingAllowed = value;
                RaisePropertyChanged();
                RaisePropertyChanged("InverseIsVideoPresentingAllowed");
                RaisePropertyChanged("HasUserPreviewOnPresenting");
            }
        }
        public bool InverseIsVideoPresentingAllowed
        {
            get { return !IsVideoPresentingAllowed; }
        }

        public CollaborationStates CollaborationState
        {
            get { return collaborationState; }
            set
            {
                collaborationState = value;
                RaisePropertyChanged();
            }
        }

        public bool IsVideoShown
        {
            get { return isVideoShown; }
            set
            {
                isVideoShown = value;
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

        public bool IsVideoTurnedOff
        {
            get { return isVideoTurnedOff; }
            set
            {
                isVideoTurnedOff = value;
                if (value)
                {
                    IsUserPreviewShown = false;
                }
                RaisePropertyChanged();
            }
        }
        public void Reset()
        {
            IsVideoShown = false;
            IsVideoConnected = false;
            IsVideoMinimized = false;
            IsSharingAllowed = false;
            IsVideoPresentingAllowed = false;
            if (IsUserScreenShared)
            {
                IsUserScreenShared = false;
            }
            CanPresentScreen = false;
            CanAgentDraw = false;
            CanUserDraw = false;
            IsCalenderVisible = true;
            IsUserPreviewShown = true;
            IsVideoTurnedOff = false;
            MainVM.TimerVM.StopTimer();
            MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.VideoExpanded;
        }

        public void ToggleCollaborationState()
        {
            switch (MainViewModel.Instance.CollaborationVM.CollaborationState)
            {
                case CollaborationStates.AgentExpanded:
                    MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.VideoExpanded;
                    break;
                case CollaborationStates.VideoExpanded:
                    MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.AgentExpanded;
                    break;
            }
        }
    }
}
