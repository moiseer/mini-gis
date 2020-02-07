using System;
using System.Drawing;

namespace TwoDimensionalFields.Drawing
{
    public class Symbol
    {
        public Symbol()
        {
            Char = (char)0x6E;
            Font = new Font("Webdings", 14);
            Format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }
        
        public char Char { get; set; }
        public Font Font { get; set; }
        public StringFormat Format { get; set; }
    }
}
