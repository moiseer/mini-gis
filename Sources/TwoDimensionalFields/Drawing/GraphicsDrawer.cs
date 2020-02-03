using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using Point = TwoDimensionalFields.MapObjects.Point;
using ScreenPoint = System.Drawing.Point;

namespace TwoDimensionalFields.Drawing
{
    public class GraphicsDrawer : IDrawer
    {
        private readonly Brush brush = new SolidBrush(Color.Gray);

        private readonly double centerX;
        private readonly double centerY;
        private readonly Graphics graphics;
        private readonly double height;
        private readonly Pen pen = new Pen(Color.Black, 1);

        private readonly Symbol pointSymbol = new Symbol();
        private readonly double scale;
        private readonly double width;

        public GraphicsDrawer(Graphics graphics, double centerX, double centerY, double scale, double width, double height)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.graphics = graphics;
            this.centerX = centerX;
            this.centerY = centerY;
            this.scale = scale;
            this.width = width;
            this.height = height;
        }

        public void Draw(IDrawable drawable)
        {
            switch (drawable)
            {
                case Point point:
                    DrawPoint(point);
                    break;
                case Line line:
                    DrawLine(line);
                    break;
                case Polygon polygon:
                    DrawPolygon(polygon);
                    break;
                case Polyline polyline:
                    DrawPolyline(polyline);
                    break;
                case SquareGrid squareGrid:
                    DrawSquareGrid(squareGrid);
                    break;
                case Layer layer:
                    DrawLayer(layer);
                    break;
                case IMap map:
                    DrawMap(map);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void DrawLayer(Layer layer)
        {
            if (!layer.Visible)
            {
                return;
            }

            foreach (var mapObject in layer.Objects)
            {
                if (mapObject is IDrawable drawable)
                {
                    drawable.Draw(this);
                }
            }
        }

        private void DrawLine(Line line)
        {
            var begin = MapToScreen(line.Begin.X, line.Begin.Y);
            var end = MapToScreen(line.End.X, line.End.Y);

            graphics.DrawLine(pen, begin, end);
        }

        private void DrawMap(IMap map)
        {
            foreach (var layer in map.Layers)
            {
                if (layer is IDrawable drawable)
                {
                    drawable.Draw(this);
                }
            }
        }

        private void DrawPoint(Point point)
        {
            graphics.DrawString(pointSymbol.Char.ToString(), pointSymbol.Font, brush, MapToScreen(point.X, point.Y), pointSymbol.Format);
        }

        private void DrawPolygon(Polygon polygon)
        {
            if (polygon.Nodes.Count < 2)
            {
                return;
            }

            ScreenPoint[] points = polygon.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            graphics.FillPolygon(brush, points.ToArray());
            graphics.DrawPolygon(pen, points.ToArray());
        }

        private void DrawPolyline(Polyline polyline)
        {
            if (polyline.Nodes.Count < 2)
            {
                return;
            }

            ScreenPoint[] points = polyline.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            graphics.DrawLines(pen, points);
        }

        private void DrawSquareGrid(SquareGrid grid)
        {
            double[,] matrix = grid.Grid;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    throw new NotImplementedException();
                }
            }
        }

        private ScreenPoint MapToScreen(double x, double y)
        {
            return new System.Drawing.Point
            {
                X = (int)((x - centerX) * scale + width / 2 + 0.5),
                Y = (int)(-(y - centerY) * scale + height / 2 + 0.5)
            };
        }
    }
}
