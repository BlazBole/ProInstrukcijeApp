using Stantehnika.Dal;
using Stantehnika.Model;
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
    public partial class FizicnaOsebaVnos : Form
    {
        public FizicnaOsebaVnos()
        {
            InitializeComponent();
        }

        #region events

        private void tbImePriimek_Click(object sender, EventArgs e)
        {
            if (tbImePriimek.Text == "ime in priimek")
            {
                tbImePriimek.Text = "";
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

        private void tbImePriimek_Leave(object sender, EventArgs e)
        {
            if (tbImePriimek.Text == "")
            {
                tbImePriimek.Text = "ime in priimek";
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
            string imeInPriimek = tbImePriimek.Text;
            string ulicaInHisnaStevilka = tbUlica.Text + " " + tbHisnaStevilka.Text;
            string postaInKraj = tbPostnaStevilka.Text + " " + tbKraj.Text;
            string email = tbEnaslov.Text;

            // Ensure all required fields are filled
            if (string.IsNullOrEmpty(imeInPriimek) || string.IsNullOrEmpty(ulicaInHisnaStevilka) ||
                string.IsNullOrEmpty(postaInKraj) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Prosimo, izpolnite vsa polja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RacunManager manager = new RacunManager();

            if (manager.PreveriEmailObstaja(email))
            {
                MessageBox.Show("Stranka s tem e-poštnim naslovom že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            manager.DodajFizicnoOsebo(imeInPriimek, ulicaInHisnaStevilka, postaInKraj, email);

            MessageBox.Show("Stranka je uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        #endregion events
    }
}
