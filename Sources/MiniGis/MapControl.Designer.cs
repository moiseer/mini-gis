namespace MiniGis
{
    partial class MapControl
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

         #region Код, автоматически созданный конструктором компонентов

         /// <summary>
         /// Required method for Designer support - do not modify
         /// the contents of this method with the code editor.
         /// </summary>
         private void InitializeComponent()
         {
             this.SuspendLayout();
             //
             // MapControl
             //
             this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.DoubleBuffered = true;
             this.Name = "MapControl";
             this.Size = new System.Drawing.Size(175, 173);
             this.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
             this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
             this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
             this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Map_MouseUp);
             this.Resize += new System.EventHandler(this.Map_Resize);
             this.ResumeLayout(false);
         }

         #endregion
    }
}
