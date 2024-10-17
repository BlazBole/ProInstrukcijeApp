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
            SetPlaceholder(tbImePriimek, "ime in priimek");
            SetPlaceholder(tbUlica, "ulica");
            SetPlaceholder(tbHisnaStevilka, "hišna številka");
            SetPlaceholder(tbKraj, "kraj");
            SetPlaceholder(tbPostnaStevilka, "poštna številka");
            SetPlaceholder(tbEnaslov, "e-naslov");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
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
        }

        private void btnVnesiStranko_Click(object sender, EventArgs e)
        {
            string imeInPriimek = tbImePriimek.Text;
            string ulicaInHisnaStevilka = tbUlica.Text + " " + tbHisnaStevilka.Text;
            string postaInKraj = tbPostnaStevilka.Text + " " + tbKraj.Text;
            string email = tbEnaslov.Text;

            // Ensure all required fields are filled
            if (string.IsNullOrWhiteSpace(imeInPriimek) || imeInPriimek == "ime in priimek" ||
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

            if (manager.PreveriEmailObstaja(email))
            {
                MessageBox.Show("Stranka s tem e-poštnim naslovom že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            manager.DodajFizicnoOsebo(imeInPriimek, ulicaInHisnaStevilka, postaInKraj, email);

            MessageBox.Show("Stranka je uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

    }
}
