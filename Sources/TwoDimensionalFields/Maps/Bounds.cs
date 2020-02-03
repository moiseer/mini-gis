using System;

namespace TwoDimensionalFields.Maps
{
    public class Bounds
    {
        private (double X, double Y) bottomRight = (0, 0);
        private (double X, double Y) topLeft = (0, 0);

        public Bounds()
        {
        }

        public Bounds(double xMin, double yMax, double xMax, double yMin)
        {
            SetBounds(xMin, yMax, xMax, yMin);
        }

        public bool Valid { get; private set; }

        public double XMax
        {
            get { return bottomRight.X; }
        }

        public double XMin
        {
            get { return topLeft.X; }
        }

        public double YMax
        {
            get { return topLeft.Y; }
        }

        public double YMin
        {
            get { return bottomRight.Y; }
        }

        public static Bounds operator +(Bounds first, Bounds second)
        {
            return first.UnionBounds(second);
        }

        public void SetBounds(double xMin, double yMax, double xMax, double yMin)
        {
            topLeft = (xMin, yMax);
            bottomRight = (xMax, yMin);
            Valid = true;
        }

        public Bounds UnionBounds(Bounds addBounds)
        {
            if (Valid && addBounds.Valid)
            {
                return new Bounds(
                    XMin < addBounds.XMin ? XMin : addBounds.XMin,
                    YMax > addBounds.YMax ? YMax : addBounds.YMax,
                    XMax > addBounds.XMax ? XMax : addBounds.XMax,
                    YMin < addBounds.YMin ? YMin : addBounds.YMin);
            }

            if (Valid && !addBounds.Valid)
            {
                return this;
            }

            if (!Valid && addBounds.Valid)
            {
                return addBounds;
            }

            return new Bounds();
        }
    }
}
