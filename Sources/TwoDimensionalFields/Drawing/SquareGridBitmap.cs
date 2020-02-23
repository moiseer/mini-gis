using System;
using System.Drawing;
using TwoDimensionalFields.Grids;

namespace TwoDimensionalFields.Drawing
{
    public class SquareGridBitmap
    {
        private readonly SquareGrid squareGrid;
        private Bitmap bitmap;

        public SquareGridBitmap(SquareGrid squareGrid)
        {
            this.squareGrid = squareGrid;
        }

        public Bitmap Bitmap => bitmap ?? (bitmap = CalcBitmap());
        public Color MaxColor { get; set; }
        public double? MaxValue => squareGrid.MaxValue;
        public Color MinColor { get; set; }
        public double? MinValue => squareGrid.MinValue;

        public void Clear()
        {
            bitmap = null;
        }

        private Bitmap CalcBitmap()
        {
            var width = squareGrid.ColumnCount;
            var height = squareGrid.RowCount;
            var newBitmap = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double? value = squareGrid[i, j];
                    var color = value.HasValue ? CalcColorByValue(value) : Color.White;
                    newBitmap.SetPixel(j, i, color);
                }
            }

            return newBitmap;
        }

        private Color CalcColorByValue(double? value)
        {
            double? foo = (value - MinValue) / (MaxValue - MinValue);

            byte CalcColorValue(byte minC, byte maxC)
            {
                return Convert.ToByte(foo * (maxC - minC) + minC);
            }

            byte alpha = CalcColorValue(MinColor.A, MaxColor.A);
            byte red = CalcColorValue(MinColor.R, MaxColor.R);
            byte green = CalcColorValue(MinColor.G, MaxColor.G);
            byte blue = CalcColorValue(MinColor.B, MaxColor.B);

            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
