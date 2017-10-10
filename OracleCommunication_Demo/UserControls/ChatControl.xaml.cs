using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OracleCommunication_Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {

        public ChatControl()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.DataContext = MainViewModel.Instance.ChatVM;
        }


        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFullScreen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullScreenProperty =
            DependencyProperty.Register("IsFullScreen", typeof(bool), typeof(ChatControl), new PropertyMetadata(false, OnIsFullScreenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ChatControl), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChatControl).UpdateOpen();
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

        private static void OnIsFullScreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChatControl).UpdateFullScreen();
        }

        private void UpdateFullScreen()
        {
            if (IsFullScreen)
            {
                LayoutRoot.Width = LayoutRoot.Height = Double.NaN;
                ExitButton.Visibility = Visibility.Visible;
            }
            else
            {
                LayoutRoot.Width = 343.0;
                LayoutRoot.Height = 310.0;
                ExitButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ChatVM.OutgoingMsg = ChatTextBox.Text;
            ChatTextBox.Text = string.Empty;
            await Task.Delay(1000);
            MainViewModel.Instance.ChatVM.IncomingMsg = "You can find “Auto Pause” by going to the recording screen.";
        }

        private void ChatTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                SendButton.IsEnabled = true;
                PlaceholderText.Visibility = Visibility.Collapsed;

            }
            else
            {
                SendButton.IsEnabled = false;
                PlaceholderText.Visibility = Visibility.Visible;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.BasicGuidanceVM.IsChatMinimized = true;
            MainViewModel.Instance.BasicGuidanceVM.IsControlOpened = true;
            MainViewModel.Instance.BasicGuidanceVM.IsChatOpen = false;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.DialogVM.ShowDialog("Exit the Session",
                "Are you sure you want to exit the customer service session?", "Yes", "No", DialogType.StopService);
        }
    }


    public class ChatTemplateSelector : DataTemplateSelector
    {


        public DataTemplate IncomingTemplate { get; set; }

        public DataTemplate OutgoingTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var type = (item as ChatItem).Type;
            switch (type)
            {
                case ChatType.Incoming:
                    return IncomingTemplate;
                case ChatType.Outgoing:
                    return OutgoingTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
    public class ChatItem
    {
        public string Message
        {
            get; set;
        }


        public ChatType Type
        {
            get; set;
        }
    }


}
