namespace OracleCommunication_Demo
{
    public class DialogViewModel : BaseViewModel
    {
        private string message;
        private string title;
        private string rightButtonText;
        private string leftButtonText;
        private bool isDialogOpen;
        private DialogType dialogState;
        private DialogMode dialogMode;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        public string RightButtonText
        {
            get { return rightButtonText; }
            set
            {
                rightButtonText = value;
                RaisePropertyChanged();
            }
        }

        public string LeftButtonText
        {
            get { return leftButtonText; }
            set
            {
                leftButtonText = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDialogOpen
        {
            get { return isDialogOpen; }
            set
            {
                isDialogOpen = value;
                RaisePropertyChanged();
            }
        }

        public DialogType DialogState
        {
            get { return dialogState; }
            set
            {
                dialogState = value;
                RaisePropertyChanged();
            }
        }

        public DialogMode DialogModes
        {
            get { return dialogMode; }
            set
            {
                dialogMode = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsNormalDialog");
            }
        }

        public bool IsNormalDialog
        {
            get
            {
                return DialogModes == DialogMode.Normal;
            }
        }

        public void ShowDialog(string titleText, string messageText, string rightText, string leftText, DialogType dialogType, DialogMode dialogMode = DialogMode.Normal)
        {
            Title = titleText;
            Message = messageText;
            RightButtonText = rightText;
            LeftButtonText = leftText;
            DialogState = dialogType;
            IsDialogOpen = true;
            DialogModes = dialogMode;
        }
    }
}
