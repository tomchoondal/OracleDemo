using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for VideoControl.xaml
    /// </summary>
    public partial class VideoControl : UserControl
    {

        public VideoControl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.DataContext = MainViewModel.Instance.RemoteSupportVM;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(VideoControl), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as VideoControl).UpdateOpen();
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


        public bool CanDraw
        {
            get { return (bool)GetValue(CanDrawProperty); }
            set { SetValue(CanDrawProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanDraw.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanDrawProperty =
            DependencyProperty.Register("CanDraw", typeof(bool), typeof(VideoControl), new PropertyMetadata(false, OnCanDrawChanged));

        private static void OnCanDrawChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as VideoControl).UpdateDraw();
        }

        private void UpdateDraw()
        {
            if (CanDraw)
            {
                (this.Resources["Draw"] as Storyboard).Begin();
            }
            else
            {
                (this.Resources["NoDraw"] as Storyboard).Begin();
            }
        }

        private void StartSharingButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RemoteSupportVM.IsSharingAllowed = true;
        }

        private void FullScreenToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RemoteSupportVM.IsVideoMinimized = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
              "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }
      
    }
}
