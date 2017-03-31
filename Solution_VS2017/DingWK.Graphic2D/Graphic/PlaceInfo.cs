using System.Windows;

namespace DingWK.Graphic2D.Graphic
{
    public struct PlaceInfo : IPlaceInfo
    {
        private Point _origin;
        private double _angle;


        public Point Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public double Angle
        {
            get => _angle;
            set => _angle = value % 360;
        }
    }
}
