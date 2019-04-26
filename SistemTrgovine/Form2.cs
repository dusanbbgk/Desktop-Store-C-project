using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemTrgovine
{
    public partial class Form2 : Form
    {
        public static int posIdKategorija = 0;
        public static int posIdArtikala = 0;
        public static List<Kategorija> kategorije = new List<Kategorija>();
        private List<Statistika> statistike = new List<Statistika>();
        public Form2()
        {
            InitializeComponent();
        }
        public Form FormToShowOnClosing { get; set; }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter pisiKategorije = new StreamWriter(@".\Kategorije.txt");
            StreamWriter pisiArtikle = new StreamWriter(@".\Artikli.txt");

            foreach (Kategorija kategorija in kategorije)
            {
                pisiKategorije.WriteLine(kategorija);
                foreach (Artikal artikal in kategorija.Artikli)
                    pisiArtikle.WriteLine(artikal);
            }

            pisiArtikle.Close();
            pisiKategorije.Close();
            kategorije.Clear();
            statistike.Clear();
            if (null != FormToShowOnClosing)
                FormToShowOnClosing.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string redKategorije, redArtikla;
            int i = 0;
            if (File.Exists(@".\Kategorije.txt"))
            {
                StreamReader citajKategorije = new StreamReader(@".\Kategorije.txt");
                StreamReader citajArtikle = new StreamReader(@".\Artikli.txt");
                while ((redKategorije = citajKategorije.ReadLine()) != null)
                {
                    string[] idNaziv = redKategorije.Split(new string[] { ". " }, StringSplitOptions.None);
                    posIdKategorija = Convert.ToInt32(idNaziv[0]);
                    Kategorija kategorija = new Kategorija(posIdKategorija, idNaziv[1]);

                    kategorije.Add(kategorija);
                    while ((redArtikla = citajArtikle.ReadLine()) != null)
                    {
                        string kategorijaToString = redArtikla.Substring(redArtikla.IndexOf("(") + 1, redArtikla.IndexOf(")") - (redArtikla.IndexOf("(") + 1)).Trim();
                        if (kategorija.Naziv == kategorijaToString)
                        {
                            posIdArtikala = Convert.ToInt32(redArtikla.Substring(0, redArtikla.IndexOf(".")));
                            string naziv = redArtikla.Substring(redArtikla.IndexOf(") ") + 2, redArtikla.IndexOf(",") - (redArtikla.IndexOf(") ") + 2)).Trim();
                            string jedinicaMere = redArtikla.Substring(redArtikla.IndexOf(", ") + 2, redArtikla.IndexOf(" -") - (redArtikla.IndexOf(", ") + 2)).Trim();
                            double cena = Convert.ToDouble(redArtikla.Substring(redArtikla.IndexOf("- ") + 2, redArtikla.IndexOf(" rsd") - (redArtikla.IndexOf("- ") + 2)));
                            kategorija.Artikli.Add(new Artikal(posIdArtikala, kategorija, naziv, cena, jedinicaMere));
                        }
                    }
                    citajArtikle.BaseStream.Position = 0;
                    listBox1.Items.Add(kategorije[i++]);
                }
                citajArtikle.Close();
                citajKategorije.Close();

            }
        }
        private void IspisArtikala(Kategorija kategorija)
        {
            foreach (Artikal artikal in kategorija.Artikli)
                listBox2.Items.Add(artikal);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string[] idNaziv = listBox1.SelectedItem.ToString().Split(new string[] { ". " }, StringSplitOptions.None);
            foreach (Kategorija kategorija in kategorije)
                if (kategorija.Naziv == idNaziv[1]) IspisArtikala(kategorija);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool nasao = false;
            if (textBox1.Text != "")
            {
                foreach (Kategorija kategorija in kategorije)
                    if (textBox1.Text.Trim() == kategorija.Naziv)
                    {
                        MessageBox.Show("Kategorija je vec u listi!!!");
                        nasao = true;
                        break;
                    }
                if (!nasao)
                {
                    kategorije.Add(new Kategorija(textBox1.Text.Trim()));
                    listBox1.Items.Clear();
                    foreach (Kategorija kategorija in kategorije)
                        listBox1.Items.Add(kategorija);
                    textBox1.Text = "";
                }
            }
            else MessageBox.Show("Morate uneti naziv kategorije!!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] idNaziv = listBox1.SelectedItem.ToString().Split(new string[] { ". " }, StringSplitOptions.None);
                foreach (Kategorija kategorija in kategorije)
                    if (Convert.ToInt32(idNaziv[0]) == kategorija.Id && idNaziv[1] == kategorija.Naziv)
                    {
                        kategorije.Remove(kategorija);
                        MessageBox.Show("Kategorija '" + idNaziv[1] + "' je obrisana!");
                        break;
                    }
                listBox1.Items.Clear();
                foreach (Kategorija kategorija in kategorije)
                    listBox1.Items.Add(kategorija);
                listBox2.Items.Clear();
            }
            else MessageBox.Show("Selektujte kategoriju koju zelite da obrisete!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool nasao = false;
            if (listBox1.SelectedIndex != -1)
            {
                string[] idNaziv = listBox1.SelectedItem.ToString().Split(new string[] { ". " }, StringSplitOptions.None);
                if (textBox2.Text != "" && textBox3.Text != "" && numericUpDown1.Value != 0)
                {
                    foreach (Kategorija kategorija in kategorije)
                    {
                        if (Convert.ToInt32(idNaziv[0]) == kategorija.Id && idNaziv[1] == kategorija.Naziv)
                        {
                            foreach (Artikal artikal in kategorija.Artikli)
                            {
                                if (artikal.Naziv == textBox2.Text.Trim())
                                {
                                    nasao = true;
                                    break;
                                }
                            }
                            if (!nasao)
                            {
                                kategorija.Artikli.Add(new Artikal(kategorija, textBox2.Text.Trim(), (double)numericUpDown1.Value, textBox3.Text.Trim()));
                                MessageBox.Show("'" + textBox2.Text.Trim() + ", " + textBox3.Text.Trim() + " - " + numericUpDown1.Value + " rsd' je dodat u kategoriju '" + idNaziv[1] + "'");
                                textBox2.Text = "";
                                textBox3.Text = "";
                                numericUpDown1.Value = 0;
                                listBox2.Items.Clear();
                                foreach (Artikal artikal in kategorija.Artikli)
                                    listBox2.Items.Add(artikal);
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Artikal '" + textBox2.Text.Trim() + "' vec postoji u kategoriji '" + idNaziv[1] + "'!");
                                break;
                            }
                        }
                    }
                }
                else MessageBox.Show("Unesite naziv, jedinicu mere i cenu artikla!");
            }
            else MessageBox.Show("Obelezite kategoriju kojoj pripada novi artikl!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool nasao = false;
            if (listBox2.SelectedIndex != -1)
            {
                string kategorijaToString = listBox2.SelectedItem.ToString().Substring(listBox2.SelectedItem.ToString().IndexOf("(") + 1, listBox2.SelectedItem.ToString().IndexOf(")") - (listBox2.SelectedItem.ToString().IndexOf("(") + 1)).Trim();
                string naziv = listBox2.SelectedItem.ToString().Substring(listBox2.SelectedItem.ToString().IndexOf(") ") + 1, listBox2.SelectedItem.ToString().IndexOf(",") - (listBox2.SelectedItem.ToString().IndexOf(") ") + 1)).Trim();
                foreach (Kategorija kategorija in kategorije)
                    if (kategorijaToString == kategorija.Naziv)
                    {
                        foreach (Artikal artikal in kategorija.Artikli)
                        {
                            if (naziv == artikal.Naziv)
                            {
                                nasao = true;
                                kategorija.Artikli.Remove(artikal);
                                MessageBox.Show("Artikal '" + naziv + "' je obrisan iz kategorije '" + kategorijaToString + "'!");
                                break;
                            }
                        }
                        if (nasao)
                        {
                            listBox2.Items.Clear();
                            foreach (Artikal artikal in kategorija.Artikli)
                                listBox2.Items.Add(artikal);
                            break;
                        }
                    }
            }
            else MessageBox.Show("Selektujte artikal koji zelite da obrisete!");
        }

        private void SPrikazibutton_Click(object sender, EventArgs e)
        {
            panelS.Controls.Clear();
            string datum = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
            Racuni(datum);
            PBar();
        }
        public void Racuni(string datum)
        {
            statistike.Clear();
            string[] fajlovi = Directory.GetFiles(@"Racuni", datum + "*");
            for (int i = 0; i < fajlovi.Length; i++)
                citajRacun(fajlovi[i]);
        }

        public void citajRacun(string fajl)
        {
            StreamReader citaj = new StreamReader(fajl);
            string red = "";
            bool nasao = false;
            while ((red = citaj.ReadLine()) != "------------------------------------")
            {
                string[] delovi = red.Split(',');
                string nazivArtikla = delovi[0];
                double kolicina = Convert.ToDouble(red.Substring(red.IndexOf("rsd x ") + 6, red.Length - (red.IndexOf("rsd x ") + 6)));
                /*if (statistike.Count == 0)
                    statistike.Add(new Statistika(nazivArtikla, kolicina));
                else
                {*/
                    for (int i = 0; i < statistike.Count; i++)
                    {
                        if (nazivArtikla == statistike[i].Naziv)
                        {
                            statistike[i].uvecajKolicinu(kolicina);
                            nasao = true;
                            break;
                        }
                    }
                    if (!nasao)
                        statistike.Add(new Statistika(nazivArtikla, kolicina));
                //}
            }
            citaj.Close();
        }
        public void PBar()
        {

            double max = 0;
            for (int i = 0; i < statistike.Count; i++)
            {
                if (max < statistike[i].Kolicina)
                    max = statistike[i].Kolicina;
            }
            int j = 0;
            for (int i = 0; i < statistike.Count; i++)
            {
                ProgressBar pb = new ProgressBar();
                pb.Minimum = 0;
                pb.Maximum = (int)(max * 100.0f);
                pb.Value = (int)(statistike[i].Kolicina * 100.0f);
                pb.Width = 350;
                pb.Location = new Point(250, j);
                Label labela = new Label();
                labela.Width = 250;
                labela.Text = statistike[i].Naziv + " (" + statistike[i].Kolicina + ")";
                labela.Location = new Point(0, j);

                panelS.Controls.Add(labela);
                panelS.Controls.Add(pb);
                j += pb.Height + 20;
            }
        }
    }
}