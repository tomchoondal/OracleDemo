using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for CollaborationVideoControl.xaml
    /// </summary>
    public partial class CollaborationVideoControl : UserControl
    {
        public CollaborationVideoControl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.DataContext = MainViewModel.Instance.CollaborationVM;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(CollaborationVideoControl), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CollaborationVideoControl).UpdateOpen();
        }

        private void UpdateOpen()
        {
            if (IsOpen)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }


        public bool IsVideoSharingStarted
        {
            get { return (bool)GetValue(IsVideoSharingStartedProperty); }
            set { SetValue(IsVideoSharingStartedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVideoSharingStarted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVideoSharingStartedProperty =
            DependencyProperty.Register("IsVideoSharingStarted", typeof(bool), typeof(CollaborationVideoControl), new PropertyMetadata(false, OnVideoSharingStartedChanged));


        private static void OnVideoSharingStartedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CollaborationVideoControl).UpdateVideoSharing();
        }

        private void UpdateVideoSharing()
        {
            if (IsVideoSharingStarted)
            {
                (this.Resources["VideoSharingOn"] as Storyboard).Begin();
            }
            else
            {
                (this.Resources["VideoSharingOff"] as Storyboard).Begin();
            }
        }

        public bool IsVideoPresentingAllowed
        {
            get { return (bool)GetValue(IsVideoPresentingAllowedProperty); }
            set { SetValue(IsVideoPresentingAllowedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVideoAllowed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVideoPresentingAllowedProperty =
            DependencyProperty.Register("IsVideoPresentingAllowed", typeof(bool), typeof(CollaborationVideoControl), new PropertyMetadata(false, OnIsVideoPresentingAllowedChanged));


        private static void OnIsVideoPresentingAllowedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CollaborationVideoControl).UpdateVideoPresenting();
        }

        private void UpdateVideoPresenting()
        {
            //if (IsVideoPresentingAllowed)
            //{
            //    (this.Resources["StartPresenting"] as Storyboard).Begin();
            //}
            //else
            //{
            //    (this.Resources["StopPresenting"] as Storyboard).Begin();
            //}
        }

        private void StartSharingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.TimerVM.StartTimer();
            MainViewModel.Instance.CollaborationVM.IsSharingAllowed = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsVideoShown = false;
            MainViewModel.Instance.CollaborationVM.IsVideoMinimized = true;
        }

        private void ExpandAgent_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.ToggleCollaborationState();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Your communication with the Agent is on Hold. You can resume your communication any time by tapping Resume");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
              "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        private void StopUserPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsUserPreviewShown = false;
        }
    }
}
