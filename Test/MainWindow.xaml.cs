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
using Graphic2D.Kernel.Controls;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        IBoundVisual b;

        public MainWindow()
        {
            InitializeComponent();

            GroupVisual host = new GroupVisual();

            // 单一图形
            Rectangle rect = new Rectangle(new Rect(100, 100, 200, 100), 20, 40);
            GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(rect);
            g1.Angle = 45;
            host.GroupIn(g1);


            Rect r1 = g1.ContentBounds;
            Rect r2 = g1.DescendantBounds;

            r1.Transform(g1.Transform.Value);

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

             r1 = group.ContentBounds;
             r2 = group.DescendantBounds;


            b = g1;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            TransformOperator ops = new TransformOperator();
            canvas.PageAdorner.Canvas.Children.Add(ops);
            ops.BoundVisual = b;
        }
    }
}
