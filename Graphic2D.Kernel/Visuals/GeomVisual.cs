using Graphic2D.Kernel.Geom;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public sealed class GeomVisual : GraphicVisual
    {
        #region Field & property

        private IGeom _geom;

        public IGeom Geom
        {
            get { return _geom; }
            set
            {
                _geom = value;
                UpdateVisual();
                NotifyPropertyChanged(nameof(Geom));
            }
        }

        #endregion


        #region Constructors

        public GeomVisual(IGeom goem, VisualInfo graphicInfo)
            : base(graphicInfo)
        {
            _geom = goem;
            UpdateGraphicInfo();
        }

        public GeomVisual(IGeom goem)
            : this(goem, VisualInfo.Default)
        {
        }
        #endregion


        #region Methods

        internal void UpdateVisual()
        {
            DrawingContext dc = RenderOpen();
            Geom.DrawGeom(dc, Fill, Stroke);
            dc.Close();
        }

        // override methods

        internal override void UpdateFill() => UpdateVisual();

        internal override void UpdateStroke() => UpdateVisual();

        internal override void UpdateGraphicInfo()
        {
            UpdateTransform();
            UpdateVisual();
        }

        #endregion

    }

}
