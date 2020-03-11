using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Searching;

namespace MiniGis
{
    public partial class CreateRegularGridForm : Form
    {
        public CreateRegularGridForm(IEnumerable<IrregularGrid> grids, IrregularGrid selectedGrid)
        {
            InitializeComponent();
            SetIrregularGrids(grids);
            SelectedIrregularGrid = selectedGrid;
        }

        public RegularGrid RegularGrid { get; private set; }

        private IrregularGrid SelectedIrregularGrid
        {
            get => IrregularGridsComboBox.SelectedItem as IrregularGrid;
            set => IrregularGridsComboBox.SelectedItem = value;
        }

        private void CancelDialogButton_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void CountRadioButton_CheckedChanged(object sender, EventArgs e) => SetCalcMethodEnabling(ValueCalculating.ByNodesCount);

        private void IrregularGridsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIrregularGrid == null)
            {
                return;
            }

            SetIrregularGridDescription(SelectedIrregularGrid);
            SetRegularGridName(SelectedIrregularGrid.Name);
            SetGeometry(SelectedIrregularGrid);
            SetRadius(SelectedIrregularGrid);
            SetCount(SelectedIrregularGrid);
            SetPow(2);
        }

        private async void OkButton_Click(object sender, EventArgs e)
        {
            var errors = ValidateForm();
            if (errors.Any())
            {
                var message = errors.Aggregate((mes, str) => $"{mes}\r\n{str}");
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var irregularGrid = SelectedIrregularGrid;
            var step = (double)StepNumericUpDown.Value;
            var position = new Node<double>((double)XCoordinateNumericUpDown.Value, (double)YCoordinateNumericUpDown.Value);
            var rowCount = RegularGridFactory.GetRowCount(SelectedIrregularGrid, step);
            var columnCount = RegularGridFactory.GetColumnCount(SelectedIrregularGrid, step);
            var delta = RadiusRadioButton.Checked ? (double)RadiusNumericUpDown.Value :
                CountRadioButton.Checked ? (double)CountNumericUpDown.Value :
                throw new ArgumentOutOfRangeException();
            var pow = (int)PowNumericUpDown.Value;
            var calcType = RadiusRadioButton.Checked ? ValueCalculating.ByRadius :
                CountRadioButton.Checked ? ValueCalculating.ByNodesCount :
                throw new ArgumentOutOfRangeException();
            var name = NameTextBox.Text;

            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            var progress = new CreateRegularGridProgressForm(cancellationTokenSource);
            progress.Show();
            Enabled = false;

            RegularGrid = await Task.Run(() => RegularGridFactory.Create(irregularGrid, step, position, rowCount, columnCount, delta, pow, calcType, token), token);
            RegularGrid.Name = name;

            progress.Close();
            DialogResult = token.IsCancellationRequested ? DialogResult.Cancel : DialogResult.OK;
        }

        private void RadiusRadioButton_CheckedChanged(object sender, EventArgs e) => SetCalcMethodEnabling(ValueCalculating.ByRadius);

        private void SetCalcMethodEnabling(ValueCalculating calcType)
        {
            RadiusLabel.Enabled = calcType == ValueCalculating.ByRadius;
            RadiusNumericUpDown.Enabled = calcType == ValueCalculating.ByRadius;
            CountLabel.Enabled = calcType == ValueCalculating.ByNodesCount;
            CountNumericUpDown.Enabled = calcType == ValueCalculating.ByNodesCount;
        }

        private void SetCount(IrregularGrid grid) =>
            CountNumericUpDown.Value = (decimal)RegularGridFactory.GetDelta(grid, ValueCalculating.ByNodesCount);

        private void SetGeometry(IrregularGrid grid)
        {
            XCoordinateNumericUpDown.Value = (decimal)grid.Bounds.XMin;
            YCoordinateNumericUpDown.Value = (decimal)grid.Bounds.YMax;
            var width = grid.Bounds.XMax - grid.Bounds.XMin;
            var height = grid.Bounds.YMax - grid.Bounds.YMin;
            WidthNumericUpDown.Value = (decimal)width;
            HeightNumericUpDown.Value = (decimal)height;
            StepNumericUpDown.Value = (decimal)(Math.Max(width, height) / 100);
        }

        private void SetIrregularGridDescription(IrregularGrid grid)
        {
            IrregularGridTextBox.Text = $@"
Name: {grid.Name};
Nodes count: {grid.Nodes.Count()};
Bounds: [x: {grid.Bounds.XMin:0.00}, y: {grid.Bounds.YMax:0.00}], [x: {grid.Bounds.XMax:0.00}, y: {grid.Bounds.YMin:0.00}];
Width: {grid.Bounds.XMax - grid.Bounds.XMin:0.00}, Height: {grid.Bounds.YMax - grid.Bounds.YMin:0.00};
Min value: {grid.MinValue:0.00}, Max value: {grid.MaxValue:0.00}.
";
        }

        private void SetIrregularGrids(IEnumerable<IrregularGrid> grids) =>
            IrregularGridsComboBox.Items.AddRange(grids.Cast<object>().ToArray());

        private void SetPow(int pow) => PowNumericUpDown.Value = pow;

        private void SetRadius(IrregularGrid grid) =>
            RadiusNumericUpDown.Value = (decimal)RegularGridFactory.GetDelta(grid, ValueCalculating.ByRadius);

        private void SetRegularGridName(string irregularGridName) =>
            NameTextBox.Text = $"{irregularGridName} (regular)";

        private void SetResult(IrregularGrid grid, double step) =>
            ResultGridLabel.Text = $"Result grid: {RegularGridFactory.GetRowCount(grid, step)}x{RegularGridFactory.GetColumnCount(grid, step)}";

        private void StepNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedIrregularGrid == null)
            {
                return;
            }

            SetResult(SelectedIrregularGrid, (double)StepNumericUpDown.Value);
        }

        private IEnumerable<string> ValidateForm()
        {
            if (SelectedIrregularGrid == null)
            {
                yield return "Irregular grid not selected.";
            }

            if (!RadiusRadioButton.Checked && !CountRadioButton.Checked)
            {
                yield return "Count method not selected.";
            }

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                yield return "Name empty.";
            }
        }
    }
}
