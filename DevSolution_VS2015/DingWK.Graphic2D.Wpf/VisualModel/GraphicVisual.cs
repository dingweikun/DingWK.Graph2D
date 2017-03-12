using DingWK.Graphic2D.Geom;
using DingWK.Graphic2D.Graphics;
using DingWK.Graphic2D.Wpf.Common;
using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf.Model
{
    public interface IGraphicVisualModel
    {

        IGraphic Graphic { get; }

        DrawingVisual Visual { get; }

        void UpdateVisual();
    }



    public sealed class GeomGraphicVisualModel : PropertyChangedNotifier, IGraphicVisualModel, IGeomGraphic
    {

        private readonly GeomGraphic _geomGraphic;

        private readonly DrawingVisual _visual;



        IGraphic Graphic => _geomGraphic;

        public DrawingVisual Visual => _visual;

        public IGeometry Geometry
        {
            get { return _geomGraphic.Geometry; }
            set
            {
                _geomGraphic.Geometry = value;
                OnPropertyChanged(nameof(Geometry));
            }
        }

        public Guid ID => Graphic.ID;

        public Point Origin
        {
            get { return _geomGraphic.Origin; }
            set
            {
                _geomGraphic.Origin = value;


            }
        }

        public double Angle
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Brush Fill
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Pen Stroke
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateVisual()
        {
            throw new NotImplementedException();
        }
    }



}
