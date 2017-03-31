using DingWK.Graphic2D.Graphic;
using DingWK.Graphic2D.Geometry;
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


            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //cc.PageGrid.Height = 500;
            //cc.PageGrid.Width = 100;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cc.PageColor = Brushes.LightGray;
            this.cc.GridColor = Brushes.Red;


            DingWK.Graphic2D.Geometry.Rectangle rect = new DingWK.Graphic2D.Geometry.Rectangle(
                new Rect(0, 0, 100, 50), 10, 10);

            GeomGraphicVisual visual = new GeomGraphicVisual(rect);

            this.cc.GraphicVisualHost.Visuals.Add(visual);
        }
    }
}
