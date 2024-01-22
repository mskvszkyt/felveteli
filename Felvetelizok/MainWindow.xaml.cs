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

namespace Felvetelizok
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Diak> diakok = new ObservableCollection<Diak>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                foreach (string sor in File.ReadAllLines(ofd.FileName).Skip(1))
                {
                    diakok.Add(new Diak(sor));
                }
                dgFelvetelizok.ItemsSource = diakok;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.Filter = "CSV fájl (*.csv) | *.csv";
            sfd.Title = "Add meg a fájl nevét";
            if (sfd.ShowDialog() == true)
            {
                StreamWriter mentes = new StreamWriter(sfd.FileName);
                foreach (var item in diakok)
                {
                    mentes.WriteLine(item.ToString());
                }
                mentes.Close();
                MessageBox.Show("sikeres mentés");
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex > 0)
            {
                diakok.RemoveAt(dgFelvetelizok.SelectedIndex);
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
            Felvetel felvetel = new Felvetel();
            felvetel.ShowDialog();
            if (felvetel.UjDiak != null)
            {
                ujDiak = new Diak(felvetel.UjDiak);
                diakok.Add(ujDiak);
                dgFelvetelizok.ItemsSource = diakok;
            }
        }
    }
}
