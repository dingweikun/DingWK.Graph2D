using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Controls.Rulers
{
    internal class HorRuler : Ruler
    {
        public HorRuler()
        {
            this.Height = 18;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // 刻度线宽宽度 1 个像素
            // Set tick thickness 1 pixel.

            Matrix mtx = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
            double dpiFactor = 1 / mtx.M11;
            Pen pixelPen = new Pen(TickColor, 1 * dpiFactor);

            // 绘制背景
            // Draw ruler background.

            drawingContext.DrawRectangle(BackColor, null, new Rect(RenderSize));

            // 绘制标尺
            // Draw ruler ticks.

            Point a = new Point(Start, ActualHeight);
            Point b = new Point(Start, ActualHeight - 4);  // 短刻度
            Point c = new Point(Start, ActualHeight - 8);  // 长刻度
            Point t = new Point(Start, ActualHeight - 16);

            int i = 0;
            while (Start + i * MinorTickSpacing <= ActualWidth)
            {
                a.X = b.X = c.X = t.X = Start + i * MinorTickSpacing;

                GuidelineSet guideline = new GuidelineSet(new double[] { a.X - pixelPen.Thickness / 2 }, null);

                drawingContext.PushGuidelineSet(guideline);

                if (i % MinorTickCount == 0)
                {
                    drawingContext.DrawLine(pixelPen, a, c);

                    FormattedText ft = new FormattedText(
                        Math.Round(StartValue + i * MinorTickSpacingValue, 4).ToString(),
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        (6 * 96.0 / 72.0),
                        TickTextColor);
                    ft.SetFontWeight(FontWeights.Regular);
                    ft.TextAlignment = TextAlignment.Center;
                    drawingContext.DrawText(ft, t);
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
                    new double[] { 0 - pixelPen.Thickness / 2, ActualWidth - pixelPen.Thickness / 2 },
                    new double[] { ActualHeight - 1 - pixelPen.Thickness / 2 }));

            //drawingContext.DrawLine(pixelPen, new Point(0, 0), new Point(0, ActualHeight));
            //drawingContext.DrawLine(pixelPen, new Point(ActualWidth, 0), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(pixelPen, new Point(0, ActualHeight - 1), new Point(ActualWidth, ActualHeight - 1));
            drawingContext.Pop();

        }

    }
}

