using System;

namespace TwoDimensionalFields.Grids
{
    public interface IGrid
    {
        double? MaxValue { get; }
        double? MinValue { get; }
        double? GetValue(double x, double y);
    }
}
