using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace OracleCommunication_Demo
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Instance = this;
        }

        private bool isConnected;
        private DemoType demoState;
        private ChatViewModel chatVM;
        private RemoteSupportViewModel remoteSupportVM;
        private DialogViewModel dialogVM;
        private ConciergeViewModel conciergeVM;
        private ToastViewModel toastVM;
        private PersonalShopperViewModel personalShopperVM;
        private CollaborationViewModel collaborationVM;
        private BitmapImage videoPreview;
        private FilterInfoCollection LocalWebCamsCollection;
        private VideoCaptureDevice LocalWebCam;
        private bool isWebcamAvailable;
        private double appHeight = 1087, appWidth = 548;
        private TimerViewModel timerVM;
        private double bezelHeight = 105.5, bezelRightWidth = 26, bezelLeftWidth = 26;
        private bool isHomeButtonVisible;
        private bool canPlayDemoVideo;
        private WebcamStates webcamState = WebcamStates.Normal;
        private BasicGuidanceViewModel basicGuidanceVM;

        public static MainViewModel Instance { get; set; }

        public ChatViewModel ChatVM
        {
            get
            {
                return chatVM ?? (chatVM = new ChatViewModel());
            }
        }

        public RemoteSupportViewModel RemoteSupportVM
        {
            get
            {
                return remoteSupportVM ?? (remoteSupportVM = new RemoteSupportViewModel());
            }
        }

        public BasicGuidanceViewModel BasicGuidanceVM
        {
            get
            {
                return basicGuidanceVM ?? (basicGuidanceVM = new BasicGuidanceViewModel());
            }
        }

        public DialogViewModel DialogVM
        {
            get
            {
                return dialogVM ?? (dialogVM = new DialogViewModel());
            }
        }

        public ToastViewModel ToastVM
        {
            get
            {
                return toastVM ?? (toastVM = new ToastViewModel());
            }
        }

        public ConciergeViewModel ConciergeVM
        {
            get
            {
                return conciergeVM ?? (conciergeVM = new ConciergeViewModel());
            }
        }

        public PersonalShopperViewModel PersonalShopperVM
        {
            get
            {
                return personalShopperVM ?? (personalShopperVM = new PersonalShopperViewModel());
            }
        }

        public CollaborationViewModel CollaborationVM
        {
            get
            {
                return collaborationVM ?? (collaborationVM = new CollaborationViewModel());
            }
        }

        public TimerViewModel TimerVM
        {
            get
            {
                return timerVM ?? (timerVM = new TimerViewModel());
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                RaisePropertyChanged();
            }
        }

        public bool CanPlayDemoVideo
        {
            get { return canPlayDemoVideo; }
            set
            {
                canPlayDemoVideo = value;
                RaisePropertyChanged();
            }
        }

        public BitmapImage VideoPreview
        {
            get { return videoPreview; }
            set
            {
                videoPreview = value;
                RaisePropertyChanged();
            }
        }

        public bool IsWebcamAvailable
        {
            get { return isWebcamAvailable; }
            set
            {
                isWebcamAvailable = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCollaboration
        {
            get { return DemoState == DemoType.Collaboration; }
        }

        public DemoType DemoState
        {
            get { return demoState; }
            set
            {
                demoState = value;
                if (demoState == DemoType.Collaboration)
                {
                    SetTabletSize();
                }
                else
                {
                    ResetAppSize();
                }
                RaisePropertyChanged();
                RaisePropertyChanged("IsCollaboration");
                if (DemoState == DemoType.Personal_Shopper)
                {
                    WebcamState = WebcamStates.Demo;
                }
                IsHomeButtonVisible = DemoState != DemoType.Collaboration;
            }
        }

        public WebcamStates WebcamState
        {
            get { return webcamState; }
            set
            {
                webcamState = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsDemoWebcamState");
            }
        }

        public bool IsDemoWebcamState
        {
            get { return WebcamState == WebcamStates.Demo; }
        }

        public double AppHeight
        {
            get { return appHeight; }
            set
            {
                appHeight = value;
                RaisePropertyChanged();
            }
        }

        public double AppWidth
        {
            get { return appWidth; }
            set
            {
                appWidth = value;
                RaisePropertyChanged();
            }
        }

        public double BezelHeight
        {
            get { return bezelHeight; }
            set
            {
                bezelHeight = value;
                RaisePropertyChanged();
            }
        }

        public double BezelRightWidth
        {
            get { return bezelRightWidth; }
            set
            {
                bezelRightWidth = value;
                RaisePropertyChanged();
            }
        }

        public double BezelLeftWidth
        {
            get { return bezelLeftWidth; }
            set
            {
                bezelLeftWidth = value;
                RaisePropertyChanged();
            }
        }

        public bool IsHomeButtonVisible
        {
            get { return isHomeButtonVisible; }
            set
            {
                isHomeButtonVisible = value;
                RaisePropertyChanged();
            }
        }

        public void StartVideoCapture()
        {
            WebcamState = WebcamStates.Normal;
            CanPlayDemoVideo = DemoState != DemoType.Collaboration;
            LocalWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (LocalWebCamsCollection.Count > 0)
            {
                LocalWebCam = new VideoCaptureDevice(LocalWebCamsCollection[0].MonikerString);
                LocalWebCam.NewFrame -= Cam_NewFrame;
                LocalWebCam.NewFrame += Cam_NewFrame;
                LocalWebCam.Start();
            }
            IsWebcamAvailable = LocalWebCamsCollection.Count > 0;
        }

        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Image img = (Bitmap)eventArgs.Frame.Clone();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();
            VideoPreview = bi;
        }

        public void StopVideoPreview()
        {
            if (LocalWebCam != null)
            {
                LocalWebCam.NewFrame -= Cam_NewFrame;
                LocalWebCam.Stop();
            }
        }

        public void SetTabletSize()
        {
            AppHeight = 1087;
            AppWidth = 1351;
            BezelRightWidth = 100;
            BezelLeftWidth = 108;
            BezelHeight = 52;
        }
        public void ResetAppSize()
        {
            AppHeight = 1087;
            AppWidth = 548;
            BezelLeftWidth = BezelRightWidth = 26;
            BezelHeight = 105.5;
        }
    }
}
