using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectONE
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		GestionComercialeEntities1 db = new GestionComercialeEntities1();

		BindingSource d   = new BindingSource();
		BindingSource d2  = new BindingSource();
		BindingSource d3  = new BindingSource();
		BindingSource d4  = new BindingSource();
		BindingSource d5  = new BindingSource();
		BindingSource d6  = new BindingSource();
        BindingSource d7  = new BindingSource();
        BindingSource d8  = new BindingSource();
        BindingSource d9  = new BindingSource();
        BindingSource d10 = new BindingSource();
        BindingSource d11 = new BindingSource();
        BindingSource d12 = new BindingSource();
        BindingSource d13 = new BindingSource();
        BindingSource d14 = new BindingSource();
        BindingSource d15 = new BindingSource();
        BindingSource d16 = new BindingSource();

        private void Form1_Load(object sender, EventArgs e)
		{

			// TODO: cette ligne de code charge les données dans la table 'gestionComercialeDataSet.Client'. Vous pouvez la déplacer ou la supprimer selon les besoins.
			//this.clientTableAdapter.Fill(this.gestionComercialeDataSet.Client);
			//numComandeV.Text = ((from x in db.CommandeVente select x.Num_CmdV).Max() + 1).ToString();

			//---------------------- Affichage ---------------------
			
			afficherclient();
			aficher_Fournisseur();
			afficheCommandeAchat();
			ComboNumArticle();
			NumCommandAchat();
            afficherOuvrier();
            AfficherCollection();
            AfficherPintage();
            Afficher_week();
            
            //---------------------- Le Remplissage ----------------
            
            remplirCmbMatricule();
            remplirComboFournisseur(cmbComdAfourn);
            remplirComboFournisseur(File_Fournisseur);

            remlpirFAmille(cmbArt_cat);
			remplirarticle();
			remplirGridVent();
			remplirClient(cmbCmdV_client);
			remplirClient(cmbFact_CL);
            remplirClient(cmbFact_CL2);
            remplirClient(cmbRegl_CL);

            REmplirCategorie();
			RemplirFacture();
			REmplirGridFamille();
            RemplirCmbTypeTobia();
            RemplirCmbTypeCl();
            RemplirCmbWeek();
            RemplirCmbCollection();
            RemplirCmbOuvrier();
            remplirCmbTypeV();
            AfficherVoiture();
            AfficherLocation();
            RemplirCmbClientLoc();
            AfficherChantier();
            remplirChantierOvr();
            remplirChantier();
           // AfficherCharge();
            AfficherConfig();
            
            RemplirFichier();
            modereg();
            REmplirBanque();
            RemplirCheckboxList(checkedListBox1,"Sable",panelCheckList);
            RemplirCheckboxList(checkedListBox2, "FERE",panelcheckList2);
        }
        public void Afficher_Detaill_vente(int num)
        {
            gridDTCmdV.DataSource = db.Dvente_Cmd(num);
        }
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
        public void RemplirCheckboxList(CheckedListBox ck,string categorie,Panel pl)
        {
            int top = 1;
            var req = from x in db.Article
                      from y in db.Famille
                      where x.Num_Fa == y.Num_Fa &&y.NomFa==categorie
                      select x.Num_At;
            ck.Items.AddRange(req.ToArray());
           // top = panelCheckList.Top;
            foreach (object item in ck.Items)
            {
                
                TextBox t = new TextBox();
                t.Top = top * 25;
                t.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                t.Name = item.ToString();
                pl.Controls.Add(t);
                top =top +1;
            }
           
        }
        public void REmplirBanque()
        {
            Banque.DataSource = db.Banque.ToList();
            Banque.DisplayMember = "NomB";
            Banque.ValueMember = "Num_Bq";
        }
        public void modereg()
        {
            ModeReg.DisplayMember = "Modalite";
            ModeReg.ValueMember = "Num_ModeReg";
            ModeReg.DataSource = db.ModeRG.ToList();
        }
        public void RemplirFichier()
        {
            d16.DataSource = db.Afficher_Fichier();
            GridFichier.DataSource = d16;

        }
        
        public void remplirCmbCmdAch(int numfr)
        {
            cmbCmdAchFichier.DataSource = db.CommandeAch.Where(x => x.Num_Fr == numfr).ToList();
            cmbCmdAchFichier.DisplayMember = "Num_Cmd";
        }
        public void AfficherConfig()
        {
            GridConfig.DataSource = db.afficher_Config();
        }
        public void AfficherCharge(int var )
        {

            try
            {
              
                var req = db.Charge.Where(x => x.Numch == var).ToList();
                gridCharge.DataSource = req.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         public void remplirChantier()
        {
            //cmbCharge_chentier.DataSource = db.Chantier.ToList();
           // cmbCharge_chentier.DisplayMember = "Num";
        }
        public void remplirChantierOvr()
        {
            cmbChant_Cin.DataSource = db.Client.ToList();
            cmbChant_Cin.DisplayMember = "NomC";
            cmbChant_Cin.ValueMember = "Num_Clt";
        }
        public void AfficherChantier()
        {
            d14.DataSource = db.aff_Chantier();
            gridChantier.DataSource = d14;
        }
        public void RemplirCmbClientLoc()
        {
            cmbLoc_Client.DataSource = db.Client.ToList();
            cmbLoc_Client.DisplayMember = "NomC";
            cmbLoc_Client.ValueMember   = "Num_Clt";
        }
        
        public void AfficherLocation()
        {
           
            d11.DataSource = db.aff_Location();
            gridLocation.DataSource = d11;
        }
        public void AfficherVoiture()
        {
            d13.DataSource= db.aff_Vehicule();
            gridVéhicule.DataSource = d13;
        }
		public void REmplirGridFamille()
		{
			GridFamille.DataSource = db.Affiche_Famille();
		}
		public void RemplirFactureBYCheque(string cheque)
		{
			var req = from c in db.Client
					  from cv in db.CommandeVente
					  from f in db.Facture
					  from df in db.DetailFac
					  from dreg in db.DetailReg
					  from reg in db.Reglement
					  where c.Num_Clt == cv.Num_Clt &&
							df.Num_CmdV == cv.Num_CmdV && f.Num_Fac == df.Num_Fac && dreg.Num_CmdV == cv.Num_CmdV
							&& dreg.Num_Reg == reg.Num_Reg
							&& reg.NumChe == cheque

					  select new
					  {
						  NumeroFacture = f.Num_Fac,
						  Date_Facture = f.DateFac,
						  Remise = f.Remise,
						  CommandeVente = cv.Num_CmdV,
						  NomClient = c.NomC,
					  };
			GridFacture.DataSource = req.ToList();
		}
		public void RemplirFactureBYClient(int numClient)
		{

			var req = from c in db.Client
					  from cv in db.CommandeVente
					  from f in db.Facture
					  from df in db.DetailFac
					  from dreg in db.DetailReg
					  from reg in db.Reglement
					  where c.Num_Clt == cv.Num_Clt &&
							df.Num_CmdV == cv.Num_CmdV && f.Num_Fac == df.Num_Fac && dreg.Num_CmdV == cv.Num_CmdV
							&& dreg.Num_Reg == reg.Num_Reg
							&& c.Num_Clt == numClient

					  select new
					  {
						  NumeroFacture = f.Num_Fac,
						  Date_Facture = f.DateFac,
						  Remise = f.Remise,
						  CommandeVente = cv.Num_CmdV,
						  NomClient = c.NomC,
					  };
			GridFacture.DataSource = req.ToList();



		}
		public void RemplirFActureBYDate(DateTime date1, DateTime date2)
		{
			if (date1 < date2)
			{
				var req = from c in db.Client
						  from cv in db.CommandeVente
						  from f in db.Facture
						  from df in db.DetailFac
						  where c.Num_Clt == cv.Num_Clt &&
								df.Num_CmdV == cv.Num_CmdV && f.Num_Fac == df.Num_Fac &&
								f.DateFac >= date1 && f.DateFac <= date2
						  select new
						  {
							  NumeroFacture = f.Num_Fac,
							  Date_Facture = f.DateFac,
							  Remise = f.Remise,
							  CommandeVente = cv.Num_CmdV,
							  NomClient = c.NomC,
						  };
				GridFacture.DataSource = req.ToList();
			}
			else
			{
				MessageBox.Show("La Première Date Doit Etre Infèrieure a la 2eme Date");
			}
		}
		public void RemplirFacture()
		{
            //var req = from c in db.Client
            //          from cv in db.CommandeVente
            //          from f in db.Facture
            //          from df in db.DetailFac
            //          from dv in db.DetailVente
            //          where c.Num_Clt == cv.Num_Clt &&
            //                df.Num_CmdV == cv.Num_CmdV && f.Num_Fac == df.Num_Fac && dv.Num_CmdV == cv.Num_CmdV
            //          select new
            //          {
            //              NumeroFacture = f.Num_Fac,
            //              Date_Facture = f.DateFac,
            //              Remise = f.Remise,
            //              CommandeVente = cv.Num_CmdV,
            //              NomClient = c.NomC,

            //          };
            GridFacture.DataSource = db.Affiche_FActuree();
          

		}
		public void REmplirCategorie()
		{

			cmbDTCmdV_catAr.DataSource = db.Famille.ToList();
			cmbDTCmdV_catAr.DisplayMember = "NomFa";
			cmbDTCmdV_catAr.ValueMember = "Num_Fa";


		}
		public void remplirClient(ComboBox cmb)
		{
			cmb.DataSource = db.Client.ToList();
			cmb.DisplayMember = "NomC";
			cmb.ValueMember = "Num_Clt";

		}

		public void remplirGridVent()
		{
			gridComdV.DataSource = db.Affiche_CmdV();
		}

		public void remplirComboFournisseur(ComboBox Fr)
		{
			Fr.DataSource = db.Fournisseur.ToList();
			Fr.DisplayMember = "NomF";
			Fr.ValueMember = "Num_Fr";
		}
        public void remplirCmbMatricule()
        {
            cmbLoc_matr.DataSource = db.Voiture.ToList();
            cmbLoc_matr.DisplayMember = "matricule";
        }
        public void remplirCmbTypeV()
        {
            cmbVéhic_Type.Items.Add("Camion");
            cmbVéhic_Type.Items.Add("Traks ");
        }
        //-------------------------------- Remplissage Utilisé BindingSource ----------------------------
         
        public void Afficher_week()
        {
           
            d9.DataSource = db.aff_Week();
            gridWeek.DataSource = d9;
        }
        public void AfficherPintage()
        {
            d8.Clear();
            d8.DataSource = db.poitage.ToList();
            gridPointage.DataSource = d8;
        }
         public void RemplirCmbOuvrier()
        {
            cmbP_Ouv.DataSource = db.Ouvrier.ToList();
            cmbP_Ouv.DisplayMember = "nom";
            cmbP_Ouv.ValueMember = "Cin";
        }
		public void remplirarticle()
		{
            
			gridArticle.DataSource = db.Affiche_Article();
		}
		public void remlpirFAmille(ComboBox b)
		{
			b.DataSource = db.Famille.ToList();
			b.DisplayMember = "NomFa";
			b.ValueMember = "Num_Fa";
		}
        public void RemplirCmbTypeCl()
        {
            //cmbTypeCl.Items.Add("Barette");
            //cmbTypeCl.Items.Add("Normale");

        }
        public void RemplirCmbTypeTobia()
        {
            cmbTypeTobia.DataSource = db.Article.Where(x => x.Num_Fa == 8).ToList();
            cmbTypeTobia.DisplayMember = "Num_At";            
        }
        public void RemplirCmbWeek()
        {
            cmbColl_week.DataSource = db.Week.ToList();
            cmbColl_week.DisplayMember = "NumWeek";
            


        }
        public void RemplirCmbCollection()
        {
            cmbPoint_Collec.DataSource = db.afficher_Collection();
            cmbPoint_Collec.DisplayMember = "NumCollection";
        }

        //------------------------------------ Affichage des table ---------------------------
       
        public void AfficherCollection()
        {

            d7.DataSource = db.afficher_Collection();
            gridCollection.DataSource = d7;
        }
       
        
        public void afficherOuvrier()
        {
            d12.DataSource = db.aff_ouvrier();
            GridOuvrier.DataSource = d12;
        }

        public void afficherclient()
		{
			d.Clear();
			d.DataSource = db.Affiche_Client();
			gridCLient.DataSource = d;
		}
		public void aficher_Fournisseur()
		{
			d2.DataSource = db.Affiche_Fournisseur();
			gridFOURN.DataSource = d2;

		}
		public void afficheCommandeAchat()
		{
			d3.DataSource = db.Affiche_CmdA();
			gridComdAch.DataSource = d3;

		}

		public void ComboNumArticle()
		{
			cmbDTCmdA_idart.DataSource = db.Article.ToList();
			cmbDTCmdA_idart.DisplayMember = "Designation";
			cmbDTCmdA_idart.ValueMember = "Num_At";

		}
		public void NumCommandAchat()
		{
			cmbDTCmdA_idcmd.DataSource = db.CommandeAch.ToList();
			cmbDTCmdA_idcmd.DisplayMember = "Num_Cmd";
			cmbDTCmdA_idcmd.ValueMember = "Num_Cmd";

		}
		public void ViderClientChamps()
		{
			txtClnom.Text = txtCladress.Text = txtCLville.Text = txtCLtele.Text =txtCLemail.Text= "";
		}
		public void ViderFournisseurChamps()
		{
			txtFORnom.Text = txtFORville.Text =txtFORadress.Text = txtFORtele.Text = txtFORemail.Text = "";
		}
        //---------------------------- Save File Function --------------------------------
        public void SaveFile(string FileName)
        {
            try
            {
                string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                string f1 = path + "\\Document\\" + FileName;
                string fn = Path.GetFileName(path + "\\Document\\"+FileName);
                string f2 = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = fn;
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    f2 = sfd.FileName;

                }
                System.IO.File.Copy(f1, f2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------- Traitement Code De CLIENT -------------------------
        private void btnCLadd_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtClnom.Text != "" && txtCLville.Text != "" && txtCladress.Text != "" && txtCLtele.Text != "" && txtCLemail.Text != "")
				{
					db.Ajouter_Client(txtClnom.Text, txtCLville.Text, txtCladress.Text, txtCLtele.Text, txtCLemail.Text, 0, txtCLice.Text);

					MessageBox.Show("Client Bien Ajouter ", "ajouter Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
					gridCLient.DataSource = db.Affiche_Client();
					ViderClientChamps();
                    remplirClient(cmbCmdV_client);
                    remplirClient(cmbFact_CL);
                    //remplirClient(cmbFact_CL2);
                    
                    RemplirCmbClientLoc();
                    remplirChantierOvr();

                }
				else
				{
					MessageBox.Show("Veillez remplir les champs !! ", "Champ Vide ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				
			}
			catch
			{
				MessageBox.Show("Error : Client N'a pas ete ajouter !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		
		private void txtCLdelete_Click(object sender, EventArgs e)
		{
			int index;
			index = gridCLient.CurrentRow.Index;

			try
			{

				int numClient = int.Parse(gridCLient.Rows[index].Cells[0].Value.ToString());
				DialogResult confirm = MessageBox.Show("Voulez vou vraiment supprimer", "Supprimer", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{

					db.Supprimer_Client(numClient);
					MessageBox.Show("Client Bien Supprimer ", "Supprimer Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
					gridCLient.DataSource = db.Affiche_Client();
                    remplirClient(cmbCmdV_client);
                    remplirClient(cmbFact_CL);
                    RemplirCmbClientLoc();
                    remplirChantierOvr();


                }
				
			}
			catch
			{
				MessageBox.Show("Error : Client N'a pas été Supprimé !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		//Client Clt1 = new Client();

		private void txtCLadite_Click(object sender, EventArgs e)
		{
			int index;
			index = gridCLient.CurrentRow.Index;
			int numClient = int.Parse(gridCLient.Rows[index].Cells[0].Value.ToString());

			try
			{
				string nom = txtClnom.Text;
				string adresse = txtCladress.Text;
				string tele = txtCLtele.Text;
				string ville = txtCLville.Text;
				string email = txtCLemail.Text;
				string ice = txtCLice.Text;
				if (txtClnom.Text == "" && txtCladress.Text == "" && txtCLtele.Text == "" && txtCLville.Text == "" && txtCLemail.Text == "" && txtCLice.Text == "")
				{
					MessageBox.Show("Veillez Remplir Tous Les Champs  !!");
				}
				else
				{
					DialogResult confirm = MessageBox.Show("Voulez vous vraiment Modifier", "Modification Client", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
					if (confirm == DialogResult.OK)
					{

						db.Modifier_Client(numClient, nom, ville, adresse, tele, email, ice);
						MessageBox.Show("Client Bien Modifier ", "Modifier Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
						gridCLient.DataSource = db.Affiche_Client();
						ViderClientChamps();
                        remplirClient(cmbCmdV_client);
                        remplirClient(cmbFact_CL);
                        RemplirCmbClientLoc();
                        remplirChantierOvr();
                    }
				}
			}
			catch
			{
				MessageBox.Show("Client n'existe pas !!! ");
			}
		}

		private void txtCLfind_OnValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtCLfind.Text == "")
				{
					//afficherclient();
					gridCLient.DataSource = db.Client.ToList();
				}
				else
				{
					gridCLient.DataSource = db.Chereher_Client(txtCLfind.Text);
				}

			}
			catch
			{
				MessageBox.Show("Client n'existe pas !!! ");
			}
		}

		private void txtCLcherch_Click(object sender, EventArgs e)
		{
	
				int index = gridCLient.CurrentRow.Index;
				int numClt = int.Parse(gridCLient.Rows[index].Cells[0].Value.ToString());
				txtClnom.Text = gridCLient.Rows[index].Cells[1].Value.ToString();
				txtCladress.Text = gridCLient.Rows[index].Cells[2].Value.ToString();
				txtCLtele.Text = gridCLient.Rows[index].Cells[3].Value.ToString();
				txtCLville.Text = gridCLient.Rows[index].Cells[4].Value.ToString();
				txtCLemail.Text = gridCLient.Rows[index].Cells[5].Value.ToString();
				txtCLice.Text = gridCLient.Rows[index].Cells[7].Value.ToString();
			
			
		
		}

		//---------------------------- Traitement Code De FOURNISSEUR -------------------------
		private void btnFORadd_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtFORnom.Text != "" && txtFORville.Text != "" && txtFORadress.Text != "" && txtFORtele.Text != "" && txtFORemail.Text != "")
				{
					db.Ajouter_Fournisseur(txtFORnom.Text, txtFORville.Text, txtFORadress.Text, txtFORtele.Text, txtFORemail.Text);
					aficher_Fournisseur();
					db.SaveChanges();
					MessageBox.Show("Fournisseur Bien Ajouter ", "ajouter Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
					ViderFournisseurChamps();
                    remplirComboFournisseur(cmbComdAfourn);
                }
				else
				{
					MessageBox.Show("Veillez remplir les champs !! ", "Champ Vide ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

			}
			catch
			{
				MessageBox.Show("Error : Inpossible d'ajouter Fournisseur ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnFORdelete_Click(object sender, EventArgs e)
		{
			int index;
			index = gridFOURN.CurrentRow.Index;

			try
			{

				int numFr = int.Parse(gridFOURN.Rows[index].Cells[0].Value.ToString());
				DialogResult confirm = MessageBox.Show("Voulez vou vraiment supprimer", "Supprimer", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{

					db.Supprimer_Fournisseur(numFr);
					db.SaveChanges();
					aficher_Fournisseur();
                    remplirComboFournisseur(cmbComdAfourn);
                    MessageBox.Show("Client Bien Supprimer ", "Supprimer Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
			catch
			{
				MessageBox.Show("Error : Client N'a pas été Supprimé !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnFORedite_Click(object sender, EventArgs e)
		{
			int index = gridFOURN.CurrentRow.Index;
			int numFR = int.Parse(gridFOURN.Rows[index].Cells[0].Value.ToString());

			try
			{
                if (txtFORnom.Text != "" && txtFORville.Text != "" && txtFORadress.Text != "" && txtFORtele.Text != "" && txtFORemail.Text != "")
                {
                    DialogResult confirm = MessageBox.Show("Voulez vous vraiment Modifier", "Modification Client", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (confirm == DialogResult.OK)
                    {
                        db.Modifier_Fournisseur(numFR, txtFORnom.Text, txtFORville.Text, txtFORadress.Text, txtFORtele.Text, txtFORemail.Text);
                        MessageBox.Show("Fournisseur Bien Modifier ", "Modifier Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        aficher_Fournisseur();
                        ViderFournisseurChamps();
                        remplirComboFournisseur(cmbComdAfourn);

                    }
                }
                else
                {
                    MessageBox.Show("Remplir tous les champs !");
                }
				

			}
			catch
			{
				MessageBox.Show("Client n'existe pas !!! ");
			}
		}

		private void txtFORcherch_OnValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtFORcherch.Text == "")
				{
					aficher_Fournisseur();
				}
				else
				{
					gridFOURN.DataSource = db.Chereher_Fournisseur(txtFORcherch.Text);
				}

			}
			catch
			{
				MessageBox.Show("Fournisseur n'existe pas !!! ");
			}
		}

		//---------------------------- Traitement Code De COMMANDE ACHAT -------------------------

		private void btnComdAadd_Click(object sender, EventArgs e)
		{
			try

			{
				db.Ajouter_CmdAchat(int.Parse(DateTime.Now.ToString("yyMMddHHss")), dteComdAdateA.Value, txtComdAidfact.Text, int.Parse(cmbComdAfourn.SelectedValue.ToString()));
				afficheCommandeAchat();
				MessageBox.Show("Commande Bien Ajouter !!", "Ajouter Commande Achat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                remplirarticle();
                NumCommandAchat();
                
                NumCommandAchat();
               

            }
			catch
			{
				MessageBox.Show("Error : Impossible d'Ajouter Commande Achat");
			}
		}

		private void txtComdAcherch_OnValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtComdAcherch.Text == "")
				{
					afficheCommandeAchat();
				}
				else
				{
					gridComdAch.DataSource = db.Chereher_CommandeAch(txtComdAcherch.Text);
				}

			}
			catch
			{
				MessageBox.Show("Error 404 ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnComdAdelet_Click(object sender, EventArgs e)
		{
			int index = gridComdAch.CurrentRow.Index;
			int numCmdAchat = int.Parse(gridComdAch.Rows[index].Cells[1].Value.ToString());
			try
			{
				DialogResult confirm = MessageBox.Show("Achat Bien Supprimer ", "Suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{
					db.Supprimer_CmdA(numCmdAchat);
					afficheCommandeAchat();
                    NumCommandAchat();
                    remplirarticle();
                    MessageBox.Show("Achat Bien Supprimer ", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
			catch
			{
				MessageBox.Show("Inpossible de  Supprimer Commande ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnComdAedit_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult confirm = MessageBox.Show("Voulez-vous Vraiment Modifier La commande Numero : " + txtComdAidcmd.Text,
													 "Modification", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{


					db.Modifier_CommandeAch(int.Parse(txtComdAidcmd.Text), dteComdAdateA.Value, txtComdAidfact.Text, int.Parse(cmbComdAfourn.SelectedValue.ToString()));
					afficheCommandeAchat();
					MessageBox.Show("Commande Bien Modifier !!", "Modification ", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtComdAidcmd.Text = "";
					txtComdAidfact.Text = "";
					dteComdAdateA.Value = DateTime.Now;
                    NumCommandAchat();
                    remplirarticle();
                }
			}
			catch
			{
				MessageBox.Show("Inpossible de  Modifier Commande ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnComdAfind_Click(object sender, EventArgs e)
		{
			try
			{
				
					int index = gridComdAch.CurrentRow.Index;
					txtComdAidcmd.Text = gridComdAch.Rows[index].Cells[1].Value.ToString();
					txtComdAidfact.Text = gridComdAch.Rows[index].Cells[3].Value.ToString();
					cmbComdAfourn.Text = gridComdAch.Rows[index].Cells[4].Value.ToString();
					dteComdAdateA.Value = DateTime.Parse(gridComdAch.Rows[index].Cells[2].Value.ToString());
				
			}
			catch
			{
				MessageBox.Show("selectonner une commande !");
			}
		}

		private void gridComdAch_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				int index = gridComdAch.CurrentRow.Index;
				int numCmd = int.Parse(gridComdAch.Rows[index].Cells[1].Value.ToString());
				d4.DataSource = db.Afficher_DetailCmdAch(numCmd);
				gridDTComdAch.DataSource = d4;
			}
			catch(Exception ex) { MessageBox.Show(ex.Message); }
		}
		//---------------------------- Traitement Code De *Détail* COMMANDE ACHAT -------------------------

		public void remplirDetailAchat(int numCmd)
		{
			d4.DataSource = db.Afficher_DetailCmdAch(numCmd);
			gridDTComdAch.DataSource = d4;
		}
		float somme = 0;

		private void btnDTCmdA_add_Click(object sender, EventArgs e)
		{

            int index = gridComdAch.CurrentRow.Index;
            int numCmd = int.Parse(gridComdAch.Rows[index].Cells[1].Value.ToString());

            try
			{
				//if (som + somme <= 5000)
				//{
					db.Ajouter_DetailsCmdAchat(numCmd,
											  cmbDTCmdA_idart.SelectedValue.ToString()
											 , float.Parse(txtDTCmdA_prix.Text),
											  float.Parse(txtDTCmdA_qte.Text));
					remplirDetailAchat(numCmd);
                remplirarticle();
                MessageBox.Show("Detail Commande Bien Ajouter !!", "Ajouter Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtDTCmdA_prix.Text = "";
					txtDTCmdA_qte.Text = "";
				}
				//else
				//{
				//	MessageBox.Show("Vous avez depasse 5000 DH par facture !\n Impossibl dajouter detail achat !!", "Ajouter Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);

				//}

			
			catch
			{
			MessageBox.Show("Impossible D'ajouter Detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnDTCmdA_delet_Click(object sender, EventArgs e)
		{
			int index = gridDTComdAch.CurrentRow.Index;
			int numCmd = int.Parse(gridDTComdAch.Rows[index].Cells[0].Value.ToString());
			string numArt = gridDTComdAch.Rows[index].Cells[1].Value.ToString();
			try
			{
				DialogResult confirm = MessageBox.Show("Volez-Vous vraiment Supprimer Detail achat ? ", "Suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{
					db.Supprimer_DetailAchat(numCmd, numArt);
					remplirDetailAchat(numCmd);
                    remplirarticle();
                    MessageBox.Show("Achat Bien Supprimer ", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}


			}
			catch
			{
				MessageBox.Show("Inpossible de  Supprimer Detail Commande Achat ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnDTCmdA_edit_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult confirm = MessageBox.Show("Voulez-vous Vraiment Modifier  detail commande Numero : " + cmbDTCmdA_idcmd.Text,
													 "Modification", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{


					db.Update_DetailsCmdAchat(int.Parse(cmbDTCmdA_idcmd.SelectedValue.ToString()),
										   cmbDTCmdA_idart.SelectedValue.ToString()
										  , float.Parse(txtDTCmdA_prix.Text),
										   float.Parse(txtDTCmdA_qte.Text));
					remplirDetailAchat(int.Parse(cmbDTCmdA_idcmd.SelectedValue.ToString()));
					MessageBox.Show("Commande Bien Modifier !!", "Modification ", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtDTCmdA_prix.Text = "";
					txtDTCmdA_qte.Text = "";

				}
			}
			catch
			{
				MessageBox.Show("Inpossible de  Modifier Commande ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		//---------------------------- Traitement Code De ARTICLE -------------------------
		private void btnArt_add_Click(object sender, EventArgs e)
		{
			try

			{
				db.Ajouter_Article(txtArt_ref.Text, 
                                   txtArt_desig.Text,
                                   float.Parse(txtArt_prixU.Text),
                                   float.Parse(txtArt_qte.Text), 
                                   int.Parse(txtArt_tva.Text),
                                   "null", 
                                   txtArt_unité.Text,
                                   int.Parse(cmbArt_cat.SelectedValue.ToString()));

				MessageBox.Show("Article Bien Ajouter !!", "Ajouter Article", MessageBoxButtons.OK, MessageBoxIcon.Information);
				remplirarticle();
                ComboNumArticle();

            }
			catch
			{
				MessageBox.Show("Error : Impossible d'Ajouter Article");
			}
		}

		private void btnArt_edit_Click(object sender, EventArgs e)
		{
			int index = gridArticle.CurrentRow.Index;
			string referArt = gridArticle.Rows[index].Cells[0].Value.ToString();

			try
			{
				DialogResult confirm = MessageBox.Show("Voulez-vous Vraiment Modifier L'Article : " + txtArt_ref.Text,
												 "Modification", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{

					var req = db.Article.Find(txtArt_ref.Text);
					req.Designation = txtArt_desig.Text;
					req.PU = float.Parse(txtArt_prixU.Text);
					req.QteS = int.Parse(txtArt_qte.Text);
					req.TVA = int.Parse(txtArt_tva.Text);
					req.Num_Fa = int.Parse(cmbArt_cat.SelectedValue.ToString());
					req.unite = txtArt_unité.Text;
					db.SaveChanges();
					remplirarticle();
                    ComboNumArticle();

                    MessageBox.Show("Commande Bien Modifier !!", "Modification ", MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
			}
			catch
			{
				MessageBox.Show("Inpossible de  Modifier Commande ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnArt_find_Click(object sender, EventArgs e)
		{
			try
			{
				int index = gridArticle.CurrentRow.Index;
				txtArt_ref.Text = gridArticle.Rows[index].Cells[0].Value.ToString();
				txtArt_ref.Enabled = false;
				txtArt_desig.Text = (gridArticle.Rows[index].Cells[1].Value.ToString());
				txtArt_prixU.Text = gridArticle.Rows[index].Cells[2].Value.ToString();
				txtArt_qte.Text = gridArticle.Rows[index].Cells[3].Value.ToString();
				txtArt_tva.Text = gridArticle.Rows[index].Cells[4].Value.ToString();
				txtArt_unité.Text = gridArticle.Rows[index].Cells[5].Value.ToString();
				//int numfamille = int.Parse(GridViewArticle.Rows[index].Cells[6].Value.ToString());
				//var ress = db.Famille.Where(x => x.Num_Fa == numfamille).FirstOrDefault();
				cmbArt_cat.Text = gridArticle.Rows[index].Cells[6].Value.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		Article c;

		private void btnArt_delet_Click(object sender, EventArgs e)
		{
			int index = gridArticle.CurrentRow.Index;
			string numArt = gridArticle.Rows[index].Cells[0].Value.ToString();
			try
			{
				DialogResult confirm = MessageBox.Show("Voulez-Vous Vraiment Supprimer Article ", "Suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{
					c = db.Article.Find(numArt);
					db.Article.Remove(c);
					db.SaveChanges();
					remplirarticle();
                    ComboNumArticle();

                    MessageBox.Show("Article Bien Supprimer ", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
			catch
			{
				MessageBox.Show("Inpossible de  Supprimer Commande ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void txtArt_cherch_OnValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtArt_cherch.Text == "")
				{
					gridArticle.DataSource = db.Affiche_Article();
				}
				else
				{
					gridArticle.DataSource = db.Chereher_Article(txtArt_cherch.Text);
				}

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//---------------------------- Traitement Code De COMMANDE VENTE -------------------------

		public float sum(int num)
		{
			var req = (from x in db.DetailVente where x.Num_CmdV == num select (x.PrixV * x.Qte + x.PrixV * x.Qte * x.TVA)).Sum();
			if (req != null)
			{
				float somme = float.Parse(req.ToString());
				return somme;
			}
			else
			{
				return 0;
			}
		}

		private void gridComdV_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int index = gridComdV.CurrentRow.Index;
			int num = int.Parse(gridComdV.Rows[index].Cells[0].Value.ToString());
            Afficher_Detaill_vente(num);
			//var req = db.DetailVente.Where(x => x.Num_CmdV == num).ToList();
			//gridDTCmdV.Rows.Clear();
			//for (int i = 0; i < req.Count; i++)
			//{
			//	gridDTCmdV.Rows.Add(req[i].Num_CmdV, req[i].Num_At, req[i].Qte, req[i].Remise, req[i].PrixV, req[i].TVA);
			//}
			try
			{
				float somme = float.Parse(db.GetMontantByCommande(num).First().Value.ToString());

				txtDTCmdV_prTTL.Text = somme.ToString();
			}
			catch
			{
				txtDTCmdV_prTTL.Text = "0";
			}
		}

		CommandeVente CmdV = new CommandeVente();
		private void btnCmdV_add_Click(object sender, EventArgs e)
		{
			//int numCmdV = int.Parse(txtCmdV_idcmdv.Text);
			Boolean isfacture = false;
			//if (bunifuCheckbox1.Checked == true)
			//{
			//	isfacture = true;
			//}
			string nom = cmbCmdV_client.Text;
			int NumClient = db.Client.Where(x => x.NomC == nom).Select(x => x.Num_Clt).First();

			try
			{
				//int test = db.CommandeVente.Where(x => x.Num_CmdV == numCmdV).Count();
				DialogResult confirm = MessageBox.Show("voulez Vous Vraiment Ajouter Commande ?", "Ajouter Comande Vente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				//if (test == 0)
				//{
					if (confirm == DialogResult.OK)
					{
                        //CmdV.Num_CmdV = numCmdV;
                        //CmdV.DateCmd = dteCmdV_dteV.Value;
                        //CmdV.IsFacture = isfacture;
                        //CmdV.Num_Clt = NumClient;
                        //db.CommandeVente.Add(CmdV);
                        //db.SaveChanges();
                        db.Ajouter_CommandeVente(int.Parse(DateTime.Now.ToString("yyMMddHHss")), dteCmdV_dteV.Value, isfacture, NumClient);
						remplirGridVent();
                        remplirarticle();
                    MessageBox.Show("Comande Bien Ajouter ", " Ajouter Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				//}
				//else
				//{
				//	MessageBox.Show("Commande Existe Deja !! ", " Ajouter Commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, " Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCmdV_adit_Click(object sender, EventArgs e)
		{
			int index = gridComdV.CurrentRow.Index;
			int numCmdV = int.Parse(gridComdV.Rows[index].Cells[0].Value.ToString());
			DialogResult confirm = MessageBox.Show("voulez Vous Vraiment modifier Commande ?", "Ajouter Comande Vente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

			try
			{
				if (confirm == DialogResult.OK)
				{
					//var req = db.CommandeVente.Find(numCmdV);
					DateTime DateCmd = dteCmdV_dteV.Value;
					int Num_Clt = int.Parse(cmbCmdV_client.SelectedValue.ToString());
                    db.Modifier_CommandeVente(numCmdV, DateCmd, Num_Clt);
					//db.SaveChanges();
					MessageBox.Show("Commande vente Bien Modifier", " Modifier Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
					remplirGridVent();
                    remplirarticle();
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, " Modifier Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnCmdV_delet_Click(object sender, EventArgs e)
		{
			int index = gridComdV.CurrentRow.Index;
			int numCmdV = int.Parse(gridComdV.Rows[index].Cells[0].Value.ToString());

			try
			{
				DialogResult confirm = MessageBox.Show("voulez Vous Vraiment Ajouter Commande ?", "Ajouter Comande Vente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (confirm == DialogResult.OK)
				{
					db.Supprimer_CmdV(numCmdV);
					MessageBox.Show("Comande Bien Supprimer ", " Supprimer Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
					remplirGridVent();
                    remplirarticle();
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, " Supprimer Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnCmdV_find_Click(object sender, EventArgs e)
		{
			int index = gridComdV.CurrentRow.Index ;
			try
			{
				
				txtCmdV_idcmdv.Text = gridComdV.Rows[index].Cells[0].Value.ToString();
				dteCmdV_dteV.Value = DateTime.Parse( gridComdV.Rows[index].Cells[1].Value.ToString());
				cmbCmdV_client.Text= gridComdV.Rows[index].Cells[3].Value.ToString();
				//if (gridComdV.Rows[index].Cells[2].Value.ToString()=="True") { bunifuCheckbox1.Checked = true; } else { bunifuCheckbox1.Checked = false; }
				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtCmdV_cherch_OnValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtCmdV_cherch.Text == "")
				{
					gridComdV.DataSource = db.Affiche_CmdV();

				}
				else
				{
					gridComdV.DataSource = db.Chereher_CommandeVente(txtCmdV_cherch.Text);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, " cherccher Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		//---------------------------- Traitement Code De *Détail* COMMANDE VENTE -------------------------


		private void cmbDTCmdV_catAr_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string valeur = cmbDTCmdV_catAr.Text;
				var req = from x in db.Article
						  from y in db.Famille
						  where x.Num_Fa == y.Num_Fa && y.NomFa == valeur
						  select x.Num_At;
				cmbDTCmdV_ref.DataSource = req.ToList();
                cmbDTCmdV_ref.DisplayMember = "Num_At";

            }
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void cmbDTCmdV_ref_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string refe = cmbDTCmdV_ref.Text;
				var req = db.Article.Where(x => x.Num_At == refe).Select(x => x.TVA).FirstOrDefault();
				txtDTCmdV_tva.Text = req.ToString();
			}
			catch(Exception ex) { MessageBox.Show(ex.Message); }
		}

		public float ToataleVente = 0;

		DetailVente cv;
		private void btnDTCmdV_add_Click(object sender, EventArgs e)
		{
			int index = gridComdV.CurrentRow.Index;
			int num = int.Parse(gridComdV.Rows[index].Cells[0].Value.ToString());
            try
            {

                //cv = new DetailVente();
                //cv.Num_At = cmbDTCmdV_ref.Text;
                //cv.Num_CmdV = num;
                //cv.PrixV = float.Parse(txtDTCmdV_prixV.Text);
                //cv.Qte = int.Parse(txtDTCmdV_qte.Text);
                //cv.TVA = int.Parse(txtDTCmdV_tva.Text);
                //cv.Remise = int.Parse(txtDTCmdV_remis.Text);
                string Num_At = cmbDTCmdV_ref.Text;

             var stock = db.Article.Where(x => x.Num_At == Num_At).Select(x => x.QteS);
            MessageBox.Show(stock.First().ToString());
            if (stock.First() >= int.Parse(txtDTCmdV_qte.Text))
            {
                //db.DetailVente.Add(cv);
                //db.SaveChanges();
                db.Ajouter_DetaillCmdv(num, cmbDTCmdV_ref.Text, float.Parse(txtDTCmdV_qte.Text),
                                       float.Parse(txtDTCmdV_remis.Text), float.Parse(txtDTCmdV_prixV.Text), int.Parse(txtDTCmdV_tva.Text));
                MessageBox.Show("Detail Vente Bien Ajouter !!");
                gridDTCmdV.DataSource = db.Dvente_Cmd(num);


                //gridDTCmdV.Rows.Add(num, cmbDTCmdV_ref.Text, txtDTCmdV_qte.Text, txtDTCmdV_remis.Text, txtDTCmdV_prixV.Text, txtDTCmdV_tva.Text);
                //float somme = float.Parse(db.GetMontantByCommande(num).First().Value.ToString());
                //txtDTCmdV_prTTL.Text = somme.ToString();
                //remplirarticle();
            }
            else
            {
                MessageBox.Show("Inpossible de vendre cette Quantite \n Stock :" + stock.First().ToString(), "Detaille Vente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("verifier si l article existe deja ou bien selectionner !", "detaille vente ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnDTCmdV_delet_Click(object sender, EventArgs e)
		{
			try
			{

				int index = gridDTCmdV.CurrentRow.Index;
				int index2 = gridComdV.CurrentRow.Index;
				string reff = gridDTCmdV.Rows[index].Cells[1].Value.ToString();
				int num = int.Parse(gridDTCmdV.Rows[index].Cells[0].Value.ToString());
				var req = db.DetailVente.Where(x => x.Num_At == reff).FirstOrDefault();
				DialogResult confirm = MessageBox.Show("voulez vous supprimer detaille vente ?", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{
					db.Supprimer_DetailVente(num, reff);
					//db.DetailVente.Remove(req);
					//db.SaveChanges();
					MessageBox.Show("detail Supprimer", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Afficher_Detaill_vente(num);
                    float somme = sum(num);
					txtDTCmdV_prTTL.Text = somme.ToString();
                    remplirarticle();

                }

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
		}

		private void btnDTCmdV_find_Click(object sender, EventArgs e)
		{
			try
			{
				int index = gridDTCmdV.CurrentRow.Index;
				cmbDTCmdV_ref.Text = gridDTCmdV.Rows[index].Cells[1].Value.ToString();
				txtDTCmdV_qte.Text = gridDTCmdV.Rows[index].Cells[2].Value.ToString();
				txtDTCmdV_remis.Text = gridDTCmdV.Rows[index].Cells[3].Value.ToString();
				txtDTCmdV_prixV.Text = gridDTCmdV.Rows[index].Cells[4].Value.ToString();
				txtDTCmdV_tva.Text = gridDTCmdV.Rows[index].Cells[5].Value.ToString();
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void btnDTCmdV_edit_Click(object sender, EventArgs e)
		{
			int index = gridDTCmdV.CurrentRow.Index;
			int num = int.Parse(gridDTCmdV.Rows[index].Cells[0].Value.ToString());
			string reff = gridDTCmdV.Rows[index].Cells[1].Value.ToString();

			try
			{
				var cv = db.DetailVente.Where(x => x.Num_CmdV == num && x.Num_At == reff).First();
				cv.Num_At = cmbDTCmdV_ref.Text;
				cv.Num_CmdV = num;
				cv.PrixV = float.Parse(txtDTCmdV_prixV.Text);
				cv.Qte = int.Parse(txtDTCmdV_qte.Text);
				cv.TVA = int.Parse(txtDTCmdV_tva.Text);
				cv.Remise = int.Parse(txtDTCmdV_remis.Text);
				db.SaveChanges();
				MessageBox.Show("Detail Vente Bien Modfier !!");
				gridDTCmdV.Rows[index].Cells[2].Value = cv.Qte;
				gridDTCmdV.Rows[index].Cells[3].Value = cv.Remise;
				gridDTCmdV.Rows[index].Cells[4].Value = cv.PrixV;
				gridDTCmdV.Rows[index].Cells[5].Value = cv.TVA;
                remplirarticle();
                txtDTCmdV_prTTL.Text = sum(num).ToString();
                Afficher_Detaill_vente(num);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//---------------------------- Traitement Code De FACTURE -------------------------
		private void btnFact_delet_Click(object sender, EventArgs e)
		{
			int index = GridFacture.CurrentRow.Index;
			int numfac = int.Parse(GridFacture.Rows[index].Cells[1].Value.ToString());
			try
			{
				DialogResult confirm = MessageBox.Show("Voulez-vus Vraiment Supprimer FActure ?", "Supprimer FActure", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (confirm == DialogResult.OK)
				{
					var req = db.Facture.Where(x => x.Num_Fac == numfac).FirstOrDefault();
					db.Facture.Remove(req);
					db.SaveChanges();
					MessageBox.Show(" FActure Bien Supprimer ", "Supprimer FActure", MessageBoxButtons.OK, MessageBoxIcon.Information);
					RemplirFacture();
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Supprimer FActure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void txtFact_cherch_OnValueChanged(object sender, EventArgs e)
		{
			//try
			//{
			//	if (txtFact_cherch.Text == "")
			//	{
			//		RemplirFacture();
			//	}
			//	else
			//	{
			//		GridFacture.DataSource = db.Chercher_FActuree(txtFact_cherch.Text);
			//	}
			//}
			//catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			RemplirFacture();
		}

		private void btnCmdV_Acct_Click(object sender, EventArgs e)
		{
			txtDTCmdV_remis.Text = txtDTCmdV_tva.Text = txtDTCmdV_qte.Text = txtDTCmdV_prixV.Text = "";
			remplirGridVent();
		}

		private void CloseDetail_Click(object sender, EventArgs e)
		{
			PanelDetailFActure.Visible = false;
		}

		public void FActure2(int numFac, DataGridView Grid)
		{
			var req = from a in db.Article
					  from cv in db.CommandeVente
					  from dv in db.DetailVente
					  from cl in db.Client
					  from df in db.DetailFac
					  from f in db.Facture
					  from dr in db.DetailReg
					  from reg in db.Reglement
					  from mr in db.ModeRG
					  from b in db.Banque
					  where
							a.Num_At == dv.Num_At &&
							cv.Num_CmdV == dv.Num_CmdV &&
							cl.Num_Clt == cv.Num_CmdV &&
							df.Num_CmdV == cv.Num_CmdV &&
							dr.Num_CmdV == cv.Num_CmdV &&
							f.Num_Fac == df.Num_Fac &&
							reg.Num_Reg == dr.Num_Reg &&
							mr.Num_ModeReg == reg.Num_ModeReg &&
							b.Num_Bq == reg.Num_Bq &&
							f.Num_Fac == numFac

					  select new
					  {
						  a.QteS,
						  a.num_fardeau,
						  a.Designation,
						  a.PU,
						  f.Num_Fac,
						  f.DateFac,
						  cl.NomC,
						  mr.Modalite,
						  b.NomB,
						  reg.NumChe
					  };
			Grid.DataSource = req.ToList();

		}

		private void GridFacture_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				PanelDetailFActure.Visible = true;

				int index = GridFacture.CurrentRow.Index;

				int numfac = int.Parse(GridFacture.Rows[index].Cells[1].Value.ToString());

				DetailFAccc.DataSource = db.Facture3(numfac);

				//FActure2(numfac, DetailFAccc);
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void btnFact_finDTE_Click(object sender, EventArgs e)
		{
			try
			{
				RemplirFActureBYDate(dteFact_dteDB.Value, dteFact_dteFin.Value);

			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void btnFact_acctua_Click(object sender, EventArgs e)
		{
			RemplirFacture();
		}

		private void btnFact_finCl_Click(object sender, EventArgs e)
		{
			try
			{
				int numClient = int.Parse(cmbFact_CL.SelectedValue.ToString());
				RemplirFactureBYClient(numClient);
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void btnFact_finCheq_Click(object sender, EventArgs e)
		{
			RemplirFactureBYCheque(txtFact_ncheq.Text);
		}

		private void btnFact_impr_Click(object sender, EventArgs e)
		{
            // int num facture 
            int index = GridFacture.CurrentRow.Index;
            int facture = int.Parse(GridFacture.Rows[index].Cells[1].Value.ToString());
            Decimal montant = Convert.ToDecimal(float.Parse(GridFacture.Rows[index].Cells[7].Value.ToString()));
            int tva = int.Parse(GridFacture.Rows[index].Cells[5].Value.ToString());
            float remise = float.Parse(GridFacture.Rows[index].Cells[4].Value.ToString());
			FactureCmdV Fac = new FactureCmdV(facture, montant,tva,remise);
			Fac.Show();
		}

		private void btnDTCmdA_fin_Click(object sender, EventArgs e)
		{
			try
			{
				int index = gridDTComdAch.CurrentRow.Index;
				cmbDTCmdA_idart.Text = (gridDTComdAch.Rows[index].Cells[2].Value.ToString());
				cmbDTCmdA_idcmd.Text = gridDTComdAch.Rows[index].Cells[0].Value.ToString();
				txtDTCmdA_prix.Text = gridDTComdAch.Rows[index].Cells[3].Value.ToString();
				txtDTCmdA_qte.Text = gridDTComdAch.Rows[index].Cells[4].Value.ToString();
				
			}
			catch
			{
				MessageBox.Show("selectonner une commande !");
			}
		}

		private void btnArt_AfficherFam_Click(object sender, EventArgs e)
		{
			panelFamiile.Visible = true; panelFamiile.Show();
		}

		private void bunifuImageButton31_Click(object sender, EventArgs e)
		{
			panelFamiile.Visible = false;
		}

		private void btnFamil_Add_Click(object sender, EventArgs e)
		{
			if (txtFamil_cat.Text == "")
			{
				MessageBox.Show("Remplir Nom Categorie !");
			}
			else
			{
				db.Ajouter_Famille(txtFamil_cat.Text);
				REmplirGridFamille();
                remlpirFAmille(cmbArt_cat);
                REmplirCategorie();
                MessageBox.Show("Article Bien Ajouter !");
			}
		}

		private void btnFamil_delete_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult d = MessageBox.Show("Voulez Vous Vraiment Supprimer Categorie ?", "Supprimer Categorie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (d == DialogResult.Yes)
				{
					int index = GridFamille.CurrentRow.Index;
					int numFamille = int.Parse(GridFamille.Rows[index].Cells[0].Value.ToString());
					db.Supprimer_Famille(numFamille);
					MessageBox.Show("Article Bien Ajouter !");
					REmplirGridFamille();
                    remlpirFAmille(cmbArt_cat);
                    REmplirCategorie();
                }
			}
			catch
			{
				MessageBox.Show("Selectionner Une Categorie !", "Supprimer Categorie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnFORfind_Click(object sender, EventArgs e)
		{
			try
			{
				int index = gridFOURN.CurrentRow.Index;
				txtFORnom.Text = gridFOURN.Rows[index].Cells[1].Value.ToString();
				txtFORville.Text = gridFOURN.Rows[index].Cells[2].Value.ToString();
				txtFORtele.Text = gridFOURN.Rows[index].Cells[4].Value.ToString();
				txtFORadress.Text = gridFOURN.Rows[index].Cells[3].Value.ToString();
				txtFORemail.Text = gridFOURN.Rows[index].Cells[5].Value.ToString();
			}
			catch
			{
				MessageBox.Show("selectionner Un fornisseur !");
			}
		}

		private void btnFact_add_Click(object sender, EventArgs e)
		{
            int idFActure = int.Parse(DateTime.Now.ToString("yyMMddHHss"));
            int numCmd=int.Parse(cmbFact_Cmdv.Text);
            try
            {
                var req = db.DetailFac.Where(x => x.Num_CmdV == numCmd).Count();
                if (req == 0)
                {
                    db.Ajouter_Facture(idFActure, DateFac.Value, float.Parse(RemiseFAc.Text),float.Parse(txtTvaFac.Text));
                    db.Ajouter_DetailFacture(idFActure, numCmd);
                    MessageBox.Show("La commande : " + numCmd.ToString() + " est bien creer !!", "Creation Fcature",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.UpdateIsFacturee(true, numCmd);
                    remplirGridVent();
                    RemplirFacture();
                }
                else
                {
                    MessageBox.Show("La commande : " + numCmd.ToString() + " est déja facturée !!", "Creation Fcature", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                   
                }

            }
            catch
            {

            }
		}

        private void btnOuvr_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous Vraiment Ajouter Ouvrier ?", "Ajouter Ouvrier", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d==DialogResult.Yes)
                {
                    db.Ajouter_Ouvrier(txtOuvr_cin.Text, txtOuvr_nom.Text, txtOuvr_prenom.Text, txtOuvr_tele.Text);
                     MessageBox.Show("Ouvier Bien Ajouter !!",
                     "Ajouter Ouvrier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    afficherOuvrier();
                    RemplirCmbOuvrier();
                    txtOuvr_cin.Text = "";
                    txtOuvr_nom.Text = "";
                    txtOuvr_prenom.Text = "";
                    txtOuvr_tele.Text = "";

                }


            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOuvr_delete_Click(object sender, EventArgs e)
        {
            int index = GridOuvrier.CurrentRow.Index;
            string cin = GridOuvrier.Rows[index].Cells[0].Value.ToString();
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous Vraiment supprimer Ouvrier ?",
                    "Supprimer Ouvrier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Ouvrier(cin);
                    MessageBox.Show("Ouvier Bien supprimer !!",
                    "Ajouter Ouvrier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    afficherOuvrier();
                    RemplirCmbOuvrier();


                }
            }
            catch
            {

            }
        }

        private void txtOuvr_cherch_OnValueChanged(object sender, EventArgs e)
        {
            if (txtArt_cherch.Text=="")
            {
                afficherOuvrier();
            }else
            {
               GridOuvrier.DataSource=db.Chercher_Ouvrier(txtOuvr_cherch.Text);
            }
        }

        private void btnOuvr_find_Click(object sender, EventArgs e)
        {
            int index = GridOuvrier.CurrentRow.Index;
            txtOuvr_cin.Text= GridOuvrier.Rows[index].Cells[0].Value.ToString();
            txtOuvr_nom.Text= GridOuvrier.Rows[index].Cells[1].Value.ToString();
            txtOuvr_prenom.Text= GridOuvrier.Rows[index].Cells[2].Value.ToString();
            txtOuvr_tele.Text= GridOuvrier.Rows[index].Cells[3].Value.ToString();
        }

        private void btnOuvr_edit_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous Vraiment Modifier Ouvrier ?", "Modifier Ouvrier", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_Ouvrier(txtOuvr_cin.Text, txtOuvr_nom.Text, txtOuvr_prenom.Text, txtOuvr_tele.Text);
                    MessageBox.Show("Ouvier Bien modifier !!",
                    "Modifier Ouvrier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    afficherOuvrier();
                    RemplirCmbOuvrier();
                    txtOuvr_cin.Text = "";
                    txtOuvr_nom.Text = "";
                    txtOuvr_prenom.Text = "";
                    txtOuvr_tele.Text = "";

                }
            }
            catch
            {

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void testStocke()
        {
            int nbItem1 = checkedListBox1.CheckedItems.Count;
            int nbItem2 = checkedListBox2.CheckedItems.Count;

            int somme=0;
            int somme1 = 0;
     
            var art2    = db.Article.Where(x => x.Num_At == "holicime").First();

            foreach (object item in checkedListBox1.CheckedItems)
            {
                Control text = panelCheckList.Controls[item.ToString()];
                string Num_Art = item.ToString();
                var art = db.Article.Where(x => x.Num_At == Num_Art).First().QteS;
                if (art >= ((art * int.Parse(text.Text)) / (art * 14)))
                {
                    somme++;
                }

            }
            foreach (object item in checkedListBox2.CheckedItems)
            {
                Control text = panelcheckList2.Controls[item.ToString()];
                string Num_Art = item.ToString();
                var art1 = db.Article.Where(x => x.Num_At == Num_Art).First();
                if( art1.QteS >= float.Parse(text.Text))
                {
                    somme1++;
                }
                
            }
             
              
            if (somme == nbItem1 && somme1== nbItem2 && art2.QteS >= float.Parse(txtColnbCoui.Text))
            {
                MessageBox.Show("Truee Somme : "+somme +" || Nbitem : "+ nbItem1 +"\n somme1 : "+somme1 + " || Nbitem2 : " + nbItem2);
                //mise a jour stock du sable, fere et cement
                
                //mise a jour stock sable 
                foreach (object item in checkedListBox1.CheckedItems)
                {
                    Control text = panelCheckList.Controls[item.ToString()];
                    string Num_Art = item.ToString();
                    var  art = db.Article.Where(x => x.Num_At == Num_Art).First();
                    art.QteS = art.QteS - ((art.QteS * int.Parse(text.Text)) / (art.QteS * 14));
                    Math.Round(art.QteS, 3);
                    db.SaveChanges();
                }

                //mise a jour stock fère

                foreach (object item in checkedListBox2.CheckedItems)
                {
                    Control text = panelcheckList2.Controls[item.ToString()];
                    string Num_Art = item.ToString();
                    var  art1 = db.Article.Where(x => x.Num_At == Num_Art).First();
                    art1.QteS = art1.QteS - float.Parse(text.Text);
                    Math.Round(art1.QteS, 3);
                    db.SaveChanges();
                }
                // mis A jour Stock Cement
                art2.QteS = art2.QteS - float.Parse(txtColnbCoui.Text);
                Math.Round(art2.QteS, 3);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show(" False Somme : " + somme + " || Nbitem : " + nbItem1 + "\n somme1 : " + somme1 + " || Nbitem2 : " + nbItem2);
            }


        }
        public void miseAjourStockSable()
        {
            testStocke();
            //Article art, art1, art2;
            //foreach (object item in checkedListBox1.CheckedItems)
            //{
            //    Control text = panelCheckList.Controls[item.ToString()];
            //    string Num_Art = item.ToString();
            //    art = db.Article.Where(x => x.Num_At == Num_Art).First();
            //    art.QteS = art.QteS - ((art.QteS * int.Parse(text.Text)) / (art.QteS * 14));
            //    Math.Round(art.QteS, 3);
            //    db.SaveChanges();
            //}
            //foreach (object item in checkedListBox2.CheckedItems)
            //{
            //    Control text = panelcheckList2.Controls[item.ToString()];
            //    string Num_Art = item.ToString();
            //    art1 = db.Article.Where(x => x.Num_At == Num_Art).First();
            //    art1.QteS = art1.QteS - float.Parse(text.Text);
            //    Math.Round(art1.QteS, 3);
            //    db.SaveChanges();
            //}
            //art2 = db.Article.Where(x => x.Num_At == "holicime").First();
            //art2.QteS = art2.QteS - float.Parse(txtColnbCoui.Text);
            //Math.Round(art2.QteS, 3);
            //db.SaveChanges();

        }
        private void btnColl_add_Click(object sender, EventArgs e)
        {
            float prix7eta, prix5encha;
            double prixJour;

            try
            {
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                string ErreurMessage = "stock insuffisant : \n ";
            int nbItem1 = checkedListBox1.CheckedItems.Count;
            int nbItem2 = checkedListBox2.CheckedItems.Count;

            int somme = 0;
            int somme1 = 0;

            var art2 = db.Article.Where(x => x.Num_At == "holicime").First();

            foreach (object item in checkedListBox1.CheckedItems)
            {
                Control text = panelCheckList.Controls[item.ToString()];
                string Num_Art = item.ToString();
                var art = db.Article.Where(x => x.Num_At == Num_Art).First().QteS;
                if (art >= ((art * int.Parse(text.Text)) / (art * 14)))
                {
                    somme++;
                }else
                {
                    ErreurMessage += Num_Art+" : "+art+"\n";
                }

            }
            foreach (object item in checkedListBox2.CheckedItems)
            {
                Control text = panelcheckList2.Controls[item.ToString()];
                string Num_Art = item.ToString();
                var art1 = db.Article.Where(x => x.Num_At == Num_Art).First();
                if (art1.QteS >= float.Parse(text.Text))
                {
                    somme1++;
                }
                else
                {
                    ErreurMessage += Num_Art + " : " + art1.QteS + "\n"; ;
                }

            }


            if (somme == nbItem1 && somme1 == nbItem2 && art2.QteS >= float.Parse(txtColnbCoui.Text))
            {
                MessageBox.Show("Truee Somme : " + somme + " || Nbitem : " + nbItem1 + "\n somme1 : " + somme1 + " || Nbitem2 : " + nbItem2);
                //mise a jour stock du sable, fere et cement

                //mise a jour stock sable 
                foreach (object item in checkedListBox1.CheckedItems)
                {
                    Control text = panelCheckList.Controls[item.ToString()];
                    string Num_Art = item.ToString();
                    var art = db.Article.Where(x => x.Num_At == Num_Art).First();
                    art.QteS = art.QteS - ((art.QteS * int.Parse(text.Text)) / (art.QteS * 14));
                    Math.Round(art.QteS, 3);
                    db.SaveChanges();
                }

                //mise a jour stock fère

                foreach (object item in checkedListBox2.CheckedItems)
                {
                    Control text = panelcheckList2.Controls[item.ToString()];
                    string Num_Art = item.ToString();
                    var art1 = db.Article.Where(x => x.Num_At == Num_Art).First();
                    art1.QteS = art1.QteS - float.Parse(text.Text);
                    Math.Round(art1.QteS, 3);
                    db.SaveChanges();
                }
                // mis A jour Stock Cement
                art2.QteS = art2.QteS - float.Parse(txtColnbCoui.Text);
                Math.Round(art2.QteS, 3);
                db.SaveChanges();
                //////////////////////////////////////////////================ ADD COLLECTION ==================/////////////////////////////////////////////////////
                //-------------------------------------------                 ----------------                  ---------------------------------------------------//
                        prix7eta = float.Parse(txtPrix7eta.Text) * int.Parse(txtColl_NbColl.Text);
                        if (radioButton1.Checked == true)
                        {
                            prix5encha = int.Parse(txtColnbCoui.Text) * float.Parse(txtCollPrixCement.Text);
                            Math.Round(prix5encha, 3);

                        }
                        else
                        {
                            prix5encha = 0;
                        }

                        prixJour = double.Parse(txtcollPriJour.Text);
                        DialogResult d = MessageBox.Show("Voulez Vous Vraiment Ajouter Collection ?", "Ajouter Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (d == DialogResult.Yes)
                        {
                            db.Ajouter_Collection(int.Parse(DateTime.Now.ToString("yyMMddHHss")),
                                              DateTime.Parse(dteColl_date.Value.ToShortDateString()),
                                              int.Parse(txtColl_NbColl.Text) * int.Parse(txtCollNbBrique.Text),
                                              int.Parse(txtColnbCoui.Text),
                                              int.Parse(cmbColl_week.Text),
                                              cmbTypeTobia.Text,
                                              int.Parse(txtColl_NbColl.Text),
                                              prix5encha + prix7eta + prixJour);
                            MessageBox.Show("Collection Bien Ajouter !!", "Ajouter Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            miseAjourStockSable();
                            AfficherCollection();
                            RemplirCmbCollection();
                            Afficher_week();
                            remplirarticle();

                            //txtColl_nombre.Text = ""; txtColl_Nbcem.Text = "";
                        }
                //-------------------------------------------------------------------------------------------------------------------------------------------------//
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                MessageBox.Show(ErreurMessage,"Ajouter Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                //-----------------------------------------------------------------------------------------------------------------------------------------------



                //  remplirarticle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ajouter Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnColl_delet_Click(object sender, EventArgs e)
        {
            int index = gridCollection.CurrentRow.Index;
            string num = gridCollection.Rows[index].Cells[0].Value.ToString();
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous Vraiment Supprimer Collection ?", "Supprimer Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Collection(num);
                    MessageBox.Show("Collection Bien Supprimer !!", "Supprimer Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherCollection();
                    remplirarticle();
                    RemplirCmbCollection();
                    Afficher_week();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Supprimer Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnColl_find_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int index = gridCollection.CurrentRow.Index;
            //    txtColl_nombre.Text = gridCollection.Rows[index].Cells[2].Value.ToString();
            //    dteColl_date.Value = DateTime.Parse(gridCollection.Rows[index].Cells[1].Value.ToString()); ;
            //    //cmbTypeCl.Text= gridCollection.Rows[index].Cells[3].Value.ToString();
            //    txtColl_Nbcem.Text = gridCollection.Rows[index].Cells[4].Value.ToString();
            //    cmbColl_week.Text = gridCollection.Rows[index].Cells[5].Value.ToString();
            //    cmbTypeTobia.Text = gridCollection.Rows[index].Cells[6].Value.ToString();
            //    txtColl_NbColl.Text = gridCollection.Rows[index].Cells[7].Value.ToString();
            //    txtColl_Prix.Text= gridCollection.Rows[index].Cells[8].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("impossible de modifier Collection avec champs vide \n svp supprimer et remlir tout les champs !");
            //}
      

        }

        private void btnColl_edit_Click(object sender, EventArgs e)
        {
            //int index = gridCollection.CurrentRow.Index;
            //int num = int.Parse(gridCollection.Rows[index].Cells[0].Value.ToString());
            //try
            //{
            //    DialogResult d = MessageBox.Show("Voulez Vous Vraiment Modifier Collection ?", "Modifier Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    if (d == DialogResult.Yes)
            //    {
            //        db.Update_Collection(num,DateTime.Parse(dteColl_date.Value.ToShortDateString()),
            //                          int.Parse(txtColl_nombre.Text),
            //                          "Barette",
            //                          int.Parse(txtColl_Nbcem.Text),
            //                          int.Parse(cmbColl_week.Text),
            //                          cmbTypeTobia.Text,
            //                          int.Parse(txtColl_NbColl.Text),
            //                          float.Parse(txtColl_Prix.Text));
            //        MessageBox.Show("Collection Bien Modifier !!", "Ajouter Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
            //        txtColl_nombre.Text = ""; txtColl_Nbcem.Text = "";
            //    }
            //    AfficherCollection();
            //    RemplirCmbCollection();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Modifier Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}

        }

        private void btnColl_cherch_OnValueChanged(object sender, EventArgs e)
        {

            if (btnColl_cherch.Text == "")
            {
               // AfficherCollection();
                gridCollection.DataSource =db.afficher_Collection();
            }else
            {
                gridCollection.DataSource = db.Chercher_collection(btnColl_cherch.Text); ;
            }
                
            
        }

        private void btnPoint_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Pointage ?", "Ajouter Pointage", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_Pointage(cmbP_Ouv.SelectedValue.ToString(),
                                    float.Parse(txtPoint_Avenc.Text),
                                   cmbPoint_Collec.Text);
                  MessageBox.Show("Pointage Bien Ajouter !!", "Ajouter Pointage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherPintage();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ajouter Pointage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPoint_delet_Click(object sender, EventArgs e)
        {
            int index = gridPointage.CurrentRow.Index;
            int num = int.Parse(gridPointage.Rows[index].Cells[0].Value.ToString());
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  supprimer Pointage ?", "Supprimer Pointage", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Pointage(num);
                    MessageBox.Show("Pointage Bien Supprimer !!", "Supprimer Pointage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherPintage();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Supprimer Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPoint_find_Click(object sender, EventArgs e)
        {
            int index = gridPointage.CurrentRow.Index;
            cmbP_Ouv.Text = gridPointage.Rows[index].Cells[1].Value.ToString();
            txtPoint_Avenc.Text= gridPointage.Rows[index].Cells[2].Value.ToString();
            cmbPoint_Collec.Text= gridPointage.Rows[index].Cells[3].Value.ToString();
        }

        private void btnPoint_edit_Click(object sender, EventArgs e)
        {
            int index = gridPointage.CurrentRow.Index;
            int num  = int.Parse(gridPointage.Rows[index].Cells[0].Value.ToString());
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Pointage ?", "Ajouter Pointage", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_Pointage(num,cmbP_Ouv.SelectedValue.ToString(),
                                    float.Parse(txtPoint_Avenc.Text),
                                    cmbPoint_Collec.Text);
                    MessageBox.Show("Pointage Bien Ajouter !!", "Ajouter Pointage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
                AfficherPintage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ajouter Pointage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWeek_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Week ?", "Ajouter Week", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_week(dteWeek_Deb.Value, dteWeek_Fin.Value, 0);
                    db.SaveChanges();
                    Afficher_week();
                    MessageBox.Show("Week Bien Ajouter !", "Ajouter Week", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWeek_delet_Click(object sender, EventArgs e)
        {
            int index = gridWeek.CurrentRow.Index;

            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Supprimer Week ?", "Supprimer Week", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    int num = int.Parse(gridWeek.Rows[index].Cells[1].Value.ToString());
                    db.Delete_week(num);
                    db.SaveChanges();
                    Afficher_week();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWeek_edit_Click(object sender, EventArgs e)
        {
            //int index = gridWeek.CurrentRow.Index;

            try
            {
                //int num = int.Parse(gridPointage.Rows[index].Cells[0].Value.ToString());

                DialogResult d = MessageBox.Show("Voulez Vous  Modifier Week ?", "Modifier Week", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_week(dteWeek_Deb.Value, dteWeek_Fin.Value,
                                   float.Parse(txtWeek_prix.Text),
                                   int.Parse(txtWeek_numW.Text));
                    db.SaveChanges();
                    Afficher_week();
                    MessageBox.Show("Week Bien Modifier !", "Modifier Week", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWeek_find_Click(object sender, EventArgs e)
        {
            int index = gridWeek.CurrentRow.Index;
            txtWeek_numW.Text = gridWeek.Rows[index].Cells[1].Value.ToString();
            txtWeek_prix.Text = gridWeek.Rows[index].Cells[4].Value.ToString();
            dteWeek_Deb.Value = DateTime.Parse(gridWeek.Rows[index].Cells[2].Value.ToString());
            dteWeek_Fin.Value = DateTime.Parse(gridWeek.Rows[index].Cells[3].Value.ToString());

        }

        private void gridWeek_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = gridWeek.CurrentRow.Index;
            try
            {
                int num = int.Parse(gridWeek.Rows[index].Cells[1].Value.ToString());
                var req = db.Collection.Where(x => x.NumWeek == num).ToList();
                gridListColl.DataSource = req;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridListColl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = gridListColl.CurrentRow.Index;

            try
            {
                string num = gridListColl.Rows[index].Cells[1].Value.ToString();
                var req = db.poitage.Where(x => x.NumCollection == num).ToList();
                gridListPointage.DataSource = req;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVéhic_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Vehicule ?",
                                                 "Ajouter Vehicule", 
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_Voiture(txtVéhic_mat.Text,txtVéhic_Mod.Text,cmbVéhic_Type.Text);
                    MessageBox.Show("Vehicule Bien Ajouter ! ?",
                                    "AJouter Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridVéhicule.DataSource = db.Voiture.ToList();
                }

                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVéhic_delet_Click(object sender, EventArgs e)
        {
            int index = gridVéhicule.CurrentRow.Index;
            try
            {
                string matricule = gridVéhicule.Rows[index].Cells[0].Value.ToString();
                DialogResult d = MessageBox.Show("Voulez Vous  Supprimer Vehicule ?",
                                                 "Supprimer Vehicule",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Voiture(matricule);
                    MessageBox.Show("Vehicule Bien Supprimer ! ?",
                                    "Supprimer Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridVéhicule.DataSource = db.Voiture.ToList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVéhic_find_Click(object sender, EventArgs e)
        {
            int index = gridVéhicule.CurrentRow.Index;
            txtVéhic_mat.Text = gridVéhicule.Rows[index].Cells[0].Value.ToString();
            txtVéhic_Mod.Text = gridVéhicule.Rows[index].Cells[1].Value.ToString();
            cmbVéhic_Type.Text = gridVéhicule.Rows[index].Cells[2].Value.ToString();
        }

        private void btnVéhic_edit_Click(object sender, EventArgs e)
        {
            int index = gridVéhicule.CurrentRow.Index;
            try
            {
                string matricule = gridVéhicule.Rows[index].Cells[0].Value.ToString();
                DialogResult d = MessageBox.Show("Voulez Vous  Modifier Vehicule ?",
                                                 "Modifier Vehicule",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_Voiture(txtVéhic_mat.Text,txtVéhic_Mod.Text,cmbVéhic_Type.Text);
                    MessageBox.Show("Vehicule Bien Modifier ! ?",
                                    "Modifier Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    afficherVehicule();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void afficherVehicule()
        {
            gridVéhicule.DataSource = db.aff_Vehicule();
        }
        private void txtVéhic_cherch_OnValueChanged(object sender, EventArgs e)
        {
            if (txtVéhic_cherch.Text == "")
            {
                AfficherVoiture();
            }
            else
            {
                gridVéhicule.DataSource = db.Chercher_Voiture(txtVéhic_cherch.Text);
            }
        }

        private void txtLoct_cherch_OnValueChanged(object sender, EventArgs e)
        {
            if (txtLoct_cherch.Text == "")
            {
                AfficherLocation();
            }
            else
            {
                
                
                gridLocation.DataSource = db.Chercher_Locatione(txtLoct_cherch.Text);
            }
        }

        private void btnLoct_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Location ?",
                                                 "Ajouter Location",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_Location(dteLoct_début.Value,dteLoct_Fin.Value,cmbLoc_matr.Text,
                        int.Parse(cmbLoc_Client.SelectedValue.ToString()), txtLoct_unite.Text,
                        float.Parse(txtLoct_Montant.Text), float.Parse(txtLoct_MontantPaye.Text));
                    MessageBox.Show("Locayion Bien Ajouter ! ?",
                                    "Location Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherLocation();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoct_delet_Click(object sender, EventArgs e)
        {
            int index = gridLocation.CurrentRow.Index;
            try
            {
                int num = int.Parse(gridVéhicule.Rows[index].Cells[0].Value.ToString());
                DialogResult d = MessageBox.Show("Voulez Vous  Supprimer Location ?",
                                                 "Supprimer Vehicule",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Location(num);
                    MessageBox.Show("Location Bien Supprimer ! ?",
                                    "Supprimer Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridVéhicule.DataSource = db.Voiture.ToList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoct_find_Click(object sender, EventArgs e)
        {
            int index = gridLocation.CurrentRow.Index;
            txtLoct_numL.Text   = gridLocation.Rows[index].Cells[0].Value.ToString();
            dteLoct_début.Value = DateTime.Parse(gridLocation.Rows[index].Cells[1].Value.ToString());
            dteLoct_Fin.Value   = DateTime.Parse(gridLocation.Rows[index].Cells[2].Value.ToString());
            cmbLoc_matr.Text    = gridLocation.Rows[index].Cells[3].Value.ToString();
            cmbLoc_Client.Text  = gridLocation.Rows[index].Cells[4].Value.ToString();
            txtLoct_unite.Text  = gridLocation.Rows[index].Cells[5].Value.ToString();
            txtLoct_Montant.Text   = gridLocation.Rows[index].Cells[6].Value.ToString();
            txtLoct_MontantPaye.Text = gridLocation.Rows[index].Cells[7].Value.ToString();
        }

        private void btnLoct_edit_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                DialogResult d = MessageBox.Show("Voulez Vous  Modifier Location ?",
                                                 "Modifier Location",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_Location(int.Parse(txtLoct_numL.Text),
                                       dteLoct_début.Value, dteLoct_Fin.Value, cmbLoc_matr.Text,
                                       int.Parse(cmbLoc_Client.Text), txtLoct_unite.Text,
                                       float.Parse(txtLoct_Montant.Text), float.Parse(txtLoct_MontantPaye.Text));
                    MessageBox.Show("Location Bien Modifier ! ?",
                                    "Location Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherLocation();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtChant_cherch_OnValueChanged(object sender, EventArgs e)
        {
            if (txtChant_cherch.Text=="")
            {
                AfficherChantier();
            }
            else
            {
                gridChantier.DataSource = db.Chercher_Chantier(txtChant_cherch.Text);
            }
        }

        private void btnCant_add_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Chnatier ?",
                                                 "Ajouter Location",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_Chantier(int.Parse(DateTime.Now.ToString("yyMMddHHss")), int.Parse(cmbChant_Cin.SelectedValue.ToString()), txtChant_adres.Text, float.Parse(txtChant_budg.Text),
                       dteChant_debut.Value, dteChant_fin.Value);
                    MessageBox.Show("Locayion Bien Ajouter ! ?",
                                    "Location Vehicule",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    AfficherChantier();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCant_delet_Click(object sender, EventArgs e)
        {
            int index = gridChantier.CurrentRow.Index;
            try
            {
                int num =int.Parse(gridChantier.Rows[index].Cells[0].Value.ToString());
                DialogResult d = MessageBox.Show("Voulez Vous  Supprimer Chnatier ?",
                                                "Supprimer chantier Location",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Chantier(num);
                    MessageBox.Show("chantier Bien supprimer ! ?",
                                    "Supprimer  chantier",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherChantier();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCant_find_Click(object sender, EventArgs e)
        {
            try
            {
                int index = gridChantier.CurrentRow.Index;
                txtChant_num.Text = gridChantier.Rows[index].Cells[0].Value.ToString();
                cmbChant_Cin.SelectedValue = int.Parse(gridChantier.Rows[index].Cells[5].Value.ToString());
                txtChant_adres.Text = gridChantier.Rows[index].Cells[1].Value.ToString();
                txtChant_budg.Text = gridChantier.Rows[index].Cells[2].Value.ToString();
                dteChant_debut.Value = DateTime.Parse(gridChantier.Rows[index].Cells[3].Value.ToString());
                dteChant_fin.Value = DateTime.Parse(gridChantier.Rows[index].Cells[4].Value.ToString());

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void btnCant_edit_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Voulez Vous  Modifier Chnatier ?",
                                                 "Ajouter Location",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    //db.Update_Chantier(int.Parse(txtChant_num.Text),cmbChant_Cin.Text,
                    //    txtChant_adres.Text, float.Parse(txtChant_budg.Text),
                    //   dteChant_debut.Value, dteChant_fin.Value);
                    //MessageBox.Show("Location Bien Modifier ! ?",
                    //                "Location Vehicule",
                    //                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherChantier();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bntadd_charge_Click(object sender, EventArgs e)
        {
            int index2 = gridChantier.CurrentRow.Index;
           
            try
            {
                int idchant = int.Parse(gridChantier.Rows[index2].Cells[0].Value.ToString());
                DialogResult d = MessageBox.Show("Voulez Vous  Ajouter Charge ?",
                                                 "Ajouter Charge",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Ajouter_Charge(int.Parse(DateTime.Now.ToString("yyMMddHHss")), txtChargeDescription.Text, dtecharge.Value, float.Parse(txtcharge_Mnt.Text), idchant);
                    MessageBox.Show("Charge Bien Ajouter !",
                                    "Ajouter Charge",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherCharge(idchant);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Selectionner un chantier !!","Ajouter Charge",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnDeletCharge_Click(object sender, EventArgs e)
        {
            int index = gridCharge.CurrentRow.Index;
            int index2 = gridChantier.CurrentRow.Index;
            
            try
            {
                int idcharge =int.Parse(gridCharge.Rows[index].Cells[0].Value.ToString());
                int idchant = int.Parse(gridChantier.Rows[index2].Cells[0].Value.ToString());


                DialogResult d = MessageBox.Show("Voulez Vous  Supprimer Charge ?",
                                                 "Supprimer Charge",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Delete_Charge(idcharge);
                    MessageBox.Show("Charge Bien Supprimer !",
                                    "Ajouter Charge",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherCharge(idchant);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFindCharge_Click(object sender, EventArgs e)
        {
            int index = gridCharge.CurrentRow.Index;
            txtcharge_id.Text = gridCharge.Rows[index].Cells[0].Value.ToString();
            txtChargeDescription.Text = gridCharge.Rows[index].Cells[1].Value.ToString();
            dtecharge.Value = DateTime.Parse(gridCharge.Rows[index].Cells[2].Value.ToString());
            txtcharge_Mnt.Text = gridCharge.Rows[index].Cells[3].Value.ToString();
         //   cmbCharge_chentier.Text = gridCharge.Rows[index].Cells[4].Value.ToString();
        }

        private void btncahrge_update_Click(object sender, EventArgs e)
        {
            int index = gridCharge.CurrentRow.Index;
            int index2 = gridChantier.CurrentRow.Index;
            try
            {
                int idcharge = int.Parse(gridCharge.Rows[index].Cells[0].Value.ToString());
                int idchant = int.Parse(gridChantier.Rows[index2].Cells[0].Value.ToString());
                DialogResult d = MessageBox.Show("Voulez Vous  Modifier Charge ?",
                                                 "Modifier Charge",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    db.Update_Charge(idcharge,txtChargeDescription.Text, dtecharge.Value, float.Parse(txtcharge_Mnt.Text), idchant);
                    MessageBox.Show("Charge Bien Modifier !",
                                    "Modifier Charge",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AfficherCharge(idchant);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtCharge_cherch_OnValueChanged(object sender, EventArgs e)
        {
            int index2 = gridChantier.CurrentRow.Index;
            try
            {
                int idchant = int.Parse(gridChantier.Rows[index2].Cells[0].Value.ToString());

                if (txtCharge_cherch.Text == "")
                {
                    AfficherCharge(idchant);
                }
                else
                {
                    gridCharge.DataSource = db.Chercher_Charge(txtCharge_cherch.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Selectionner un chantier !!", "Ajouter Charge", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Application.Restart();
            
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void btnCmdV_print_Click(object sender, EventArgs e)
        {
            
            int index = gridComdV.CurrentRow.Index;
            int num = int.Parse(gridComdV.Rows[index].Cells[0].Value.ToString());
            if(num != null)
            {
                Form2 cmd = new Form2(num);
                cmd.Show();
            }
            else
            {
                MessageBox.Show("selectioner une valeur");
            }
           
          
        }

        private void PanelCollection_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btnConfig_logo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                pictureBox1.Image = image;
            }
        }

        public static byte[] converterDemo(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void btnConfig_Add_Click(object sender, EventArgs e)
        {
            byte[] img;
            if (pictureBox1.Image==null)
            {
                img = null;
            }
            else
            {
                img= converterDemo(pictureBox1.Image);
            }
             

            try
            {
                DialogResult dr = MessageBox.Show("Voulez vous Ajouter info societe ?", "Ajouter Societe", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    db.Ajouter_Config(
                                      txtConf_NomSte.Text, txtConfig_Dom.Text,img,
                                      txtConf_Adresse.Text, txtConfig_tele.Text, txtConfig_Rc.Text,
                                      txtConfig_patente.Text,
                                      txtConfig_iff.Text, txtConfig_Cnss.Text, txtConfig_ICE.Text,
                                      txtConfig_fax.Text,txtConfig_email.Text,txtConfig_site.Text);
                    AfficherConfig();
                    ResetConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panelConfig_header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            int index = GridConfig.CurrentRow.Index;
            string NomSte = GridConfig.Rows[index].Cells[0].Value.ToString();
            try
            {
                db.Delete_Config(NomSte);
                AfficherConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ResetConfig()
        {
            txtConf_NomSte.Text = "";
            txtConfig_Dom.Text = "";
            txtConf_Adresse.Text = "";
            txtConfig_tele.Text = "";
            txtConfig_Rc.Text = "";
            txtConfig_patente.Text = "";
            txtConfig_iff.Text = "";
            txtConfig_Cnss.Text = "";
            txtConfig_ICE.Text = "";
            txtConfig_fax.Text = "";
            txtConfig_site.Text = "";
            txtConfig_email.Text = "";
        }
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int index = GridConfig.CurrentRow.Index;
                txtConf_NomSte.Text = GridConfig.Rows[index].Cells[0].Value.ToString();
                txtConfig_Dom.Text = GridConfig.Rows[index].Cells[1].Value.ToString();
                txtConf_Adresse.Text = GridConfig.Rows[index].Cells[3].Value.ToString();
                txtConfig_tele.Text = GridConfig.Rows[index].Cells[4].Value.ToString();
                txtConfig_Rc.Text = GridConfig.Rows[index].Cells[5].Value.ToString();
                txtConfig_patente.Text = GridConfig.Rows[index].Cells[6].Value.ToString();
                txtConfig_iff.Text = GridConfig.Rows[index].Cells[7].Value.ToString();
                txtConfig_Cnss.Text = GridConfig.Rows[index].Cells[8].Value.ToString();
                txtConfig_ICE.Text = GridConfig.Rows[index].Cells[9].Value.ToString();
                txtConfig_email.Text = GridConfig.Rows[index].Cells[10].Value.ToString();
                txtConfig_fax.Text = GridConfig.Rows[index].Cells[10].Value.ToString();
                txtConfig_site.Text = GridConfig.Rows[index].Cells[10].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            byte[] img;
            if (pictureBox1.Image == null)
            {
                img = null;
            }
            else
            {
                img = converterDemo(pictureBox1.Image);
            }


            try
           {
                DialogResult dr = MessageBox.Show("Voulez vous Modifier info societe ?", "Modifier Societe", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    db.Upadate_Config(
                                      txtConf_NomSte.Text, txtConfig_Dom.Text, img,
                                      txtConf_Adresse.Text, txtConfig_tele.Text, txtConfig_Rc.Text,
                                      txtConfig_patente.Text,
                                      txtConfig_iff.Text, txtConfig_Cnss.Text, txtConfig_ICE.Text,
                                      txtConfig_email.Text,txtConfig_fax.Text,txtConfig_site.Text);
                    AfficherConfig();
                    ResetConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(openFileDialog2.FileName);
            try
            {
                if (filename == null || filename== "openFileDialog2")
                {
                    MessageBox.Show("Please select a valid document.");
                    return;
                }
                else
                {

                    //we already define our connection globaly. We are just calling the object of connection.
                    string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    System.IO.File.Copy(openFileDialog2.FileName, path + "\\Document\\" + filename);
                    db.Ajouter_Fichier(btnFcihier_Desc.Text, filename, int.Parse(cmbCmdAchFichier.Text));
                    MessageBox.Show("Document uploaded.");
                    RemplirFichier();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog2.CheckFileExists)
                {
                    string path = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                    LbFileName.Text = path;

                }
            }
            else
            {
                MessageBox.Show("Please Upload document.");
            }
        }

        private void GridFichier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //int index = GridFichier.CurrentRow.Index;

            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            //string f1 =path+"\\Document\\"+ GridFichier.Rows[index].Cells[3].Value.ToString();
            //string fn = Path.GetFileName(f1);
            //string f2 = "";
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.FileName = fn;
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{

            //    f2 = sfd.FileName;
            //    MessageBox.Show(f1 + "\n" + f2);
            //}
            //System.IO.File.Copy(f2, f1);
            //////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                int index = GridFichier.CurrentRow.Index;

                string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                string f1 = path + "\\Document\\" + GridFichier.Rows[index].Cells[3].Value.ToString();
                string fn = Path.GetFileName(f1);
                string f2 = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = fn;
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    f2 = sfd.FileName;
                    System.IO.File.Copy(f1, f2);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            int index = GridFichier.CurrentRow.Index;
            int id = int.Parse(GridFichier.Rows[index].Cells[1].Value.ToString());
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            string f1 = path + "\\Document\\" + GridFichier.Rows[index].Cells[3].Value.ToString();
            try
            {
                File.Delete(path+ "\\Document\\"+ GridFichier.Rows[index].Cells[3].Value.ToString());
                db.Delete_Fichier(id);
                RemplirFichier();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtColnbCoui.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbFact_CL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int NumClt = int.Parse(cmbFact_CL2.SelectedValue.ToString());
                var req = from x in db.CommandeVente
                          from y in db.DetailReg where x.Num_CmdV == y.Num_CmdV && x.Num_Clt == NumClt select x;
                cmbFact_Cmdv.DataSource = req.Distinct().ToList();
                   // db.CommandeVente.Where(x => x.Num_Clt == NumClt).ToList();
                cmbFact_Cmdv.DisplayMember = "Num_CmdV";
            }catch{ }
            

        }

        private void cmbFact_CL2_SelectedValueChanged(object sender, EventArgs e)
        {
            ////int NumClt = int.Parse(cmbFact_CL.SelectedValue.ToString());
            //cmbFact_Cmdv.DataSource = db.CommandeVente.Where(x => x.Num_Clt == 38).ToList();
            //cmbFact_Cmdv.DisplayMember = "Num_CmdV";
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnListBox_Click(object sender, EventArgs e)
        {
            
            Article art;
            foreach(object item in checkedListBox1.CheckedItems)
            {
                Control text = panelCheckList.Controls[item.ToString()];
                string Num_Art = item.ToString();
                art = db.Article.Where(x => x.Num_At == Num_Art).First();
                //MessageBox.Show(text.Name);
                MessageBox.Show((float.Parse(art.QteS.ToString())-((float.Parse(art.QteS.ToString()) * float.Parse(text.Text)) / (float.Parse(art.QteS.ToString()) * 14))).ToString());
            }
          
        }

        private void cmbRegl_CL_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridClientCmv.DataSource = db.Chereher_CommandeVente(cmbRegl_CL.Text);
        }

        private void GridClientCmv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = GridClientCmv.CurrentRow.Index;
                int numCmd = int.Parse(GridClientCmv.Rows[index].Cells[1].Value.ToString());
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

        private void txtConfig_Cnss_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConf_NomSte_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConf_Adresse_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConfig_Dom_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConfig_tele_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConfig_Rc_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConfig_ICE_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtConfig_site_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void gridChantier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void gridChantier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
                int index = gridChantier.CurrentRow.Index;
                int numch = int.Parse(gridChantier.Rows[index].Cells[0].Value.ToString());
                AfficherCharge(numch);
            
           
        }
        
        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            Reglement reg = new Reglement();

            int Num_reg = int.Parse(DateTime.Now.ToString("yyMMddHHss"));
            int index = GridClientCmv.CurrentRow.Index;
            int numCmd = int.Parse(GridClientCmv.Rows[index].Cells[1].Value.ToString());
            int numModeReg = int.Parse(ModeReg.SelectedValue.ToString());
            int numBanque = int.Parse(Banque.SelectedValue.ToString());

            try {
                var reqq = from x in db.DetailVente
                           from y in db.DetailReg
                           where x.Num_CmdV == y.Num_CmdV && y.Num_CmdV == numCmd
                           select y.Num_Reg;
                if (reqq.Count() == 0)
                {
                    if (float.Parse(Montant.Text)< float.Parse(montantPaye.Text))
                    {
                        MessageBox.Show("Le Montant deposé Doit etre inferieur au Montant ");

                    }
                    else
                    {
                        db.Ajouter_Reglement(float.Parse(Montant.Text), NumChequeFac.Text, numModeReg, numBanque, Num_reg, float.Parse(montantPaye.Text));
                        db.Ajouter_DetailReglement(Num_reg, numCmd);
                        MessageBox.Show("Commande Bien Règler");
                        reglementByCmdv(numCmd);
                    }
            }
            else
               {
                   MessageBox.Show("Comande DEja Regler");
               }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            int index = GRidDetailFAc.CurrentRow.Index;
            int numreg = int.Parse(GRidDetailFAc.Rows[index].Cells[0].Value.ToString());
            int index2 = GridClientCmv.CurrentRow.Index;
            int numcmd = int.Parse(GridClientCmv.Rows[index2].Cells[1].Value.ToString());

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

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            int index = GRidDetailFAc.CurrentRow.Index;
            int numReg = int.Parse(GRidDetailFAc.Rows[index].Cells[0].Value.ToString());
            int index2 = GridClientCmv.CurrentRow.Index;
            int numCmd = int.Parse(GridClientCmv.Rows[index2].Cells[0].Value.ToString());
            try
            {
                db.Modifier_Reglement(numReg,
                    float.Parse(Montant.Text),
                    NumChequeFac.Text,
                    int.Parse(ModeReg.SelectedValue.ToString()),
                    int.Parse(Banque.SelectedValue.ToString()),
                    float.Parse(montantPaye.Text));
                MessageBox.Show("Reglement Bien Modifer !");
              //  reglementByCmdv(numCmd);
                reglementByCmdv(numCmd);
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
                
                int index = GRidDetailFAc.CurrentRow.Index;
                Montant.Text = GRidDetailFAc.Rows[index].Cells[1].Value.ToString();
                montantPaye.Text = GRidDetailFAc.Rows[index].Cells[5].Value.ToString();
                ModeReg.SelectedValue = int.Parse(GRidDetailFAc.Rows[index].Cells[3].Value.ToString());
                Banque.SelectedValue = int.Parse(GRidDetailFAc.Rows[index].Cells[4].Value.ToString());
                NumChequeFac.Text = GRidDetailFAc.Rows[index].Cells[2].Value.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {

        }

        private void GridClientCmv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                int idex = e.RowIndex;
                int idcmd = int.Parse(GridClientCmv.Rows[idex].Cells[1].Value.ToString());
                var rep = db.CommandeVente.Where(x => x.Num_CmdV == idcmd).First();
                var rep2 = db.DetailReg.Where(x => x.Num_CmdV == idcmd).Count();
                if(rep2 !=0)
                {
                    //db.Ajouter_DetailFacture(idFActure, numCmd);
                    MessageBox.Show("Commande Facturee");
                }
                else
                {
                    MessageBox.Show("Commande pas Encore regler");
                }
                
            }
          
           
        }

        private void cmbFact_Cmdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var req = db.GetMontantByCommande(int.Parse(cmbFact_Cmdv.Text)).First();
               if(req == null)
                {
                     MntCommande.Text = "0 DH";

                }
                else
                {
                    float text = float.Parse(req.ToString());
                     MntCommande.Text = text.ToString() + " DH";

                }

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel addexcel = new ImportExcel();
                addexcel.Importer_Article();
                remplirarticle();
                ComboNumArticle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnImpFour_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel addexcel = new ImportExcel();
                addexcel.Impoter_Fournisseur();
                aficher_Fournisseur();
                remplirComboFournisseur(cmbComdAfourn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImpOvrier_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel addexcel = new ImportExcel();
                addexcel.Importer_Ouvrier();
                afficherOuvrier();
                RemplirCmbOuvrier();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImpClient_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel addexcel = new ImportExcel();
                addexcel.Importer_Client();
                gridCLient.DataSource = db.Affiche_Client();
                ViderClientChamps();
                remplirClient(cmbCmdV_client);
                remplirClient(cmbFact_CL);
                //remplirClient(cmbFact_CL2);

                RemplirCmbClientLoc();
                remplirChantierOvr();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImpChantier_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel addexcel = new ImportExcel();
                addexcel.Importer_Chantier();
                AfficherChantier();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtWeek_cherch_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtWeek_cherch.Text == "")
                {
                    Afficher_week();
                }
                else
                {
                    
                    gridWeek.DataSource = db.Chercher_week(txtWeek_cherch.Text);
                }
               
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            SaveFile("Importer_Article.xlsx");
        }
        
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            SaveFile("Importer_Fournisseur.xlsx");
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SaveFile("Importer_Client.xlsx");
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            SaveFile("Importer_Ouvrier.xlsx");
        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void File_Fournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (File_Fournisseur.SelectedValue.ToString() !="projectONE.Fournisseur")
            {
                int num = int.Parse(File_Fournisseur.SelectedValue.ToString());
                remplirCmbCmdAch(num);
                

            }

        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                
              GridFichier.DataSource = db.chercher_Fichier(bunifuMetroTextbox1.Text);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
