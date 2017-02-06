using Graphic2D.Kernel.Common;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    public class VerPageRuler : PageRuler
    {
        public VerPageRuler()
        {
            this.Width = 18;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // 刻度线宽宽度 1 个像素

            Matrix mtx = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
            double dpiFactor = 1 / mtx.M11;
            Pen pixelPen = new Pen(TickColor, 1 * dpiFactor);

            // 绘制标尺

            Point a = new Point(ActualWidth, Start);
            Point b = new Point(ActualWidth - 4, Start);  // 短刻度
            Point c = new Point(ActualWidth - 8, Start);  // 长刻度
            Point t = new Point(ActualWidth - 16, Start);

            int i = 0;
            while (Start + i * MinorTickSpacing <= ActualHeight)
            {
                a.Y = b.Y = c.Y = t.Y = Start + i * MinorTickSpacing;

                GuidelineSet guideline = new GuidelineSet(null, new double[] { a.Y - pixelPen.Thickness / 2 });

                drawingContext.PushGuidelineSet(guideline);

                if (i % MinorTickCount == 0)
                {
                    drawingContext.DrawLine(pixelPen, a, c);

                    FormattedText ft = new FormattedText(Func.FormatZeroString(StartValue + i * MinorTickSpacingValue),
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        (6 * 96.0 / 72.0),
                        TickTextColor);
                    ft.SetFontWeight(FontWeights.Regular);
                    ft.TextAlignment = TextAlignment.Center;

                    drawingContext.PushTransform(new RotateTransform(-90, t.X, t.Y));
                    drawingContext.DrawText(ft, t);
                    drawingContext.Pop();
                }
                else
                {
                    drawingContext.DrawLine(pixelPen, a, b);
                }

                drawingContext.Pop();

                i++;
            }

            drawingContext.PushGuidelineSet(
                new GuidelineSet(
                    new double[] { ActualWidth - 1 - pixelPen.Thickness / 2 },
                    new double[] { 0 - pixelPen.Thickness / 2, ActualHeight - pixelPen.Thickness / 2 }));

            drawingContext.DrawLine(pixelPen, new Point(0, 0), new Point(ActualWidth, 0));
            drawingContext.DrawLine(pixelPen, new Point(0, ActualHeight), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(pixelPen, new Point(ActualWidth - 1, 0), new Point(ActualWidth - 1, ActualHeight));
            drawingContext.Pop();

        }

    }
}
