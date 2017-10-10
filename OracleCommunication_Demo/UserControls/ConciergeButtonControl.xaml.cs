using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ConciergeButtonControl.xaml
    /// </summary>
    public partial class ConciergeButtonControl : UserControl
    {
        private bool IsVideoMaximized;
        public ConciergeButtonControl()
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
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(ConciergeButtonControl), new PropertyMetadata(false));

       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        public void Reset()
        {
            PlayPauseToggleButton.IsChecked = false;
            IsVideoMaximized = false;
            IsOpened = false;
            (this.Resources["VideoMinimized"] as Storyboard).Begin();
        }

        private void VideoMinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["VideoMinimized"] as Storyboard).Begin();
            IsVideoMaximized = false;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsVideoMaximized)
            {
                (this.Resources["VideoMaximized"] as Storyboard).Begin();
                IsVideoMaximized = true;
            }
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ConciergeVM.IsFullScreen = true;
        }
    }
}
