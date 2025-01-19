using Stantehnika.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Stantehnika.APP.UsersControls
{
    public partial class UC_Statistika : UserControl
    {
        #region constructor
        public UC_Statistika()
        {
            InitializeComponent();
            PrikaziNajdonosnejsiMesec();
            PrikaziNajdonosnejsoLeto();
            PrikaziStrankoZNajvecPrihodki();
            PrikaziSteviloStrank();
            NastaviLetaComboBox();
            int trenutnoLeto = int.Parse(cmbLeto.SelectedItem.ToString());
            PrikaziGrafPrilivov(trenutnoLeto);
            PrikaziGrafNovihStrank(trenutnoLeto);
        }
        #endregion constructor

        #region methods
        private void PrikaziNajdonosnejsiMesec()
        {
            RacunManager racunManager = new RacunManager();
            int trenutnoLeto = DateTime.Now.Year;

            var zasluzkiPoMesecih = racunManager.GetZasluzkiPoMesecihZaLeto(trenutnoLeto);

            if (zasluzkiPoMesecih.Any())
            {
                var najdonosnejsiMesec = zasluzkiPoMesecih.OrderByDescending(z => z.Value).First();

                string imeMeseca = PridobiImeMeseca(najdonosnejsiMesec.Key);

                lblNajdonosnejsiMesecLeta.Text = imeMeseca;
                lblZnesekNajdonosnejsegaMesca.Text = $"{najdonosnejsiMesec.Value:0.00} €";
            }
            else
            {
                lblNajdonosnejsiMesecLeta.Text = "Ni podatkov";
            }
        }

        private string PridobiImeMeseca(int mesec)
        {
            string[] imenaMesecov = {
                "Januar", "Februar", "Marec", "April", "Maj", "Junij",
                "Julij", "Avgust", "September", "Oktober", "November", "December"
            };

            return imenaMesecov[mesec - 1]; 
        }

        private string PridobiKraticoMeseca(int mesec)
        {
            string[] imenaMesecov = {
                "Jan", "Feb", "Mar", "Apr", "Maj", "Jun",
                "Jul", "Avg", "Sep", "Okt", "Nov", "Dec"
            };

            return imenaMesecov[mesec - 1];
        }
        private void PrikaziNajdonosnejsoLeto()
        {
            RacunManager racunManager = new RacunManager();

            var prihodkiPoLetih = racunManager.GetSkupniPrihodkiPoLetih();

            if (prihodkiPoLetih.Any())
            {
                var najdonosnejsoLeto = prihodkiPoLetih.OrderByDescending(p => p.Value).First();

                lblNajdonosnejsoLeto.Text = najdonosnejsoLeto.Key.ToString();
                lblNajdonosnejsoLetoInfo.Text = najdonosnejsoLeto.Key.ToString();
                lblZnesekNajdonosnejsegaLeta.Text = $"{najdonosnejsoLeto.Value:C}";
            }
            else
            {
                lblNajdonosnejsoLeto.Text = "Ni podatkov";
            }
        }

        private void PrikaziStrankoZNajvecPrihodki()
        {
            RacunManager racunManager = new RacunManager();
            var rezultat = racunManager.GetStrankaZNajvecPrihodki();

            if (!string.IsNullOrEmpty(rezultat.nazivStranke))
            {
                lblStrankaNajvecDonosi.Text = rezultat.nazivStranke;
                lblSkupniPriliviZaStrankoInfo.Text = $"{rezultat.znesek:C}";
            }
            else
            {
                lblStrankaNajvecDonosi.Text = "Ni podatkov";
            }
        }

        private void PrikaziSteviloStrank()
        {
            RacunManager racunManager = new RacunManager();
            var (steviloFizicnih, steviloPravnih) = racunManager.GetSteviloStrank();

            lblSteviloFizicnihStrank.Text = steviloFizicnih.ToString();
            lblSteviloPravnihStrank.Text = steviloPravnih.ToString();

            int skupnoSteviloStrank = steviloFizicnih + steviloPravnih;

            if (skupnoSteviloStrank > 0)
            {
                int delezFizicnih = (int)Math.Round((decimal)steviloFizicnih / skupnoSteviloStrank * 100);
                int delezPravnih = (int)Math.Round((decimal)steviloPravnih / skupnoSteviloStrank * 100);

                lblDelezFizicnihStrank.Text = $"{delezFizicnih} %";
                lblDelezPravnihStrank.Text = $"{delezPravnih} %";
            }
            else
            {
                lblDelezFizicnihStrank.Text = "0 %";
                lblDelezPravnihStrank.Text = "0 %";
            }
        }

        private void NastaviLetaComboBox()
        {
            int letoOdprtjeSP = 2024; 
            int trenutnoLeto = DateTime.Now.Year;

            cmbLeto.Items.Clear();
            for (int leto = letoOdprtjeSP; leto <= trenutnoLeto; leto++)
            {
                cmbLeto.Items.Add(leto);
            }

            cmbLeto.SelectedIndex = cmbLeto.Items.Count - 1;
        }

        private void PrikaziGrafPrilivov(int leto)
        {
            RacunManager racunManager = new RacunManager();
            var zasluzkiPoMesecih = racunManager.GetZasluzkiPoMesecihZaLeto(leto);

            chPrilivi.Series.Clear();

            Series columnSeries = new Series
            {
                Name = "Prilivi",
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            decimal najvecjiZasluzek = 0;

            foreach (var mesec in Enumerable.Range(1, 12))
            {
                string imeMeseca = PridobiKraticoMeseca(mesec);
                decimal zasluzek = zasluzkiPoMesecih.ContainsKey(mesec) ? zasluzkiPoMesecih[mesec] : 0;
                columnSeries.Points.AddXY(imeMeseca, zasluzek);

                if (zasluzek > najvecjiZasluzek)
                {
                    najvecjiZasluzek = zasluzek;
                }
            }

            chPrilivi.Series.Add(columnSeries);

            Series lineSeries = new Series
            {
                Name = "Krivulja",
                ChartType = SeriesChartType.Line,
                BorderWidth = 1,
                XValueType = ChartValueType.String,
                Color = Color.LightSkyBlue 
            };

            foreach (var mesec in Enumerable.Range(1, 12))
            {
                decimal zasluzek = zasluzkiPoMesecih.ContainsKey(mesec) ? zasluzkiPoMesecih[mesec] : 0;
                lineSeries.Points.AddXY(PridobiImeMeseca(mesec), zasluzek);
            }

            chPrilivi.Series.Add(lineSeries);

            var xAxis = chPrilivi.ChartAreas[0].AxisX;
            xAxis.Interval = 1;
            xAxis.Title = "Meseci";
            xAxis.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            var yAxis = chPrilivi.ChartAreas[0].AxisY;
            yAxis.Title = "Prilivi (€)";
            yAxis.Maximum = (double)(najvecjiZasluzek + najvecjiZasluzek * 0.1m);
            yAxis.Minimum = 0;

            yAxis.Interval = Math.Ceiling((double)(najvecjiZasluzek / 5));
            chPrilivi.Legends.Clear();
        }

        private void PrikaziGrafNovihStrank(int leto)
        {
            RacunManager racunManager = new RacunManager();
            var steviloNovihStrankPoMesecih = racunManager.GetSteviloNovihStrankPoMesecih(leto);

            chStranke.Series.Clear();

            // Stolpčni graf za število novih strank
            Series columnSeries = new Series
            {
                Name = "Novih Strank",
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            int najvecNovihStrank = 0;

            foreach (var mesec in Enumerable.Range(1, 12))
            {
                string imeMeseca = PridobiKraticoMeseca(mesec); // Skrajšana imena mesecev (npr. Jan, Feb)
                int steviloNovihStrank = steviloNovihStrankPoMesecih.ContainsKey(mesec)
                    ? steviloNovihStrankPoMesecih[mesec]
                    : 0;

                columnSeries.Points.AddXY(imeMeseca, steviloNovihStrank);

                if (steviloNovihStrank > najvecNovihStrank)
                {
                    najvecNovihStrank = steviloNovihStrank;
                }
            }

            chStranke.Series.Add(columnSeries);

            // Linijski graf za krivuljo
            Series lineSeries = new Series
            {
                Name = "Krivulja",
                ChartType = SeriesChartType.Line,
                BorderWidth = 1,
                XValueType = ChartValueType.String,
                Color = Color.LightSkyBlue
            };

            foreach (var mesec in Enumerable.Range(1, 12))
            {
                int steviloNovihStrank = steviloNovihStrankPoMesecih.ContainsKey(mesec)
                    ? steviloNovihStrankPoMesecih[mesec]
                    : 0;

                lineSeries.Points.AddXY(PridobiImeMeseca(mesec), steviloNovihStrank);
            }

            chStranke.Series.Add(lineSeries);

            // Nastavitve osi
            var xAxis = chStranke.ChartAreas[0].AxisX;
            xAxis.Interval = 1;
            xAxis.Title = "Meseci";
            xAxis.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            var yAxis = chStranke.ChartAreas[0].AxisY;
            yAxis.Title = "Število Novih Strank";
            yAxis.Maximum = najvecNovihStrank + Math.Ceiling(najvecNovihStrank * 0.1); // 10 % dodatek za večji prostor
            yAxis.Minimum = 0;

            yAxis.Interval = Math.Ceiling(najvecNovihStrank / 5.0);
            chStranke.Legends.Clear(); // Odstranimo legendo za bolj čist izgled
        }


        #endregion methods

        #region events
        private void cmbLeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLeto.SelectedItem != null)
            {
                int izbranoLeto = int.Parse(cmbLeto.SelectedItem.ToString());
                PrikaziGrafPrilivov(izbranoLeto);
                PrikaziGrafNovihStrank(izbranoLeto);
            }
        }

        private void chPrilivi_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chPrilivi.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                int pointIndex = result.PointIndex;
                var series = result.Series;
                string mesec = series.Points[pointIndex].AxisLabel;
                decimal znesek = (decimal)series.Points[pointIndex].YValues[0];

                MessageBox.Show($"Mesec: {mesec}\nZnesek: {znesek} €", "Podrobnosti");
            }
        }

        private void chStranke_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chStranke.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                int pointIndex = result.PointIndex;
                var series = result.Series;
                string mesec = series.Points[pointIndex].AxisLabel;
                int steviloStrank = (int)series.Points[pointIndex].YValues[0];

                MessageBox.Show($"Mesec: {mesec}\nŠtevilo novih strank: {steviloStrank}", "Podrobnosti");
            }
        }
        private void lblShraniRacun_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Shrani graf kot sliko",
                FileName = "ProinstrukcijeStatistika_" + DateTime.Now.Year + ".png"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                chPrilivi.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
                MessageBox.Show("Graf uspešno shranjen!", "Obvestilo");
            }
        }
        #endregion events
    }
}
