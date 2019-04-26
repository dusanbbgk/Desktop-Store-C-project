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
    public partial class Form3 : Form
    {
        public static List<Kategorija> kategorije1;
        private Button dugmeKategorije = null;
        private Button dugmeArtikla = null;
        private string textboxCalcText = "";
        private string kolicina = "";
        private double cenaArtikla = 0;
        private double ukupnaCena;
        public Form3()
        {
            InitializeComponent();
        }
        public Form FormToShowOnClosing { get; set; }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2.kategorije.Clear();
            kategorije1.Clear();
            if (null != FormToShowOnClosing)
                FormToShowOnClosing.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            neaktivno();
            dugmiciKalk();
            button15.Enabled = false;
            int razmak = 3;
            if (Form2.kategorije.Count > 0)
                kategorije1 = Form2.kategorije;
            else
            {
                kategorije1 = new List<Kategorija>();
                string redKategorije, redArtikla;
                if (File.Exists(@".\Kategorije.txt"))
                {
                    StreamReader citajKategorije = new StreamReader(@".\Kategorije.txt");
                    StreamReader citajArtikle = new StreamReader(@".\Artikli.txt");
                    while ((redKategorije = citajKategorije.ReadLine()) != null)
                    {
                        string[] idNaziv = redKategorije.Split(new string[] { ". " }, StringSplitOptions.None);
                        int idKategorije = Convert.ToInt32(idNaziv[0]);
                        Kategorija kategorija = new Kategorija(idKategorije, idNaziv[1]);

                        kategorije1.Add(kategorija);
                        while ((redArtikla = citajArtikle.ReadLine()) != null)
                        {
                            string kategorijaToString = redArtikla.Substring(redArtikla.IndexOf("(") + 1, redArtikla.IndexOf(")") - (redArtikla.IndexOf("(") + 1)).Trim();
                            if (kategorija.Naziv == kategorijaToString)
                            {
                                int idArtikla = Convert.ToInt32(redArtikla.Substring(0, redArtikla.IndexOf(".")));
                                string naziv = redArtikla.Substring(redArtikla.IndexOf(") ") + 1, redArtikla.IndexOf(",") - (redArtikla.IndexOf(") ") + 1)).Trim();
                                string jedinicaMere = redArtikla.Substring(redArtikla.IndexOf(", ") + 1, redArtikla.IndexOf(" -") - (redArtikla.IndexOf(", ") + 1)).Trim();
                                double cena = Convert.ToDouble(redArtikla.Substring(redArtikla.IndexOf("- ") + 1, redArtikla.IndexOf(" rsd") - (redArtikla.IndexOf("- ") + 1)));
                                kategorija.Artikli.Add(new Artikal(idArtikla, kategorija, naziv, cena, jedinicaMere));
                            }
                        }
                        citajArtikle.BaseStream.Position = 0;
                    }
                    citajArtikle.Close();
                    citajKategorije.Close();

                }
            }
            foreach (Kategorija kategorija in kategorije1)
            {
                Button dugme = new Button();
                dugme.Location = new Point(razmak, 13);
                dugme.Size = new Size(95, 70);
                dugme.BackColor = Color.Gray;
                dugme.ForeColor = Color.Black;
                dugme.Click += new EventHandler(buttonKA_Click);
                dugme.Text = kategorija.Naziv;
                dugme.Name = kategorija.Naziv;
                panel1.Controls.Add(dugme);
                razmak += 110;
            }
        }
        private void buttonKA_Click(object sender, EventArgs e)
        {
            Button trenutnoDugme = ((Button)sender);
            trenutnoDugme.BackColor = Color.Red;
            if (dugmeKategorije != null)
                dugmeKategorije.BackColor = Color.Gray;
            dugmeKategorije = trenutnoDugme;
            prikaziArtikle(trenutnoDugme.Name.ToString());
        }
        private void prikaziArtikle(string nazivKategorije)
        {
            panel2.Controls.Clear();
            int visina = 5;
            int razmak = 3;
            int pomi = 0;
            foreach (Kategorija kategorija in kategorije1)
            {
                if (kategorija.Naziv == nazivKategorije)
                {
                    foreach (Artikal artikal in kategorija.Artikli)
                    {
                        Button dugme = new Button();
                        dugme.Location = new Point(razmak, visina);
                        dugme.Size = new Size(95, 70);
                        dugme.BackColor = Color.Red;
                        dugme.ForeColor = Color.Black;
                        dugme.Click += new EventHandler(buttonA_Click);
                        dugme.Text = artikal.Naziv;
                        dugme.Name = artikal.Naziv + ", " + artikal.JedinicaMere + " - " + artikal.Cena + " rsd";
                        panel2.Controls.Add(dugme);
                        if (pomi == 5)
                        {
                            pomi = 0;
                            visina += 76;
                            razmak = 3;
                            continue;
                        }
                        else
                            razmak += 110;
                        pomi++;
                    }
                    break;
                }

            }
        }
        private void buttonA_Click(object sender, EventArgs e)
        {
            Button trenutnoDugme = ((Button)sender);
            trenutnoDugme.BackColor = Color.DarkRed;
            if (dugmeArtikla != null)
                dugmeArtikla.BackColor = Color.Red;
            dugmeArtikla = trenutnoDugme;
            aktivno();
            textboxCalcText = trenutnoDugme.Name;
            textBoxCalc.Text = textboxCalcText;
        }

        public void neaktivno()
        {
            panel3.Enabled = false;
            panelCalc.Enabled = false;
        }
        public void aktivno()
        {
            panelCalc.Enabled = true;
            tabPage2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            buttonC.Enabled = false;
        }
        public void dugmiciKalk()
        {
            button1.Click += new EventHandler(dugmiciKalk_Click);
            button2.Click += new EventHandler(dugmiciKalk_Click);
            button3.Click += new EventHandler(dugmiciKalk_Click);
            button4.Click += new EventHandler(dugmiciKalk_Click);
            button5.Click += new EventHandler(dugmiciKalk_Click);
            button6.Click += new EventHandler(dugmiciKalk_Click);
            button7.Click += new EventHandler(dugmiciKalk_Click);
            button8.Click += new EventHandler(dugmiciKalk_Click);
            button9.Click += new EventHandler(dugmiciKalk_Click);
            button10.Click += new EventHandler(dugmiciKalk_Click);
            button11.Click += new EventHandler(dugmiciKalk_Click);
        }

        private void dugmiciKalk_Click(object sender, EventArgs e)
        {
            Button current = sender as Button;
            kolicina += current.Text;
            textBoxCalc.Text += current.Text;
        }

        private void buttonMnozilac_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            buttonC.Enabled = true;
            textBoxCalc.Text += " x ";
        }

        private void buttonPonisti_Click(object sender, EventArgs e)
        {
            kolicina = "";
            textBoxCalc.Clear();
            panelCalc.Enabled = false;
            dugmeArtikla.BackColor = Color.Red;
            dugmeArtikla = null;
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            buttonC.Enabled = false;
            kolicina = "";
            textBoxCalc.Text = textboxCalcText;
        }

        private void buttonUnesi_Click(object sender, EventArgs e)
        {
            bool nasao = false;
            if (kolicina == "")
                kolicina = "1";
            listBox1.Items.Add(textboxCalcText + " x " + kolicina);

            foreach (Kategorija kategorija in kategorije1)
            {
                if (dugmeKategorije.Text == kategorija.Naziv)
                {
                    foreach (Artikal artikal in kategorija.Artikli)
                    {
                        if (dugmeArtikla.Text == artikal.Naziv)
                        {
                            cenaArtikla = artikal.Cena;
                            nasao = true;
                            break;
                        }
                    }
                    if (nasao) break;
                }
            }
            double kolicinaDouble = Convert.ToDouble(kolicina);
            ukupnaCena += cenaArtikla * kolicinaDouble;
            labelIznos.Text = "Ukupna cena: " + ukupnaCena.ToString() + " rsd";
            textBoxCalc.Clear();
            kolicina = "";
            panel3.Enabled = true;
            panelCalc.Enabled = false;
            dugmeArtikla.BackColor = Color.Red;
            dugmeArtikla = null;
        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            bool nasao = false;
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    string selektovanArtikal = listBox1.SelectedItem.ToString();
                    string[] delovi = selektovanArtikal.Split(',');
                    foreach (Kategorija kategorija in kategorije1)
                    {

                        foreach (Artikal artikal in kategorija.Artikli)
                        {
                            if (delovi[0] == artikal.Naziv)
                            {
                                cenaArtikla = artikal.Cena;
                                nasao = true;
                                break;
                            }
                        }
                        if (nasao) break;
                    }
                    double kolicinaArtikla = Convert.ToDouble(selektovanArtikal.Substring(selektovanArtikal.IndexOf("rsd x ") + 6, selektovanArtikal.Length - (selektovanArtikal.IndexOf("rsd x ") + 6)));
                    ukupnaCena -= cenaArtikla * kolicinaArtikla;
                    labelIznos.Text = "Ukupna cena: " + ukupnaCena.ToString() + " dinara";
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }  
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonDalje_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) MessageBox.Show("Korpa je prazna!");
            else
            {
                panel1.Enabled = false;
                panel2.Enabled = false;
                tabPage2.Enabled = true;
                tabControl1.SelectTab(tabPage2);
                for (int i = 0; i < listBox1.Items.Count; i++)
                    textBoxFR.Text += listBox1.Items[i].ToString() + "\r\n";
                textBoxFR.Text += "------------------------------------\r\nZA UPLATU:\t" +
                    ukupnaCena.ToString() + "\r\nUPLAĆENO:\t" + ukupnaCena.ToString() +
                    "\r\n\r\n" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") +
                    "\r\n\r\n***********************************************\r\n\tHVALA NA POSETI\r\n***********************************************\r\n";
            }
        }

        private void buttonSnimi_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory("Racuni");
            string datum = DateTime.Now.ToString("dd-MM-yyyy");
            string path = @"Racuni\" + datum + "-" + brojRacuna() + ".rac" + ".txt";
            File.WriteAllText(path, textBoxFR.Text);
            MessageBox.Show("Uspešno obavljena transakcija! Za novu kupovinu, kliknite na dugme 'Nova transakcija'!");
            panel3.Enabled = false;
            button15.Enabled = true;
        }
        public string brojRacuna()
        {
            int fileCount = (from file in Directory.EnumerateFiles("Racuni", "*.txt", SearchOption.AllDirectories) select file).Count();
            if (fileCount < 9)
                return "000" + Convert.ToString(fileCount + 1);
            else if (fileCount < 99)
                return "00" + Convert.ToString(fileCount + 1);
            else if (fileCount < 999)
                return "0" + Convert.ToString(fileCount + 1);
            else
                return Convert.ToString(fileCount + 1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form3 forma3 = new Form3();
            this.Dispose();
            forma3.FormClosing += new FormClosingEventHandler(Form3_FormClosing);
            forma3.Show();
        }
    }
}
