namespace Sample.ViewModels
{
    using Base;
    using Xamarin.Forms;

    public class MainViewModel : BaseViewModel
    {
        private Command testCommand;
        private int count;

        public MainViewModel()
        {
            testCommand = new Command(TestCommandExecute);
        }

        public Command TestCommand => testCommand;

        public int Count
        {
            get { return count; }
            set { RaiseAndSet(ref count, value); }
        }

        private void TestCommandExecute()
        {
            Count++;
        }
    }
}
