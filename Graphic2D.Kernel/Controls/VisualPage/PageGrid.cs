using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{

    public class PageGrid
    {
        #region Private fields

        const int GridSizeStep = 5;

        private DrawingVisual _drawingVisual;
        private int _size;
        private double _scale;
        private Size _rangeSize;
        private Color _lineColor;
        private Color _backColor;

        #endregion


        #region Properties

        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value > 0 ? value : 1;
                UpdateGirdVisual();
            }
        }

        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;
                UpdateGirdVisual();
            }
        }

        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                UpdateGirdVisual();
            }
        }

        public int GridSize
        {
            get { return _size; }
            set
            {
                _size = value / GridSizeStep;
                _size = _size > 0 ? _size * GridSizeStep : GridSizeStep;
                UpdateGirdVisual();
            }
        }

        public Size RangeSize
        {
            get { return _rangeSize; }
            set
            {
                _rangeSize = value;
                UpdateGirdVisual();
            }
        }

        public DrawingVisual GridVisual
        {
            get
            {
                if (_drawingVisual == null)
                {
                    UpdateGirdVisual();
                }
                return _drawingVisual;
            }
        }

        #endregion


        #region Constructors

        public PageGrid(double width, double height)
        {
            _size = GridSizeStep;
            _scale = 1.0;
            _rangeSize = new Size(width, height);
            _lineColor = Colors.LightGray;
            _backColor = Colors.White;
        }

        public PageGrid()
            : this(800, 600)
        {
        }

        #endregion


        public void UpdateGirdVisual()
        {
            if (_drawingVisual == null)
                _drawingVisual = new DrawingVisual();

            DrawingContext dc = _drawingVisual.RenderOpen();

            Matrix mtx = PresentationSource.FromVisual(_drawingVisual).CompositionTarget.TransformToDevice;
            double dpiFactor = 1 / mtx.M11;
            double delt = dpiFactor / 2;

            double xlen = RangeSize.Width * Scale;
            double ylen = RangeSize.Height * Scale;

            Brush brush = new SolidColorBrush(LineColor);
            Pen majorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);
            brush.Opacity = 0.5;
            Pen minorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);

            if (majorPen.CanFreeze) majorPen.Freeze();
            if (minorPen.CanFreeze) minorPen.Freeze();

            //double border = 10 * PageScale;

            GuidelineSet guidelineSet = new GuidelineSet();
            guidelineSet.GuidelinesX.Add(0 - delt);
            guidelineSet.GuidelinesX.Add(xlen - delt);
            //guidelineSet.GuidelinesX.Add(-border - delt);
            //guidelineSet.GuidelinesX.Add(xlen + border * 2.0 - delt);

            guidelineSet.GuidelinesY.Add(0 - delt);
            guidelineSet.GuidelinesY.Add(ylen - delt);
            //guidelineSet.GuidelinesY.Add(-border - delt);
            //guidelineSet.GuidelinesY.Add(ylen + border * 2.0 - delt);

            dc.PushGuidelineSet(guidelineSet);
            //dc.DrawRectangle(PageBackColor, majorPen, new Rect(-border, -border, xlen + border * 2.0, ylen + border * 2.0));
            dc.DrawRectangle(null, minorPen, new Rect(0, 0, xlen, ylen));
            dc.Pop();

            LineGeometry lgx = new LineGeometry(new Point(0, 0), new Point(0, ylen));
            LineGeometry lgy = new LineGeometry(new Point(0, 0), new Point(xlen, 0));
            if (lgx.CanFreeze) lgx.Freeze();
            if (lgy.CanFreeze) lgy.Freeze();


            for (double x = 0; x <= RangeSize.Width; x += GridSize)
            {
                GuidelineSet gridGuidelines = new GuidelineSet();
                gridGuidelines.GuidelinesX.Add(x * Scale - delt);

                dc.PushGuidelineSet(gridGuidelines);
                dc.PushTransform(new TranslateTransform(x * Scale, 0));
                dc.DrawGeometry(null, (x / GridSize) % 5 == 0 ? majorPen : minorPen, lgx);
                dc.Pop();
                dc.Pop();
            }

            for (double y = 0; y <= RangeSize.Height; y += GridSize)
            {
                GuidelineSet gridGuidelines = new GuidelineSet();
                gridGuidelines.GuidelinesY.Add(y * Scale - delt);

                dc.PushGuidelineSet(gridGuidelines);
                dc.PushTransform(new TranslateTransform(0, y * Scale));
                dc.DrawGeometry(null, (y / GridSize) % 5 == 0 ? majorPen : minorPen, lgy);
                dc.Pop();
                dc.Pop();
            }

            dc.Close();
        }

    }

}
