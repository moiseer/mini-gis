using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Parsers;

namespace MiniGis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ButtonSelect.Checked = true;
            toolStripStatusLabelMouse.Text = string.Empty;
            toolStripStatusLabelArea.Text = string.Empty;
            toolStripStatusLabelValue.Text = string.Empty;
        }

        private void AddLayersFromFiles(object sender, EventArgs e)
        {
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|MIF files (*.mif)|*.mif";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var fileName in openFileDialog.FileNames.Reverse())
            {
                var extension = Path.GetExtension(fileName);

                switch (extension)
                {
                    case ".mif":
                    {
                        var layer = new MifParser().ParseLayerFromFile(fileName);
                        mapControl.AddLayer(layer);
                        break;
                    }
                    case ".csv":
                    {
                        var grid = new CsvParser().ParseIrregularGridFromFile(fileName);
                        mapControl.AddLayer(grid);
                        break;
                    }
                    default:
                        throw new FileLoadException($"Unknown extension: {extension}");
                }
            }

            layersControl.UpdateLayers();
        }

        private void ButtonCalcRegular_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonPan_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.Pan;
            ButtonSelect.Checked = false;
            ButtonPan.Checked = true;
            ButtonZoomIn.Checked = false;
            ButtonZoomOut.Checked = false;
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            mapControl.ActiveTool = MapToolType.Select;
            ButtonSelect.Checked = true;
            ButtonPan.Checked = false;
            ButtonZoomIn.Checked = false;
            ButtonZoomOut.Checked = false;
        }

        private void ButtonZoomAll_Click(object sender, EventArgs e)
        {
            if (layersControl.SelectedItemsCount == 0)
            {
                mapControl.ZoomAll();
            }
            else
            {
                mapControl.ZoomLayers(layersControl.GetSelectedLayers());
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

        private void DisplayGridValue()
        {
            toolStripStatusLabelValue.Text = string.Empty;

            if (!mapControl.SelectedValues.Any())
            {
                return;
            }

            foreach (var gridValue in mapControl.SelectedValues)
            {
                toolStripStatusLabelValue.Text += $"\"{gridValue.Key.Name}\": {Math.Round(gridValue.Value, 4)}";
            }
        }

        private void DisplayMousePosition(MouseEventArgs mouse)
        {
            (double x, double y) = mapControl.ScreenToMap(mouse.Location);
            toolStripStatusLabelMouse.Text = $"x = {Math.Round(x, 4)}; y = {Math.Round(y, 4)}";
        }

        private void DisplayPolygonArea()
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

            toolStripStatusLabelArea.Text += $"Polygon's Area = {Math.Round(polygon.Area(), 4)}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // mapControl.Layers.AddRange(TestHelper.CreateTestGrids());
            // mapControl.Layers.AddRange(TestHelper.CreateTestLayers());

            layersControl.MapControl = mapControl;
            layersControl.AddLayer += AddLayersFromFiles;
            layersControl.UpdateLayers();
        }

        private void map_MouseMove(object sender, MouseEventArgs e) => DisplayMousePosition(e);

        private void map_MouseUp(object sender, MouseEventArgs e)
        {
            DisplayPolygonArea();
            DisplayGridValue();
        }
    }
}
