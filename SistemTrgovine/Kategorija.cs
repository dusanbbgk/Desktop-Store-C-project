using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemTrgovine
{
    public class Kategorija
    {
        private int id;
        private string naziv;
        private List<Artikal> artikli;

        public string Naziv { get => naziv; set => naziv = value; }
        internal List<Artikal> Artikli { get => artikli; set => artikli = value; }
        public int Id { get => id; set => id = value; }

        public Kategorija(string naziv)
        {
            this.naziv = naziv;
            id = ++Form2.posIdKategorija;
            artikli = new List<Artikal>();
        }
        public Kategorija(int id, string naziv) //KONSTRUKTOR ZA CITANJE IZ FAJLA
        {
            this.id = id;
            this.naziv = naziv;
            artikli = new List<Artikal>();
        }

        public override string ToString()
        {
            return id + ". " + naziv;
        }
    }
}
