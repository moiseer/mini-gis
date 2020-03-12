using System;
using System.Threading;
using System.Windows.Forms;
using TwoDimensionalFields.Grids;

namespace MiniGis
{
    public partial class CreateRegularGridProgressForm : Form
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly DateTimeOffset start;
        public CreateRegularGridProgressForm(CancellationTokenSource cancellationTokenSource, Progress helper = null)
        {
            this.cancellationTokenSource = cancellationTokenSource;
            InitializeComponent();

            if (helper != null)
            {
                CalcProgressBar.Maximum = helper.MaxValue;
                CalcProgressBar.Style = ProgressBarStyle.Continuous;
                helper.ProgressChanged += SetProgress;
            }

            start = DateTimeOffset.Now;
            CalcTimer.Start();
        }

        private void CalcTimer_Tick(object sender, EventArgs e) =>
            Text = $"Creating regular grid... ({DateTimeOffset.Now - start:hh\\:mm\\:ss})";

        private void CancelCalcButton_Click(object sender, EventArgs e) => cancellationTokenSource.Cancel();

        private void SetProgress(int progress)
        {
            if(!InvokeRequired)
            {
                CalcProgressBar.Value = progress;
            }
            else
            {
                Invoke(new Action<int>(SetProgress), progress);
            }
        }
    }
}
