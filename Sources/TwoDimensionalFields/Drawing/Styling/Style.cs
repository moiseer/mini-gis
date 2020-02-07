using System;
using System.Drawing;

namespace TwoDimensionalFields.Drawing.Styling
{
    public class Style
    {
        public Brush Brush { get; set; }
        public Pen Pen { get; set; }
        public Symbol Symbol { get; set; }

        public static Style CreateDefault()
        {
            return new Style
            {
                Symbol = new Symbol(),
                Pen = new Pen(Color.Black, 1),
                Brush = new SolidBrush(Color.Gray)
            };
        }
    }
}
