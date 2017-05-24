namespace Sample.ViewModels.Base
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseAndSet<T>(ref T storage, T newValue, [CallerMemberName]string property = "")
        {
            storage = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
