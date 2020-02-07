using System;

namespace TwoDimensionalFields.Maps
{
    public interface IMapObject
    {
        Bounds Bounds { get; }

        bool Selected { get; set; }
    }
}
