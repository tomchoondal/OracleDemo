using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for DialogControl.xaml
    /// </summary>
    public partial class DialogControl : UserControl
    {
        public event EventHandler CloseDialog;


        public DialogControl()
        {
            this.InitializeComponent();
            this.DataContext = MainViewModel.Instance.DialogVM;
        }

        public bool IsAllowed
        {
            get { return (bool)GetValue(IsAllowedProperty); }
            set { SetValue(IsAllowedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowedProperty =
            DependencyProperty.Register("IsAllowed", typeof(bool), typeof(DialogControl), new PropertyMetadata(false));



        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(DialogControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsOpenPropertyChanged)));


        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DialogControl).UpdateOpen();
        }

        private void UpdateOpen()
        {
            if (IsOpen)
            {
                (this.Resources["Open"] as Storyboard).Begin();
            }
            else
            {
                (this.Resources["Close"] as Storyboard).Begin();
            }
        }


        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            IsAllowed = true;
            MainViewModel.Instance.DialogVM.IsDialogOpen = false;
            CloseDialog?.Invoke(this, new EventArgs());
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            IsAllowed = false;
            MainViewModel.Instance.DialogVM.IsDialogOpen = false;
            CloseDialog?.Invoke(this, new EventArgs());
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.IsDialogOpen = false;
        }
    }

}