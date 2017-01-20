using Graphic2D.Kernel.Visuals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphic2D.Kernel.ViewModel
{
    public class VisualEditViewModel : BaseViewModel
    {

        private ReadOnlyCollection<GraphicVisual> _visualSelection;

        public ReadOnlyCollection<GraphicVisual> VisualSelection
        {
            get { return _visualSelection; }
            private set
            {
                _visualSelection = value;
                NotifyPropertyChanged(nameof(VisualSelection));
            }
        }

        public VisualInfo VisualInfo { get; private set; }

        private Point _unionOffset = new Point(0, 0);
        private double _unionAngle = 0;


        public void ClearVisualSelection() => VisualSelection = null;

        public Rect GetBoundRect()
        {
            return VisualEditViewModel.GetBoundRect(_visualSelection);
        }

        public static Rect GetBoundRect(ReadOnlyCollection<GraphicVisual> visuals)
        {
            Rect rect = Rect.Empty;
            if (visuals != null)
            {
                foreach (GraphicVisual gv in visuals)
                {
                    rect = Rect.Union(gv.DescendantBounds, rect);
                }
            }
            return rect;
        }


    }
}
