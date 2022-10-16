namespace Client
{
    partial class Form1
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
            this.QueryMsgTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerMsgTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.IpTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.SendMsgBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // QueryMsgTextBox
            // 
            this.QueryMsgTextBox.Location = new System.Drawing.Point(142, 109);
            this.QueryMsgTextBox.Name = "QueryMsgTextBox";
            this.QueryMsgTextBox.Size = new System.Drawing.Size(268, 20);
            this.QueryMsgTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Query message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Received message:";
            // 
            // ServerMsgTextBox
            // 
            this.ServerMsgTextBox.Location = new System.Drawing.Point(142, 153);
            this.ServerMsgTextBox.Name = "ServerMsgTextBox";
            this.ServerMsgTextBox.Size = new System.Drawing.Size(268, 20);
            this.ServerMsgTextBox.TabIndex = 3;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(170, 13);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(50, 20);
            this.PortTextBox.TabIndex = 4;
            // 
            // IpTextBox
            // 
            this.IpTextBox.Location = new System.Drawing.Point(35, 12);
            this.IpTextBox.Name = "IpTextBox";
            this.IpTextBox.Size = new System.Drawing.Size(100, 20);
            this.IpTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(-2, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(141, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 36);
            this.label4.TabIndex = 7;
            this.label4.Text = ":";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(239, 13);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 8;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // SendMsgBtn
            // 
            this.SendMsgBtn.Location = new System.Drawing.Point(427, 107);
            this.SendMsgBtn.Name = "SendMsgBtn";
            this.SendMsgBtn.Size = new System.Drawing.Size(101, 23);
            this.SendMsgBtn.TabIndex = 9;
            this.SendMsgBtn.Text = "Send Message";
            this.SendMsgBtn.UseVisualStyleBackColor = true;
            this.SendMsgBtn.Click += new System.EventHandler(this.SendMsgBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 203);
            this.Controls.Add(this.SendMsgBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IpTextBox);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.ServerMsgTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QueryMsgTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox QueryMsgTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ServerMsgTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.TextBox IpTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button SendMsgBtn;
    }
}

