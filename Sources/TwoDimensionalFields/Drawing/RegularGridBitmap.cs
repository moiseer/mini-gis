using System;
using System.Collections.Generic;
using System.Drawing;
using TwoDimensionalFields.Grids;

namespace TwoDimensionalFields.Drawing
{
    public class RegularGridBitmap
    {
        private readonly RegularGrid grid;
        private readonly Color nullColor;
        private Bitmap bitmap;

        public RegularGridBitmap(RegularGrid grid)
        {
            this.grid = grid;
            nullColor = Color.FromArgb(0, 0, 0, 0);
        }

        public Bitmap Bitmap => bitmap ?? (bitmap = CalcBitmap());
        public Dictionary<double, Color> Colors { get; set; }
        public double? MaxValue => grid.MaxValue;
        public double? MinValue => grid.MinValue;

        public void Clear() => bitmap = null;

        private Bitmap CalcBitmap()
        {
            var width = grid.ColumnCount;
            var height = grid.RowCount;
            var newBitmap = new Bitmap(width, height);

            var palette = new GridPalette(Colors, MinValue, MaxValue);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double? value = grid[i, j];
                    var color = value.HasValue ? palette.GetColor(value.Value) : nullColor;
                    newBitmap.SetPixel(j, i, color);
                }
            }

            return newBitmap;
        }
    }
}
