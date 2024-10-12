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
    public partial class DodajOpombo : Form
    {
        #region private members
        public string Opomba { get; private set; }
        #endregion private members

        #region constructor
        public DodajOpombo(string trenutnaOpomba)
        {
            InitializeComponent();
            tbOpomba.Text = trenutnaOpomba;
        }
        #endregion constructor

        #region private methods



        #endregion private methods

        #region events
        private void btnVnesiOpombo_Click(object sender, EventArgs e)
        {
            Opomba = tbOpomba.Text;

            // Zapri formo
            this.DialogResult = DialogResult.OK; // Vrni uspešno stanje
            this.Close();
        }
        #endregion events
    }
}
