using System;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    public class Line : MapObject
    {
        public Line((double X, double Y) begin, (double X, double Y) end) : this()
        {
            Begin = begin;
            End = end;
        }

        public Line(double beginX, double beginY, double endX, double endY) : this()
        {
            Begin = (beginX, beginY);
            End = (endX, endY);
        }

        private Line()
        {
            objectType = MapObjectType.Line;
        }

        public (double X, double Y) Begin { get; }

        public (double X, double Y) End { get; }

        protected override Bounds GetBounds()
        {
            return bounds = new Bounds(
                Begin.X < End.X ? Begin.X : End.X,
                Begin.Y > End.Y ? Begin.Y : End.Y,
                Begin.X > End.X ? Begin.X : End.X,
                Begin.Y < End.Y ? Begin.Y : End.Y);
        }

        /*internal override bool IsIntersectsWithQuad(Vertex searchPoint, double d)
        {
            if (IsSegmentIntersectsWithQuad(begin, end, searchPoint, d))
                return true;
            return false;
        }*/
    }
}
