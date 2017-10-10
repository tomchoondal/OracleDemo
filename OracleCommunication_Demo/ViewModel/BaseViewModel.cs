using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OracleCommunication_Demo
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public MainViewModel MainVM
        {
            get
            {
                return MainViewModel.Instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
