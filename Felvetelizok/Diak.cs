using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felvetelizok
{
    class Diak : IFelvetelizo
    {
        string om_Azonosito;
        string neve;
        string ertesitesiCime;
        string email;
        DateTime szuletesiDatum;
        int matematika, magyar;
         

        public Diak(string sor)
        {
            string[] splitelt = sor.Split(';');
            this.om_Azonosito = splitelt[0];
            this.neve = splitelt[1];
            this.email = splitelt[2];
            this.szuletesiDatum = DateTime.Parse(splitelt[3]);
            this.ertesitesiCime = splitelt[4];
            this.matematika = splitelt[5] != "NULL" ? Int32.Parse(splitelt[5]) : -1;
            this.magyar = splitelt[6] != "NULL" ? Int32.Parse(splitelt[6]) : -1;
        }

        public string OM_Azonosito { get => om_Azonosito; set => om_Azonosito = value; }
        public string Neve { get => neve; set => neve = value; }
        public string ErtesitesiCime { get => ertesitesiCime; set => ertesitesiCime = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletesiDatum; set => szuletesiDatum = value; }
        public int Matematika { get => matematika; set => matematika = value; }
        public int Magyar { get => magyar; set => magyar = value; }

        public String CSVSortAdVissza()
        {
            return $"{om_Azonosito};{neve};{email};{szuletesiDatum};{ertesitesiCime};{matematika};{magyar}";
        }

        public void ModositCSVSorral(string sor)
        {
            string[] splitelt = sor.Split(';');
            om_Azonosito = splitelt[0];
            neve = splitelt[1];
            ertesitesiCime = splitelt[4];
            email = splitelt[2];
            szuletesiDatum = DateTime.Parse(splitelt[3]);
            matematika = splitelt[5] != "NULL" ? Int32.Parse(splitelt[5]) : -1;
            magyar = splitelt[6] != "NULL" ? Int32.Parse(splitelt[6]) : -1;
        }
    }
}
