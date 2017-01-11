using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    public abstract class PageOperator : Control
    {



        #region Scale
        /// <summary>
        /// 
        /// </summary>
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(
                nameof(Scale),
                typeof(double),
                typeof(PageOperator),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, value) =>
                    {
                        return ((double)value) > 0 ? value : 1.0;
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                       (d as PageOperator).OnScalePropertyChanged();
                    }
                });

        internal abstract void OnScalePropertyChanged();
        #endregion



        #region RenderX

        public double RenderX
        {
            get { return (double)GetValue(RenderXProperty); }
            set { SetValue(RenderXProperty, value); }
        }

        public static readonly DependencyProperty RenderXProperty =
            DependencyProperty.Register(
                nameof(RenderX),
                typeof(double),
                typeof(PageOperator),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as PageOperator)?.UpdateRenderTranform();
                    }
                });
        #endregion

        #region RenderY

        public double RenderY
        {
            get { return (double)GetValue(RenderYProperty); }
            set { SetValue(RenderYProperty, value); }
        }

        public static readonly DependencyProperty RenderYProperty =
            DependencyProperty.Register(
                nameof(RenderY),
                typeof(double),
                typeof(PageOperator),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as PageOperator)?.UpdateRenderTranform();
                    }
                });
        #endregion

        #region RenderScale

        public double RendserScale
        {
            get { return (double)GetValue(RendserScaleProperty); }
            set { SetValue(RendserScaleProperty, value); }
        }

        public static readonly DependencyProperty RendserScaleProperty =
            DependencyProperty.Register(nameof(RendserScale), typeof(double), typeof(PageOperator), new PropertyMetadata(1.0));

        #endregion

        #region RenderAngle

        public double RenderAngle
        {
            get { return (double)GetValue(RenderAngleProperty); }
            set { SetValue(RenderAngleProperty, value); }
        }

        public static readonly DependencyProperty RenderAngleProperty =
            DependencyProperty.Register(
                nameof(RenderAngle), 
                typeof(double), 
                typeof(PageOperator),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as PageOperator)?.UpdateRenderTranform();
                    }
                });
        #endregion

        protected void UpdateRenderTranform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(RenderAngle));
            tr.Children.Add(new TranslateTransform(RenderX, RenderY));
            RenderTransform = tr;
        }


    }
}
