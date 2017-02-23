using DingWK.Wpf.Graph2D.Geom;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public sealed class GeomVisual : GraphicVisual
    {
        #region field & property

        private Geom.Geometry _geometry;

        public Geom.Geometry Geometry
        {
            get { return _geometry; }
            set
            {
                _geometry = value;
                UpdateVisual();
                OnPropertyChanged(nameof(Geometry));
            }
        }

        #endregion


        #region Constructors

        public GeomVisual(Geom.Geometry goem, VisualInfo graphicInfo)
            : base(graphicInfo)
        {
            _geometry = goem;
            UpdateVisualInfo();
        }

        public GeomVisual(Geom.Geometry goem)
            : this(goem, VisualInfo.Default)
        {
        }
        #endregion


        #region methods

        internal void UpdateVisual()
        {
            DrawingContext dc = RenderOpen();
            Geometry.Draw(dc, Fill, Stroke);
            dc.Close();
        }

        // override methods

        internal override void UpdateFill() => UpdateVisual();

        internal override void UpdateStroke() => UpdateVisual();

        internal override void UpdateVisualInfo()
        {
            UpdateTransform();
            UpdateVisual();
        }

        #endregion

    }
}
