using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for CollaborationControl.xaml
    /// </summary>
    public partial class CollaborationControl : UserControl
    {
        private bool IsVideoMaximized;
        public CollaborationControl()
        {
            InitializeComponent();
            MainViewModel.Instance.TimerVM.InitTimer();
        }

        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpened.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(CollaborationControl), new PropertyMetadata(false));

       
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsVideoMaximized)
            {
                (this.Resources["VideoMaximized"] as Storyboard).Begin();
                IsVideoMaximized = true;
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
               "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        private void VideoMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["VideoMinimized"] as Storyboard).Begin();
            IsVideoMaximized = false;
        }

        private void ScreenPreview_VideoMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsVideoShown = true;
            MainViewModel.Instance.CollaborationVM.IsVideoMinimized = false;
            MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.AgentExpanded;
        }

        private void AgentPreview_VideoMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsVideoShown = true;
            MainViewModel.Instance.CollaborationVM.IsVideoMinimized = false;
            MainViewModel.Instance.CollaborationVM.CollaborationState = CollaborationStates.VideoExpanded;
        }

        private void StopUserPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.IsUserPreviewShown = false;
            MainViewModel.Instance.ToastVM.ShowToast("You have stopped streaming your video. Agent video streaming and Audio communication will continue");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CollaborationVM.CanUserDraw = true;
        }
    }
}
