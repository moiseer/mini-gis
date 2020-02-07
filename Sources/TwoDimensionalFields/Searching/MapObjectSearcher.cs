using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Searching
{
    public class MapObjectSearcher : ISearcher<MapObject>
    {
        private readonly double delta;
        private readonly Node<double> searchPoint;

        public MapObjectSearcher(Node<double> searchPoint, double delta)
        {
            this.searchPoint = searchPoint;
            this.delta = delta;
        }

        public MapObject Search(ISearchable<MapObject> searchable)
        {
            switch (searchable)
            {
                case Point point:
                    return SearchPoint(point);
                case Line line:
                    return SearchLine(line);
                case Polygon polygon:
                    return SearchPolygon(polygon);
                case Polyline polyline:
                    return SearchPolyline(polyline);
                case Layer layer:
                    return SearchLayer(layer);
                /*case SquareGrid squareGrid:
                    return SearchSquareGrid(squareGrid);*/
                case IMap map:
                    return SearchMap(map);
                default:
                    throw new NotImplementedException();
            }
        }

        /*private MapObject SearchSquareGrid(SquareGrid squareGrid)
        {
            (double xMin, double yMax) = squareGrid.Position;
            double xMax = xMin + squareGrid.Width;
            double yMin = yMax - squareGrid.Height;

            if(IsPointInsideArea(searchPoint, xMin, yMin, xMax, yMax))
            {
                return new Point(searchPoint);
            }

            return null;
        }*/

        private static bool IsContainPoint(List<Node<double>> polygon, Node<double> searchPoint)
        {
            (double x, double y) = searchPoint;

            var c = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y <= y && y < polygon[j].Y || polygon[j].Y <= y && y < polygon[i].Y) &&
                    x > (polygon[j].X - polygon[i].X) * (y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    c = !c;
                }
            }

            return c;
        }

        private static bool IsPointInsideArea(Node<double> point, double spXMin, double spYMin, double spXMax, double spYMax)
        {
            (double x, double y) = point;
            bool beginInside = x > spXMin && y > spYMin && x < spXMax && y < spYMax;
            return beginInside;
        }

        private static bool IsPointNear(Node<double> point, Node<double> searchPoint, double delta)
        {
            (double x, double y) = searchPoint;
            var spXMin = x - delta;
            var spXMax = x + delta;
            var spYMin = y - delta;
            var spYMax = y + delta;

            return IsPointInsideArea(point, spXMin, spYMin, spXMax, spYMax);
        }

        private static bool IsSegmentIntersectsWithQuad(Node<double> begin, Node<double> end, Node<double> searchPoint, double delta)
        {
            (double x, double y) = searchPoint;
            var spXMin = x - delta;
            var spXMax = x + delta;
            var spYMin = y - delta;
            var spYMax = y + delta;

            bool beginInside = IsPointInsideArea(begin, spXMin, spYMin, spXMax, spYMax);
            bool endInside = IsPointInsideArea(end, spXMin, spYMin, spXMax, spYMax);
            if (beginInside || endInside)
            {
                return true;
            }

            var point1 = new Node<double>(spXMin, spYMax);
            var point2 = new Node<double>(spXMax, spYMax);
            if (IsSegmentsIntersect(begin, end, point1, point2))
            {
                return true;
            }

            var point3 = new Node<double>(spXMax, spYMin);
            if (IsSegmentsIntersect(begin, end, point2, point3))
            {
                return true;
            }

            var point4 = new Node<double>(spXMin, spYMin);
            if (IsSegmentsIntersect(begin, end, point3, point4))
            {
                return true;
            }

            if (IsSegmentsIntersect(begin, end, point4, point1))
            {
                return true;
            }

            return false;
        }

        private static bool IsSegmentsIntersect(Node<double> beginA, Node<double> endA, Node<double> beginB, Node<double> endB)
        {
            double v1 = VectorMultiplication(endB.X - beginB.X, endB.Y - beginB.Y, beginA.X - beginB.X, beginA.Y - beginB.Y);
            double v2 = VectorMultiplication(endB.X - beginB.X, endB.Y - beginB.Y, endA.X - beginB.X, endA.Y - beginB.Y);
            double v3 = VectorMultiplication(endA.X - beginA.X, endA.Y - beginA.Y, beginB.X - beginA.X, beginB.Y - beginA.Y);
            double v4 = VectorMultiplication(endA.X - beginA.X, endA.Y - beginA.Y, endB.X - beginA.X, endB.Y - beginA.Y);

            if (v1 * v2 < 0 && v3 * v4 < 0)
            {
                return true;
            }

            return false;
        }

        private static double VectorMultiplication(double ax, double ay, double bx, double by)
        {
            return ax * by - bx * ay;
        }

        private MapObject SearchLayer(Layer layer)
        {
            if (!layer.Visible)
            {
                return null;
            }

            for (int i = layer.Objects.Count - 1; i >= 0; i--)
            {
                var mapObject = layer.Objects[i].Search(this);
                if (mapObject != null)
                {
                    return mapObject;
                }
            }

            return null;
        }

        private MapObject SearchLine(Line line)
        {
            if (IsSegmentIntersectsWithQuad(line.Begin, line.End, searchPoint, delta))
            {
                return line;
            }

            return null;
        }

        private MapObject SearchMap(IMap map)
        {
            for (int i = map.Layers.Count - 1; i >= 0; i--)
            {
                if (map.Layers[i] is ISearchable<MapObject> searchable)
                {
                    MapObject mapObject = searchable.Search(this);
                    if (mapObject != null)
                    {
                        return mapObject;
                    }
                }
            }

            return null;
        }

        private MapObject SearchPoint(Point point)
        {
            if (IsPointNear(point.Position, searchPoint, delta))
            {
                return point;
            }

            return null;
        }

        private MapObject SearchPolygon(Polygon polygon)
        {
            var baseResult = SearchPolyline(polygon);
            if (baseResult != null)
            {
                return baseResult;
            }

            if (IsSegmentIntersectsWithQuad(polygon.Nodes.Last(), polygon.Nodes.First(), searchPoint, delta))
            {
                return polygon;
            }

            if (IsContainPoint(polygon.Nodes, searchPoint))
            {
                return polygon;
            }

            return null;
        }

        private MapObject SearchPolyline(Polyline polyline)
        {
            if (polyline.Nodes.Count == 0)
            {
                return null;
            }

            if (polyline.Nodes.Count == 1 && IsPointNear(polyline.Nodes.First(), searchPoint, delta))
            {
                return polyline;
            }

            for (int x = 0; x < polyline.Nodes.Count - 1; x++)
            {
                if (IsSegmentIntersectsWithQuad(polyline.Nodes[x], polyline.Nodes[x + 1], searchPoint, delta))
                {
                    return polyline;
                }
            }

            return null;
        }
    }
}
