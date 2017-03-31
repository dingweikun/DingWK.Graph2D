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
using DingWK.Graphic2D;
using DingWK.Graphic2D.Geometry;
using DingWK.Graphic2D.Graphic;
using DingWK.Graphic2D.Controls;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //GraphicVisualHost host = new GraphicVisualHost();

            //Rectangle rect = new Rectangle(new Rect(0, 0, 400, 200), 5, 5);

            //var geomGraphic = new GeomGraphicVisual<Rectangle>(rect)
            //{
            //    Fill = Brushes.Yellow,
            //    Stroke = new Pen(Brushes.Red, 3)
            //};

            //host.Visuals.Add(geomGraphic);

            //cav.Children.Add(host);

            //GridPage grid = new GridPage();
            //grid.Width = 800;
            //grid.Height = 600;
            //grid.GridSize = 50;
            //grid.RenderTransform = new TranslateTransform(200, 100);

            //cav.Children.Add(grid);
        }
    }
}
