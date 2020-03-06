using System.Collections.Generic;
using System.Drawing;

namespace TwoDimensionalFields.Drawing.GridGraphics
{
    public interface IGridGraphics
    {
        Dictionary<double, Color> Colors { get; set; }
        double? MaxValue { get; }
        double? MinValue { get; }
        void Clear();
    }
}
