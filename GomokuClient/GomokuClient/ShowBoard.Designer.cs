namespace Gomoku
{
    partial class ShowBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.boardMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // boardMsg
            // 
            this.boardMsg.Location = new System.Drawing.Point(29, 32);
            this.boardMsg.Multiline = true;
            this.boardMsg.Name = "boardMsg";
            this.boardMsg.Size = new System.Drawing.Size(288, 271);
            this.boardMsg.TabIndex = 0;
            // 
            // ShowBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 401);
            this.Controls.Add(this.boardMsg);
            this.Name = "ShowBoard";
            this.Text = "ShowBoard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boardMsg;
    }
}