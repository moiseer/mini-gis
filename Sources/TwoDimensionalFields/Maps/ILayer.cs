using System;

namespace TwoDimensionalFields.Maps
{
    public interface ILayer : IMapObject
    {
        string Name { get; set; }
        bool Visible { get; set; }
    }
}
