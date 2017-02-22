using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Geometries
{
    /// <summary>
    /// Abstract class representing geometry.
    /// </summary>
    public abstract class Geometry
    {

        /// <summary>
        /// Draw Geometry.
        /// </summary>
        public abstract void Draw(Brush fill, Pen stroke, DrawingContext drawingContext);


        /// <summary>
        /// Get a point collection which indicates the geometry information.
        /// </summary>
        public abstract List<Point> GetGeometryPoints();


        /// <summary>
        /// Set the geometry by a point collection.
        /// </summary>
        /// <param name="points">point collection indicating the geometry information.</param>
        /// <returns>whether setting operation is a success.</returns>
        public abstract bool SetGeometryByPoints(IList<Point> points);

    }
}
