using System;
using System.Windows;
using System.Windows.Controls;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for WebcamPreviewControl.xaml
    /// </summary>
    public partial class WebcamPreviewControl : UserControl
    {
        public WebcamPreviewControl()
        {
            InitializeComponent();
        }

        private void video_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.Instance.CanPlayDemoVideo)
            {
                PlayDemoVideo();
            }
            video.MediaEnded -= video_MediaEnded;
            video.MediaEnded += video_MediaEnded;
        }

        public void PlayDemoVideo()
        {
            video.Play();
        }

        public void StopDemoVideo()
        {
            video.Stop();
        }

        private void video_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.Instance.CanPlayDemoVideo)
            {
                video.Position = TimeSpan.Zero;
                video.Play();
            }
            else
            {
                video.Position = TimeSpan.Zero;
            }
        }
}
}
