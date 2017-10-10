using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ShopperVideoControl.xaml
    /// </summary>
    public partial class ShopperVideoControl : UserControl
    {
        private DispatcherTimer timer;

        public ShopperVideoControl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.DataContext = MainViewModel.Instance.PersonalShopperVM;
            MainViewModel.Instance.TimerVM.InitTimer();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            switch (WebcamPreviewControl.Visibility)
            {
                case Visibility.Collapsed:
                    WebcamPreviewControl.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    WebcamPreviewControl.Visibility = Visibility.Collapsed;
                    break;
            }
            switch (WebcamPlaceHolderControl.Visibility)
            {
                case Visibility.Collapsed:
                    WebcamPlaceHolderControl.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    WebcamPlaceHolderControl.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ShopperVideoControl), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShopperVideoControl).UpdateOpen();
        }

        private void UpdateOpen()
        {
            if (IsOpen)
            {
                this.Visibility = Visibility.Visible;
                WebcamPreviewControl.PlayDemoVideo();
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick -= Timer_Tick;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                WebcamPreviewControl.StopDemoVideo();
                this.Visibility = Visibility.Collapsed;
                timer.Stop();
            }
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.PersonalShopperVM.IsVideoMaximized = false;
        }

        private void CaptureScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Your snapshot has been saved to your device’s photo library.");
        }

        private void BookMarkButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("You added a boomark to the video");
        }

        private void StopserviceButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            EditToggleButton.IsChecked = false;
            MainViewModel.Instance.PersonalShopperVM.IsRecordingStarted = false;
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Start Recording?", "Do you want to start recording this session for future reference", "Allow", "Deny", DialogType.StartRecording);
        }
    }
}
