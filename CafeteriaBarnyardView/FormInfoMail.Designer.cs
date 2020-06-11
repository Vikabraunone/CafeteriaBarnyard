namespace CafeteriaBarnyardView
{
    partial class FormInfoMail
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonSendToExcelFile = new System.Windows.Forms.Button();
            this.buttonSendToWord = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(26, 24);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(414, 67);
            this.textBox.TabIndex = 0;
            // 
            // buttonSendToExcelFile
            // 
            this.buttonSendToExcelFile.Location = new System.Drawing.Point(235, 161);
            this.buttonSendToExcelFile.Name = "buttonSendToExcelFile";
            this.buttonSendToExcelFile.Size = new System.Drawing.Size(134, 44);
            this.buttonSendToExcelFile.TabIndex = 1;
            this.buttonSendToExcelFile.Text = "Отправить в Excel";
            this.buttonSendToExcelFile.UseVisualStyleBackColor = true;
            this.buttonSendToExcelFile.Click += new System.EventHandler(this.buttonSendToExcelFile_Click);
            // 
            // buttonSendToWord
            // 
            this.buttonSendToWord.Location = new System.Drawing.Point(95, 161);
            this.buttonSendToWord.Name = "buttonSendToWord";
            this.buttonSendToWord.Size = new System.Drawing.Size(134, 44);
            this.buttonSendToWord.TabIndex = 2;
            this.buttonSendToWord.Text = "Отправить в Word";
            this.buttonSendToWord.UseVisualStyleBackColor = true;
            this.buttonSendToWord.Click += new System.EventHandler(this.buttonSendToWord_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(153, 211);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(134, 39);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(26, 112);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(415, 21);
            this.comboBox.TabIndex = 4;
            // 
            // FormInfoMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 272);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSendToWord);
            this.Controls.Add(this.buttonSendToExcelFile);
            this.Controls.Add(this.textBox);
            this.Name = "FormInfoMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Отправить заявку на почту";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonSendToExcelFile;
        private System.Windows.Forms.Button buttonSendToWord;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBox;
    }
}