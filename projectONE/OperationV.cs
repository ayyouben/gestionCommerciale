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
	public partial class OperationV : Form
	{
		public OperationV()
		{
			InitializeComponent();
		}

		GestionComercialeEntities1 db = new GestionComercialeEntities1();

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void OperationV_Load(object sender, EventArgs e)
		{
			try
			{
				REmplirBanque();
				remplirClient();
				NUMFACTURE.Text = (db.GetLastFacture().First().Value + 1).ToString();
				modereg();
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }

		}

		BindingSource b1 = new BindingSource();
		public void modereg()
		{
			ModeReg.DisplayMember = "Modalite";
			ModeReg.ValueMember = "Num_ModeReg";
			ModeReg.DataSource = db.ModeRG.ToList();
		}
		public void remplirClient()
		{
			b1.DataSource = db.Client.ToList();
			clientFac.DataSource = b1;
			clientFac.DisplayMember = "NomC";
			clientFac.ValueMember = "Num_Clt";

		}
		public void REmplirBanque()
		{
			Banque.DataSource = db.Banque.ToList();
			Banque.DisplayMember = "NomB";
			Banque.ValueMember = "Num_Bq";
		}

        CommandeVente CmdV = new CommandeVente();
        Facture facture = new Facture();
        DetailFac defactutre = new DetailFac();
        Reglement reg = new Reglement();
        DetailReg detailReg = new DetailReg();
        float somme = 0;

        public void reglementByCmdv(int num)
		{
			var req = from r in db.Reglement
					  from dr in db.DetailReg
					  from dv in db.DetailVente
					  where dr.Num_Reg == r.Num_Reg &&
							dr.Num_CmdV == dv.Num_CmdV &&
							dv.Num_CmdV == num
					  select r;
			GRidDetailFAc.DataSource = req.Distinct().ToList();

		}
		private void GridClientCmv_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				int index = GridClientCmv.CurrentRow.Index;
				int numCmd = int.Parse(GridClientCmv.Rows[index].Cells[0].Value.ToString());
				var req = db.GetMontantByCommande(numCmd).First();
				if (req != null)
				{
					Montant.Text = req.Value.ToString();
				}
				else
				{
					Montant.Text = "0";
				}
				reglementByCmdv(numCmd);
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void bunifuImageButton9_Click(object sender, EventArgs e)
		{
			int index = GRidDetailFAc.CurrentRow.Index;
			int numreg = int.Parse(GRidDetailFAc.Rows[index].Cells[0].Value.ToString());
			int index2 = GridClientCmv.CurrentRow.Index;
			int numcmd = int.Parse(GridClientCmv.Rows[index2].Cells[0].Value.ToString());

			try
			{
				var req = db.Reglement.Where(x => x.Num_Reg == numreg).First();
				db.Reglement.Remove(req);
				db.SaveChanges();
				MessageBox.Show("Reglement Supprimer !!");
				reglementByCmdv(numcmd);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void chercheDEtailReg_Click(object sender, EventArgs e)
		{
			try
			{
				PanelBanque.Visible = true;
				int index = GRidDetailFAc.CurrentRow.Index;
				Montant.Text = GRidDetailFAc.Rows[index].Cells[1].Value.ToString();
				montantPaye.Text = GRidDetailFAc.Rows[index].Cells[5].Value.ToString();
				ModeReg.SelectedValue = int.Parse(GRidDetailFAc.Rows[index].Cells[3].Value.ToString());
				Banque.SelectedValue = int.Parse(GRidDetailFAc.Rows[index].Cells[4].Value.ToString());
				NumChequeFac.Text = GRidDetailFAc.Rows[index].Cells[2].Value.ToString();
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void bunifuImageButton8_Click(object sender, EventArgs e)
		{
			int index = GRidDetailFAc.CurrentRow.Index;

			int numReg = int.Parse(GRidDetailFAc.Rows[index].Cells[0].Value.ToString());
			try
			{
				//db.Modifier_Reglement(numReg, float.Parse(Montant.Text), NumChequeFac.Text, int.Parse(ModeReg.SelectedValue.ToString()), int.Parse(Banque.SelectedValue.ToString()));
				MessageBox.Show("Reglement Bien Modifer !");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ModeReg_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ModeReg.SelectedIndex == 1 || ModeReg.SelectedIndex == 3)
			{
				PanelBanque.Visible = true;
			}
			else
			{
				PanelBanque.Visible = false;
			}
		}

		private void bunifuImageButton2_Click(object sender, EventArgs e)
		{
			int index = GridClientCmv.CurrentRow.Index;
			int numCmd = int.Parse(GridClientCmv.Rows[index].Cells[0].Value.ToString());
			try
			{
				db.Ajouter_Facture(int.Parse(DateTime.Now.ToString("yyMMddHHss")),DateFac.Value, float.Parse(RemiseFAc.Text),0);
				MessageBox.Show("Facture Bien Ajouter");
				reglementByCmdv(numCmd);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            OperationV p = new OperationV();

            int index = GridClientCmv.CurrentRow.Index;
            int numCmd = int.Parse(GridClientCmv.Rows[index].Cells[0].Value.ToString());
            int idFActure = db.GetLastFacture().First().Value;
            int numModeReg = int.Parse(ModeReg.SelectedValue.ToString());
            int numBanque = int.Parse(Banque.SelectedValue.ToString());
            int numClient = int.Parse(clientFac.SelectedValue.ToString());
            int mount = DateTime.Now.Month;


            try
            {
                var reqq = from x in db.DetailVente
                           from y in db.DetailReg
                           where x.Num_CmdV == y.Num_CmdV && y.Num_CmdV == numCmd
                           select y.Num_Reg;
                if (reqq.Count() == 0)
                {
                    if (float.Parse(Montant.Text) <= 5000)
                    {
                        db.Ajouter_DetailFacture(idFActure, numCmd);
                        reg.NumChe = NumChequeFac.Text;
                        reg.Montant = double.Parse(Montant.Text);
                        reg.Num_ModeReg = numModeReg;
                        reg.Num_Bq = numBanque;
                        reg.montantDepose = double.Parse(montantPaye.Text);
                        reg.reste = reg.Montant - reg.montantDepose;
                        db.Reglement.Add(reg);
                        db.SaveChanges();
                        int lastreg = db.GetLastReglement().First().Value;
                        db.Ajouter_DetailReglement(lastreg, numCmd);
                        MessageBox.Show("Facture Regler");
                        reglementByCmdv(numCmd);
                    }
                    else
                    {
                        MessageBox.Show("Vus aver depasse 5000 DH par Facture");
                    }
                }
                else
                {
                    MessageBox.Show("Facture DEja Regler");
                }
                p.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clientFac_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridClientCmv.DataSource = db.Chereher_CommandeVente(clientFac.Text);
        }
    }
}
