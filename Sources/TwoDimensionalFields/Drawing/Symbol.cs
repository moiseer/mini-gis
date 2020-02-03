using System;
using System.Drawing;

namespace TwoDimensionalFields.Drawing
{
    public class Symbol
    {
        public char Char { get; set; } = (char)0x6E;

        public Font Font { get; set; } = new Font("Webdings", 14);

        public StringFormat Format { get; set; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }
}
