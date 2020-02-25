using System;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    public class Line : MapObject
    {
        public Line(Node<double> begin, Node<double> end) : this()
        {
            Begin = begin;
            End = end;
        }

        public Line(double beginX, double beginY, double endX, double endY)
            : this(new Node<double>(beginX, beginY), new Node<double>(endX, endY))
        {
        }

        private Line()
        {
            objectType = MapObjectType.Line;
        }

        public Node<double> Begin { get; }
        public Node<double> End { get; }

        protected override Bounds GetBounds()
        {
            return new Bounds(
                Begin.X < End.X ? Begin.X : End.X,
                Begin.Y > End.Y ? Begin.Y : End.Y,
                Begin.X > End.X ? Begin.X : End.X,
                Begin.Y < End.Y ? Begin.Y : End.Y
            );
        }
    }
}
