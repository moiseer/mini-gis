using System;
using System.ComponentModel;

namespace MiniGis
{
    partial class CreateRegularGridForm
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
            this.IrregularGridsGroupBox = new System.Windows.Forms.GroupBox();
            this.IrregularGridTextBox = new System.Windows.Forms.TextBox();
            this.IrregularGridsComboBox = new System.Windows.Forms.ComboBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelDialogButton = new System.Windows.Forms.Button();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.NameGroupBox = new System.Windows.Forms.GroupBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.GeometryGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultGridLabel = new System.Windows.Forms.Label();
            this.StepLabel = new System.Windows.Forms.Label();
            this.StepNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SizeGroupBox = new System.Windows.Forms.GroupBox();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.TopLeftCoordinatesGroupBox = new System.Windows.Forms.GroupBox();
            this.YCoordinateLabel = new System.Windows.Forms.Label();
            this.YCoordinateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.XCoordinateLabel = new System.Windows.Forms.Label();
            this.XCoordinateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CalcMethodGroupBox = new System.Windows.Forms.GroupBox();
            this.PowLabel = new System.Windows.Forms.Label();
            this.PowNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CountLabel = new System.Windows.Forms.Label();
            this.CountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.RadiusNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CountRadioButton = new System.Windows.Forms.RadioButton();
            this.RadiusRadioButton = new System.Windows.Forms.RadioButton();
            this.IrregularGridsGroupBox.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            this.NameGroupBox.SuspendLayout();
            this.GeometryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNumericUpDown)).BeginInit();
            this.SizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
            this.TopLeftCoordinatesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YCoordinateNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCoordinateNumericUpDown)).BeginInit();
            this.CalcMethodGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // IrregularGridsGroupBox
            // 
            this.IrregularGridsGroupBox.Controls.Add(this.IrregularGridTextBox);
            this.IrregularGridsGroupBox.Controls.Add(this.IrregularGridsComboBox);
            this.IrregularGridsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.IrregularGridsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.IrregularGridsGroupBox.Name = "IrregularGridsGroupBox";
            this.IrregularGridsGroupBox.Size = new System.Drawing.Size(231, 352);
            this.IrregularGridsGroupBox.TabIndex = 2;
            this.IrregularGridsGroupBox.TabStop = false;
            this.IrregularGridsGroupBox.Text = "Irregular grid";
            // 
            // IrregularGridTextBox
            // 
            this.IrregularGridTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IrregularGridTextBox.Location = new System.Drawing.Point(3, 42);
            this.IrregularGridTextBox.Multiline = true;
            this.IrregularGridTextBox.Name = "IrregularGridTextBox";
            this.IrregularGridTextBox.ReadOnly = true;
            this.IrregularGridTextBox.Size = new System.Drawing.Size(225, 307);
            this.IrregularGridTextBox.TabIndex = 1;
            // 
            // IrregularGridsComboBox
            // 
            this.IrregularGridsComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.IrregularGridsComboBox.FormattingEnabled = true;
            this.IrregularGridsComboBox.Location = new System.Drawing.Point(3, 19);
            this.IrregularGridsComboBox.Name = "IrregularGridsComboBox";
            this.IrregularGridsComboBox.Size = new System.Drawing.Size(225, 23);
            this.IrregularGridsComboBox.TabIndex = 0;
            this.IrregularGridsComboBox.SelectedIndexChanged += new System.EventHandler(this.IrregularGridsComboBox_SelectedIndexChanged);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(471, 3);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelDialogButton
            // 
            this.CancelDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelDialogButton.Location = new System.Drawing.Point(390, 3);
            this.CancelDialogButton.Name = "CancelDialogButton";
            this.CancelDialogButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDialogButton.TabIndex = 1;
            this.CancelDialogButton.Text = "Cancel";
            this.CancelDialogButton.UseVisualStyleBackColor = true;
            this.CancelDialogButton.Click += new System.EventHandler(this.CancelDialogButton_Click);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.CancelDialogButton);
            this.ButtonsPanel.Controls.Add(this.OkButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 352);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(559, 29);
            this.ButtonsPanel.TabIndex = 1;
            // 
            // NameGroupBox
            // 
            this.NameGroupBox.Controls.Add(this.NameTextBox);
            this.NameGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameGroupBox.Location = new System.Drawing.Point(231, 0);
            this.NameGroupBox.Name = "NameGroupBox";
            this.NameGroupBox.Size = new System.Drawing.Size(328, 52);
            this.NameGroupBox.TabIndex = 5;
            this.NameGroupBox.TabStop = false;
            this.NameGroupBox.Text = "Name";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(5, 18);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(310, 23);
            this.NameTextBox.TabIndex = 0;
            // 
            // GeometryGroupBox
            // 
            this.GeometryGroupBox.Controls.Add(this.ResultGridLabel);
            this.GeometryGroupBox.Controls.Add(this.StepLabel);
            this.GeometryGroupBox.Controls.Add(this.StepNumericUpDown);
            this.GeometryGroupBox.Controls.Add(this.SizeGroupBox);
            this.GeometryGroupBox.Controls.Add(this.TopLeftCoordinatesGroupBox);
            this.GeometryGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GeometryGroupBox.Location = new System.Drawing.Point(231, 52);
            this.GeometryGroupBox.MinimumSize = new System.Drawing.Size(328, 186);
            this.GeometryGroupBox.Name = "GeometryGroupBox";
            this.GeometryGroupBox.Size = new System.Drawing.Size(328, 186);
            this.GeometryGroupBox.TabIndex = 6;
            this.GeometryGroupBox.TabStop = false;
            this.GeometryGroupBox.Text = "Geometry";
            // 
            // ResultGridLabel
            // 
            this.ResultGridLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultGridLabel.AutoSize = true;
            this.ResultGridLabel.Location = new System.Drawing.Point(16, 159);
            this.ResultGridLabel.Name = "ResultGridLabel";
            this.ResultGridLabel.Size = new System.Drawing.Size(0, 15);
            this.ResultGridLabel.TabIndex = 6;
            // 
            // StepLabel
            // 
            this.StepLabel.AutoSize = true;
            this.StepLabel.Location = new System.Drawing.Point(16, 133);
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StepLabel.Size = new System.Drawing.Size(30, 15);
            this.StepLabel.TabIndex = 5;
            this.StepLabel.Text = "Step";
            // 
            // StepNumericUpDown
            // 
            this.StepNumericUpDown.DecimalPlaces = 4;
            this.StepNumericUpDown.Location = new System.Drawing.Point(61, 132);
            this.StepNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.StepNumericUpDown.Name = "StepNumericUpDown";
            this.StepNumericUpDown.Size = new System.Drawing.Size(90, 23);
            this.StepNumericUpDown.TabIndex = 4;
            this.StepNumericUpDown.ValueChanged += new System.EventHandler(this.StepNumericUpDown_ValueChanged);
            // 
            // SizeGroupBox
            // 
            this.SizeGroupBox.Controls.Add(this.HeightLabel);
            this.SizeGroupBox.Controls.Add(this.HeightNumericUpDown);
            this.SizeGroupBox.Controls.Add(this.WidthLabel);
            this.SizeGroupBox.Controls.Add(this.WidthNumericUpDown);
            this.SizeGroupBox.Location = new System.Drawing.Point(6, 75);
            this.SizeGroupBox.Name = "SizeGroupBox";
            this.SizeGroupBox.Size = new System.Drawing.Size(313, 50);
            this.SizeGroupBox.TabIndex = 3;
            this.SizeGroupBox.TabStop = false;
            this.SizeGroupBox.Text = "Size";
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(164, 20);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(43, 15);
            this.HeightLabel.TabIndex = 4;
            this.HeightLabel.Text = "Height";
            // 
            // HeightNumericUpDown
            // 
            this.HeightNumericUpDown.DecimalPlaces = 4;
            this.HeightNumericUpDown.Location = new System.Drawing.Point(213, 18);
            this.HeightNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.HeightNumericUpDown.Name = "HeightNumericUpDown";
            this.HeightNumericUpDown.Size = new System.Drawing.Size(86, 23);
            this.HeightNumericUpDown.TabIndex = 3;
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(10, 20);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.WidthLabel.Size = new System.Drawing.Size(39, 15);
            this.WidthLabel.TabIndex = 2;
            this.WidthLabel.Text = "Width";
            // 
            // WidthNumericUpDown
            // 
            this.WidthNumericUpDown.DecimalPlaces = 4;
            this.WidthNumericUpDown.Location = new System.Drawing.Point(55, 18);
            this.WidthNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.WidthNumericUpDown.Name = "WidthNumericUpDown";
            this.WidthNumericUpDown.Size = new System.Drawing.Size(90, 23);
            this.WidthNumericUpDown.TabIndex = 1;
            // 
            // TopLeftCoordinatesGroupBox
            // 
            this.TopLeftCoordinatesGroupBox.Controls.Add(this.YCoordinateLabel);
            this.TopLeftCoordinatesGroupBox.Controls.Add(this.YCoordinateNumericUpDown);
            this.TopLeftCoordinatesGroupBox.Controls.Add(this.XCoordinateLabel);
            this.TopLeftCoordinatesGroupBox.Controls.Add(this.XCoordinateNumericUpDown);
            this.TopLeftCoordinatesGroupBox.Location = new System.Drawing.Point(6, 18);
            this.TopLeftCoordinatesGroupBox.Name = "TopLeftCoordinatesGroupBox";
            this.TopLeftCoordinatesGroupBox.Size = new System.Drawing.Size(313, 50);
            this.TopLeftCoordinatesGroupBox.TabIndex = 2;
            this.TopLeftCoordinatesGroupBox.TabStop = false;
            this.TopLeftCoordinatesGroupBox.Text = "TopLeft coordinates";
            // 
            // YCoordinateLabel
            // 
            this.YCoordinateLabel.AutoSize = true;
            this.YCoordinateLabel.Location = new System.Drawing.Point(164, 20);
            this.YCoordinateLabel.Name = "YCoordinateLabel";
            this.YCoordinateLabel.Size = new System.Drawing.Size(14, 15);
            this.YCoordinateLabel.TabIndex = 4;
            this.YCoordinateLabel.Text = "Y";
            // 
            // YCoordinateNumericUpDown
            // 
            this.YCoordinateNumericUpDown.DecimalPlaces = 8;
            this.YCoordinateNumericUpDown.Location = new System.Drawing.Point(185, 18);
            this.YCoordinateNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.YCoordinateNumericUpDown.Minimum = new decimal(new int[] { -1, -1, -1, -2147483648 });
            this.YCoordinateNumericUpDown.Name = "YCoordinateNumericUpDown";
            this.YCoordinateNumericUpDown.Size = new System.Drawing.Size(115, 23);
            this.YCoordinateNumericUpDown.TabIndex = 3;
            // 
            // XCoordinateLabel
            // 
            this.XCoordinateLabel.AutoSize = true;
            this.XCoordinateLabel.Location = new System.Drawing.Point(10, 20);
            this.XCoordinateLabel.Name = "XCoordinateLabel";
            this.XCoordinateLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.XCoordinateLabel.Size = new System.Drawing.Size(14, 15);
            this.XCoordinateLabel.TabIndex = 2;
            this.XCoordinateLabel.Text = "X";
            // 
            // XCoordinateNumericUpDown
            // 
            this.XCoordinateNumericUpDown.DecimalPlaces = 8;
            this.XCoordinateNumericUpDown.Location = new System.Drawing.Point(30, 18);
            this.XCoordinateNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.XCoordinateNumericUpDown.Minimum = new decimal(new int[] { -1, -1, -1, -2147483648 });
            this.XCoordinateNumericUpDown.Name = "XCoordinateNumericUpDown";
            this.XCoordinateNumericUpDown.Size = new System.Drawing.Size(115, 23);
            this.XCoordinateNumericUpDown.TabIndex = 1;
            // 
            // CalcMethodGroupBox
            // 
            this.CalcMethodGroupBox.Controls.Add(this.PowLabel);
            this.CalcMethodGroupBox.Controls.Add(this.PowNumericUpDown);
            this.CalcMethodGroupBox.Controls.Add(this.CountLabel);
            this.CalcMethodGroupBox.Controls.Add(this.CountNumericUpDown);
            this.CalcMethodGroupBox.Controls.Add(this.RadiusLabel);
            this.CalcMethodGroupBox.Controls.Add(this.RadiusNumericUpDown);
            this.CalcMethodGroupBox.Controls.Add(this.CountRadioButton);
            this.CalcMethodGroupBox.Controls.Add(this.RadiusRadioButton);
            this.CalcMethodGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.CalcMethodGroupBox.Location = new System.Drawing.Point(231, 238);
            this.CalcMethodGroupBox.Name = "CalcMethodGroupBox";
            this.CalcMethodGroupBox.Size = new System.Drawing.Size(328, 112);
            this.CalcMethodGroupBox.TabIndex = 7;
            this.CalcMethodGroupBox.TabStop = false;
            this.CalcMethodGroupBox.Text = "Calc method";
            // 
            // PowLabel
            // 
            this.PowLabel.AutoSize = true;
            this.PowLabel.Location = new System.Drawing.Point(101, 78);
            this.PowLabel.Name = "PowLabel";
            this.PowLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PowLabel.Size = new System.Drawing.Size(110, 15);
            this.PowLabel.TabIndex = 11;
            this.PowLabel.Text = "Degree of influence";
            // 
            // PowNumericUpDown
            // 
            this.PowNumericUpDown.Location = new System.Drawing.Point(220, 77);
            this.PowNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.PowNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.PowNumericUpDown.Name = "PowNumericUpDown";
            this.PowNumericUpDown.Size = new System.Drawing.Size(86, 23);
            this.PowNumericUpDown.TabIndex = 10;
            this.PowNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Enabled = false;
            this.CountLabel.Location = new System.Drawing.Point(171, 51);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CountLabel.Size = new System.Drawing.Size(40, 15);
            this.CountLabel.TabIndex = 9;
            this.CountLabel.Text = "Count";
            // 
            // CountNumericUpDown
            // 
            this.CountNumericUpDown.Enabled = false;
            this.CountNumericUpDown.Location = new System.Drawing.Point(220, 48);
            this.CountNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.CountNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.CountNumericUpDown.Name = "CountNumericUpDown";
            this.CountNumericUpDown.Size = new System.Drawing.Size(86, 23);
            this.CountNumericUpDown.TabIndex = 8;
            this.CountNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Location = new System.Drawing.Point(170, 22);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RadiusLabel.Size = new System.Drawing.Size(42, 15);
            this.RadiusLabel.TabIndex = 7;
            this.RadiusLabel.Text = "Radius";
            // 
            // RadiusNumericUpDown
            // 
            this.RadiusNumericUpDown.DecimalPlaces = 4;
            this.RadiusNumericUpDown.Location = new System.Drawing.Point(220, 20);
            this.RadiusNumericUpDown.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            this.RadiusNumericUpDown.Name = "RadiusNumericUpDown";
            this.RadiusNumericUpDown.Size = new System.Drawing.Size(86, 23);
            this.RadiusNumericUpDown.TabIndex = 6;
            // 
            // CountRadioButton
            // 
            this.CountRadioButton.AutoSize = true;
            this.CountRadioButton.Location = new System.Drawing.Point(6, 48);
            this.CountRadioButton.Name = "CountRadioButton";
            this.CountRadioButton.Size = new System.Drawing.Size(113, 19);
            this.CountRadioButton.TabIndex = 1;
            this.CountRadioButton.Text = "By nearest count";
            this.CountRadioButton.UseVisualStyleBackColor = true;
            this.CountRadioButton.CheckedChanged += new System.EventHandler(this.CountRadioButton_CheckedChanged);
            // 
            // RadiusRadioButton
            // 
            this.RadiusRadioButton.AutoSize = true;
            this.RadiusRadioButton.Checked = true;
            this.RadiusRadioButton.Location = new System.Drawing.Point(6, 20);
            this.RadiusRadioButton.Name = "RadiusRadioButton";
            this.RadiusRadioButton.Size = new System.Drawing.Size(73, 19);
            this.RadiusRadioButton.TabIndex = 0;
            this.RadiusRadioButton.TabStop = true;
            this.RadiusRadioButton.Text = "By radius";
            this.RadiusRadioButton.UseVisualStyleBackColor = true;
            this.RadiusRadioButton.CheckedChanged += new System.EventHandler(this.RadiusRadioButton_CheckedChanged);
            // 
            // CreateRegularGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 381);
            this.Controls.Add(this.CalcMethodGroupBox);
            this.Controls.Add(this.GeometryGroupBox);
            this.Controls.Add(this.NameGroupBox);
            this.Controls.Add(this.IrregularGridsGroupBox);
            this.Controls.Add(this.ButtonsPanel);
            this.MinimumSize = new System.Drawing.Size(575, 420);
            this.Name = "CreateRegularGridForm";
            this.Text = "Regular grid creation";
            this.IrregularGridsGroupBox.ResumeLayout(false);
            this.IrregularGridsGroupBox.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            this.NameGroupBox.ResumeLayout(false);
            this.NameGroupBox.PerformLayout();
            this.GeometryGroupBox.ResumeLayout(false);
            this.GeometryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNumericUpDown)).EndInit();
            this.SizeGroupBox.ResumeLayout(false);
            this.SizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
            this.TopLeftCoordinatesGroupBox.ResumeLayout(false);
            this.TopLeftCoordinatesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YCoordinateNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCoordinateNumericUpDown)).EndInit();
            this.CalcMethodGroupBox.ResumeLayout(false);
            this.CalcMethodGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusNumericUpDown)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox IrregularGridsGroupBox;
        private System.Windows.Forms.ComboBox IrregularGridsComboBox;
        private System.Windows.Forms.TextBox IrregularGridTextBox;
        private System.Windows.Forms.GroupBox GeometryGroupBox;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.GroupBox CalcMethodGroupBox;
        private System.Windows.Forms.GroupBox TopLeftCoordinatesGroupBox;
        private System.Windows.Forms.Label XCoordinateLabel;
        private System.Windows.Forms.NumericUpDown XCoordinateNumericUpDown;
        private System.Windows.Forms.NumericUpDown YCoordinateNumericUpDown;
        private System.Windows.Forms.Label YCoordinateLabel;
        private System.Windows.Forms.GroupBox SizeGroupBox;
        private System.Windows.Forms.Label ResultGridLabel;
        private System.Windows.Forms.NumericUpDown StepNumericUpDown;
        private System.Windows.Forms.Label StepLabel;
        private System.Windows.Forms.NumericUpDown WidthNumericUpDown;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.NumericUpDown HeightNumericUpDown;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.NumericUpDown RadiusNumericUpDown;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.GroupBox NameGroupBox;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.NumericUpDown CountNumericUpDown;
        private System.Windows.Forms.RadioButton CountRadioButton;
        private System.Windows.Forms.RadioButton RadiusRadioButton;
        private System.Windows.Forms.NumericUpDown PowNumericUpDown;
        private System.Windows.Forms.Label PowLabel;
        private System.Windows.Forms.Button CancelDialogButton;
    }
}

