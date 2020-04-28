using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Reporting.WinForms;
using Convertisseur;
using Convertisseur.Entite;
using Convertisseur.Extension; 

namespace projectONE
{
	public partial class FactureCmdV : Form
	{
		public FactureCmdV(int numFac , Decimal montant,int tva,float remise)
		{
			InitializeComponent();
            numfac = numFac;
            Montant = montant;
            Tva = tva;
            Remise = remise;
		}
        int numfac;
        Decimal Montant;
        int Tva;float Remise;
        GestionComercialeEntities1 db = new GestionComercialeEntities1();

		private void FactureCmdV_Load(object sender, EventArgs e)
		{
            // TODO: This line of code loads data into the 'GestionComercialeDataSet1.print_Fac' table. You can move, or remove it, as needed.
           // this.print_FacTableAdapter.Fill(this.GestionComercialeDataSet2.print_Fac);
            remplirste();
			RemplirNumfac();
           

            this.reportViewer1.RefreshReport();
		}
     

		public void remplirste()
		{
            Cmbste.DataSource = db.config.ToList();
            Cmbste.DisplayMember = "NonSo";

		}
		public void RemplirNumfac()
		{
			//comboBox1.DataSource = db.Facture.ToList();
			//comboBox1.DisplayMember = "Num_Fac";
           
		}

		private void btnImpr1_Click(object sender, EventArgs e)
		{
            //try
            //{
                var convertisseur = ConvertisseurNombreEnLettre
                .Parametrage
                .AppliquerUneUnite(Unite.Creer("dirham", "dirhams", "centime", "Centimes"))
                .ModifierLaVirgule("et ").AppliquerLaRegleDesTiretsDe1990(true)
                .ValiderLeParametrage();

                var data = db.print_Fac(numfac, Cmbste.Text);
				var reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
				reportDataSource1.Name = "DataSet1";
				reportDataSource1.Value = data;
             
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("Montant", convertisseur.Convertir(Montant));
                parameters[1] = new ReportParameter("TVA", Tva.ToString());
                parameters[2] = new ReportParameter("TTC", Montant.ToString());


            this.reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.LocalReport.DataSources.Clear();

				this.reportViewer1.LocalReport.Refresh();
				this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
				//this.reportViewer1.LocalReport.ReportEmbeddedResource = "GESTIONCOMMECIALE.Report1.rdlc";

				this.reportViewer1.RefreshReport();
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Cmbste_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void getClientFacture(ComboBox cmb2 , ComboBox cmb1)
        {

            int numClt = int.Parse(cmb2.SelectedValue.ToString());
            var req = (from x in db.CommandeVente
                       from y in db.DetailFac
                       from z in db.Facture
                       where x.Num_CmdV == y.Num_CmdV && y.Num_Fac == z.Num_Fac && x.Num_Clt == numClt
                       select z).ToList();
            cmb1.DataSource = req;
            cmb1.DisplayMember = "Num_Fac";
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Thread.Sleep(4000);
            //  if (comboBox2.SelectedItem == null) return;
            try
            {
           


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
               
        }
    }
}
