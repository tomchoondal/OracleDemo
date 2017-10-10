using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ToastControl.xaml
    /// </summary>
    public partial class ToastControl : UserControl
    {
        public ToastControl()
        {
            InitializeComponent();
            this.DataContext = MainViewModel.Instance.ToastVM;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ToastControl), new PropertyMetadata(false, OnIsOpenChnaged));




        public bool IsTabletMode
        {
            get { return (bool)GetValue(IsTabletModeProperty); }
            set { SetValue(IsTabletModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTabletMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTabletModeProperty =
            DependencyProperty.Register("IsTabletMode", typeof(bool), typeof(ToastControl), new PropertyMetadata(false,OnTabletModeChanged));

        private static void OnTabletModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToastControl).UpdateTabletMode();
        }

        private void UpdateTabletMode()
        {
            if(IsTabletMode)
            {
                border.Width = 790;
            }
            else
            {
                border.Width = 348;
            }
        }

        private static void OnIsOpenChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ToastControl).UpdateOpen();
        }

        private void UpdateOpen()
        {
            if (MainViewModel.Instance.ToastVM.IsToastOpen)
            {
                (this.Resources["Open"] as Storyboard).Begin();
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            MainViewModel.Instance.ToastVM.IsToastOpen = false;
        }


    }
}
