namespace MiniGis
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.ButtonPan = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonZoomAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelArea = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelMapCursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.mapControl = new MiniGis.MapControl();
            this.layersControl = new MiniGis.LayersControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonSelect,
            this.ButtonPan,
            this.ButtonZoomIn,
            this.ButtonZoomOut,
            this.toolStripSeparator2,
            this.ButtonZoomAll});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(624, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonSelect.Image = global::MiniGis.Properties.Resources.arrow1;
            this.ButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(23, 22);
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonPan
            // 
            this.ButtonPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonPan.Image = global::MiniGis.Properties.Resources.hand1;
            this.ButtonPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPan.Name = "ButtonPan";
            this.ButtonPan.Size = new System.Drawing.Size(23, 22);
            this.ButtonPan.Text = "Pan";
            this.ButtonPan.Click += new System.EventHandler(this.ButtonPan_Click);
            // 
            // ButtonZoomIn
            // 
            this.ButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonZoomIn.Image = global::MiniGis.Properties.Resources.zoom_in1;
            this.ButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonZoomIn.Name = "ButtonZoomIn";
            this.ButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ButtonZoomIn.Text = "ZoomIn";
            this.ButtonZoomIn.Click += new System.EventHandler(this.ButtonZoomIn_Click);
            // 
            // ButtonZoomOut
            // 
            this.ButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonZoomOut.Image = global::MiniGis.Properties.Resources.zoom_out1;
            this.ButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonZoomOut.Name = "ButtonZoomOut";
            this.ButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ButtonZoomOut.Text = "ZoomOut";
            this.ButtonZoomOut.Click += new System.EventHandler(this.ButtonZoomOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonZoomAll
            // 
            this.ButtonZoomAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonZoomAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonZoomAll.Name = "ButtonZoomAll";
            this.ButtonZoomAll.Size = new System.Drawing.Size(57, 22);
            this.ButtonZoomAll.Text = "ZoomAll";
            this.ButtonZoomAll.Click += new System.EventHandler(this.ButtonZoomAll_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 6);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMouse,
            this.toolStripStatusLabelArea});
            this.statusStrip.Location = new System.Drawing.Point(0, 419);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(624, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMouse
            // 
            this.toolStripStatusLabelMouse.Name = "toolStripStatusLabelMouse";
            this.toolStripStatusLabelMouse.Size = new System.Drawing.Size(85, 17);
            this.toolStripStatusLabelMouse.Text = "MouseCoursor";
            // 
            // toolStripStatusLabelArea
            // 
            this.toolStripStatusLabelArea.Name = "toolStripStatusLabelArea";
            this.toolStripStatusLabelArea.Size = new System.Drawing.Size(78, 17);
            this.toolStripStatusLabelArea.Text = "Polygon Area";
            // 
            // LabelMapCursorPosition
            // 
            this.LabelMapCursorPosition.Name = "LabelMapCursorPosition";
            this.LabelMapCursorPosition.Size = new System.Drawing.Size(0, 17);
            // 
            // splitter1
            // 
            this.splitter.Location = new System.Drawing.Point(161, 25);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 394);
            this.splitter.TabIndex = 6;
            this.splitter.TabStop = false;
            // 
            // map
            // 
            this.mapControl.ActiveTool = MiniGis.MapToolType.Select;
            this.mapControl.BackColor = System.Drawing.SystemColors.Window;
            this.mapControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.Location = new System.Drawing.Point(164, 25);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(460, 394);
            this.mapControl.TabIndex = 0;
            this.mapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map_MouseMove);
            this.mapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.map_MouseUp);
            // 
            // layersControl
            // 
            this.layersControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.layersControl.Location = new System.Drawing.Point(0, 25);
            this.layersControl.Name = "layersControl";
            this.layersControl.Size = new System.Drawing.Size(161, 394);
            this.layersControl.TabIndex = 5;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            this.openFileDialog.Title = "Добавить слои";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.layersControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "MiniGIS";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MapControl mapControl;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel LabelMapCursorPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.ToolStripButton ButtonSelect;
        private System.Windows.Forms.ToolStripButton ButtonPan;
        private System.Windows.Forms.ToolStripButton ButtonZoomIn;
        private System.Windows.Forms.ToolStripButton ButtonZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ButtonZoomAll;
        private LayersControl layersControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMouse;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelArea;
    }
}

