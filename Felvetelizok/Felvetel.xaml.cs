using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Felvetelizok
{
    public partial class Felvetel : Window
    {
        public string DiakVissza { get; set; }
        List<string> azonositok = new List<string>();

        // Constructor for adding a new entry
        public Felvetel(List<string> adat)
        {
            InitializeComponent();
            azonositok = adat;
        }

        // Constructor for modifying an existing entry
        public Felvetel(List<string> adat, Diak diak) : this(adat)
        {
            azonositok.Remove(diak.OM_Azonosito);
            tb_az.Text = diak.OM_Azonosito;
            tb_az.IsReadOnly = true;
            tb_neve.Text = diak.Neve;
            dp_szuletes.Text = Convert.ToString(diak.SzuletesiDatum);
            tb_ertCim.Text = diak.ErtesitesiCime;
            tb_email.Text = diak.Email;
            sli_matek.Value =  diak.Matematika;
            sli_magyar.Value = diak.Magyar;
            btn_hozzaad.Content = "Módosít";

            tb_az.IsEnabled = false;
        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        { 
            if(tb_az.Text == "" || tb_neve.Text == "" || dp_szuletes.Text == "" || tb_ertCim.Text == "" || tb_email.Text == "")
            {
                string hianyzoMezok = "";


                hianyzoMezok += tb_az.Text == "" ? " Azonosító," : "";
                hianyzoMezok += tb_neve.Text == "" ? " Név," : "";
                hianyzoMezok += tb_ertCim.Text == "" ? " Értesítési Cím," : "";
                hianyzoMezok += tb_email.Text == "" ? " Email Cím," : "";
                hianyzoMezok += dp_szuletes.Text == "" ? " Születési Dátum," : "";


                MessageBox.Show($"Nincs minden adat megadva a következő mezőkben:{hianyzoMezok.Substring(0, hianyzoMezok.Length - 1)}");
            }
            else if (tb_az.Text.Length != 11)
            {
                MessageBox.Show("Nem megfelelő hosszúságú az OM azonosító");
            }
            else if (azonositok.Contains(tb_az.Text))
            {
                MessageBox.Show("Ez az OM azonosító már használt");
            }
            else if (tb_neve.Text.Split(' ').Length == 1)
            {
                MessageBox.Show("A névben lennie kell legalább egy szóköznek");
            }
            else{ 
                DiakVissza = $"{tb_az.Text};{tb_neve.Text};{tb_email.Text};{dp_szuletes.Text};{tb_ertCim.Text};{sli_matek.Value};{sli_magyar.Value}";
                
                Close();
            }
        }

        private void tb_az_TextChanged(object sender, TextCompositionEventArgs e)
        {
            //TODO: ' ' kezelése
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
         
    }
}
