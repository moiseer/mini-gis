using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Drawing.GridGraphics
{
    public class IrregularGridGraphics : IGridGraphics
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
                    Brush = new SolidBrush(palette.GetColor(node.Z)),
                    Symbol = new Symbol
                    {
                        Font = new Font("Webdings", 7)
                    }
                }
            });
        }
    }
}
