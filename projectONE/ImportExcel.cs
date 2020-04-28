using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using ExcelApp = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace projectONE
{
    class ImportExcel
    {
       public GestionComercialeEntities1 db ;
       public ExcelApp.Application excelApp;
       public OpenFileDialog openFileDialog1;
       public ExcelApp.Workbook   excelBook;
       public ExcelApp._Worksheet excelSheet;
       public ExcelApp.Range      excelRange;
       public int rows;
      public ImportExcel()
        {
            db = new GestionComercialeEntities1();
            excelApp = new ExcelApp.Application();
            openFileDialog1 = new OpenFileDialog();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed!!");
                return;
            }
        }



        public void Imoprter_excel()
        {
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xlsm;*.xlsb;*.xltx;*.xltm;*.xls;*.xlt;*.xls;*.xml;*.xml;*.xlam;*.xla;*.xlw;*.xlr;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                excelBook = excelApp.Workbooks.Open(openFileDialog1.FileName);
                excelSheet = excelBook.Sheets[1];
                excelRange = excelSheet.UsedRange;
                
                rows = excelRange.Rows.Count;
            }
        
        }


        public void Importer_Article()
        {
            try
            {
                Imoprter_excel();

                if (rows > 1)
                {
                    for (int i = 2; i <= rows; i++)
                    {
                        db.Ajouter_Article(
                           excelRange.Cells[i, 1].Value2.ToString(),
                           excelRange.Cells[i, 2].Value2.ToString(),
                           float.Parse(excelRange.Cells[i, 3].Value2.ToString()),
                           float.Parse(excelRange.Cells[i, 4].Value2.ToString()),
                           int.Parse(excelRange.Cells[i, 5].Value2.ToString()),
                           "null",
                           excelRange.Cells[i, 7].Value2.ToString(),
                           int.Parse(excelRange.Cells[i, 8].Value2.ToString())
                            );
                       
                    }
                    MessageBox.Show("Bien Importer");
                }
                else
                {
                    MessageBox.Show("veillez remplir le fichier par des valeur Correcte !");
                }
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
           
        }
        public void Impoter_Fournisseur()
        {
            try
            {
                Imoprter_excel();
                if (rows>1)
                {
                    for (int i=2;i<=rows;i++)
                    {
                        db.Ajouter_Fournisseur
                            (
                               excelRange.Cells[i, 1].Value2.ToString(),
                               excelRange.Cells[i, 2].Value2.ToString(),
                               excelRange.Cells[i, 3].Value2.ToString(),
                               excelRange.Cells[i, 4].Value2.ToString(),
                               excelRange.Cells[i, 5].Value2.ToString()            
                            );
                    }
                }
                else
                {
                    MessageBox.Show("veillez remplir le fichier par des valeur Correcte !");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Importer_Ouvrier()
        {
            try
            {
                Imoprter_excel();

                if (rows > 1)
                {
                    for (int i = 2; i <= rows; i++)
                    {
                        db.Ajouter_Ouvrier
                            (
                               excelRange.Cells[i, 1].Value2.ToString(),
                               excelRange.Cells[i, 2].Value2.ToString(),
                               excelRange.Cells[i, 3].Value2.ToString(),
                               excelRange.Cells[i, 4].Value2.ToString()
                            );

                    }
                    MessageBox.Show("Bien Importer");
                }
                else
                {
                    MessageBox.Show("veillez remplir le fichier par des valeur Correcte !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Importer_Client()
        {
            try
            {
                Imoprter_excel();

                if (rows > 1)
                {
                    for (int i = 2; i <= rows; i++)
                    {
                        db.Ajouter_Client(
                           excelRange.Cells[i, 1].Value2.ToString(),
                           excelRange.Cells[i, 2].Value2.ToString(),
                           excelRange.Cells[i, 3].Value2.ToString(),
                           excelRange.Cells[i, 4].Value2.ToString(),
                           excelRange.Cells[i, 5].Value2.ToString(),
                           float.Parse(excelRange.Cells[i, 6].Value2.ToString()),
                           excelRange.Cells[i, 7].Value2.ToString()
                            );

                    }
                    MessageBox.Show("Bien Importer");
                }
                else
                {
                    MessageBox.Show("veillez remplir le fichier par des valeur Correcte !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Importer_Chantier()
        {
            try
            {
                Imoprter_excel();

                if (rows > 1)
                {
                    for (int i = 2; i <= rows; i++)
                    {
                        
                        db.Ajouter_Chantier
                            (
                           int.Parse(excelRange.Cells[i, 1].Value2.ToString()),
                           int.Parse(excelRange.Cells[i, 2].Value2.ToString()),
                           excelRange.Cells[i, 3].Value2.ToString(),
                           float.Parse(excelRange.Cells[i, 4].Value2.ToString()),
                           DateTime.Parse(excelRange.Cells[i, 5].value2.Tostring("dd-MMM-yyyy")),
                           DateTime.Parse(excelRange.Cells[i, 6].Value2.ToString("dd-MMM-yyyy"))
                            );

                    }
                    MessageBox.Show("Bien Importer");
                  //  MessageBox.Show(excelRange.Cells[2, 6].Value2.ToString());

                }
                else
                {
                    MessageBox.Show("veillez remplir le fichier par des valeur Correcte !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
