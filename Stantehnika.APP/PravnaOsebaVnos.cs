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
        }

        #region events
        private void tbNazivPodjetja_Click(object sender, EventArgs e)
        {
            if (tbNazivPodjetja.Text == "naziv podjetja")
            {
                tbNazivPodjetja.Text = "";
            }
        }

        private void tbDavcnaStevilka_Click(object sender, EventArgs e)
        {
            if (tbDavcnaStevilka.Text == "davčna številka")
            {
                tbDavcnaStevilka.Text = "";
            }
        }

        private void tbUlica_Click(object sender, EventArgs e)
        {
            if (tbUlica.Text == "ulica")
            {
                tbUlica.Text = "";
            }
        }

        private void tbHisnaStevilka_Click(object sender, EventArgs e)
        {
            if (tbHisnaStevilka.Text == "hišna številka")
            {
                tbHisnaStevilka.Text = "";
            }
        }

        private void tbKraj_Click(object sender, EventArgs e)
        {
            if (tbKraj.Text == "kraj")
            {
                tbKraj.Text = "";
            }
        }

        private void tbPostnaStevilka_Click(object sender, EventArgs e)
        {
            if (tbPostnaStevilka.Text == "poštna številka")
            {
                tbPostnaStevilka.Text = "";
            }
        }

        private void tbEnaslov_Click(object sender, EventArgs e)
        {
            if (tbEnaslov.Text == "e-naslov")
            {
                tbEnaslov.Text = "";
            }
        }

        private void tbNazivPodjetja_Leave(object sender, EventArgs e)
        {
            if (tbNazivPodjetja.Text == "")
            {
                tbNazivPodjetja.Text = "naziv podjetja";
            }
        }

        private void tbDavcnaStevilka_Leave(object sender, EventArgs e)
        {
            if (tbDavcnaStevilka.Text == "")
            {
                tbDavcnaStevilka.Text = "davčna številka";
            }
        }

        private void tbUlica_Leave(object sender, EventArgs e)
        {
            if (tbUlica.Text == "")
            {
                tbUlica.Text = "ulica";
            }
        }

        private void tbHisnaStevilka_Leave(object sender, EventArgs e)
        {
            if (tbHisnaStevilka.Text == "")
            {
                tbHisnaStevilka.Text = "hišna številka";
            }
        }

        private void tbKraj_Leave(object sender, EventArgs e)
        {
            if (tbKraj.Text == "")
            {
                tbKraj.Text = "kraj";
            }
        }

        private void tbPostnaStevilka_Leave(object sender, EventArgs e)
        {
            if (tbPostnaStevilka.Text == "")
            {
                tbPostnaStevilka.Text = "poštna številka";
            }
        }

        private void tbEnaslov_Leave(object sender, EventArgs e)
        {
            if (tbEnaslov.Text == "")
            {
                tbEnaslov.Text = "e-naslov";
            }
        }

        private void btnVnesiStranko_Click(object sender, EventArgs e)
        {
            string nazivPodjetja = tbNazivPodjetja.Text;
            string davcnaStevilka = tbDavcnaStevilka.Text;
            string sedezPodjetja = tbUlica.Text + " " + tbHisnaStevilka.Text + ", " +tbPostnaStevilka.Text + " " + tbKraj.Text;
            string email = tbEnaslov.Text;

            // Preveri, če so vsa polja izpolnjena
            if (string.IsNullOrEmpty(nazivPodjetja) || string.IsNullOrEmpty(davcnaStevilka) ||
                string.IsNullOrEmpty(sedezPodjetja) || string.IsNullOrEmpty(email))
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

        #endregion events
    }
}
