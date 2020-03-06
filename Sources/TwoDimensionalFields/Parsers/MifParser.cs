using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using Point = TwoDimensionalFields.MapObjects.Point;

namespace TwoDimensionalFields.Parsers
{
    public class MifParser
    {
        public MifParser()
        {
            Version = 300;
            Charset = string.Empty;
            Delimiter = '\t';
            Unique = new List<int>();
            Index = new List<int>();
            Transform = new List<int>();
            ColumnsN = 0;
            Data = new List<MapObject>();
        }

        public string Charset { get; set; }
        public List<Column> Columns { get; set; }
        public int ColumnsN { get; set; }
        public List<MapObject> Data { get; set; }
        public char Delimiter { get; set; }
        public List<int> Index { get; set; }
        public List<int> Transform { get; set; }
        public List<int> Unique { get; set; }
        public int Version { get; set; }

        public Layer ParseLayerFromFile(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine()?.Trim();
                    string[] worlds = line?.Split(' ');
                    switch (worlds?[0])
                    {
                        case "Version":
                            SetVersion(line);
                            break;
                        case "Charset":
                            SetCharset(line);
                            break;
                        case "Delimiter":
                            SetDelimiter(line);
                            break;
                        case "Unique":
                            SetUnique(line);
                            break;
                        case "Index":
                            SetIndex(line);
                            break;
                        case "Transform":
                            SetTransform(line);
                            break;
                        case "Columns":
                            SetColumns(stream, line);
                            break;
                        case "Data":
                            SetData(stream);
                            break;
                    }
                }
            }

            var layerName = Path.GetFileNameWithoutExtension(filePath);
            var layer = new Layer(layerName);

            foreach (var mapObject in Data)
            {
                layer.Add(mapObject);
            }

            return layer;
        }

        private static Color IntToColor(int dec)
        {
            var red = (byte)((dec >> 16) & 0xff);
            var green = (byte)((dec >> 8) & 0xff);
            var blue = (byte)(dec & 0xff);
            return Color.FromArgb(red, green, blue);
        }

        private static void SetSymbol(string[] line, ref Point point)
        {
            var fontFamily = "MapInfo Symbols";
            var symbolByte = Convert.ToInt32(line[0].Substring(1, line[0].Length - 1)) + 1;
            var color = IntToColor(Convert.ToInt32(line[1]));
            var symbolSize = Convert.ToInt32(line[2].Substring(0, line[2].Length - 1));
            var style = new Style
            {
                Brush = new SolidBrush(color),
                Symbol = new Symbol
                {
                    Font = new Font(fontFamily, symbolSize),
                    Char = (char)symbolByte
                }
            };
            point.Style = style;
        }

        private void AddLine(StreamReader sr, ref string tmp, ref string[] line)
        {
            // Координаты начала и конца линии
            double x1 = double.Parse(line[1], CultureInfo.InvariantCulture);
            double y1 = double.Parse(line[2], CultureInfo.InvariantCulture);
            double x2 = double.Parse(line[3], CultureInfo.InvariantCulture);
            double y2 = double.Parse(line[4], CultureInfo.InvariantCulture);
            var newline = new Line(x1, y1, x2, y2);
            // Pen
            if (line.Length == 5)
            {
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }

                line = tmp?.Split(' ');
                if (line?[0] == "Pen")
                {
                    tmp = tmp?.Replace(" ", string.Empty).Replace(line[0], string.Empty);
                    line = tmp?.Split(',');
                    var style = new Style
                    {
                        Pen = GetPen(line[0].Substring(1, line[0].Length - 1), line[1], line[2].Substring(0, line[2].Length - 1))
                    };
                    newline.Style = style;
                    if (!sr.EndOfStream)
                    {
                        tmp = sr.ReadLine()?.Trim();
                    }
                }
            }
            else if (line[5] == "Pen")
            {
                tmp = tmp.Replace(" ", string.Empty).Replace(line[0] + line[1] + line[2] + line[3] + line[4] + line[5], string.Empty);
                line = tmp.Split(',');
                var style = new Style
                {
                    Pen = GetPen(line[0].Substring(1, line[0].Length - 1), line[1], line[2].Substring(0, line[2].Length - 1))
                };
                newline.Style = style;
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }
            }

            Data.Add(newline);
        }

        private void AddPline(StreamReader sr, ref string tmp, ref string[] line)
        {
            // Количество узлов
            int n;
            try
            {
                n = Convert.ToInt32(line[1]);
            }
            catch
            {
                n = Convert.ToInt32(sr.ReadLine()?.Trim());
            }

            // Вершины полилинии
            var polyline = new Polyline();
            for (int i = 0; i < n; ++i)
            {
                line = sr.ReadLine()?.Trim().Split(' ');
                if (line != null)
                {
                    double x = double.Parse(line[0], CultureInfo.InvariantCulture);
                    double y = double.Parse(line[1], CultureInfo.InvariantCulture);
                    polyline.AddNode(x, y);
                }
            }

            // Pen
            if (!sr.EndOfStream)
            {
                tmp = sr.ReadLine()?.Trim();
            }

            line = tmp?.Split(' ');
            if (line?[0] == "Pen")
            {
                tmp = tmp?.Replace(" ", string.Empty).Replace(line[0], string.Empty);
                line = tmp?.Split(',');
                var style = new Style
                {
                    Pen = GetPen(line[0].Substring(1, line[0].Length - 1), line[1], line[2].Substring(0, line[2].Length - 1))
                };
                polyline.Style = style;
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }
            }

            Data.Add(polyline);
        }

        private void AddPoint(StreamReader sr, ref string tmp, ref string[] line)
        {
            // Координаты точки
            var x = double.Parse(line[1], CultureInfo.InvariantCulture);
            var y = double.Parse(line[2], CultureInfo.InvariantCulture);
            var point = new Point(x, y);
            // Чтение символа
            if (line.Length == 3)
            {
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }

                line = tmp?.Split(' ');
                if (line?[0] == "Symbol")
                {
                    tmp = tmp?.Replace(" ", string.Empty).Replace(line[0], string.Empty);
                    line = tmp?.Split(',');
                    SetSymbol(line, ref point);
                    if (!sr.EndOfStream)
                    {
                        tmp = sr.ReadLine()?.Trim();
                    }
                }
            }
            else if (line[3] == "Symbol")
            {
                tmp = tmp.Replace(" ", string.Empty).Replace(line[0] + line[1] + line[2] + line[3], string.Empty);
                line = tmp.Split(',');
                SetSymbol(line, ref point);
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }
            }

            Data.Add(point);
        }

        private void AddRegion(StreamReader sr, ref string tmp, ref string[] line)
        {
            // Количество регионов
            tmp = tmp.Replace(" ", string.Empty).Replace(line[0], string.Empty);
            var regionsNumb = Convert.ToInt32(tmp);
            // Вершины полигонов
            var polygonsList = new List<Polygon>();
            for (int i = 0; i < regionsNumb; ++i)
            {
                line = sr.ReadLine()?.Trim().Split(' ');
                int pointsNumb = Convert.ToInt32(line?[0]);
                var polygon = new Polygon();
                for (int j = 0; j < pointsNumb; ++j)
                {
                    line = sr.ReadLine()?.Split(' ');
                    if (line == null)
                    {
                        continue;
                    }

                    double x = double.Parse(line[0], CultureInfo.InvariantCulture);
                    double y = double.Parse(line[1], CultureInfo.InvariantCulture);
                    polygon.AddNode(x, y);
                }

                // Добавление полигонов во временный список
                polygonsList.Add(polygon);
            }

            // Pen
            if (!sr.EndOfStream)
            {
                tmp = sr.ReadLine()?.Trim();
            }

            line = tmp?.Split(' ');
            var style = new Style
            {
                Pen = new Pen(Color.Black),
                Brush = new SolidBrush(Color.Black)
            };
            var hasOwnStyle = false;
            if (line?[0] == "Pen")
            {
                tmp = tmp?.Replace(" ", string.Empty).Replace(line[0], string.Empty);
                line = tmp?.Split(',');

                style.Pen = GetPen(line?[0].Substring(1, line[0].Length - 1), line?[1], line?[2].Substring(0, line[2].Length - 1));
                hasOwnStyle = true;

                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }
            }

            line = tmp?.Split(' ');
            if (line?[0] == "Brush")
            {
                tmp = tmp?.Replace(" ", string.Empty).Replace(line[0], string.Empty);
                line = tmp?.Split(',');

                if (line != null && line.Length == 3)
                {
                    style.Brush = GetBrush(line[0].Substring(1, line[0].Length - 1), line[1], line[2].Substring(0, line[2].Length - 1));
                }

                if (line != null && line.Length == 2)
                {
                    style.Brush = GetBrush(line[0].Substring(1, line[0].Length - 1), line[1].Substring(0, line[1].Length - 1));
                }

                hasOwnStyle = true;
                if (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine()?.Trim();
                }
            }

            // Добавление полигонов в основной список и задание стилей
            foreach (var polygon in polygonsList)
            {
                if (hasOwnStyle)
                {
                    polygon.Style = style;
                }

                Data.Add(polygon);
            }
        }

        private Brush GetBrush(string p, string fc, string bc = "0")
        {
            var foreColor = IntToColor(Convert.ToInt32(fc));
            var backColor = IntToColor(Convert.ToInt32(bc));
            var pattern = Convert.ToInt32(p);
            Brush brush;
            switch (pattern)
            {
                case 1:
                    brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
                    break;
                case 2:
                    brush = new SolidBrush(foreColor);
                    break;
                case 3:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                    brush = new HatchBrush(HatchStyle.Horizontal, foreColor, backColor);
                    break;
                case 4:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    brush = new HatchBrush(HatchStyle.Vertical, foreColor, backColor);
                    break;
                case 5:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                    brush = new HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor);
                    break;
                case 6:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                    brush = new HatchBrush(HatchStyle.ForwardDiagonal, foreColor, backColor);
                    break;
                case 7:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                    brush = new HatchBrush(HatchStyle.Cross, foreColor, backColor);
                    break;
                case 8:
                case 44:
                case 45:
                case 46:
                case 47:
                    brush = new HatchBrush(HatchStyle.DiagonalCross, foreColor, backColor);
                    break;
                case 12:
                case 13:
                case 14:
                case 15:
                    brush = new HatchBrush(HatchStyle.Percent90, foreColor, backColor);
                    break;
                case 16:
                case 17:
                case 18:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                    brush = new HatchBrush(HatchStyle.Percent10, foreColor, backColor);
                    break;
                default:
                    brush = new HatchBrush(HatchStyle.Cross, foreColor, backColor);
                    break;
            }

            return brush;
        }

        private Pen GetPen(string w, string p, string c)
        {
            var width = float.Parse(w);
            var pattern = int.Parse(p);
            var color = IntToColor(Convert.ToInt32(c));
            var pen = new Pen(color, width);
            switch (pattern)
            {
                case 1:
                    pen.Color = Color.FromArgb(0, color);
                    break;
                case 2:
                    pen.DashStyle = DashStyle.Solid;
                    break;
                case 3:
                case 4:
                case 5:
                case 10:
                    pen.DashStyle = DashStyle.Dot;
                    break;
                case 6:
                case 9:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 2, 1 };
                    break;
                case 7:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 3, 1 };
                    break;
                case 8:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 4, 1 };
                    break;
                case 11:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 1, 2 };
                    break;
                case 12:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 3, 3 };
                    break;
                case 13:
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 5, 5 };
                    break;
                case 14:
                    pen.DashStyle = DashStyle.DashDot;
                    pen.DashPattern = new float[] { 4, 2, 1, 2 };
                    break;
                case 15:
                    pen.DashStyle = DashStyle.DashDot;
                    pen.DashPattern = new float[] { 6, 2, 1, 2 };
                    break;
                case 16:
                    pen.DashStyle = DashStyle.DashDot;
                    pen.DashPattern = new float[] { 7, 2, 2, 2 };
                    break;
                case 17:
                    pen.DashStyle = DashStyle.DashDot;
                    pen.DashPattern = new float[] { 8, 2, 2, 2 };
                    break;
                case 18:
                case 19:
                    pen.DashStyle = DashStyle.DashDotDot;
                    pen.DashPattern = new float[] { 8, 2, 2, 2, 2, 2 };
                    break;
                case 20:
                    pen.DashStyle = DashStyle.DashDotDot;
                    pen.DashPattern = new float[] { 4, 2, 1, 2, 1, 2 };
                    break;
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                    pen.DashStyle = DashStyle.DashDotDot;
                    pen.DashPattern = new float[] { 6, 2, 1, 2, 1, 2 };
                    break;
                default:
                    pen.DashStyle = DashStyle.Dot;
                    break;
            }

            return pen;
        }

        private void SetCharset(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Charset", string.Empty);
            Charset = tmp.Substring(1, tmp.Length - 2);
        }

        private void SetColumns(StreamReader sr, string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Columns", string.Empty);
            ColumnsN = Convert.ToInt32(tmp);
            Columns = new List<Column>(ColumnsN);
            for (int i = 0; i < ColumnsN; ++i)
            {
                string[] lineC = sr.ReadLine()?.Trim().Split(' ');
                Columns.Add(new Column(lineC?[0], tmp.Replace($"{lineC?[0]} ", string.Empty)));
            }
        }

        private void SetData(StreamReader sr)
        {
            var tmp = sr.ReadLine()?.Trim(' ');
            while (!sr.EndOfStream)
            {
                string[] line = tmp?.Split(' ');
                switch (line?[0])
                {
                    case "Point":
                        AddPoint(sr, ref tmp, ref line);
                        break;
                    case "Line":
                        AddLine(sr, ref tmp, ref line);
                        break;
                    case "Pline":
                        AddPline(sr, ref tmp, ref line);
                        break;
                    case "Region":
                        AddRegion(sr, ref tmp, ref line);
                        break;
                    default:
                        if (!sr.EndOfStream)
                        {
                            tmp = sr.ReadLine()?.Trim();
                        }

                        break;
                }
            }
        }

        private void SetDelimiter(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Delimiter", string.Empty);
            Delimiter = tmp.Substring(1, tmp.Length - 2)[0];
        }

        private void SetIndex(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Index", string.Empty);
            string[] indexStr = tmp.Split(',');
            foreach (var number in indexStr)
            {
                Index.Add(Convert.ToInt32(number));
            }
        }

        private void SetTransform(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Transform", string.Empty);
            string[] transformStr = tmp.Split(',');
            foreach (var number in transformStr)
            {
                Transform.Add(Convert.ToInt32(number));
            }
        }

        private void SetUnique(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Unique", string.Empty);
            string[] uniqueStr = tmp.Split(',');
            for (int i = 0; i < uniqueStr.Length; ++i)
            {
                Unique[i] = Convert.ToInt32(uniqueStr[i]);
            }
        }

        private void SetVersion(string tmp)
        {
            tmp = tmp.Replace(" ", string.Empty).Replace("Version", string.Empty);
            Version = Convert.ToInt32(tmp);
        }

        public class Column
        {
            public Column(string name, string type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; set; }
            public string Type { get; set; }
        }
    }
}
