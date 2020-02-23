using System;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Grids
{
    public interface IGrid
    {
        Node<double> Position { get; }
        double? GetValue(double x, double y);
    }
}
