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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Stantehnika.APP.UsersControls
{
    public partial class UC_Domov : UserControl
    {
        #region private members
        private RacunManager racunManager;
        #endregion private members

        #region constructor
        public UC_Domov()
        {
            InitializeComponent();
            pripravitabeloRacunov();
            NapolniComboBoxStranka();
            NastaviFiltre();
        }
        #endregion constructor

        #region methods
        public void pripravitabeloRacunov()
        {
            racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetAllRacuni();

            // Nastavi podatke v DataGridView
            dataGridView.DataSource = racuni;

            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Datum"].HeaderText = "Izdano";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Opravljeno";
            dataGridView.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridView.Columns["CenaDelo"].HeaderText = "Cena dela";
            dataGridView.Columns["CenaMaterial"].HeaderText = "Cena materiala";
            dataGridView.Columns["SkupnaCena"].HeaderText = "Skupna cena";


            // Velikost celic v DataGridView
            dataGridView.Columns["StevilkaRacuna"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Datum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["DatumOpravljeno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["NazivPodjetja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["CenaDelo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["CenaMaterial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["SkupnaCena"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Skrij neuporabljene stolpce
            dataGridView.Columns["Datumzapade"].Visible = false;
            dataGridView.Columns["Kraj"].Visible = false;
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

        public void NastaviFiltre()
        {
        }
        public void NapolniComboBoxStranka()
        {
            var items = new List<dynamic>
            {
                new { Key = -1, Value = "Izberi stranko" }
            };

            RacunManager racunManager = new RacunManager();
            Dictionary<int, string> stranke = racunManager.GetAllStranke();

            foreach (var stranka in stranke)
            {
                items.Add(new { Key = stranka.Key, Value = stranka.Value });
            }

            cmbStranka.DataSource = new BindingSource(items, null);
            cmbStranka.DisplayMember = "Value";
            cmbStranka.ValueMember = "Key";
            cmbStranka.SelectedIndex = 0; // Nastavi začetni element kot izbran
        }

        public void FiltrirajTabeloPoStranki(int strankaID)
        {
            RacunManager racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetRacuniPoStranki(strankaID);

            dataGridView.DataSource = racuni;

            // Nastavi imena stolpcev
            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Datum"].HeaderText = "Izdano";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Opravljeno";
            dataGridView.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridView.Columns["CenaDelo"].HeaderText = "Cena dela";
            dataGridView.Columns["CenaMaterial"].HeaderText = "Cena materiala";
            dataGridView.Columns["SkupnaCena"].HeaderText = "Skupna cena";

            dataGridView.Columns["RacunGlavaID"].Visible = false;
            dataGridView.Columns["StrankaID"].Visible = false;
        }

        public void FiltrirajTabeloPoDatumu(DateTime datumOd, DateTime datumDo)
        {
            RacunManager racunManager = new RacunManager();

            List<RacunGlava> racuni = racunManager.GetRacuniPoDatumu(datumOd, datumDo);

            dataGridView.DataSource = racuni;

            // Nastavi imena stolpcev
            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Datum"].HeaderText = "Izdano";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Opravljeno";
            dataGridView.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridView.Columns["CenaDelo"].HeaderText = "Cena dela";
            dataGridView.Columns["CenaMaterial"].HeaderText = "Cena materiala";
            dataGridView.Columns["SkupnaCena"].HeaderText = "Skupna cena";

            dataGridView.Columns["RacunGlavaID"].Visible = false;
            dataGridView.Columns["StrankaID"].Visible = false;
        }
        #endregion methods

        #region events
        private void btnFiltriraj_Click(object sender, EventArgs e)
        {
            if (cmbStranka.SelectedValue != null && cmbStranka.Text != "Izberi stranko" && btnIzberiStranko.Enabled == false && btnIzberiDatumOdDo.Enabled == true && btnIzberiSkupnoCenoOdDo.Enabled == true)
            {
                int izbranaStrankaID = (int)cmbStranka.SelectedValue;
                FiltrirajTabeloPoStranki(izbranaStrankaID);
            }

            else if(btnIzberiStranko.Enabled == true && btnIzberiDatumOdDo.Enabled == false && btnIzberiSkupnoCenoOdDo.Enabled == true)
            {
                cmbStranka.Text = "Izberi stranko";
                DateTime datumOd = dtpDatumIzdanegaRacunaOd.Value;
                DateTime datumDo = dtpDatumIzdanegaRacunaDo.Value;

                FiltrirajTabeloPoDatumu(datumOd, datumDo);
            }
            else if(btnIzberiStranko.Enabled == false && btnIzberiDatumOdDo.Enabled == false && btnIzberiSkupnoCenoOdDo.Enabled == true)
            {
                //TODO
            }
        }

        private void btnResetirajFilter_Click(object sender, EventArgs e)
        {
            pripravitabeloRacunov();
            cmbStranka.Text = "Izberi stranko";
        }

        #endregion events

        private void btnIzberiStranko_Click(object sender, EventArgs e)
        {
            btnIzberiStranko.Enabled = false;
            btnIzberiDatumOdDo.Enabled = true;
            btnIzberiSkupnoCenoOdDo.Enabled=true;

        }

        private void btnIzberiDatumOdDo_Click(object sender, EventArgs e)
        {
            btnIzberiStranko.Enabled = true;
            btnIzberiDatumOdDo.Enabled = false;
            btnIzberiSkupnoCenoOdDo.Enabled = true;
        }

        private void btnIzberiSkupnoCenoOdDo_Click(object sender, EventArgs e)
        {
            btnIzberiStranko.Enabled = false;
            btnIzberiDatumOdDo.Enabled = true;
            btnIzberiSkupnoCenoOdDo.Enabled = false;
        }
    }

}