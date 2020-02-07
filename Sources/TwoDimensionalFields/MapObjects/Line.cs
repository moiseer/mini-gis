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

        public Line(double beginX, double beginY, double endX, double endY) : this()
        {
            Begin = new Node<double>(beginX, beginY);
            End = new Node<double>(endX, endY);
        }

        private Line()
        {
            objectType = MapObjectType.Line;
        }

        public Node<double> Begin { get; }

        public Node<double> End { get; }

        protected override Bounds GetBounds()
        {
            return bounds = new Bounds(
                Begin.X < End.X ? Begin.X : End.X,
                Begin.Y > End.Y ? Begin.Y : End.Y,
                Begin.X > End.X ? Begin.X : End.X,
                Begin.Y < End.Y ? Begin.Y : End.Y);
        }
    }
}
