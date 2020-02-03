using System;
using System.Linq;
using System.Windows.Forms;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using TwoDimensionalFields.Parsers;

namespace MiniGis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ButtonSelect.Checked = true;
            toolStripStatusLabelMouse.Text = "";
            toolStripStatusLabelArea.Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var layer1 = new Layer("Test1");
            var layer2 = new Layer("Test2");
            var layer3 = new Layer("Test3");

            var point = new Point(0, 500);
            var line1 = new Line(0, -50,0, 50);
            var line2 = new Line(-50, 0, 50, 0);
            // line1.Pen = new Pen(Color.Red, 2);
            // line1.Pen.DashStyle = DashStyle.Dash;
            // line1.UseOwnStyle = true;
            // line2.Pen = new Pen(Color.Red, 2);
            // line2.Pen.DashStyle = DashStyle.Dash;
            // line2.UseOwnStyle = true;

            var polyline = new Polyline();
            polyline.AddNode(0, 0);
            polyline.AddNode(200, 100);
            polyline.AddNode(200, -100);

            var polygon1 = new Polygon();
            polygon1.AddNode(0, 0);
            polygon1.AddNode(-200, 100);
            polygon1.AddNode(-200, -100);
            // polygon1.Brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Red, Color.Green);
            // polygon1.UseOwnStyle = true;
            layer1.Add(polygon1);

            var polygon2 = new Polygon();
            polygon2.AddNode(0, 0.111);
            polygon2.AddNode(0.55, 0.99);
            polygon2.AddNode(0.8, 0.7);
            // polygon2.Brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Red, Color.Green);
            // polygon2.UseOwnStyle = true;
            layer1.Add(polygon2);

            /*point = new Point(0, 0);
            point.Brush = new SolidBrush(Color.Black);

            layer.AddObject(polyline);
            layer.AddObject(polygon);
            layer.AddObject(line1);
            layer.AddObject(line2);
            layer.AddObject(point);
            Random rnd = new Random();
            for (int x = 0; x < 1000; x++)
            {
            Point point = new Point(rnd.Next(-300,300), rnd.Next(-300, 300));
            point.Brush = new SolidBrush(Color.Black);
            layer.AddObject(point);
            }*/

            //int b = -349;
            //for (int x = 0; x < 100; x++)
            //{
            //    int a = -349;
            //    for (int j = 0; j<100;j++)
            //    {
            //        Polygon polygon = new Polygon();
            //        polygon.AddNode(a, b);
            //        polygon.AddNode(a+5, b);
            //        polygon.AddNode(a+5, b+5);
            //        polygon.AddNode(a, b+5);
            //        layer1.AddObject(polygon);
            //        a += 7;
            //    }
            //    b += 7;
            //}
            layer2.Add(line1);
            layer2.Add(line2);
            layer2.Add(polyline);
            layer3.Add(point);

            mapControl.AddLayer(layer1);
            mapControl.AddLayer(layer2);
            mapControl.AddLayer(layer3);
            
            layersControl.MapControl = mapControl;
            layersControl.AddLayer += AddLayersFromFiles;
            layersControl.UpdateLayers();
        }

        private void AddLayersFromFiles(object sender, EventArgs e)
        {
            openFileDialog.Filter = @"MIF|*.mif";
            openFileDialog.Multiselect = true;
            
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            var filePathName = openFileDialog.FileNames;
            var filename = openFileDialog.SafeFileNames;
            for (int i = 0; i < filePathName.Length; ++i)
            {
                var extIndex = filename[i].IndexOf('.');
                var layerName = filename[i].Substring(0, extIndex);
                var layer = new Layer(layerName);
                var parser = new MifParser(filePathName[i]);
                foreach (var mapObject in parser.Data)
                {
                    layer.Add(mapObject);
                }
                mapControl.AddLayer(layer);
                layersControl.UpdateLayers();
            }
        }

        private void ButtonZoomIn_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.ZoomIn;
            ButtonSelect.Checked = false;
            ButtonPan.Checked = false;
            ButtonZoomIn.Checked = true;
            ButtonZoomOut.Checked = false;
        }

        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.ZoomOut;
            ButtonSelect.Checked = false;
            ButtonPan.Checked = false;
            ButtonZoomIn.Checked = false;
            ButtonZoomOut.Checked = true;
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.Select;
            ButtonSelect.Checked = true;
            ButtonPan.Checked = false;
            ButtonZoomIn.Checked = false;
            ButtonZoomOut.Checked = false;
        }

        private void ButtonPan_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.Pan;
            ButtonSelect.Checked = false;
            ButtonPan.Checked = true;
            ButtonZoomIn.Checked = false;
            ButtonZoomOut.Checked = false;
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            var (x, y) = mapControl.ScreenToMap(e.Location);
            toolStripStatusLabelMouse.Text = $@"x={Math.Round(x, 4)}; y={Math.Round(y, 4)}";
        }

        private void ButtonZoomAll_Click(object sender, EventArgs e)
        {
            if (layersControl.SelectedItemsCount == 0)
                mapControl.ZoomAll();
            else
                mapControl.ZoomLayers(layersControl.GetSelectedLayers());
        }

        private void map_MouseUp(object sender, MouseEventArgs e)
        {
            toolStripStatusLabelArea.Text = string.Empty;
            
            if (mapControl.SelectedObjects.Count != 1)
            {
                return;
            }

            if (!(mapControl.SelectedObjects.First() is Polygon polygon))
            {
                return;
            }
            
            toolStripStatusLabelArea.Text += @"Polygon's Area = ";
            toolStripStatusLabelArea.Text += Math.Round(polygon.Area(), 4);
        }
    }
}
