namespace CafeteriaBarnyardView
{
    partial class FormReportPeriod
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonToPdf = new System.Windows.Forms.Button();
            this.buttonForm = new System.Windows.Forms.Button();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.buttonToPdf);
            this.panelMenu.Controls.Add(this.buttonForm);
            this.panelMenu.Controls.Add(this.dateTimePickerTo);
            this.panelMenu.Controls.Add(this.textBoxTo);
            this.panelMenu.Controls.Add(this.dateTimePickerFrom);
            this.panelMenu.Controls.Add(this.textBoxFrom);
            this.panelMenu.Location = new System.Drawing.Point(2, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1105, 46);
            this.panelMenu.TabIndex = 5;
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.Location = new System.Drawing.Point(562, 10);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(186, 28);
            this.buttonToPdf.TabIndex = 5;
            this.buttonToPdf.Text = "Отправить на почту в Pdf";
            this.buttonToPdf.UseVisualStyleBackColor = true;
            this.buttonToPdf.Click += new System.EventHandler(this.buttonToPdf_Click);
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(384, 10);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(160, 28);
            this.buttonForm.TabIndex = 4;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(229, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerTo.TabIndex = 3;
            // 
            // textBoxTo
            // 
            this.textBoxTo.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTo.Location = new System.Drawing.Point(200, 15);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(23, 13);
            this.textBoxTo.TabIndex = 2;
            this.textBoxTo.Text = "по";
            this.textBoxTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(54, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerFrom.TabIndex = 1;
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFrom.Location = new System.Drawing.Point(11, 15);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(23, 13);
            this.textBoxFrom.TabIndex = 0;
            this.textBoxFrom.Text = "C";
            this.textBoxFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "CafeteriaBarnyardView.ReportPeriodRequestsAndOrders.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(2, 44);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1105, 638);
            this.reportViewer.TabIndex = 6;
            // 
            // FormReportPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 694);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.panelMenu);
            this.Name = "FormReportPeriod";
            this.Text = "Отчет по заявкам и заказам";
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button buttonToPdf;
        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.TextBox textBoxFrom;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}