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
using Graphic2D.Kernel.ViewModel;
using Graphic2D.Kernel.Model;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        // IBoundVisual b;
        //VisualEditViewModel model;
        VisualSelection model;// = new VisualSelection();

        public MainWindow()
        {
            InitializeComponent();

            GroupVisual host = new GroupVisual();

            // 单一图形
            Rectangle rect = new Rectangle(new Rect(0, 0, 100, 200), 10, 10);
            GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(rect);
            g1.Fill = Brushes.Blue.CloneCurrentValue();
            g1.Angle = 45;
            g1.Origin = new Point(100, 100);
            host.AddIntoGroup(g1);


            //Rect r1 = g1.ContentBounds;
            //Rect r2 = g1.DescendantBounds;

            //r1.Transform(g1.Transform.Value);

            // 组合图形
            GroupVisual group = new GroupVisual();

            GeomVisual<Rectangle> ga = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(250, 250, 50, 50), 0, 0));
            group.AddIntoGroup(ga);
            GeomVisual<Rectangle> gb = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(200, 300, 20, 80), 0, 0));
            group.AddIntoGroup(gb);
            host.AddIntoGroup(group);

            group.Angle = 30;

            canvas.DataContext = host;

            //r1 = group.ContentBounds;
            //r2 = group.DescendantBounds;


            //b = g1;
            //model = new VisualEditViewModel();

            model = canvas.VisualSelection;

            List<GraphicVisual> list = new List<GraphicVisual>() { g1, group };
            model.AddIntoSelection(list);
            //model.AddIntoSelection(g1);
            //model.AddIntoSelection(group);


            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            TransformOperator ops = new TransformOperator();
            ops.SelectedVisuals = model;
            canvas.OperatorAdorner.Canvas.Children.Add(ops);


        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double r = e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0;
            canvas.Page.PageScale *= r;
        }
    }
}
