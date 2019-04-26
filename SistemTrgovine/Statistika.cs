using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemTrgovine
{
    public class Statistika
    {
        private string naziv;
        private double kolicina;
        public Statistika(string naziv, double kolicina)
        {
            this.naziv = naziv;
            this.kolicina = kolicina;
        }

        public string Naziv { get => naziv; set => naziv = value; }
        public double Kolicina { get => kolicina; set => kolicina = value; }

        public void uvecajKolicinu(double dodaj)
        {
            kolicina += dodaj;
        }
    }
}
