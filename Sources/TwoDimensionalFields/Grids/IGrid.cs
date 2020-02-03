using System;

namespace TwoDimensionalFields.Grids
{
    public interface IGrid
    {
        (double X, double Y) Position { get; set; }
        double GetValue(double x, double y);
    }
}
