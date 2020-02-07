﻿using System;
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
        private readonly double centerX;
        private readonly double centerY;
        private readonly Style defaultStyle = Style.CreateDefault();
        private readonly Graphics graphics;
        private readonly double height;
        private readonly double scale;

        private readonly Style selectionStyle = new Style
        {
            Pen = new Pen(Color.Blue, 2),
            Brush = new SolidBrush(Color.DodgerBlue)
        };

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

            var pen = GetPen(line);

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
            var brush = GetBrush(point);
            var symbol = GetSymbol(point);

            graphics.DrawString(symbol.Char.ToString(), symbol.Font, brush, MapToScreen(point.X, point.Y), symbol.Format);
        }

        private void DrawPolygon(Polygon polygon)
        {
            if (polygon.Nodes.Count < 2)
            {
                return;
            }

            System.Drawing.Point[] points = polygon.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            var pen = GetPen(polygon);
            var brush = GetBrush(polygon);

            graphics.FillPolygon(brush, points.ToArray());
            graphics.DrawPolygon(pen, points.ToArray());
        }

        private void DrawPolyline(Polyline polyline)
        {
            if (polyline.Nodes.Count < 2)
            {
                return;
            }

            System.Drawing.Point[] points = polyline.Nodes
                .Select(node => MapToScreen(node.X, node.Y))
                .ToArray();

            var pen = GetPen(polyline);

            graphics.DrawLines(pen, points);
        }

        private void DrawSquareGrid(SquareGrid grid)
        {
            if (!grid.Visible)
            {
                return;
            }

            for (int i = 0; i < grid.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Grid.GetLength(1); j++)
                {
                    var x = grid.Position.X + i * grid.Edge;
                    var y = grid.Position.Y - j * grid.Edge;
                    var point = new Point(x, y);

                    DrawPoint(point);

                    var drawFont = new Font("Arial", 10);
                    var brush = GetBrush(point);

                    graphics.DrawString($"{grid.Grid[i, j]: 0.00}", drawFont, brush, MapToScreen(x, y));
                }
            }
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
