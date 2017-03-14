using DrawingDemo.Common;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingDemo.Settings
{
    /// <summary>
    /// ApperanceSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ApperanceSetting : UserControl
    {
        public ApperanceSetting()
        {
            InitializeComponent();

             Loaded += ApperanceSetting_Loaded;
        }

        private void ApperanceSetting_Loaded(object sender, RoutedEventArgs e)
        {
            // 绑定 ViewModel
            DataContext = new ApperanceSettingViewModel();

            // 初始化页面控件状态
            PrimaryListBox.SelectedValue = ApperanceHelper.Singleton.CurrentPrimary;
            AccentListBox.SelectedValue = ApperanceHelper.Singleton.CurrentAccent;
            ThemeToggleButton.IsChecked = ApperanceHelper.Singleton.IsDarkTheme;
        }
    }
}
