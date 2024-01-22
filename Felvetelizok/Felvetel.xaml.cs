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
using System.Windows.Shapes;

namespace Felvetelizok
{
    /// <summary>
    /// Interaction logic for Felvetel.xaml
    /// </summary>
    public partial class Felvetel : Window
    {
        public string UjDiak { get; set; }
        public Felvetel( )
        {
            InitializeComponent(); 
            
         
        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        {
            if(tb_az.Text == "" || tb_neve.Text == "" || dp_szuletes.Text == "" || tb_ertCim.Text == "")
            {
                MessageBox.Show("Nincs minden adat megadva");
            }
            else
            {
                UjDiak = $"{tb_az.Text};{tb_neve.Text};{tb_email.Text};{dp_szuletes.Text};{tb_ertCim.Text};{sli_matek.Value};{sli_magyar.Value}";
                Close();
            }
        }
    }
}
