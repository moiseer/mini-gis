using System;
using System.Collections.Generic;
using System.Drawing;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public abstract class Grid : IGrid, IDrawable, ILayer
    {
        private Bounds bounds;
        private double? maxValue;
        private double? minValue;

        public Bounds Bounds
        {
            get => bounds ?? (bounds = GetBounds());
            protected set => bounds = value;
        }

        public double? MaxValue
        {
            get => maxValue ?? (maxValue = GetMaxValue());
            protected set => maxValue = value;
        }

        public double? MinValue
        {
            get => minValue ?? (minValue = GetMinValue());
            protected set => minValue = value;
        }

        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Visible { get; set; }

        public void Draw(IDrawer drawer) => drawer.Draw(this);
        public abstract double? GetValue(double x, double y);
        public abstract void SetColors(Dictionary<double, Color> colors);
        public override string ToString() => Name;
        protected abstract Bounds GetBounds();
        protected abstract double? GetMaxValue();
        protected abstract double? GetMinValue();
    }
}
