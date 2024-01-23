﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for Felvetel.xaml
    /// </summary>
    public partial class Felvetel : Window
    {
        public string UjDiak { get; set; }
        List<string> azonositok = new();
        public Felvetel(List<string> adat)
        {
            InitializeComponent();
            azonositok = adat;

        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        { 
            if(tb_az.Text == "" || tb_neve.Text == "" || dp_szuletes.Text == "" || tb_ertCim.Text == "")
            {
                string hianyzoMezok = "";


                hianyzoMezok += tb_az.Text == "" ? " Azonosító," : "";
                hianyzoMezok += tb_neve.Text == "" ? " Név," : "";
                hianyzoMezok += tb_ertCim.Text == "" ? " Értesítési Cím," : "";
                hianyzoMezok += tb_email.Text == "" ? " Értesítési Cím," : "";
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
                MessageBox.Show("A névben kéne lenni legalább egy szóköznek");
            }
            else{
                UjDiak = $"{tb_az.Text};{tb_neve.Text};{tb_email.Text};{dp_szuletes.Text};{tb_ertCim.Text};{sli_matek.Value};{sli_magyar.Value}";
                Close();
            }
        }

        private void tb_az_TextChanged(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; 
            }
        }
         
    }
}