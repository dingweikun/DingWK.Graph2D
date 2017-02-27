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

using DingWK.Graphic2D;
using DingWK.Graphic2D.Geom;
using DingWK.Graphic2D.Graphics;
using DingWK.Graphic2D.Wpf;

namespace test
{

    public class ELE : FrameworkElement
    {
        #region Members related to graphic rendering support in WPF  


        private readonly VisualCollection _children;

        public VisualCollection Children => _children;

        protected override Visual GetVisualChild(int index) => Children[index];

        protected override int VisualChildrenCount => Children.Count;

        #endregion


        public ELE()
        {
            _children = new VisualCollection(this);

            
        }
    }
        



    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        ELE ele = new ELE();


        public MainWindow()
        {
            InitializeComponent();


            DingWK.Graphic2D.Geom.Rectangle r = new DingWK.Graphic2D.Geom.Rectangle(100, 100, 400, 200);
            r.RadiusX = 20;
            r.RadiusY = 20;

            Graphic graphic = new GeomGraphic<DingWK.Graphic2D.Geom.Rectangle>(r);
            graphic.Angle = 5;
            GraphicVisual visual = new GeomVisual<DingWK.Graphic2D.Geom.Rectangle>
                (graphic as GeomGraphic<DingWK.Graphic2D.Geom.Rectangle>);
            graphic.Angle = 45;
            visual.Graphic = graphic;


            visual.Fill = Brushes.LightYellow;

            ele.Children.Add(visual.Visual);

            this.cav.Children.Add(ele);
        }
    }
}
