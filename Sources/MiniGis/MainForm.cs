using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TwoDimensionalFields.Grids;
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
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|MIF files (*.mif)|*.mif";
            openFileDialog.Multiselect = true;
        }

        private void AddLayersFromFiles(object sender, EventArgs e)
        {
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
            var irregularGrids = mapControl.Layers.OfType<IrregularGrid>();
            if (!irregularGrids.Any())
            {
                MessageBox.Show("Cannot find any irregular grids.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIrregularGrid = layersControl.GetSelectedLayers().OfType<IrregularGrid>().FirstOrDefault();
            var createRegularGridDialog = new CreateRegularGridForm(irregularGrids, selectedIrregularGrid);

            if (createRegularGridDialog.ShowDialog() == DialogResult.OK)
            {
                mapControl.AddLayer(createRegularGridDialog.RegularGrid);
                layersControl.UpdateLayers();
            }
        }

        private void ButtonPan_Click(object sender, EventArgs e) => SetActiveTool(MapToolType.Pan);
        private void ButtonSelect_Click(object sender, EventArgs e) => SetActiveTool(MapToolType.Select);

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

        private void ButtonZoomIn_Click(object sender, EventArgs e) => SetActiveTool(MapToolType.ZoomIn);
        private void ButtonZoomOut_Click(object sender, EventArgs e) => SetActiveTool(MapToolType.ZoomOut);

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
            mapControl.Layers.AddRange(TestHelper.CreateTestGrids());
            mapControl.Layers.AddRange(TestHelper.CreateTestLayers());

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

        private void SetActiveTool(MapToolType mapToolType)
        {
            mapControl.ActiveTool = mapToolType;
            ButtonSelect.Checked = mapToolType == MapToolType.Select;
            ButtonPan.Checked = mapToolType == MapToolType.Pan;
            ButtonZoomIn.Checked = mapToolType == MapToolType.ZoomIn;
            ButtonZoomOut.Checked = mapToolType == MapToolType.ZoomOut;
        }
    }
}
