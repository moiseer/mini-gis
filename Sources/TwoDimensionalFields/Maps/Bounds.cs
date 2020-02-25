using System;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Maps
{
    public class Bounds
    {
        private Node<double> bottomRight;
        private Node<double> topLeft;

        public Bounds()
        {
            bottomRight = new Node<double>(0, 0);
            topLeft = new Node<double>(0, 0);
        }

        public Bounds(double xMin, double yMax, double xMax, double yMin) : this()
        {
            SetBounds(xMin, yMax, xMax, yMin);
        }

        public bool Valid { get; private set; }
        public double XMax => bottomRight.X;
        public double XMin => topLeft.X;
        public double YMax => topLeft.Y;
        public double YMin => bottomRight.Y;

        public static Bounds operator +(Bounds first, Bounds second) => first.UnionBounds(second);

        public bool IntersectsWith(Bounds bounds)
        {
            if (!Valid || !bounds.Valid)
            {
                return false;
            }

            return bounds.XMin < XMax && XMin < bounds.XMax && bounds.YMin < YMax && YMin < bounds.YMax;
        }

        public void SetBounds(double xMin, double yMax, double xMax, double yMin)
        {
            topLeft = new Node<double>(xMin, yMax);
            bottomRight = new Node<double>(xMax, yMin);
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
                    YMin < addBounds.YMin ? YMin : addBounds.YMin
                );
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
