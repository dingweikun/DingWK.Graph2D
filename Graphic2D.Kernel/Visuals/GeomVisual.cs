using Graphic2D.Kernel.Geom;
using System.Windows.Media;
using System;
using System.Windows;

namespace Graphic2D.Kernel.Visuals
{
    public class GeomVisual<T> : GraphicVisual
        where T : IGeom
    {

        #region Field & property

        private T _geom;

        public T Geom
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

        public GeomVisual(T goem, VisualInfo graphicInfo)
            : base(graphicInfo)
        {
            _geom = goem;

            UpdateGraphicInfo();
        }

        public GeomVisual(T goem)
            : this(goem, VisualInfo.Default)
        {
        }

        public GeomVisual(GeomVisual<T> graphicGeom)
            : this(graphicGeom.Geom, graphicGeom.GraphicInfo)
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
