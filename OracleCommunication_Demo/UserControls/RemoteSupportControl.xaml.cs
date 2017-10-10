using System.Windows;
using System.Windows.Controls;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for RemoteSupportControl.xaml
    /// </summary>
    public partial class RemoteSupportControl : UserControl
    {
        public RemoteSupportControl()
        {
            this.InitializeComponent();
            MainViewModel.Instance.TimerVM.InitTimer();
        }

        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpened.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(RemoteSupportControl), new PropertyMetadata(false));
        
        private void FullScreenToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RemoteSupportVM.IsVideoMinimized = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ToastVM.ShowToast("Loudspeakers engaged, your conversation with the representative will be heard on the loudspeaker.");
        }
    }

}