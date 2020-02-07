using System;

namespace TwoDimensionalFields.Drawing.Styling
{
    public interface IStyled
    {
        bool HasOwnStyle { get; set; }
        Style Style { get; set; }
    }
}
