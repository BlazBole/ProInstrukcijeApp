using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stantehnika.Dal;
using Stantehnika.Model;

namespace Stantehnika.APP
{
    public partial class Form1 : Form
    {
        #region private members
        private RacunManager racunManager;
        #endregion
        public Form1()
        {
            InitializeComponent();
            
        }

        #region methods
        private void Form1_Load(object sender, EventArgs e)
        {
            pripravitabeloRacunov();
        }

        public void pripravitabeloRacunov()
        {
            racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetAllRacuni();

            // Nastavi podatke v DataGridView
            dataGridView.DataSource = racuni;

            // Nastavi imena stolpcev
            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Kraj"].HeaderText = "Kraj";
            dataGridView.Columns["Datum"].HeaderText = "Datum izdaje računa";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Datum opravljenje storitve";
            dataGridView.Columns["Datumzapade"].HeaderText = "Rok plačila";
            dataGridView.Columns["NazivPodjetja"].HeaderText = "Stranka";

            // Velikost celic v DataGridView
            dataGridView.Columns["StevilkaRacuna"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Kraj"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Datum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["DatumOpravljeno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Datumzapade"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["NazivPodjetja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Skrij neuporabljene stolpce
            dataGridView.Columns["RacunGlavaID"].Visible = false;
            dataGridView.Columns["StrankaID"].Visible = false;
            dataGridView.Columns["Stranka"].Visible = false;

            // Predpostavimo, da imamo vsaj en vnos. Če je prazen seznam, ta korak preskočimo.
            if (racuni.Count > 0)
            {
                // Preverimo prvi vnos (in predpostavljamo, da imajo vsi vnosi isto strukturo glede podjetja)
                if (string.IsNullOrEmpty(racuni[0].NazivPodjetja))
                {
                    // Če je NazivPodjetja null ali prazen, skrij NazivPodjetja in pokaži Ime in Priimek
                    dataGridView.Columns["NazivPodjetja"].Visible = false;
                    dataGridView.Columns["ImeInPriimek"].Visible = true;
                }
                else
                {
                    // Če je NazivPodjetja prisoten, skrij Ime in Priimek
                    dataGridView.Columns["NazivPodjetja"].Visible = true;
                    dataGridView.Columns["ImeInPriimek"].Visible = false;
                }

            }
        }
        #endregion methods
    }
}
