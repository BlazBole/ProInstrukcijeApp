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
    public partial class DodajMaterial : Form
    {
        #region private members
        public string MaterialName { get; private set; }
        #endregion

        public DodajMaterial()
        {
            InitializeComponent();
        }

        private void btnVnesiStranko_Click(object sender, EventArgs e)
        {
            // Preveri, ali je vnos prazen
            if (!string.IsNullOrEmpty(tbNazivMateriala.Text.Trim()))
            {
                MaterialName = tbNazivMateriala.Text.Trim();
                this.DialogResult = DialogResult.OK; // Vrnemo OK, da signaliziramo uspešno dodajanje
                this.Close(); // Zapri okno
            }
            else
            {
                MessageBox.Show("Prosimo, vnesite ime materiala.");
            }
        }
    }
}
