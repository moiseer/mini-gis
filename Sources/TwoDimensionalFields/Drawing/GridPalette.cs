using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TwoDimensionalFields.Drawing
{
    public class GridPalette
    {
        private readonly Dictionary<double, Color> colors;
        private readonly double maxColorKey;
        private readonly double? maxValue;
        private readonly double minColorKey;
        private readonly double? minValue;

        public GridPalette(Dictionary<double, Color> colors, double? minValue, double? maxValue)
        {
            this.colors = colors != null && colors.Count > 1 ? colors : GetWhiteBlackColors();

            minColorKey = this.colors.Keys.Min();
            maxColorKey = this.colors.Keys.Max();

            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public Color GetColor(double value)
        {
            if (!minValue.HasValue || !maxValue.HasValue)
            {
                return Color.FromArgb(0, 0, 0, 0);
            }

            var currentKey = (value - minValue) / (maxValue - minValue) * (maxColorKey - minColorKey) + minColorKey;

            var minKey = colors
                .Where(color => color.Key <= currentKey)
                .Max(color => color.Key);
            var maxKey = colors
                .Where(color => color.Key >= currentKey)
                .Min(color => color.Key);

            if (minKey.Equals(maxKey))
            {
                return colors[minKey];
            }

            var minColor = colors[minKey];
            var maxColor = colors[maxKey];

            var foo = (maxValue - minValue) / (maxColorKey - minColorKey);
            var localMinValue = foo * (minKey - minColorKey) + minValue;
            var localMaxValue = foo * (maxKey - minColorKey) + minValue;

            var bar = (value - localMinValue) / (localMaxValue - localMinValue);
            byte CalcColorValue(byte minC, byte maxC) => Convert.ToByte(bar * (maxC - minC) + minC);

            byte alpha = CalcColorValue(minColor.A, maxColor.A);
            byte red = CalcColorValue(minColor.R, maxColor.R);
            byte green = CalcColorValue(minColor.G, maxColor.G);
            byte blue = CalcColorValue(minColor.B, maxColor.B);

            return Color.FromArgb(alpha, red, green, blue);
        }

        private Dictionary<double, Color> GetWhiteBlackColors()
        {
            return new Dictionary<double, Color>
            {
                [0] = Color.Black,
                [100] = Color.White
            };
        }
    }
}
