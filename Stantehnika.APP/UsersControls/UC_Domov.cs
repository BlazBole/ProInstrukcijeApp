using Google.Protobuf.WellKnownTypes;
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
        private bool jeIzbranaStranka = false;
        private bool jeIzbranDatum = false;
        private bool jeIzbranaCena = false;
        #endregion private members

        #region constructor
        public UC_Domov()
        {
            InitializeComponent();

            PripraviPodatkePrilivov();
            PripravitabeloRacunov();
            NapolniComboBoxStranka();
            NapolniComboBoxRacun();
            NastaviZacetniInKoncniDatum();
            lblDatum.Text = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("sl-SI"));
        }
        #endregion constructor

        #region methods

        public void PripraviPodatkePrilivov()
        {
            var (prviDanMeseca, zadnjiDanMeseca) = PridobiDatumskiObsegZaTekociMesec();

            PrikaziSteviloIzdaniRacunov(prviDanMeseca, zadnjiDanMeseca);
            PrikaziPriliveMaterialov(prviDanMeseca, zadnjiDanMeseca);
            PrikaziPrilivNeto(prviDanMeseca, zadnjiDanMeseca);
            PrikaziSkupniPriliv(prviDanMeseca, zadnjiDanMeseca);
            PrikaziStevilkoZadnjegaRacuna();
            PrikaziStrankoZadnjegaRacuna();
            PrikazidatumZadnjegaRacuna();
            PrikaziSkupnoCenoZadnjegaRacuna();
        }

        public (DateTime prviDanMeseca, DateTime zadnjiDanMeseca) PridobiDatumskiObsegZaTekociMesec()
        {
            DateTime prviDanMeseca = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime zadnjiDanMeseca = prviDanMeseca.AddMonths(1).AddDays(-1);
            return (prviDanMeseca, zadnjiDanMeseca);
        }

        public void PrikaziSteviloIzdaniRacunov(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            RacunManager racunManager = new RacunManager();
            int steviloRacunov = racunManager.GetSteviloIzdaniRacunovZaMesec(prviDanMeseca, zadnjiDanMeseca);

            lblStIzdanihRacunov.Text = steviloRacunov.ToString();
        }

        public void PrikaziPriliveMaterialov(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            RacunManager racunManager = new RacunManager();
            decimal vsotaMaterialov = racunManager.GetVsotaMaterialovZaMesec(prviDanMeseca, zadnjiDanMeseca);

            lblPrilivMaterial.Text = vsotaMaterialov.ToString("N2") + " €";
        }

        public void PrikaziPrilivNeto(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            RacunManager racunManager = new RacunManager();
            decimal vsotaDela = racunManager.GetVsotaDelaZaMesec(prviDanMeseca, zadnjiDanMeseca);

            lblPrilivNeto.Text = vsotaDela.ToString("N2") + " €";
        }

        private void PrikaziSkupniPriliv(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            RacunManager racunManager = new RacunManager();
            decimal skupniPriliv = racunManager.GetSkupniPrilivZaMesec(prviDanMeseca, zadnjiDanMeseca);

            lblPrilivSkupno.Text = skupniPriliv.ToString("N2") + " €";
        }

        public void PrikaziStevilkoZadnjegaRacuna()
        {
            RacunManager racunManager = new RacunManager();
            string stevilkaZadnjegaRacuna = racunManager.GetStevilkaZadnjegaRacuna();

            lblStevilkaZadnjegaRacuna.Text = stevilkaZadnjegaRacuna ?? "N/A";
        }

        public void PrikaziStrankoZadnjegaRacuna()
        {
            RacunManager racunManager = new RacunManager();
            string strankaZadnjegaRacuna = racunManager.GetStrankaZadnjegaRacuna();

            lblStrankaZadnjegaRacuna.Text = strankaZadnjegaRacuna ?? "N/A";
        }

        public void PrikazidatumZadnjegaRacuna()
        {
            RacunManager racunManager = new RacunManager();
            DateTime? datumZadnjegaRacuna = racunManager.GetDatumZadnjegaRacuna();

            if (datumZadnjegaRacuna.HasValue)
            {
                lblDatumIzdajeZadnjegaRacuna.Text = datumZadnjegaRacuna.Value.ToString("dd.MM.yyyy");
            }
            else
            {
                lblDatumIzdajeZadnjegaRacuna.Text = "N/A";
            }
        }

        public void PrikaziSkupnoCenoZadnjegaRacuna()
        {
            RacunManager racunManager = new RacunManager();
            decimal skupnaCena = racunManager.GetSkupnaCenaZadnjegaRacuna();

            lblSkupnaCenaZadnjegaRacuna.Text = skupnaCena.ToString("N2") + " €";
        }

        public void PripravitabeloRacunov()
        {
            racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetAllRacuni();

            dataGridView.DataSource = racuni;

            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Datum"].HeaderText = "Izdano";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Opravljeno";
            dataGridView.Columns["NazivPodjetja"].HeaderText = "Stranka";
            dataGridView.Columns["CenaDelo"].HeaderText = "Cena dela";
            dataGridView.Columns["SkupnaCena"].HeaderText = "Skupna cena";


            dataGridView.Columns["StevilkaRacuna"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Datum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["DatumOpravljeno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["NazivPodjetja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["CenaDelo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["CenaMaterial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["SkupnaCena"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.Columns["Datumzapade"].Visible = false;
            dataGridView.Columns["Kraj"].Visible = false;
            dataGridView.Columns["RacunGlavaID"].Visible = false;
            dataGridView.Columns["StrankaID"].Visible = false;
            dataGridView.Columns["Stranka"].Visible = false;
            dataGridView.Columns["ImeInPriimek"].Visible = false;
            dataGridView.Columns["CenaMaterial"].Visible = false;
            dataGridView.Columns["XMLPodatki"].Visible = false;

            if (racuni.Count > 0)
            {
                if (string.IsNullOrEmpty(racuni[0].NazivPodjetja))
                {
                    dataGridView.Columns["NazivPodjetja"].Visible = false;
                    dataGridView.Columns["ImeInPriimek"].Visible = true;
                }
                else
                {
                    dataGridView.Columns["NazivPodjetja"].Visible = true;
                    dataGridView.Columns["ImeInPriimek"].Visible = false;
                }
            }
        }

        public void NapolniComboBoxRacun()
        {
            var items = new List<dynamic>
            {
                new { Key = -1, Value = "Izberi račun" }
            };

            RacunManager racunManager = new RacunManager();
            Dictionary<int, string> racuni = racunManager.GetRacun();

            foreach (var racun in racuni)
            {
                items.Add(new { Key = racun.Key, Value = racun.Value });
            }

            cmbRacun.DataSource = new BindingSource(items, null);
            cmbRacun.DisplayMember = "Value";
            cmbRacun.ValueMember = "Key";
            cmbRacun.SelectedIndex = 0;
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
            cmbStranka.SelectedIndex = 0; 
        }

        public void NastaviZacetniInKoncniDatum()
        {
            dtpDatumIzdanegaRacunaOd.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpDatumIzdanegaRacunaDo.Value = DateTime.Today;
        }

        public void FiltrirajTabeloPoRacunu(int racunID)
        {
            RacunManager racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetRacuniPoRacunu(racunID);

            dataGridView.DataSource = racuni;

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

        public void FiltrirajTabeloPoStranki(int strankaID)
        {
            RacunManager racunManager = new RacunManager();
            List<RacunGlava> racuni = racunManager.GetRacuniPoStranki(strankaID);

            dataGridView.DataSource = racuni;

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

        public void FiltrirajTabeloPoSkupniceni(int cenaOd, int cenaDo)
        {
            RacunManager racunManager = new RacunManager();

            List<RacunGlava> racuni = racunManager.GetRacuniPoSkupniceni(cenaOd, cenaDo);

            dataGridView.DataSource = racuni;

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

        public int PretvoriBesediloVStevilko(string besedilo)
        {
            if (int.TryParse(besedilo.Replace(".", "").Replace(",", ""), out int stevilo))
            {
                return stevilo;
            }
            else
            {
                return 0;
            }
        }

        private void ShowForm_VnosRacuna()
        {
            VnosRacuna form = new VnosRacuna();

            form.ShowDialog();
        }
        #endregion methods

        #region events
        private void btnFiltriraj_Click(object sender, EventArgs e)
        {

            if (jeIzbranaStranka && !jeIzbranDatum && !jeIzbranaCena)
            {
                int izbranaStrankaID = (int)cmbStranka.SelectedValue;
                FiltrirajTabeloPoStranki(izbranaStrankaID);
            }

            else if(!jeIzbranaStranka && jeIzbranDatum && !jeIzbranaCena)
            {
                cmbStranka.SelectedIndex = 0;
                DateTime datumOd = dtpDatumIzdanegaRacunaOd.Value;
                DateTime datumDo = dtpDatumIzdanegaRacunaDo.Value;

                FiltrirajTabeloPoDatumu(datumOd, datumDo);
            }

            else if(!jeIzbranaStranka && !jeIzbranDatum && jeIzbranaCena)
            {
                cmbStranka.SelectedIndex = 0;
                int cenaOd = PretvoriBesediloVStevilko(tbSkupnaCenaOd.Text);
                int cenaDo = PretvoriBesediloVStevilko(tbSkupnaCenaDo.Text);

                FiltrirajTabeloPoSkupniceni(cenaOd, cenaDo);
            }

            jeIzbranaStranka = false;
            jeIzbranDatum = false;
            jeIzbranaCena = false;

            btnIzberiStranko.Image = Properties.Resources.close;
            btnIzberiDatumOdDo.Image = Properties.Resources.close;
            btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.close;

        }

        private void btnIzberiStranko_Click(object sender, EventArgs e)
        {
            jeIzbranaStranka = !jeIzbranaStranka;

            jeIzbranDatum = false;
            jeIzbranaCena = false;
            btnIzberiDatumOdDo.Image = Properties.Resources.close;
            btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.close;

            if (jeIzbranaStranka)
            {
                btnIzberiStranko.Image = Properties.Resources.check;
            }
            else
            {
                btnIzberiStranko.Image = Properties.Resources.close;
            }
        }

        private void btnIzberiDatumOdDo_Click(object sender, EventArgs e)
        {
            jeIzbranDatum = !jeIzbranDatum;

            jeIzbranaStranka = false;
            jeIzbranaCena = false;

            cmbStranka.SelectedIndex = 0;

            btnIzberiStranko.Image = Properties.Resources.close;
            btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.close;

            if (jeIzbranDatum)
            {
                btnIzberiDatumOdDo.Image = Properties.Resources.check;
            }
            else
            {
                btnIzberiDatumOdDo.Image = Properties.Resources.close;
            };
        }

        private void btnIzberiSkupnoCenoOdDo_Click(object sender, EventArgs e)
        {
            jeIzbranaCena = !jeIzbranaCena;

            jeIzbranaStranka = false;
            jeIzbranDatum = false;
            btnIzberiStranko.Image = Properties.Resources.close;
            btnIzberiDatumOdDo.Image = Properties.Resources.close;

            if (jeIzbranaCena)
            {
                btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.check;
            }
            else
            {
                btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.close;
            };
        }

        private void tbSkupnaCenaOd_Leave(object sender, EventArgs e)
        {
            if(tbSkupnaCenaOd.Text == "")
            {
                tbSkupnaCenaOd.Text = "Cena od";
            }
        }

        private void tbSkupnaCenaDo_Leave(object sender, EventArgs e)
        {
            if (tbSkupnaCenaDo.Text == "")
            {
                tbSkupnaCenaDo.Text = "Cena do";
            }
        }

        private void cmbRacun_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbRacun.SelectedValue is int izbranRacunID && izbranRacunID != -1)
            {
                FiltrirajTabeloPoRacunu(izbranRacunID);
            }
        }

        private void cmbRacun_Leave(object sender, EventArgs e)
        {
            cmbRacun.SelectedIndex = 0;
        }

        private void tbSkupnaCenaOd_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSkupnaCenaOd.Text))
            {
                return;
            }

            if (decimal.TryParse(tbSkupnaCenaOd.Text, out decimal amount))
            {
                tbSkupnaCenaOd.Text = string.Format("{0:N0}", amount);
                tbSkupnaCenaOd.SelectionStart = tbSkupnaCenaOd.Text.Length;
                tbSkupnaCenaOd.ForeColor=Color.Black;
            }
        }

        private void tbSkupnaCenaDo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSkupnaCenaDo.Text))
            {
                return;
            }

            if (decimal.TryParse(tbSkupnaCenaDo.Text.Replace(",", "").Replace(".", ""), out decimal amount))
            {
                tbSkupnaCenaDo.Text = string.Format("{0:N0}", amount);
                tbSkupnaCenaDo.SelectionStart = tbSkupnaCenaDo.Text.Length;
                tbSkupnaCenaDo.ForeColor = Color.Black;
            }

        }

        private void tbSkupnaCenaOd_Click(object sender, EventArgs e)
        {
            tbSkupnaCenaOd.Text = "";
        }

        private void tbSkupnaCenaDo_Click(object sender, EventArgs e)
        {
            tbSkupnaCenaDo.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://stantehnika.si/",
                UseShellExecute = true
            });
        }

        private void phHelpFilter_MouseHover(object sender, EventArgs e)
        {
            lblFilterHelp.Visible = true;
        }

        private void phHelpFilter_MouseLeave(object sender, EventArgs e)
        {
            lblFilterHelp.Visible = false;
        }

        private void pbIzbrisiFiltre_Click(object sender, EventArgs e)
        {
            PripravitabeloRacunov();
            jeIzbranaStranka = false;
            jeIzbranDatum = false;
            jeIzbranaCena = false;

            cmbStranka.SelectedIndex = 0;
            cmbRacun.SelectedIndex = 0;
            tbSkupnaCenaOd.Text = "Cena od";
            tbSkupnaCenaDo.Text = "Cena do";
            tbSkupnaCenaOd.ForeColor = Color.DimGray;
            tbSkupnaCenaDo.ForeColor = Color.DimGray;

            btnIzberiStranko.Image = Properties.Resources.close;
            btnIzberiDatumOdDo.Image = Properties.Resources.close;
            btnIzberiSkupnoCenoOdDo.Image = Properties.Resources.close;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ShowForm_VnosRacuna();
        }
        #endregion events
    }
}