using Stantehnika.Dal;
using Stantehnika.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Stantehnika.APP.UsersControls
{
    public partial class UC_Stranke : UserControl
    {

        #region private members
        #endregion
        public UC_Stranke()
        {
            InitializeComponent();
            PrikaziVseStranke();
        }

        #region methods

        private void PrikaziVseStranke()
        {
            RacunManager strankaManager = new RacunManager();
            List<Stranka> stranke = strankaManager.PridobiVseStranke();

            dataGridView1.DataSource = stranke;

            dataGridView1.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridView1.Columns["Email"].HeaderText = "E-naslov";
            dataGridView1.Columns["Naslov"].HeaderText = "Naslov";
            dataGridView1.Columns["DavcnaStevilka"].HeaderText = "Davčna številka";

            dataGridView1.Columns["NazivPodjetja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Naslov"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["DavcnaStevilka"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns["StrankaID"].Visible = false;
            dataGridView1.Columns["ImeInPriimek"].Visible = false;
            dataGridView1.Columns["UlicaInHisnaStevilka"].Visible = false;
            dataGridView1.Columns["PostaInKraj"].Visible = false;
            dataGridView1.Columns["SedezPodjetja"].Visible = false;

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current column is DavcnaStevilka
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DavcnaStevilka")
            {
                // Check if the value is null or empty
                if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    e.Value = "Fizična oseba"; // Set to default text
                }
            }
        }

        private int SteviloFiltriranihStrank(string iskalniPojem)
        {
            RacunManager strankaManager = new RacunManager();
            List<Stranka> stranke = strankaManager.PridobiStrankePoNazivu(iskalniPojem);
            int steviloStrank = stranke.Count;
            return steviloStrank;
        }

        private void FiltrirajStranke(string iskalniPojem)
        {
            // Kliče se metoda managerja, ki vrne stranke na podlagi vnosa
            RacunManager strankaManager = new RacunManager();
            List<Stranka> stranke = strankaManager.PridobiStrankePoNazivu(iskalniPojem);


            dataGridViewStranke.DataSource = stranke;

            dataGridViewStranke.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridViewStranke.Columns["Email"].HeaderText = "E-naslov";

            dataGridViewStranke.Columns["NazivPodjetja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStranke.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewStranke.Columns["StrankaID"].Visible = false;
            dataGridViewStranke.Columns["ImeInPriimek"].Visible = false;
            dataGridViewStranke.Columns["UlicaInHisnaStevilka"].Visible = false;
            dataGridViewStranke.Columns["PostaInKraj"].Visible = false;
            dataGridViewStranke.Columns["DavcnaStevilka"].Visible = false;
            dataGridViewStranke.Columns["SedezPodjetja"].Visible = false;
            dataGridViewStranke.Columns["Naslov"].Visible = false;
            ;
        }
        #endregion methods

        #region events
        private void tbIsciStranko_TextChanged(object sender, EventArgs e)
        {
            string iskalniPojem = tbIsciStranko.Text;
            
            if(iskalniPojem.Length < 4)
            {
                lblInformacijaIskanihStrank.Text = "Vnesi vsaj 4 znake...";
                dataGridViewStranke.Visible = false;
            }
            else if (iskalniPojem.Length >= 4 && iskalniPojem != "Išči...")
            {
                if (SteviloFiltriranihStrank(iskalniPojem) > 0)
                {
                    dataGridViewStranke.Visible = true;
                    FiltrirajStranke(iskalniPojem);
                    lblInformacijaIskanihStrank.Text = "";
                }
                else
                {
                    lblInformacijaIskanihStrank.Text = "V bazi ne najdem nič ustreznega :(";
                }
            }
            else
            {
                dataGridViewStranke.Visible = false;
            }
        }

        private void tbIsciStranko_Leave(object sender, EventArgs e)
        {
            if (tbIsciStranko.Text == "")
            {
                tbIsciStranko.Text = "Išči...";
            }
        }

        private void tbIsciStranko_Click(object sender, EventArgs e)
        {
            if(tbIsciStranko.Text == "Išči...")
            {
                tbIsciStranko.Text = "";
            }
        }

        private void cbFizicnaOseba_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFizicnaOseba.Checked)
            {
                cbPravnaOseba.Checked = false;
            }
        }

        private void cbPravnaOseba_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPravnaOseba.Checked)
            {
                cbFizicnaOseba.Checked = false;
            }
        }

        private void btnFiltriraj_Click(object sender, EventArgs e)
        {
            if (cbFizicnaOseba.Checked)
            {
                FizicnaOsebaVnos form = new FizicnaOsebaVnos();

                form.ShowDialog();
            }
            else if (cbPravnaOseba.Checked)
            {
                PravnaOsebaVnos form = new PravnaOsebaVnos();

                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Izberi tip subjekta!", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }

    #endregion events
}
