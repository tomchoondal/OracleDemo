using OracleCommunication_Demo.UserControls;
using System.Collections.ObjectModel;

namespace OracleCommunication_Demo
{
    public class ChatViewModel : BaseViewModel
    {
        private string outgoingMsg;
        private string incomingMsg;
        private ChatType chatType;
        private ObservableCollection<ChatItem> chatMsgCollection;

        public ChatViewModel()
        {
            Init();
        }

        public void Init()
        {
            ChatMsgCollection.Clear();
            IncomingMsg = "Let us know what kind of help you need";
        }

        public string OutgoingMsg
        {
            get { return outgoingMsg; }
            set
            {
                outgoingMsg = value;
                var newChatItem = new ChatItem() { Message = outgoingMsg, Type = ChatType.Outgoing };
                ChatMsgCollection.Add(newChatItem);
                RaisePropertyChanged("OutgoingMsg");
            }
        }


        public string IncomingMsg
        {
            get { return incomingMsg; }
            set
            {
                incomingMsg = value;
                var newChatItem = new ChatItem() { Message = incomingMsg, Type = ChatType.Incoming };
                ChatMsgCollection.Add(newChatItem);
                RaisePropertyChanged("IncomingMsg");
            }
        }


        public ChatType ChatType
        {
            get { return chatType; }
            set
            {
                chatType = value;
                RaisePropertyChanged("ChatType");
            }
        }

        public ObservableCollection<ChatItem> ChatMsgCollection
        {
            get
            {
                return chatMsgCollection ?? (chatMsgCollection = new ObservableCollection<ChatItem>());
            }
        }
    }
}
