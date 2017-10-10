using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for HomeButtonControl.xaml
    /// </summary>
    public partial class HomeButtonControl : UserControl
    {

        public HomeButtonControl()
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
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(HomeButtonControl), new PropertyMetadata(false));


        public bool IsChatMinimized
        {
            get { return (bool)GetValue(IsChatMinimizedProperty); }
            set { SetValue(IsChatMinimizedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChatMinimized.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChatMinimizedProperty =
            DependencyProperty.Register("IsChatMinimized", typeof(bool), typeof(HomeButtonControl), new PropertyMetadata(false, OnIsChatminimizedChanged));


        private static void OnIsChatminimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as HomeButtonControl).UpdateChatMinimizedChanged();
        }

        private void UpdateChatMinimizedChanged()
        {
            if (IsChatMinimized)
            {
                (this.Resources["ShowNotification"] as Storyboard).Begin();
            }
            else
            {
                (this.Resources["HideNotification"] as Storyboard).Begin();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                 "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = true;
            MainViewModel.Instance.BasicGuidanceVM.IsControlOpened = false;
            MainViewModel.Instance.BasicGuidanceVM.IsChatMinimized = false;
        }
    }
}