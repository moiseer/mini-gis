using System;
using System.Collections.Generic;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Maps
{
    public interface IMap
    {
        Node<double> Center { get; set; }
        List<ILayer> Layers { get; }
        double Scale { get; set; }
    }
}
