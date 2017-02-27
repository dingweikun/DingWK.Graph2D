using DingWK.Graphic2D.Graphics;
using DingWK.Graphic2D.Wpf.Common;
using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf
{
    public abstract class GraphicVisual : PropertyChangedNotifier, IGraphic
    {
        #region fields

        private Graphic _graphic;
        private readonly DrawingVisual _visual = new DrawingVisual();

        #endregion


        #region properties

        /// <summary>
        /// The Graphic object stored in GraohicVisual instance，can not be null.
        /// </summary>
        public Graphic Graphic
        {
            get { return _graphic; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Graphic));
                _graphic = value;
                UpdateGraphic();
                OnPropertyChanged(nameof(Graphic));
            }
        }

        /// <summary>
        /// Visual object displayed in WPF.
        /// </summary>
        public DrawingVisual Visual => _visual;

        public Guid ID => _graphic.ID;

        public Point Origin
        {
            get { return _graphic.Origin; }
            set
            {
                _graphic.Origin = value;
                UpdateVisualTransform();
                OnPropertyChanged(nameof(Origin));
            }
        }

        public double Angle
        {
            get { return _graphic.Angle; }
            set
            {
                _graphic.Angle = value;
                UpdateVisualTransform();
                OnPropertyChanged(nameof(Angle));
            }
        }

        public Brush Fill
        {
            get { return _graphic.Fill; }
            set
            {
                _graphic.Fill = value;
                UpdateFill();
                OnPropertyChanged(nameof(Fill));
            }
        }

        public Pen Stroke
        {
            get { return _graphic.Stroke; }
            set
            {
                _graphic.Stroke = value;
                UpdateStroke();
                OnPropertyChanged(nameof(Stroke));
            }
        }

        #endregion


        #region constructor

        protected GraphicVisual(Graphic graphic)
        {
            Graphic = graphic;
        }

        #endregion


        #region methods

        protected virtual void UpdateVisualTransform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(Angle));
            tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            Visual.Transform = tr;
        }

        protected abstract void UpdateGraphic();
        protected abstract void UpdateFill();
        protected abstract void UpdateStroke();

        #endregion


    }



    //public interface IGraphicVisual : IGraphic
    //{
    //    DrawingVisual Visual { get; }

    //    void UpdateVisual();
    //}

    //public sealed class GraphicVisual<T> : PropertyChangedNotifier, IGraphicVisual
    //    where T : Graphic
    //{
    //    private T _graphic;


    //    /// <summary>
    //    /// The Graphic object stored in GraohicVisual instance，can not be null.
    //    /// </summary>
    //    public T Graphic
    //    {
    //        get { return _graphic; }
    //        set
    //        {
    //            if (value == null)
    //                throw new ArgumentNullException(nameof(Graphic));
    //            _graphic = value;
    //            OnPropertyChanged(nameof(Graphic));
    //        }
    //    }


    //    public double Angle 
    //    {
    //        get { return _graphic.Angle; }
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Brush Fill
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Point Origin
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Pen Stroke
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public DrawingVisual Visual
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
    //}





}
