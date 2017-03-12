using MahApps.Metro.Controls;

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

    }
}
