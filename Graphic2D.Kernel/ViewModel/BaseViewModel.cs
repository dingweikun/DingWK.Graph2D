using System.ComponentModel;

namespace Graphic2D.Kernel.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged interface implementation

        public event PropertyChangedEventHandler PropertyChanged;

        // 触发 PropertyChanged 事件的通用方法。
        // A general method to raise the PropertyChanged event.
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
