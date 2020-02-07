using System;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Grids
{
    public interface IGrid
    {
        Node<double> Position { get; set; }
        double? GetValue(double x, double y);
    }
}
