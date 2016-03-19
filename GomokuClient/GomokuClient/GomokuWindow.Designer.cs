namespace Gomoku
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.AllMessage = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.Account = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.IPaddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Ready = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.selectFile = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.clearButton = new System.Windows.Forms.Button();
            this.identityBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AllMessage
            // 
            this.AllMessage.Location = new System.Drawing.Point(12, 237);
            this.AllMessage.Name = "AllMessage";
            this.AllMessage.Size = new System.Drawing.Size(484, 92);
            this.AllMessage.TabIndex = 0;
            this.AllMessage.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(313, 345);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Visible = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Location = new System.Drawing.Point(168, 346);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(128, 22);
            this.textBoxMsg.TabIndex = 3;
            this.textBoxMsg.Visible = false;
            // 
            // Account
            // 
            this.Account.Location = new System.Drawing.Point(187, 75);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(159, 22);
            this.Account.TabIndex = 4;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(370, 76);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 6;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14F);
            this.label1.Location = new System.Drawing.Point(12, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Type message :";
            this.label1.Visible = false;
            // 
            // IPaddr
            // 
            this.IPaddr.Location = new System.Drawing.Point(187, 33);
            this.IPaddr.Name = "IPaddr";
            this.IPaddr.Size = new System.Drawing.Size(159, 22);
            this.IPaddr.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 14F);
            this.label2.Location = new System.Drawing.Point(62, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "Server IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 14F);
            this.label3.Location = new System.Drawing.Point(82, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Account";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(370, 31);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 12;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 14F);
            this.label5.Location = new System.Drawing.Point(112, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 22);
            this.label5.TabIndex = 15;
            this.label5.Text = "Side";
            // 
            // Ready
            // 
            this.Ready.Location = new System.Drawing.Point(421, 344);
            this.Ready.Name = "Ready";
            this.Ready.Size = new System.Drawing.Size(75, 23);
            this.Ready.TabIndex = 16;
            this.Ready.Text = "Ready";
            this.Ready.UseVisualStyleBackColor = true;
            this.Ready.Click += new System.EventHandler(this.Ready_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 14F);
            this.label6.Location = new System.Drawing.Point(52, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 22);
            this.label6.TabIndex = 17;
            this.label6.Text = "AI program";
            // 
            // selectFile
            // 
            this.selectFile.Location = new System.Drawing.Point(370, 190);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(75, 23);
            this.selectFile.TabIndex = 18;
            this.selectFile.Text = "Open";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(187, 192);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(159, 22);
            this.fileName.TabIndex = 19;
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "MyAI.exe";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(370, 153);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(79, 23);
            this.clearButton.TabIndex = 20;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // identityBox
            // 
            this.identityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.identityBox.FormattingEnabled = true;
            this.identityBox.Items.AddRange(new object[] {
            "Black (黑方)",
            "White (白方)",
            "Spectator (觀察者)"});
            this.identityBox.Location = new System.Drawing.Point(187, 123);
            this.identityBox.Name = "identityBox";
            this.identityBox.Size = new System.Drawing.Size(121, 20);
            this.identityBox.TabIndex = 21;
            this.identityBox.SelectedIndexChanged += new System.EventHandler(this.identityBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 382);
            this.Controls.Add(this.identityBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.selectFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Ready);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPaddr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.AllMessage);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "GomokuClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox AllMessage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.TextBox Account;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPaddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Ready;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ComboBox identityBox;
    }
}

