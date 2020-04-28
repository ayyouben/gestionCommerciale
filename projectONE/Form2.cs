using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectONE
{
    public partial class Form2 : Form
    {
        public Form2(int CmdV)
        {
            InitializeComponent();
            num = CmdV;
        }
        int num;
        GestionComercialeEntities1 db = new GestionComercialeEntities1();
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'GestionComercialeDataSet3.print_Fac' table. You can move, or remove it, as needed.
            //this.print_FacTableAdapter.Fill(this.GestionComercialeDataSet3.print_Fac);
            // TODO: This line of code loads data into the 'GestionComercialeDataSet7.BonneCommande' table. You can move, or remove it, as needed.
            // this.BonneCommandeTableAdapter.Fill(this.GestionComercialeDataSet7.BonneCommande);
            var data =db.print_PV(num);
            var reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = data;
            reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
