using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using TwoDimensionalFields.Searching;
using Point = TwoDimensionalFields.MapObjects.Point;

namespace MiniGis
{
    public class TestHelper
    {
        public static IEnumerable<Grid> CreateTestGrids()
        {
            var nodes = new[]
            {
                new Node3d<double>(10, 30, 1),
                new Node3d<double>(20, 40, 2),
                new Node3d<double>(20, 30, 3),
                new Node3d<double>(50, 10, 4),
                new Node3d<double>(30, 50, 5),
            };

            var grid1 = new IrregularGrid(nodes);
            var grid2 = RegularGridFactory.Create(grid1, 2, ValueCalculating.ByNodesCount);
            var grid3 = RegularGridFactory.CreateTestGrid();

            grid1.Name = "Нерегулярная сеть";
            grid2.Name = "Расчитанная регулярная сеть";
            grid3.Name = "Тестовая сеть";

            return new Grid[] { grid3, grid2, grid1 };
        }

        public static IEnumerable<Layer> CreateTestLayers()
        {
            var lineStyle = new Style
            {
                Pen = new Pen(Color.Red, 2)
                {
                    DashStyle = DashStyle.Dash
                }
            };

            var polygonStyle = new Style
            {
                Brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Red, Color.Green)
            };

            var point = new Point(0, 500);

            var line1 = new Line(0, -50, 0, 50) { Style = lineStyle };

            var line2 = new Line(-50, 0, 50, 0) { Style = lineStyle };

            var polyline = new Polyline();
            polyline.AddNode(0, 0);
            polyline.AddNode(200, 100);
            polyline.AddNode(200, -100);

            var polygon1 = new Polygon { Style = polygonStyle };
            polygon1.AddNode(0, 0);
            polygon1.AddNode(-200, 100);
            polygon1.AddNode(-200, -100);

            var polygon2 = new Polygon { Style = polygonStyle };
            polygon2.AddNode(0, 0.111);
            polygon2.AddNode(0.55, 0.99);
            polygon2.AddNode(0.8, 0.7);

            var polygons = new List<Polygon>(10000);
            for (int x = 0, b =-349; x < 100; x++, b += 7)
            {
                for (int j = 0, a = -349; j < 100; j++, a += 7)
                {
                    Polygon polygon = new Polygon();
                    polygon.AddNode(a, b);
                    polygon.AddNode(a + 5, b);
                    polygon.AddNode(a + 5, b + 5);
                    polygon.AddNode(a, b + 5);
                    polygons.Add(polygon);
                }
            }

            var layer1 = new Layer("Test1");
            layer1.Add(polygon1);
            layer1.Add(polygon2);
            polygons.ForEach(polygon => layer1.Add(polygon));

            var layer2 = new Layer("Test2");
            layer2.Add(line1);
            layer2.Add(line2);
            layer2.Add(polyline);

            var layer3 = new Layer("Test3");
            layer3.Add(point);

            return new Layer[] { layer1, layer2, layer3 };
        }
    }
}
