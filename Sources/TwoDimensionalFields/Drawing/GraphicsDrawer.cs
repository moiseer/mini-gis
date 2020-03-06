using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using Point = TwoDimensionalFields.MapObjects.Point;

namespace TwoDimensionalFields.Drawing
{
    public class GraphicsDrawer : IDrawer
    {
        private readonly Style defaultStyle;
        private readonly Graphics graphics;
        private readonly Style selectionStyle;

        private Bounds bounds;
        private double centerX;
        private double centerY;
        private double height;
        private double scale;
        private double width;

        public GraphicsDrawer(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.graphics = graphics;

            defaultStyle = Style.CreateDefault();
            selectionStyle = new Style
            {
                Pen = new Pen(Color.Blue, 2),
                Brush = new SolidBrush(Color.DodgerBlue)
            };
        }

        public void Draw(IDrawable drawable)
        {
            switch (drawable)
            {
                case IMapObject mapObject when !bounds.IntersectsWith(mapObject.Bounds):
                case ILayer layerObject when !layerObject.Visible:
                case Polyline polylineObject when polylineObject.Nodes.Count < 2:
                    return;
                case ValuedPoint valuedPoint:
                    DrawValuedPoint(valuedPoint);
                    break;
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
                case RegularGrid regularGrid:
                    DrawRegularGrid(regularGrid);
                    break;
                case IrregularGrid irregularGrid:
                    DrawIrregularGrid(irregularGrid);
                    break;
                case Layer layer:
                    DrawLayer(layer);
                    break;
                case IMap map:
                    DrawMap(map);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void SetParams(double centerX, double centerY, double scale, double width, double height, Bounds bounds)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.scale = scale;
            this.width = width;
            this.height = height;
            this.bounds = bounds;
        }

        private void DrawIrregularGrid(IrregularGrid grid)
        {
            foreach (var point in grid.GridGraphics.ColoredPoints)
            {
                Draw(point);
            }
        }

        private void DrawLayer(Layer layer)
        {
            foreach (var mapObject in layer.Objects)
            {
                mapObject.Draw(this);
            }
        }

        private void DrawLine(Line line)
        {
            var begin = MapToScreen(line.Begin.X, line.Begin.Y);
            var end = MapToScreen(line.End.X, line.End.Y);

            var pen = GetPen(line);

            graphics.DrawLine(pen, begin, end);
        }

        private void DrawMap(IMap map)
        {
            foreach (var layer in map.Layers.OfType<IDrawable>())
            {
                layer.Draw(this);
            }
        }

        private void DrawPoint(Point point)
        {
            var brush = GetBrush(point);
            var symbol = GetSymbol(point);

            graphics.DrawString(symbol.Char.ToString(), symbol.Font, brush, MapToScreen(point.X, point.Y), symbol.Format);
        }

        private void DrawPolygon(Polygon polygon)
        {
            System.Drawing.Point[] points = polygon.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            var pen = GetPen(polygon);
            var brush = GetBrush(polygon);

            graphics.FillPolygon(brush, points);
            graphics.DrawPolygon(pen, points);
        }

        private void DrawPolyline(Polyline polyline)
        {
            System.Drawing.Point[] points = polyline.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            var pen = GetPen(polyline);

            graphics.DrawLines(pen, points);
        }

        private void DrawRegularGrid(RegularGrid grid)
        {
            var bitmap = grid.GridGraphics.Bitmap;
            var drawPosition = MapToScreen(grid.Position.X, grid.Position.Y);
            var size = new Size((int)(grid.Width * scale), (int)(grid.Height * scale));
            var drawArea = new Rectangle(drawPosition, size);

            graphics.DrawImage(bitmap, drawArea);
        }

        private void DrawValuedPoint(ValuedPoint point)
        {
            var brush = GetBrush(point);
            var symbol = GetSymbol(point);

            var textFont = new Font("Arial", 6);
            var textBrush = new SolidBrush(Color.Black);

            var screenPoint = MapToScreen(point.X, point.Y);

            graphics.DrawString(symbol.Char.ToString(), symbol.Font, brush, screenPoint, symbol.Format);
            graphics.DrawString($"{point.Value:0.00}", textFont, textBrush, screenPoint);
        }

        private Brush GetBrush(MapObject mapObject)
        {
            return mapObject.Selected ? selectionStyle.Brush :
                mapObject.HasOwnStyle ? mapObject.Style?.Brush ?? defaultStyle.Brush :
                defaultStyle.Brush;
        }

        private Pen GetPen(MapObject mapObject)
        {
            return mapObject.Selected ? selectionStyle.Pen :
                mapObject.HasOwnStyle ? mapObject.Style?.Pen ?? defaultStyle.Pen :
                defaultStyle.Pen;
        }

        private Symbol GetSymbol(MapObject mapObject)
        {
            return mapObject.HasOwnStyle ? mapObject.Style?.Symbol ?? defaultStyle.Symbol : defaultStyle.Symbol;
        }

        private System.Drawing.Point MapToScreen(double x, double y)
        {
            return new System.Drawing.Point
            {
                X = (int)((x - centerX) * scale + width / 2 + 0.5),
                Y = (int)(-(y - centerY) * scale + height / 2 + 0.5)
            };
        }
    }
}
