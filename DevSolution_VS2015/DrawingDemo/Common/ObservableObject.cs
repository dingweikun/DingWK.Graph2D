using System.ComponentModel;

namespace DrawingDemo.Common
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string changedProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedProperty));
        }
    }
}
