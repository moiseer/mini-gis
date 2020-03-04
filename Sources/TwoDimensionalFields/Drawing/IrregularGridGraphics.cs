using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using Point = TwoDimensionalFields.MapObjects.Point;

namespace TwoDimensionalFields.Drawing
{
    public class IrregularGridGraphics
    {
        private readonly IrregularGrid grid;
        private IEnumerable<ValuedPoint> coloredPoints;

        public IrregularGridGraphics(IrregularGrid grid)
        {
            this.grid = grid;
        }

        public IEnumerable<ValuedPoint> ColoredPoints => coloredPoints ?? (coloredPoints = CalcColoredPoints());
        public Dictionary<double, Color> Colors { get; set; }
        public double? MaxValue => grid.MaxValue;
        public double? MinValue => grid.MinValue;

        public void Clear() => coloredPoints = null;

        private IEnumerable<ValuedPoint> CalcColoredPoints()
        {
            var palette = new GridPalette(Colors, MinValue, MaxValue);

            return grid.Nodes.Select(node => new ValuedPoint(node)
            {
                Style = new Style
                {
                    Brush = new SolidBrush(palette.GetColor(node.Z))
                }
            });
        }

        public class ValuedPoint : Point
        {
            public ValuedPoint(Node3d<double> position) : base(position)
            {
                Value = position.Z;
            }

            public double Value { get; set; }
        }
    }
}
