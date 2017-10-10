using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for PersonalShopperControl.xaml
    /// </summary>
    public partial class PersonalShopperControl : UserControl
    {
        private bool isVideoMaximized;
        private DispatcherTimer timer;

        public PersonalShopperControl()
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
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(PersonalShopperControl), new PropertyMetadata(false));


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        public void Reset()
        {
            isVideoMaximized = false;
            IsOpened = false;
            (this.Resources["VideoMinimized"] as Storyboard).Begin();
        }

        private void VideoMinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["VideoMinimized"] as Storyboard).Begin();
            isVideoMaximized = false;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!isVideoMaximized)
            {
                (this.Resources["VideoMaximized"] as Storyboard).Begin();
                isVideoMaximized = true;
                InitTimer();
                MainViewModel.Instance.CanPlayDemoVideo = true;
                WebcamPreview.PlayDemoVideo();
            }
        }

        private void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void VidoMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.PersonalShopperVM.IsVideoMaximized = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            switch (WebcamPreview.Visibility)
            {
                case Visibility.Collapsed:
                    WebcamPreview.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    WebcamPreview.Visibility = Visibility.Collapsed;
                    break;
            }
            switch (VideoPreview.Visibility)
            {
                case Visibility.Collapsed:
                    VideoPreview.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    VideoPreview.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
