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
    public partial class DodajObjekt : Form
    {
        #region private members
        public string Objekt { get; private set; }
        public string UlicaObjekt { get; private set; }
        public string HisnaStevilkaObjekt { get; private set; }
        public string KrajObjekt { get; private set; }
        public string PostaObjekt { get; private set; }
        #endregion private members

        #region constructor
        public DodajObjekt(string objekt, string ulicaObjekt, string hisnaStevilkaObjekt, string krajObjekt, string postaObjekt)
        {
            InitializeComponent();
            pbOdstraniObjekt.Visible = false;
            lblOdstraniObjekt.Visible = false;
            SetDefaultText(tbObjekt, objekt, "naziv objekta");
            SetDefaultText(tbUlica, ulicaObjekt, "ulica");
            SetDefaultText(tbHisnaStevilka, hisnaStevilkaObjekt, "hišna številka");
            SetDefaultText(tbKraj, krajObjekt, "kraj");
            SetDefaultText(tbPostnaStevilka, postaObjekt, "poštna številka");
        }
        #endregion constructor

        #region private methods
        private void SetDefaultText(TextBox textBox, string value, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;
            }
            else
            {
                textBox.Text = value;
                textBox.ForeColor = Color.Black;
            }

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
            // Preverimo, ali je katero koli polje izpolnjeno
            bool isAnyDataEntered =
                (!string.IsNullOrWhiteSpace(tbObjekt.Text) && tbObjekt.Text != "naziv objekta") ||
                (!string.IsNullOrWhiteSpace(tbUlica.Text) && tbUlica.Text != "ulica") ||
                (!string.IsNullOrWhiteSpace(tbHisnaStevilka.Text) && tbHisnaStevilka.Text != "hišna številka") ||
                (!string.IsNullOrWhiteSpace(tbKraj.Text) && tbKraj.Text != "kraj") ||
                (!string.IsNullOrWhiteSpace(tbPostnaStevilka.Text) && tbPostnaStevilka.Text != "poštna številka");

            // Če je katero koli polje izpolnjeno, prikažemo elemente
            pbOdstraniObjekt.Visible = isAnyDataEntered;
            lblOdstraniObjekt.Visible = isAnyDataEntered;
        }

        #endregion private methods

        #region events
        private void btnDodajObjekt_Click(object sender, EventArgs e)
        {
            Objekt = tbObjekt.Text;
            UlicaObjekt = tbUlica.Text;
            HisnaStevilkaObjekt = tbHisnaStevilka.Text;
            KrajObjekt = tbKraj.Text;
            PostaObjekt = tbPostnaStevilka.Text;

            this.DialogResult = DialogResult.OK; // Vrni uspešno stanje
            this.Close();

        }

        private void pbOdstraniObjekt_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
            "Ali želite odstraniti objekt iz računa?",
            "Potrditev odstranitve",
            MessageBoxButtons.YesNo, // Dodaj gumba "Da" in "Ne"
            MessageBoxIcon.Question // Ikona vprašanja
            );

            // Če je uporabnik izbral "Da", nadaljuj z odstranitvijo
            if (dialogResult == DialogResult.Yes)
            {
                Objekt = "";
                UlicaObjekt = "";
                HisnaStevilkaObjekt = "";
                KrajObjekt = "";
                PostaObjekt = "";

                // Ponastavi polja obrazca na privzete vrednosti ali prazno
                tbObjekt.Text = "naziv objekta";
                tbUlica.Text = "ulica";
                tbHisnaStevilka.Text = "hišna številka";
                tbKraj.Text = "kraj";
                tbPostnaStevilka.Text = "poštna številka";

                // Uporabnik je uspešno odstranil objekt
                this.DialogResult = DialogResult.OK;
                this.Close(); // Zapri formo po odstranitvi
            }
        }

        #endregion events
    }
}
