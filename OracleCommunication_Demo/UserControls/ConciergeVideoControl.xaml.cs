using System.Windows;
using System.Windows.Controls;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ConciergeVideoControl.xaml
    /// </summary>
    public partial class ConciergeVideoControl : UserControl
    {
        public ConciergeVideoControl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.DataContext = MainViewModel.Instance.ConciergeVM;
        }


        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ConciergeVideoControl), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ConciergeVideoControl).UpdateOpen();
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

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ConciergeVM.IsFullScreen = false;
        }

        private void StopserviceButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
               "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }
    }
}
