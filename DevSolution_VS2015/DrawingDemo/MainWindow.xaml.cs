using MahApps.Metro.Controls;
using System.Windows.Input;

namespace DrawingDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) => Common.ConfigureHelper.Singleton.LoadConfiguration();
            Closing += (sender, e) => Common.ConfigureHelper.Singleton.SaveConfiguration();
        }


        public ICommand OpenMainFlyoutCommand =>
            new Common.RelayCommand(o => OpenMainFlyout((bool)o));

        private void OpenMainFlyout(bool isOpen)
        {
            MainFlyout.IsOpen = isOpen;
        }

    }
}
