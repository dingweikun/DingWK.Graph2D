using System.Collections.Generic;
using System.Windows;

namespace DingWK.Graphic2D.Geom
{
    /// <summary>
    /// Abstract class representing geometries.
    /// </summary>
    public abstract class Geomerty
    {

        /// <summary>
        /// Get a point collection which representing the geometry information.
        /// </summary>
        public abstract List<Point> GetGeometryPoints();


        /// <summary>
        /// Set the geometry by a point collection.
        /// </summary>
        /// <param name="points">point collection indicating the geometry information.</param>
        /// <returns>whether setting operation is a success.</returns>
        public abstract bool SetGeometryByPoints(IList<Point> points);


        /// <summary>
        /// Get a clone instance of geometry object.
        /// </summary>
        public abstract Geomerty Clone();

    }
}
