using System;
using System.Threading;
using System.Windows.Forms;

namespace MiniGis
{
    public partial class CreateRegularGridProgressForm : Form
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly DateTimeOffset start;

        public CreateRegularGridProgressForm(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
            InitializeComponent();
            start = DateTimeOffset.Now;
            CalcTimer.Start();
        }

        private void CalcTimer_Tick(object sender, EventArgs e) =>
            Text = $"Creating regular grid... ({DateTimeOffset.Now - start:hh\\:mm\\:ss})";

        private void CancelCalcButton_Click(object sender, EventArgs e) => cancellationTokenSource.Cancel();
    }
}
