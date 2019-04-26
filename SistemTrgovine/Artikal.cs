using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemTrgovine
{
    public class Artikal
    {
        private int id;
        private string naziv;
        private double cena;
        private string jedinicaMere;
        private Kategorija kategorija;

        public string Naziv { get => naziv; set => naziv = value; }
        public double Cena { get => cena; set => cena = value; }
        public string JedinicaMere { get => jedinicaMere; set => jedinicaMere = value; }

        public Artikal(int id, Kategorija kategorija, string naziv, double cena, string jedinicaMere)
        {
            this.id = id; this.kategorija = kategorija;
            this.naziv = naziv; this.cena = cena; this.jedinicaMere = jedinicaMere;
        }
        public Artikal(Kategorija kategorija, string naziv, double cena, string jedinicaMere)
        {
            this.id = ++Form2.posIdArtikala; this.kategorija = kategorija;
            this.naziv = naziv; this.cena = cena; this.jedinicaMere = jedinicaMere;
        }
        public override string ToString()
        {
            return id + ". (" + kategorija.Naziv + ") " + naziv + ", " + jedinicaMere + " - " + cena + " rsd";
        }
    }
}
