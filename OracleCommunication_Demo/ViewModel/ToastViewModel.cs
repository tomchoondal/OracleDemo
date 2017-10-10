namespace OracleCommunication_Demo
{
    public class ToastViewModel : BaseViewModel
    {
        private string message;
        private string iconSource;
        private bool isToastOpen;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        public string IconSource
        {
            get { return iconSource; }
            set
            {
                iconSource = value;
                RaisePropertyChanged();
            }
        }

        public bool IsToastOpen
        {
            get { return isToastOpen; }
            set
            {
                isToastOpen = value;
                RaisePropertyChanged();
            }
        }

        public void ShowToast(string toastMessage, string toastIconSource = "/OracleCommunication_Demo;component/Images/ToastIcon.png")
        {
            Message = toastMessage;
            IconSource = toastIconSource;
            IsToastOpen = true;
        }
    }
}
