using System.ComponentModel;

namespace MiniGis
{
    partial class CreateRegularGridProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CalcProgressBar = new System.Windows.Forms.ProgressBar();
            this.CalcTimer = new System.Windows.Forms.Timer(this.components);
            this.CancelCalcButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // CalcProgressBar
            //
            this.CalcProgressBar.Location = new System.Drawing.Point(3, 1);
            this.CalcProgressBar.Name = "CalcProgressBar";
            this.CalcProgressBar.Size = new System.Drawing.Size(238, 23);
            this.CalcProgressBar.Step = 1;
            this.CalcProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.CalcProgressBar.TabIndex = 0;
            //
            // CalcTimer
            //
            this.CalcTimer.Interval = 1000;
            this.CalcTimer.Tick += new System.EventHandler(this.CalcTimer_Tick);
            //
            // CancelCalcButton
            //
            this.CancelCalcButton.Location = new System.Drawing.Point(247, 1);
            this.CancelCalcButton.Name = "CancelCalcButton";
            this.CancelCalcButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCalcButton.TabIndex = 1;
            this.CancelCalcButton.Text = "Cancel";
            this.CancelCalcButton.UseVisualStyleBackColor = true;
            this.CancelCalcButton.Click += new System.EventHandler(this.CancelCalcButton_Click);
            //
            // CreateRegularGridProgressForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 27);
            this.ControlBox = false;
            this.Controls.Add(this.CancelCalcButton);
            this.Controls.Add(this.CalcProgressBar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(341, 66);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(341, 66);
            this.Name = "CreateRegularGridProgressForm";
            this.Text = "Creating regular grid...";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ProgressBar CalcProgressBar;
        private System.Windows.Forms.Timer CalcTimer;
        private System.Windows.Forms.Button CancelCalcButton;
    }
}

