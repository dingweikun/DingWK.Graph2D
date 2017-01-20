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

        //#region Origin
        ///// <summary>
        ///// 
        ///// </summary>
        //public Point Origin
        //{
        //    get { return (Point)GetValue(OriginProperty); }
        //    set { SetValue(OriginProperty, value); }
        //}
        ////
        //// Dependency property definition
        ////
        //private static readonly DependencyProperty OriginProperty =
        //    DependencyProperty.Register(
        //        nameof(Origin),
        //        typeof(Point),
        //        typeof(PageOperator),
        //        new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender)
        //        {
        //            PropertyChangedCallback = (d, e) =>
        //             {
        //                 (d as PageOperator).UpdateRenderTranform();
        //             }
        //        });
        //#endregion

        //#region Angle
        ///// <summary>
        ///// 
        ///// </summary>
        //public double Angle
        //{
        //    get { return (double)GetValue(AngleProperty); }
        //    set { SetValue(AngleProperty, value); }
        //}
        ////
        //// Dependency property definition
        ////
        //private static readonly DependencyProperty AngleProperty =
        //    DependencyProperty.Register(
        //        nameof(Angle),
        //        typeof(double),
        //        typeof(PageOperator),
        //        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
        //        {
        //            PropertyChangedCallback = (d, e) =>
        //            {
        //                (d as PageOperator).UpdateRenderTranform();
        //            }
        //        });
        //#endregion
        
        //internal void UpdateRenderTranform()
        //{
        //    TransformGroup tr = new TransformGroup();
        //    tr.Children.Add(new RotateTransform(Angle));
        //    tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
        //    RenderTransform = tr;
        //}

    }
}
