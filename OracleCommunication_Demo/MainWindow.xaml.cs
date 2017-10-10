using OracleCommunication_Demo.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isconnecting;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainViewModel.Instance;
        }

        private void Dialog_CloseDialog(object sender, EventArgs e)
        {
            var dialog = sender as DialogControl;
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.Chat && dialog.IsAllowed)
            {
                MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = true;
                if (ChatControl.IsFullScreen)
                {
                    ChatControl.IsFullScreen = false;
                }
            }
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.Audio && dialog.IsAllowed)
            {
                MainViewModel.Instance.TimerVM.StartTimer();
                MainViewModel.Instance.BasicGuidanceVM.HasAudio = true;
                MainViewModel.Instance.BasicGuidanceVM.IsChatMinimized = true;
                MainViewModel.Instance.BasicGuidanceVM.IsControlOpened = true;
                MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = false;
                StartAudioPromptButton.IsEnabled = false;
                StopAudioPromptButton.IsEnabled = true;
                if (!StopControlPromptButton.IsEnabled)
                {
                    TakeControlPromptButton.IsEnabled = true;
                }
            }
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.AllowControl && dialog.IsAllowed)
            {
                StopControlButton.Visibility = Visibility.Visible;
                TakeControlPromptButton.IsEnabled = false;
                StopControlPromptButton.IsEnabled = true;
                MainViewModel.Instance.ConciergeVM.HasControl = true;
                ConciergeButton.IsOpened = true;
                MainViewModel.Instance.TimerVM.StartTimer();
            }
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.StopControl && dialog.IsAllowed)
            {
                StopControlButton.Visibility = Visibility.Collapsed;
                TakeControlPromptButton.IsEnabled = true;
                MainViewModel.Instance.ConciergeVM.HasControl = false;
            }

            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.StopService && dialog.IsAllowed)
            {
                isconnecting = false;
                MainViewModel.Instance.IsConnected = false;
                AppCombo.IsEnabled = true;
                StartAudioPromptButton.IsEnabled = true;
                RemoteNetworkGrid.IsEnabled = false;
                MainViewModel.Instance.CanPlayDemoVideo = false;
                ConciergeButton.Reset();
                MainViewModel.Instance.RemoteSupportVM.Reset();
                MainViewModel.Instance.ConciergeVM.StopService();
                MainViewModel.Instance.PersonalShopperVM.StopService();
                StopPresentingButton.IsEnabled = false;
                StartServiceButton.IsEnabled = true;
                StopControlButton.Visibility = Visibility.Collapsed;
                MainViewModel.Instance.BasicGuidanceVM.StopService();
                StopTwoWayVideoButton.IsEnabled = true;
                MainViewModel.Instance.CollaborationVM.Reset();
                (this.Resources["ConnectingAnimation"] as Storyboard).Stop();
                (this.Resources["ConnectingProgressAnim"] as Storyboard).Stop();
                (this.Resources["NotconnectingState"] as Storyboard).Begin();
                StopAudioPromptButton.IsEnabled = TakeControlPromptButton.IsEnabled = StopControlPromptButton.IsEnabled = false;
                MainViewModel.Instance.TimerVM.StopTimer();
            }

            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.StartVideo && dialog.IsAllowed)
            {
                RemoteNetworkGrid.IsEnabled = true;
                RemoteStartVideoSharing.IsEnabled = false;
                RemoteStopVideoSharing.IsEnabled = true;
                MainViewModel.Instance.RemoteSupportVM.IsVideoConnected = true;
            }
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.Meeting && dialog.IsAllowed)
            {
                StartConnecting();
                StartServiceButton.IsEnabled = false;
                AppCombo.IsEnabled = false;
                MainViewModel.Instance.IsConnected = true;
                MainViewModel.Instance.CollaborationVM.IsVideoShown = true;
                MainViewModel.Instance.CollaborationVM.IsCalenderVisible = false;
                MainViewModel.Instance.StartVideoCapture();
            }

            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.StopUserSharing && dialog.IsAllowed)
            {
                MainViewModel.Instance.CollaborationVM.IsUserScreenShared = false;
            }
            if (MainViewModel.Instance.DialogVM.DialogState == DialogType.StartRecording && dialog.IsAllowed)
            {
                MainViewModel.Instance.PersonalShopperVM.IsRecordingStarted = true;
            }

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            StopService();
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartConnecting();
        }

        private void StartConnecting()
        {
            if (!isconnecting)
            {
                isconnecting = true;
                (this.Resources["ConnectingAnimation"] as Storyboard).Begin();
                (this.Resources["ConnectingProgressAnim"] as Storyboard).Begin();
                MainViewModel.Instance.ChatVM.Init();
            }
            else
            {
                isconnecting = false;
                MainViewModel.Instance.IsConnected = false;
                AppCombo.IsEnabled = true;
                (this.Resources["ConnectingAnimation"] as Storyboard).Stop();
                (this.Resources["ConnectingProgressAnim"] as Storyboard).Stop();
                (this.Resources["NotconnectingState"] as Storyboard).Begin();

            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

            (this.Resources["ConnectedAnim"] as Storyboard).Begin();
        }

        private void AudioPromptButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Allow Us to Speak with You",
               "We will be able to hear each other. You can mute yourself at any time by tapping Hold.",
               "Allow", "Deny", DialogType.Audio);
        }

        private void TakeControlPromptButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Allow Us to Take Control",
                  "We will be able to control this app remotely and change your settings. You can take back control at any time.",
                  "Allow", "Deny", DialogType.AllowControl);
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Representative has stopped remote sharing. You have control again.");
            StopControlButton.Visibility = Visibility.Collapsed;
            StopControlPromptButton.IsEnabled = false;
            TakeControlPromptButton.IsEnabled = true;
            MainViewModel.Instance.ConciergeVM.HasControl = false;
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
            MainViewModel.Instance.IsConnected = true;
            AppCombo.IsEnabled = false;
            if (MainViewModel.Instance.DemoState == DemoType.BasicGuidance)
            {
                MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = true;
            }
            if (MainViewModel.Instance.DemoState == DemoType.RemoteSupport)
            {
                MainViewModel.Instance.TimerVM.StartTimer();
                HomeRemoteButton.IsOpened = true;
                RemoteStartVideoSharing.IsEnabled = true;
                RemoteStopVideoSharing.IsEnabled = false;
            }
            if (MainViewModel.Instance.DemoState == DemoType.Concierge)
            {
                MainViewModel.Instance.DialogVM.ShowDialog("Allow Us to Take Control",
                "We will be able to control this app remotely and change your settings. You can take back control at any time.",
                "Allow", "Deny", DialogType.AllowControl);

            }
            if (MainViewModel.Instance.DemoState == DemoType.Personal_Shopper)
            {
                PersonalShopperControl.IsOpened = true;
                MainViewModel.Instance.CanPlayDemoVideo = true;
                MainViewModel.Instance.TimerVM.StartTimer();
            }

            if (MainViewModel.Instance.DemoState == DemoType.Collaboration)
            {
                MainViewModel.Instance.CollaborationVM.CanPresentScreen = true;
                CollaborationControl.IsOpened = true;
            }
        }

        private void StopAudioButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Voice Conversation Stopped", "Representative has stopped Voice conversation.Chat conversation will continue", "Ok", null, DialogType.Audio, DialogMode.SingleAction);
            MainViewModel.Instance.BasicGuidanceVM.HasAudio = false;
            MainViewModel.Instance.BasicGuidanceVM.IsChatMinimized = false;
            MainViewModel.Instance.BasicGuidanceVM.IsControlOpened = false;
            MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = true;
            MainViewModel.Instance.BasicGuidanceVM.IsAudioPaused = false;
            MainViewModel.Instance.TimerVM.StopTimer();
            TakeControlPromptButton.IsEnabled = false;
            StopAudioPromptButton.IsEnabled = false;
            StartAudioPromptButton.IsEnabled = true;
        }

        private void StopControlButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Stop Control",
               "Do you wish to stop the Agent from remotely controlling your app?", "Yes", "No", DialogType.StopControl);
            StopControlPromptButton.IsEnabled = false;
        }

        private void ChromeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ChatControl_OnFullScreenToggled(object sender, EventArgs e)
        {
            if (ChatControl.IsFullScreen)
            {
                (this.Resources["HomeChatButtonCollapsed"] as Storyboard).Begin();
            }
            else
            {
                (this.Resources["HomeChatButtonVisible"] as Storyboard).Begin();
            }
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
            StopService();
        }

        private void StopService()
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Service Ended", "Service ended. Thank you for using our service today!", "Ok", null, DialogType.Audio, DialogMode.SingleAction);
            isconnecting = false;
            MainViewModel.Instance.IsConnected = false;
            AppCombo.IsEnabled = true;
            StartAudioPromptButton.IsEnabled = true;
            MainViewModel.Instance.BasicGuidanceVM.StopService();
            MainViewModel.Instance.CanPlayDemoVideo = false;
            StopControlButton.Visibility = Visibility.Collapsed;
            RemoteNetworkGrid.IsEnabled = false;
            StartServiceButton.IsEnabled = true;
            MainViewModel.Instance.RemoteSupportVM.Reset();
            MainViewModel.Instance.ConciergeVM.StopService();
            MainViewModel.Instance.PersonalShopperVM.StopService();
            ConciergeButton.Reset();
            StopTwoWayVideoButton.IsEnabled = true;
            StopPresentingButton.IsEnabled = false;
            MainViewModel.Instance.CollaborationVM.Reset();
            MainViewModel.Instance.TimerVM.StopTimer();
            (this.Resources["ConnectingAnimation"] as Storyboard).Stop();
            (this.Resources["ConnectingProgressAnim"] as Storyboard).Stop();
            (this.Resources["NotconnectingState"] as Storyboard).Begin();
            StopAudioPromptButton.IsEnabled = TakeControlPromptButton.IsEnabled = StopControlPromptButton.IsEnabled = false;
            DisconnectAudioJackButton.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.IsDialogOpen = false;
            ComboBoxItem selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;
            DemoType demoType = DemoType.BasicGuidance;
            switch (selectedItem.Content.ToString())
            {
                case "Basic Guidance":
                    demoType = DemoType.BasicGuidance;
                    break;
                case "Remote Support":
                    demoType = DemoType.RemoteSupport;
                    break;
                case "Personal Shopper":
                    demoType = DemoType.Personal_Shopper;
                    break;
                case "Concierge":
                    demoType = DemoType.Concierge;
                    break;
                case "Collaboration":
                    demoType = DemoType.Collaboration;
                    MainViewModel.Instance.CollaborationVM.IsCalenderVisible = false;
                    break;

            }
            MainViewModel.Instance.DemoState = demoType;
        }

        private void RemoteStartVideoSharing_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Allow Us to See Your Surroundings",
                "We will use your device's camera to better assess your needs. You can stop the video and mute the audio at any time by tapping Hold.",
                "Allow", "Deny", DialogType.StartVideo);
        }



        private void RemoteStopVideoSharing_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Thank you we've finished using your camera");
            RemoteNetworkGrid.IsEnabled = false;
            MainViewModel.Instance.RemoteSupportVM.StopVideo();
            RemoteStopVideoSharing.IsEnabled = false;
            RemoteStartVideoSharing.IsEnabled = true;
            MainViewModel.Instance.CanPlayDemoVideo = false;
        }

        private void DrawOnVideoButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RemoteSupportVM.IsDrawAllowed = true;
        }



        private void ReconnectVideoButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Oops! we've had a problem. We are attempting to reconnect you");
            MainViewModel.Instance.RemoteSupportVM.VideoFallBackStates = VideoFallBackType.Reconnecting;
        }

        private void DowngradeVideoToAudioButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Video sharing temporarily unavailable. Let’s switch to Audio conversation.",
                 "/OracleCommunication_Demo;component/Images/Toast_Notconnected.png");
            RemoteNetworkGrid.IsEnabled = false;
            RemoteStopVideoSharing.IsEnabled = false;
            RemoteStartVideoSharing.IsEnabled = true;
            MainViewModel.Instance.RemoteSupportVM.StopVideo();
            MainViewModel.Instance.CanPlayDemoVideo = false;
        }

        private void TroubleReconnectingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Sorry we are having trouble reconnecting. Please refer FAQ for more details",
                   "/OracleCommunication_Demo;component/Images/Toast_TroubleConnecting.png");
            MainViewModel.Instance.RemoteSupportVM.VideoFallBackStates = VideoFallBackType.Reconnectionfailed;
        }

        private void StopVideoPersonalShopperButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Agent has stopped video communication. Audio communication will continue");
            MainViewModel.Instance.PersonalShopperVM.IsVideoConnected = MainViewModel.Instance.PersonalShopperVM.IsVideoMaximized = false;
            MainViewModel.Instance.CanPlayDemoVideo = false;
        }

        private void StartPresentingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.ToggleCollaborationState();
            MainViewModel.Instance.CollaborationVM.IsVideoPresentingAllowed = true;
            MainViewModel.Instance.CollaborationVM.CanPresentScreen = false;
            StopPresentingButton.IsEnabled = true;
        }

        private void StopPresentingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsVideoShown = false;
            MainViewModel.Instance.CollaborationVM.IsVideoMinimized = true;
            MainViewModel.Instance.CollaborationVM.CanPresentScreen = true;
            StopPresentingButton.IsEnabled = false;
            MainViewModel.Instance.CollaborationVM.IsVideoPresentingAllowed = false;
            MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.VideoExpanded;
            MainViewModel.Instance.CollaborationVM.CanAgentDraw = false;
            MainViewModel.Instance.ToastVM.ShowToast("Representative have stopped screen sharing. Video conversation will Continue");
        }

        private void ScheduleMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsCalenderVisible = true;
        }

        private void StartDrawButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.CanAgentDraw = true;
        }

        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Calender Reminder", "Meeting with Aaron to discuss investment options", "Attend", "Close", DialogType.Meeting);
        }

        private void StopScreenShareButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Stop Screen Sharing", "Representative is requesting you to stop sharing your screen.", "Stop Sharing", "Cancel", DialogType.StopUserSharing);
        }

        private void DisconnectAudioJackButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RemoteSupportVM.IsAudioConnected = false;
            MainViewModel.Instance.ToastVM.ShowToast("You have unplugged the headphones. Please use your earpiece to continue conversation.Use the speaker button for loud speakers");
            DisconnectAudioJackButton.IsEnabled = false;
        }

        private void StopTwoWayVideoButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsVideoTurnedOff = true;
            StopTwoWayVideoButton.IsEnabled = false;
            MainViewModel.Instance.ToastVM.ShowToast("Agent has stopped video communication. Audio communication will continue");
        }

        private void StopPresentingCollaborationButtonClick(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsUserScreenShared = false;
        }
    }

}
