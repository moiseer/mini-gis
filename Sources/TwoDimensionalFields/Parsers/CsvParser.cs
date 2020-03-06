using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Parsers
{
    public class CsvParser
    {
        public char[] Separators { get; set; }

        public CsvParser()
        {
            Separators = new[] { ' ', '\t', ';' };
        }

        public IrregularGrid ParseIrregularGridFromFile(string filePath)
        {
            var nodes = new List<Node3d<double>>();

            using (var stream = new StreamReader(filePath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine().Trim();
                    var node = ParseNode3d(line);
                    if (node != null)
                    {
                        nodes.Add(node);
                    }
                }
            }

            var gridName = Path.GetFileNameWithoutExtension(filePath);
            return new IrregularGrid(nodes) { Name = gridName };
        }

        private Node3d<double> ParseNode3d(string line)
        {
            string[] worlds = line.Replace(',', '.').Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (worlds.Length != 3)
            {
                return null;
            }

            if (!double.TryParse(worlds[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double x) ||
                !double.TryParse(worlds[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double y) ||
                !double.TryParse(worlds[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double z))
            {
                return null;
            }

            return new Node3d<double>(x, y, z);
        }
    }
}
