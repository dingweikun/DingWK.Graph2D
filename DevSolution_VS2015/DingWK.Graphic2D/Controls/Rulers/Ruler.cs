using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Controls.Rulers
{
    internal class Ruler : FrameworkElement
    {
        protected double Start { get; set; }
        protected double StartValue => (Start - OrignPosition) / ZoomScale;
        protected double MinorTickCount { get; set; }
        protected double MinorTickSpacingValue { get; set; }
        protected double MinorTickSpacing => MinorTickSpacingValue * ZoomScale;


        #region OrignPosition 
        /// <summary>
        /// 标尺0刻度位置
        /// </summary>
        public double OrignPosition
        {
            get { return (double)GetValue(OrignPositionProperty); }
            set { SetValue(OrignPositionProperty, value); }
        }
        //
        // 依赖定义
        //
        public static readonly DependencyProperty OrignPositionProperty =
            DependencyProperty.Register(
                nameof(OrignPosition),
                typeof(double),
                typeof(Ruler),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region ScaleColor 
        /// <summary>
        /// 标尺刻度颜色
        /// </summary>
        public Brush TickColor
        {
            get { return (Brush)GetValue(TickColorProperty); }
            set { SetValue(TickColorProperty, value); }
        }
        //
        // 依赖定义
        //
        public static readonly DependencyProperty TickColorProperty =
            DependencyProperty.Register(
                nameof(TickColor),
                typeof(Brush),
                typeof(Ruler),
                new FrameworkPropertyMetadata(Brushes.DimGray, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region TickTextColor 
        /// <summary>
        /// 标尺刻度文字颜色
        /// </summary>
        public Brush TickTextColor
        {
            get { return (Brush)GetValue(TickTextColorProperty); }
            set { SetValue(TickTextColorProperty, value); }
        }
        //
        // 依赖定义
        //
        public static readonly DependencyProperty TickTextColorProperty =
            DependencyProperty.Register(
                nameof(TickTextColor),
                typeof(Brush),
                typeof(Ruler),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region ZoomScale 
        /// <summary>
        /// 标尺缩放比例
        /// </summary>
        public double ZoomScale
        {
            get { return (double)GetValue(ZoomScaleProperty); }
            set { SetValue(ZoomScaleProperty, value); }
        }
        //
        // 依赖定义
        //
        public static readonly DependencyProperty ZoomScaleProperty =
            DependencyProperty.Register(
                nameof(ZoomScale),
                typeof(double),
                typeof(Ruler),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region DesignTickSpacing
        /// <summary>
        /// 标尺大刻度设计间距，该值为标尺大刻度的设计期望间距，实际间距是根据该值计算获得。
        /// </summary>
        public double DesignTickSpacing
        {
            get { return (double)GetValue(DesignTickSpacingProperty); }
            set { SetValue(DesignTickSpacingProperty, value); }
        }
        //
        // 依赖定义
        //
        public static readonly DependencyProperty DesignTickSpacingProperty =
            DependencyProperty.Register(
                nameof(DesignTickSpacing),
                typeof(double),
                typeof(Ruler),
                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        protected Ruler()
        {
            ClipToBounds = true;
            SnapsToDevicePixels = true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //
            // 计算标尺动态刻度 小刻度 MinorTickSpacing 大刻度 TickSpacing
            //

            double designTickSpacingValue = DesignTickSpacing / ZoomScale;

            double radix = designTickSpacingValue < 1 ? 0.1 : 10;

            double factor = 1;
            while (designTickSpacingValue < 1 || designTickSpacingValue >= 10)
            {
                designTickSpacingValue /= radix;
                factor *= radix;
            }

            double round = Math.Round(designTickSpacingValue);

            double tickSpacingValue = factor * (round > 5 ? 10 : (round > 2 ? 5 : round));

            MinorTickCount = round != 2 ? 5 : 2;

            MinorTickSpacingValue = tickSpacingValue / MinorTickCount;

            //
            // 计算标尺刻度绘制起始位置
            //
            Start = OrignPosition <= 0 ?
                OrignPosition - (int)(OrignPosition / (tickSpacingValue * ZoomScale)) * tickSpacingValue * ZoomScale :
                OrignPosition - (int)(OrignPosition / (tickSpacingValue * ZoomScale) + 1) * tickSpacingValue * ZoomScale;

        }


    }
}


