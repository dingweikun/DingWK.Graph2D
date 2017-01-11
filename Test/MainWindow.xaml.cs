using Graphic2D.Kernel.Visuals;
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

using Graphic2D.Kernel.Geom;
namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GroupVisual host = new GroupVisual();

            // 单一图形
            Rectangle rect = new Rectangle(new Rect(100, 100, 200, 100), 20, 40);
            GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(rect);
            host.GroupIn(g1);

            // 组合图形
            GroupVisual group = new GroupVisual();

            GeomVisual<Rectangle> ga = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(250, 250, 50, 50), 0, 0));
            group.GroupIn(ga);
            GeomVisual<Rectangle> gb = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(200, 300, 20, 80), 0, 0));
            group.GroupIn(gb);
            host.GroupIn(group);

            group.Angle = 45;

            canvas.DataContext = host;
        }
    }
}
