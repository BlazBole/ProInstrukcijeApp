using Stantehnika.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stantehnika.APP
{
    public partial class PravnaOsebaVnos : Form
    {
        public PravnaOsebaVnos()
        {
            InitializeComponent();
            SetDefaultText(tbNazivPodjetja, "naziv podjetja");
            SetDefaultText(tbDavcnaStevilka, "davčna številka");
            SetDefaultText(tbUlica, "ulica");
            SetDefaultText(tbHisnaStevilka, "hišna številka");
            SetDefaultText(tbKraj, "kraj");
            SetDefaultText(tbPostnaStevilka, "poštna številka");
            SetDefaultText(tbEnaslov, "e-naslov");
        }

        private void SetDefaultText(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };

            textBox.TextChanged += (s, e) => UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            // Tvoja logika za prikaz/skrivanje elementov
        }

        // Ostali dogodki in logika ostanejo nespremenjeni
        private void btnVnesiStranko_Click(object sender, EventArgs e)
        {
            string nazivPodjetja = tbNazivPodjetja.Text;
            string davcnaStevilka = tbDavcnaStevilka.Text;
            string sedezPodjetja = tbUlica.Text + " " + tbHisnaStevilka.Text + ", " + tbPostnaStevilka.Text + " " + tbKraj.Text;
            string email = tbEnaslov.Text;

            // Preveri, če so vsa polja izpolnjena
            if (string.IsNullOrWhiteSpace(nazivPodjetja) || nazivPodjetja == "naziv podjetja" ||
                string.IsNullOrWhiteSpace(davcnaStevilka) || davcnaStevilka == "davčna številka" ||
                string.IsNullOrWhiteSpace(tbUlica.Text) || tbUlica.Text == "ulica" ||
                string.IsNullOrWhiteSpace(tbHisnaStevilka.Text) || tbHisnaStevilka.Text == "hišna številka" ||
                string.IsNullOrWhiteSpace(tbPostnaStevilka.Text) || tbPostnaStevilka.Text == "poštna številka" ||
                string.IsNullOrWhiteSpace(tbKraj.Text) || tbKraj.Text == "kraj" ||
                string.IsNullOrWhiteSpace(email) || email == "e-naslov")
            {
                MessageBox.Show("Prosimo, izpolnite vsa polja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RacunManager manager = new RacunManager();

            // Preveri, če e-pošta že obstaja
            if (manager.PreveriEmailObstaja(email))
            {
                MessageBox.Show("Podjetje s tem e-poštnim naslovom že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Vnos novega podjetja
            manager.DodajPravnoOsebo(nazivPodjetja, davcnaStevilka, sedezPodjetja, email);

            MessageBox.Show("Podjetje je uspešno dodano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Po uspešnem vnosu lahko zaprete obrazec ali počistite polja
            this.Close();
        }
    }
}
