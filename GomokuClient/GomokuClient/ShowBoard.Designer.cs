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
            this.button1 = new System.Windows.Forms.Button();
            this.battle = new System.Windows.Forms.TextBox();
            this.blackNameText = new System.Windows.Forms.Label();
            this.whiteNameText = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.AILabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(701, 582);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // battle
            // 
            this.battle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.battle.Location = new System.Drawing.Point(600, 173);
            this.battle.Multiline = true;
            this.battle.Name = "battle";
            this.battle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.battle.Size = new System.Drawing.Size(176, 391);
            this.battle.TabIndex = 2;
            // 
            // blackNameText
            // 
            this.blackNameText.AutoSize = true;
            this.blackNameText.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackNameText.Location = new System.Drawing.Point(571, 22);
            this.blackNameText.Name = "blackNameText";
            this.blackNameText.Size = new System.Drawing.Size(103, 28);
            this.blackNameText.TabIndex = 3;
            this.blackNameText.Text = "Black: ";
            // 
            // whiteNameText
            // 
            this.whiteNameText.AutoSize = true;
            this.whiteNameText.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteNameText.Location = new System.Drawing.Point(571, 73);
            this.whiteNameText.Name = "whiteNameText";
            this.whiteNameText.Size = new System.Drawing.Size(103, 28);
            this.whiteNameText.TabIndex = 4;
            this.whiteNameText.Text = "White: ";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SaveButton.Location = new System.Drawing.Point(701, 126);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AILabel
            // 
            this.AILabel.AutoSize = true;
            this.AILabel.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AILabel.Location = new System.Drawing.Point(38, 22);
            this.AILabel.Name = "AILabel";
            this.AILabel.Size = new System.Drawing.Size(195, 44);
            this.AILabel.TabIndex = 6;
            this.AILabel.Text = "AI Name - ";
            // 
            // ShowBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 627);
            this.Controls.Add(this.AILabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.whiteNameText);
            this.Controls.Add(this.blackNameText);
            this.Controls.Add(this.battle);
            this.Controls.Add(this.button1);
            this.Name = "ShowBoard";
            this.Text = "Board";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox battle;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label whiteNameText;
        private System.Windows.Forms.Label blackNameText;
        private System.Windows.Forms.Label AILabel;
    }
}