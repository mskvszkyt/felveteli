using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Felvetelizok
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Diak> diakok = new ObservableCollection<Diak>();
        public string connectionString = "datasource=127.0.0.1;port=3310;username=root;password=;database=minikifir;";
        public MySqlConnection connection;

        public void Betoltes()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                foreach (string sor in File.ReadAllLines(ofd.FileName).Skip(1))
                {
                    Diak ujDiak = new Diak(sor);
                    if (!diakok.Any(x => x.OM_Azonosito == ujDiak.OM_Azonosito))
                        diakok.Add(ujDiak);
                    else
                        MessageBox.Show("Már van feltöltve ilyen OM azonosítójú diák az adatokhoz");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            dgFelvetelizok.ItemsSource = diakok;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            
            if (diakok.Count == 0)
            {
                Betoltes();
            }
            else
            {
                if (MessageBox.Show("Fölülírod a meglévő adatokat?", "Fölülírás", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no
                    Betoltes();
                }
                else
                {
                    //do yes
                    diakok.Clear();
                    Betoltes();
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.Filter = "CSV fájl (*.csv) | *.csv| JSON fájl (*.json) | *.json";
            sfd.Title = "Add meg a fájl nevét";
            if (sfd.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(sfd.FileName);

                switch (extension.ToLower())
                {
                    
                    case ".json":
                        // ToDo: Save as JSON
                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                        options.WriteIndented = true;

                        string json = JsonSerializer.Serialize(diakok);
                        File.WriteAllText(sfd.FileName, json);
                        MessageBox.Show("sikeres mentés");
                        break;
                    case ".csv":
                        // ToDo: Save as CSV
                        StreamWriter mentes = new StreamWriter(sfd.FileName);
                        foreach (var item in diakok)
                        {
                            mentes.WriteLine(item.CSVSortAdVissza());
                        }
                        mentes.Close();
                        MessageBox.Show("sikeres mentés");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(extension);
                }
                
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedItems.Count > 0)
            {
                List<Diak> toroltDiakok = new();

                foreach (var sor in dgFelvetelizok.SelectedItems)
                {
                    toroltDiakok.Add((Diak)sor);
                }
                foreach (var sor in toroltDiakok)
                {
                    diakok.Remove(sor);
                }
                MessageBox.Show("Sikeres törlés");
            }
            else
            {
                MessageBox.Show("Nincs kijelölve elem");
            }
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            Diak ujDiak;
            List<string> azonositok = diakok.Select(x => x.OM_Azonosito).ToList();
            Felvetel felvetel = new Felvetel(azonositok);
            felvetel.ShowDialog();
            if (felvetel.DiakVissza != null)
            {
                ujDiak = new Diak(felvetel.DiakVissza);
                diakok.Add(ujDiak);
            }
        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedItems.Count > 0)
            {
                Diak ujDiak;
                List<string> azonositok = diakok.Select(x => x.OM_Azonosito).ToList();
                Felvetel modositas = new Felvetel(azonositok, dgFelvetelizok.SelectedItems[0] as Diak);
                modositas.ShowDialog();
                if (modositas.DiakVissza != null)
                {
                    diakok.Remove((Diak)dgFelvetelizok.SelectedItems[0]);
                    ujDiak = new Diak(modositas.DiakVissza); 
                    diakok.Add(ujDiak);
                }

            }

        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT OM_azon,nev,ertesitesi_cim,szul_datum,elerhetoseg,pt_matek,pt_magyar FROM felvetelizok";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string OM_azon = reader.GetString(0);
                    string nev = reader.GetString(1);
                    string ertesitesi_cim = reader.GetString(2);
                    DateTime szul_datum = reader.GetDateTime(3);
                    string elerhetoseg = reader.GetString(4);
                    int matek = reader.GetInt32(5);
                    int magyar = reader.GetInt32(6);

                    string csvSor = $"{OM_azon};{nev};{ertesitesi_cim};{szul_datum};{elerhetoseg};{matek};{matek}";
                    MessageBox.Show(csvSor);
                    Diak uj = new(csvSor);
                    diakok.Add(uj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFeltolt_Click(object sender, RoutedEventArgs e)
        {
            if(diakok.Count > 0)
            {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                string queryText = $"DELETE FROM felvetelizok";
                using (MySqlCommand command = new MySqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();

                                
                }

                foreach (var item in diakok)
                {
                    string newQueryText = $"INSERT INTO felvetelizok (OM_azon, nev, ertesitesi_cim, szul_datum, elerhetoseg, pt_matek, pt_magyar) VALUES ('{item.OM_Azonosito}', '{item.Neve}', '{item.ErtesitesiCime}', '{item.SzuletesiDatum}', '{item.Email}', {item.Matematika}, {item.Magyar})";

                    MySqlCommand command = new MySqlCommand(newQueryText, connection);
                    command.ExecuteNonQuery();


                }
                    connection.Close();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
    }
}
