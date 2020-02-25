using System;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    /// <summary>
    /// Класс для работы с точечными объектами
    /// </summary>
    public class Point : MapObject
    {
        public Point(double x, double y) : this(new Node<double>(x, y))
        {
        }

        public Point(Node<double> position) : this()
        {
            Position = position;
        }

        private Point()
        {
            objectType = MapObjectType.Point;
        }

        public Node<double> Position { get; }
        public double X => Position.X;
        public double Y => Position.Y;

        protected override Bounds GetBounds()
        {
            return new Bounds(Position.X, Position.Y, Position.X, Position.Y);
        }
    }
}
