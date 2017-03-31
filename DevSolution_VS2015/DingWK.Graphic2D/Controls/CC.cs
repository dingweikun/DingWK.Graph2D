using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DingWK.Graphic2D.Controls
{
    public partial class CC : Control
    {
        static CC()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CC), new FrameworkPropertyMetadata(typeof(CC)));
        }
    }
}
