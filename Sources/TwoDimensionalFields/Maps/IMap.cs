using System;
using System.Collections.Generic;

namespace TwoDimensionalFields.Maps
{
    public interface IMap
    {
        List<ILayer> Layers { get; }
        (double X, double Y) MapCenter { get; set; }
        double MapScale { get; set; }
    }
}
