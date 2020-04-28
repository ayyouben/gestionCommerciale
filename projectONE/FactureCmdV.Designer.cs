namespace projectONE
{
	partial class FactureCmdV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactureCmdV));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.btnImpr1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmbste = new System.Windows.Forms.ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.btnImpr1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImpr1
            // 
            this.btnImpr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.btnImpr1.Image = ((System.Drawing.Image)(resources.GetObject("btnImpr1.Image")));
            this.btnImpr1.ImageActive = null;
            this.btnImpr1.Location = new System.Drawing.Point(398, 62);
            this.btnImpr1.Margin = new System.Windows.Forms.Padding(4);
            this.btnImpr1.Name = "btnImpr1";
            this.btnImpr1.Size = new System.Drawing.Size(115, 33);
            this.btnImpr1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnImpr1.TabIndex = 89;
            this.btnImpr1.TabStop = false;
            this.btnImpr1.Zoom = 10;
            this.btnImpr1.Click += new System.EventHandler(this.btnImpr1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Cmbste);
            this.panel1.Controls.Add(this.btnImpr1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1527, 124);
            this.panel1.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Gold;
            this.panel11.Controls.Add(this.label3);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1527, 38);
            this.panel11.TabIndex = 174;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gold;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(713, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 25);
            this.label3.TabIndex = 171;
            this.label3.Text = "Imprimer Facture";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(13, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 173;
            this.label1.Text = "Societe";
            // 
            // Cmbste
            // 
            this.Cmbste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.Cmbste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmbste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmbste.ForeColor = System.Drawing.SystemColors.Window;
            this.Cmbste.FormattingEnabled = true;
            this.Cmbste.Location = new System.Drawing.Point(105, 62);
            this.Cmbste.Margin = new System.Windows.Forms.Padding(4);
            this.Cmbste.Name = "Cmbste";
            this.Cmbste.Size = new System.Drawing.Size(285, 33);
            this.Cmbste.TabIndex = 172;
            this.Cmbste.SelectedIndexChanged += new System.EventHandler(this.Cmbste_SelectedIndexChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "projectONE.FactureFinal.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 124);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1527, 592);
            this.reportViewer1.TabIndex = 2;
            // 
            // FactureCmdV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 716);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FactureCmdV";
            this.Text = "FactureCmdV";
            this.Load += new System.EventHandler(this.FactureCmdV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnImpr1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private Bunifu.Framework.UI.BunifuImageButton btnImpr1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmbste;
        private System.Windows.Forms.Panel panel11;
    }
}