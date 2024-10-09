using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using OfficeOpenXml;
using Spire.Doc.Documents;
using Spire.Doc;
using Stantehnika.Dal;
using Stantehnika.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Xml;
using System.Xml.Xsl;
using System.Web.UI.WebControls.WebParts;
using System.Web;

using ceTe.DynamicPDF.HtmlConverter;


namespace Stantehnika.APP
{
    public partial class VnosRacuna : Form
    {
        #region private members
        private List<string> materialiList = new List<string>();
        #endregion private members

        public VnosRacuna()
        {
            InitializeComponent();
            PripraviIzbiroRacunov();
            PripraviDatumeZaRacun();
            PripraviZadetkeZaStranke();
            NastaviTabeloPostavk();

        }

        #region private methods
        public void PripraviIzbiroRacunov()
        {
            cmbVrstaRacuna.Items.Clear();

            cmbVrstaRacuna.Items.Add("RAČUN");
            cmbVrstaRacuna.Items.Add("PREDRAČUN");

            cmbVrstaRacuna.SelectedIndex = 0;
        }

        public void PripraviDatumeZaRacun()
        {
            dtpDatum.Value = DateTime.Today;
            dtpDatumOpravljeno.Value = DateTime.Today;
            DateTime datumZapadlosti = DateTime.Today.AddDays(5);
            if (datumZapadlosti.DayOfWeek == DayOfWeek.Saturday)
            {
                datumZapadlosti = datumZapadlosti.AddDays(2);
            }
            else if (datumZapadlosti.DayOfWeek == DayOfWeek.Sunday)
            {
                datumZapadlosti = datumZapadlosti.AddDays(1);
            }

            dtpDatumZapade.Value = datumZapadlosti;
        }

        public void PripraviZadetkeZaStranke()
        {
            dataGridViewPredlogi.Visible = false;
            lblNaslovnikPodjetje.Visible = false;
            gbNaslovnikPodjetje.Visible= false;
            lblIzberiStranko.Visible = false;
            gbPodatkiFizicneOsebe.Visible = false;
        }

        public void ValidateStevikaRacuna()
        {
            string racunStevilka = tbStevikaRacuna.Text.Replace(" ", "");

            string pattern = @"^\d{4}-\d{1,2}$";

            if (System.Text.RegularExpressions.Regex.IsMatch(racunStevilka, pattern))
            {
                string[] parts = racunStevilka.Split('-');
                string leto = parts[0];  
                string stevilka = parts[1]; 

                if (int.TryParse(stevilka, out int number))
                {
                    if (number < 10)
                    {
                        stevilka = number.ToString();  
                    }

                    tbStevikaRacuna.Text = $"{leto}-{stevilka}";

                    tbStevikaRacuna.SelectionStart = tbStevikaRacuna.Text.Length;
                }
            }
            else
            {
                MessageBox.Show("Številka računa mora biti v formatu LLLL-N ali LLLL-NN (npr. 2024-1 ali 2024-23).",
                                "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tbStevikaRacuna.Clear();
                tbStevikaRacuna.Focus();
            }
        }

        private void NastaviTabeloPostavk()
        {
            dgvPostavke.Columns.Clear();

            dgvPostavke.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPostavke.DefaultCellStyle.Font = new Font("Segoe UI", 14);

            dgvPostavke.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvPostavke.EnableHeadersVisualStyles = false;

            dgvPostavke.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Poz.",
                Name = "Poz",
                DataPropertyName = "StevilkaPostavke",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true
            });

            dgvPostavke.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Vrsta Blaga - storitev",
                DataPropertyName = "Storitev",
                Name = "storitev",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvPostavke.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Kol.",
                DataPropertyName = "Kolicina",
                Name = "Kol",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Enota",
                DataPropertyName = "EnotaMerjenja",
                Name = "EnotaMerjenja",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DataSource = new string[] { "m²", "m", "tm", "kom.", "kpl.", "ura", "kos"}
            };
            dgvPostavke.Columns.Add(comboBoxColumn);

            dgvPostavke.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cena",
                DataPropertyName = "CenaEneKolicine",
                Name = "CenaEneKolicine",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Format = "C2" } 
            });

            dgvPostavke.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "EUR",
                DataPropertyName = "CenaPostavke",
                Name = "CenaPostavke",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, 
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Format = "C2" }
            });

            dgvPostavke.AllowUserToAddRows = false;
            DodajVrstico();

            dgvPostavke.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvPostavke.CellValidating += dgvPostavke_CellValidating;
            dgvPostavke.CellValueChanged += dgvPostavke_CellValueChanged;

        }

        public void DodajVrstico()
        {
            int newRowIndex = dgvPostavke.Rows.Add();

            dgvPostavke.Rows[newRowIndex].Cells["Poz"].Value = newRowIndex + 1;
        }

        public void OdstraniVrstico()
        {
            if (dgvPostavke.SelectedRows.Count > 0)
            {
                dgvPostavke.Rows.RemoveAt(dgvPostavke.SelectedRows[0].Index);
                PosodobiStevilkaPostavke();
                IzracunajSkupnoCeno();
            }
            else
            {
                MessageBox.Show("Izberite vrstico, ki jo želite odstraniti.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void PosodobiStevilkaPostavke()
        {
            for (int i = 0; i < dgvPostavke.Rows.Count; i++)
            {
                dgvPostavke.Rows[i].Cells["Poz"].Value = i + 1;
            }
        }

        public void IzracunajSkupnoCeno()
        {
            decimal skupnaCena = 0;
            foreach (DataGridViewRow row in dgvPostavke.Rows)
            {
                if (row.Cells["CenaPostavke"].Value != null && decimal.TryParse(row.Cells["CenaPostavke"].Value.ToString(), out decimal cena))
                {
                    skupnaCena += cena;
                }
            }
            lblSkupajzaPlacilo.Text = $"{skupnaCena:N2} €";
        }

        private void ShraniVExcel()
        {
            string imeDatoteke = "račun_" + tbStevikaRacuna.Text + ".xlsx";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = imeDatoteke; 
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Shrani Excel datoteko";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Račun");

                    int currentRow = 1;

                    if (gbNaslovnikPodjetje.Visible)
                    {
                        worksheet.Cells[currentRow, 1].Value = "Naziv podjetja:";
                        worksheet.Cells[currentRow, 2].Value = lblNaslovnikPodjetje.Text;

                        currentRow++;
                        worksheet.Cells[currentRow, 1].Value = "Davčna številka:";
                        worksheet.Cells[currentRow, 2].Value = lblDavcnaStevilkaPodjetje.Text;

                        currentRow++;
                        worksheet.Cells[currentRow, 1].Value = "PE:";
                        worksheet.Cells[currentRow, 2].Value = lblPEPodjetje.Text;

                        currentRow++;
                        worksheet.Cells[currentRow, 1].Value = "E-naslov:";
                        worksheet.Cells[currentRow, 2].Value = lblEnaslovPodjetje.Text;
                    }

                    if (gbPodatkiFizicneOsebe.Visible)
                    {
                        worksheet.Cells[currentRow, 1].Value = "Fizična oseba:";
                        worksheet.Cells[currentRow, 2].Value = lblStrankaFizicnaOseba.Text;

                        currentRow++;
                        worksheet.Cells[currentRow, 1].Value = "Naslov:";
                        worksheet.Cells[currentRow, 2].Value = lblNaslovFizicnaOseba.Text;

                        currentRow++;
                        worksheet.Cells[currentRow, 1].Value = "E-naslov:";
                        worksheet.Cells[currentRow, 2].Value = lblEnaslovFizicnaOseba.Text;
                    }

                    currentRow++; 

                    worksheet.Cells[currentRow, 1].Value = "Številka računa:";
                    worksheet.Cells[currentRow, 2].Value = tbStevikaRacuna.Text;

                    currentRow++;
                    worksheet.Cells[currentRow, 1].Value = "Kraj:";
                    worksheet.Cells[currentRow, 2].Value = lblKraj.Text;

                    currentRow++;
                    worksheet.Cells[currentRow, 1].Value = "Datum:";
                    worksheet.Cells[currentRow, 2].Value = dtpDatum.Value.ToShortDateString();

                    currentRow++;
                    worksheet.Cells[currentRow, 1].Value = "Datum opravljeno:";
                    worksheet.Cells[currentRow, 2].Value = dtpDatumOpravljeno.Value.ToShortDateString();

                    currentRow++;
                    worksheet.Cells[currentRow, 1].Value = "Datum zapade:";
                    worksheet.Cells[currentRow, 2].Value = dtpDatumZapade.Value.ToShortDateString();

                    currentRow++; 

                    // 3. Tabela postavk
                    worksheet.Cells[currentRow, 1].Value = "EM"; 
                    worksheet.Cells[currentRow, 2].Value = "Kol.";
                    worksheet.Cells[currentRow, 3].Value = "Cena";
                    worksheet.Cells[currentRow, 4].Value = "CenaPostavke";

                    currentRow++; 

                    for (int i = 0; i < dgvPostavke.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvPostavke.Columns.Count; j++)
                        {
                            worksheet.Cells[currentRow, j + 1].Value = dgvPostavke.Rows[i].Cells[j].Value;
                        }
                        currentRow++; 
                    }

                    currentRow++; 

                    worksheet.Cells[currentRow, 1].Value = "Skupaj za plačilo:";
                    worksheet.Cells[currentRow, 2].Value = lblSkupajzaPlacilo.Text;

                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                    excelPackage.SaveAs(excelFile);

                    MessageBox.Show("Datoteka je bila uspešno shranjena.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<Stantehnika.Model.RacunPostavka> PridobiPostavkeIzDataGridView()
        {
            List<Stantehnika.Model.RacunPostavka> postavke = new List<Stantehnika.Model.RacunPostavka>();

            foreach (DataGridViewRow row in dgvPostavke.Rows)
            {
                if (row.IsNewRow) continue; // Preskoči prazne vrstice

                Stantehnika.Model.RacunPostavka postavka = new Stantehnika.Model.RacunPostavka
                {
                    StevilkaPostavke = Convert.ToInt32(row.Cells["Poz"].Value),
                    Storitev = row.Cells["storitev"].Value.ToString(),
                    Kolicina = Convert.ToDecimal(row.Cells["Kol"].Value),
                    EnotaMerjenja = row.Cells["EnotaMerjenja"].Value.ToString(),
                    CenaEneKolicine = Convert.ToDecimal(row.Cells["CenaEneKolicine"].Value),
                    CenaPostavke = Convert.ToDecimal(row.Cells["CenaPostavke"].Value)
                };

                postavke.Add(postavka);
            }

            return postavke;
        }

        public string pripraviPodatkeXML()
        {
            // Zberi podatke iz forme
            string nazivPodjetja = gbNaslovnikPodjetje.Visible ? lblNaslovnikPodjetje.Text : null;
            string davcnaStevilka = gbNaslovnikPodjetje.Visible ? lblDavcnaStevilkaPodjetje.Text : null;
            string pe = gbNaslovnikPodjetje.Visible ? lblPEPodjetje.Text : null;
            string eNaslovPodjetja = gbNaslovnikPodjetje.Visible ? lblEnaslovPodjetje.Text : null;

            string fizicnaOseba = gbPodatkiFizicneOsebe.Visible ? lblStrankaFizicnaOseba.Text : null;
            string naslovFizicneOsebe = gbPodatkiFizicneOsebe.Visible ? lblNaslovFizicnaOseba.Text : null;
            string eNaslovFizicneOsebe = gbPodatkiFizicneOsebe.Visible ? lblEnaslovFizicnaOseba.Text : null;

            string stevilkaRacuna = tbStevikaRacuna.Text;
            string kraj = lblKraj.Text;
            string datum = dtpDatum.Value.ToShortDateString();
            string datumOpravljeno = dtpDatumOpravljeno.Value.ToShortDateString();
            string datumZapade = dtpDatumZapade.Value.ToShortDateString();
            string skupajZaPlacilo = lblSkupajzaPlacilo.Text;

            // Uporabi StringWriter za zapisovanje XML-ja
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Racun");

                    // Podjetje
                    if (gbNaslovnikPodjetje.Visible)
                    {
                        writer.WriteStartElement("Podjetje");
                        writer.WriteElementString("Naziv", nazivPodjetja);
                        writer.WriteElementString("PE", pe);
                        writer.WriteElementString("DavcnaStevilka", davcnaStevilka);
                        writer.WriteElementString("ENaslov", eNaslovPodjetja);
                        writer.WriteEndElement(); // Podjetje
                    }

                    // Fizična oseba
                    if (gbPodatkiFizicneOsebe.Visible)
                    {
                        writer.WriteStartElement("FizicnaOseba");
                        writer.WriteElementString("Ime", fizicnaOseba);
                        writer.WriteElementString("Naslov", naslovFizicneOsebe);
                        writer.WriteElementString("ENaslov", eNaslovFizicneOsebe);
                        writer.WriteEndElement(); // FizicnaOseba
                    }

                    // Račun
                    writer.WriteStartElement("RacunPodatki");
                    writer.WriteElementString("Stevilka", stevilkaRacuna);
                    writer.WriteElementString("Kraj", kraj);
                    writer.WriteElementString("Datum", datum);
                    writer.WriteElementString("DatumOpravljeno", datumOpravljeno);
                    writer.WriteElementString("DatumZapade", datumZapade);
                    writer.WriteElementString("SkupajZaPlacilo", skupajZaPlacilo);
                    writer.WriteEndElement(); // RacunPodatki

                    // Postavke
                    writer.WriteStartElement("Postavke");
                    for (int i = 0; i < dgvPostavke.Rows.Count; i++)
                    {
                        writer.WriteStartElement("Postavka");

                        // Preveri, ali celice vsebujejo vrednosti
                        if (dgvPostavke.Rows[i].Cells[0].Value != null)
                        {
                            writer.WriteElementString("Poz", dgvPostavke.Rows[i].Cells[0].Value.ToString());
                        }
                        if (dgvPostavke.Rows[i].Cells[1].Value != null)
                        {
                            writer.WriteElementString("Storitev", dgvPostavke.Rows[i].Cells[1].Value.ToString());
                        }
                        if (dgvPostavke.Rows[i].Cells[2].Value != null)
                        {
                            writer.WriteElementString("Kol", dgvPostavke.Rows[i].Cells[2].Value.ToString());
                        }
                        if (dgvPostavke.Rows[i].Cells[3].Value != null)
                        {
                            writer.WriteElementString("Enota", dgvPostavke.Rows[i].Cells[3].Value.ToString());
                        }
                        if (dgvPostavke.Rows[i].Cells[4].Value != null)
                        {
                            writer.WriteElementString("Cena", dgvPostavke.Rows[i].Cells[4].Value.ToString());
                        }
                        if (dgvPostavke.Rows[i].Cells[5].Value != null)
                        {
                            writer.WriteElementString("Skupno", dgvPostavke.Rows[i].Cells[5].Value.ToString());
                        }

                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    // Dodajanje materiala
                    writer.WriteStartElement("Material"); // material

                    // Pridobi vse vrstice iz RichTextBox
                    string[] materials = rtbmaterial.Lines;
                    foreach (var material in materials)
                    {
                        if (!string.IsNullOrWhiteSpace(material)) // Preveri, ali vrstica ni prazna
                        {
                            // Odstrani prvo pikico in presledek (npr. "• " postane "")
                            string cleanedMaterial = material.TrimStart('•', ' '); // Odstrani '•' in presledek

                            // Zapiši material kot element
                            writer.WriteElementString("MaterialItem", cleanedMaterial);
                        }
                    }

                    writer.WriteEndElement(); // Končaj element Material

                    // Zaključek
                    writer.WriteEndElement(); // Racun
                    writer.WriteEndDocument();
                }

                // Pridobi niz iz StringWriter
                string xmlString = stringWriter.ToString();

                // Vrni ustvarjeni XML niz
                return xmlString;
            }
        }

        public void PretvoriV_PDF()
        {
            Uri htmlFilePath = new Uri(@"C:\Users\bole\source\repos\Stantehnika.APP\Stantehnika.APP\bin\Debug\racun.html"); // Pot do vaše HTML datoteke

            string pdfFilePath = @"C:\Users\bole\source\repos\Stantehnika.APP\Stantehnika.APP\bin\Debug\racun.pdf";

            Converter.Convert(htmlFilePath, pdfFilePath);
        }


        #endregion private methods

        #region events
        private void pbPrikaziVecInfoPodjetja_Click_1(object sender, EventArgs e)
        {
            PregledInfoPodjetja form = new PregledInfoPodjetja();

            form.ShowDialog();
        }

        private void pbPrikaziVecInfoPodjetja_MouseEnter_1(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Hand;
        }

        private void pbPrikaziVecInfoPodjetja_MouseLeave_1(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Default;
        }

        private void btnKoncajRacun_Click_1(object sender, EventArgs e)
        {
            RacunManager racunManager = new RacunManager();

            string strankaIme = "";
            bool jePodjetje = gbNaslovnikPodjetje.Visible;

            if (jePodjetje)
            {
                strankaIme = lblNaslovnikPodjetje.Text;
            }
            else
            {
                strankaIme = lblStrankaFizicnaOseba.Text;
            }

            string ulica = jePodjetje ? null : lblNaslovFizicnaOseba.Text;
            string email = jePodjetje ? lblEnaslovPodjetje.Text : lblEnaslovFizicnaOseba.Text;
            string davcnaStevilka = jePodjetje ? lblDavcnaStevilkaPodjetje.Text : null;
            string sedezPodjetja = jePodjetje ? lblPEPodjetje.Text : null;

            int strankaID = racunManager.GetStrankaID(strankaIme, jePodjetje, ulica, email, davcnaStevilka, sedezPodjetja);

            //int racunGlavaID = racunManager.DodajRacunGlava(tbStevikaRacuna.Text, lblKraj.Text, dtpDatum.Value, dtpDatumOpravljeno.Value, dtpDatumZapade.Value, strankaID);

            List<Stantehnika.Model.RacunPostavka> postavke = PridobiPostavkeIzDataGridView();

            //racunManager.DodajPostavkeZaRacun(racunGlavaID, postavke);

            // Shranjevanje materialov v bazo
            foreach (var material in materialiList)
            {
                //racunManager.DodajRacunMaterial(material, racunGlavaID);
            }

            // Pripravi podatke XML
            string xmlPodatki = pripraviPodatkeXML(); // Generirajte XML podatke
            string htmlContent = "";

            // Transformirajte XML v HTML
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("RacunPredloga.xslt"); // Pot do vašega XSLT datoteke

                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        using (StringReader sr = new StringReader(xmlPodatki))
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(sr.ReadToEnd());
                            xslt.Transform(xmlDoc, writer);
                        }
                    }
                    htmlContent = stringWriter.ToString(); // Pridobite HTML vsebino
                }

                // Shranite HTML v datoteko
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "C:\\Users\\bole\\source\\repos\\Stantehnika.APP\\Stantehnika.APP\\bin\\Debug\\racun.html");
                File.WriteAllText(filePath, htmlContent); // Shrani HTML v datoteko
                PretvoriV_PDF();

                MessageBox.Show($"Racun uspešno shranjen!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri generiranju HTML: {ex.Message}");
            }

        }

        private void tbIsciStranko_TextChanged_1(object sender, EventArgs e)
        {
            string input = tbIsciStranko.Text;

            if(input.Length >= 2 && input != "Išči stranko...")
            {

                RacunManager racunManager = new RacunManager();

                List<Stranka> predlogiStrank = racunManager.IsciStranke(input);

                if(predlogiStrank.Count > 0)
                {
                    dataGridViewPredlogi.Visible = true;
                    lblIzberiStranko.Visible = true;
                }
                else
                {
                  
                }
                dataGridViewPredlogi.DataSource = predlogiStrank;

                dataGridViewPredlogi.Columns["ImeInPriimek"].HeaderText = "Stranka";
                dataGridViewPredlogi.Columns["Email"].HeaderText = "E-naslov";


                dataGridViewPredlogi.Columns["StrankaID"].Visible = false;
                dataGridViewPredlogi.Columns["UlicaInHisnaStevilka"].Visible = false;
                dataGridViewPredlogi.Columns["PostaInKraj"].Visible = false;
                dataGridViewPredlogi.Columns["SedezPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["NazivPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["SedezPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["Naslov"].Visible = false;
                dataGridViewPredlogi.Columns["DavcnaStevilka"].Visible = false;

                dataGridViewPredlogi.Columns["ImeInPriimek"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewPredlogi.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                dataGridViewPredlogi.Visible = false;
                lblIzberiStranko.Visible = false;
            }
        }

        private void dataGridViewPredlogi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var selectedRow = dataGridViewPredlogi.Rows[e.RowIndex];
                string izbranaStranka = selectedRow.Cells["ImeInPriimek"].Value.ToString();


                lblNaslovnikPodjetje.Text = izbranaStranka;

                lblIzberiStranko.Visible = false;
                dataGridViewPredlogi.Visible = false;
                tbIsciStranko.Text = "Išči stranko...";
                lblNaslovnikPodjetje.Visible = true;

                RacunManager racunManager = new RacunManager();
                Stranka izbranaStrankaPodatki = racunManager.PridobiPodrobnostiStranke(izbranaStranka);

                if (izbranaStrankaPodatki != null && izbranaStrankaPodatki.NazivPodjetja != null)
                {
                    lblDavcnaStevilkaPodjetje.Text = izbranaStrankaPodatki.DavcnaStevilka;
                    lblPEPodjetje.Text = izbranaStrankaPodatki.SedezPodjetja;
                    lblEnaslovPodjetje.Text = izbranaStrankaPodatki.Email;
                    gbNaslovnikPodjetje.Visible = true;

                }
                else
                {
                    lblStrankaFizicnaOseba.Text = izbranaStrankaPodatki.ImeInPriimek;
                    lblNaslovFizicnaOseba.Text = $"{izbranaStrankaPodatki.UlicaInHisnaStevilka}, {izbranaStrankaPodatki.PostaInKraj}";
                    lblEnaslovFizicnaOseba.Text = izbranaStrankaPodatki.Email;
                    gbPodatkiFizicneOsebe.Visible = true;
                }

                dataGridViewPredlogi.Visible = false;
                tbIsciStranko.Text = "Išči stranko...";
            }
        }

        private void tbIsciStranko_Click(object sender, EventArgs e)
        {
            if (tbIsciStranko.Text == "Išči stranko...")
            {
                tbIsciStranko.Text = "";
                gbNaslovnikPodjetje.Visible = false;
                gbPodatkiFizicneOsebe.Visible = false;
            }
        }

        private void tbIsciStranko_Leave(object sender, EventArgs e)
        {
            if (tbIsciStranko.Text == "")
            {
                tbIsciStranko.Text = "Išči stranko...";
            }
        }


        private void cmbVrstaRacuna_TextChanged(object sender, EventArgs e)
        {
            if(cmbVrstaRacuna.Text == "RAČUN")
            {
                lblŠtevilkaRacunaInfo.Visible = true;
                tbStevikaRacuna.Visible = true;
                lblDatumOpravljenoInfo.Visible = true;
                dtpDatumOpravljeno.Visible = true;
                lblDatumZapadeInfo.Visible = true;
                dtpDatumZapade.Visible = true;
            }
            else if(cmbVrstaRacuna.Text == "PREDRAČUN")
            {
                lblŠtevilkaRacunaInfo.Visible = false;
                tbStevikaRacuna.Visible = false;
                lblDatumOpravljenoInfo.Visible = false;
                dtpDatumOpravljeno.Visible = false;
                lblDatumZapadeInfo.Visible = false;
                dtpDatumZapade.Visible = false;
            }
        }

        private void pbDodajVrstico_Click(object sender, EventArgs e)
        {
            DodajVrstico();
        }

        private void pbOdstraniVrstico_Click(object sender, EventArgs e)
        {
            OdstraniVrstico();
        }

        private void dgvPostavke_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvPostavke.Columns[e.ColumnIndex].Name == "Kol")
            {
                string inputValue = e.FormattedValue.ToString();

                if (inputValue.Contains("."))
                {
                    inputValue = inputValue.Replace(".", ",");
                }

                if (decimal.TryParse(inputValue, out decimal result))
                {
                    if (result % 1 != 0)
                    {
                        dgvPostavke.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = result.ToString("F2");
                    }
                    else
                    {
                        dgvPostavke.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = result.ToString();
                    }

                    dgvPostavke.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dgvPostavke.EndEdit();
                    dgvPostavke.Refresh();

                    dgvPostavke.Rows[e.RowIndex].ErrorText = string.Empty;
                }
                else
                {
                    dgvPostavke.Rows[e.RowIndex].ErrorText = "Vnesite veljavno število.";
                }
            }

            if (dgvPostavke.Columns[e.ColumnIndex].Name == "CenaEneKolicine")
            {
                string inputValue = e.FormattedValue.ToString();

                if (inputValue.Contains("."))
                {
                    inputValue = inputValue.Replace(".", ",");
                }

                if (decimal.TryParse(inputValue, out decimal result))
                {
                    dgvPostavke.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = result.ToString("N2");

                    dgvPostavke.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dgvPostavke.EndEdit();
                    dgvPostavke.Refresh();

                    dgvPostavke.Rows[e.RowIndex].ErrorText = string.Empty;
                }
                else
                {
                    dgvPostavke.Rows[e.RowIndex].ErrorText = "Vnesite veljavno število.";
                }
            }

        }

        private void dgvPostavke_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPostavke.Columns[e.ColumnIndex].Name == "Kol" || dgvPostavke.Columns[e.ColumnIndex].Name == "CenaEneKolicine")
            {
                var kolValue = dgvPostavke.Rows[e.RowIndex].Cells["Kol"].Value;
                var cenaValue = dgvPostavke.Rows[e.RowIndex].Cells["CenaEneKolicine"].Value;

                if (kolValue != null && decimal.TryParse(kolValue.ToString(), out decimal kolicina) &&
                    cenaValue != null && decimal.TryParse(cenaValue.ToString().Replace(".", ","), out decimal cena))
                {
                    decimal cenaPostavke = kolicina * cena;

                    dgvPostavke.Rows[e.RowIndex].Cells["CenaPostavke"].Value = cenaPostavke.ToString("N2");

                    IzracunajSkupnoCeno();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ShraniVExcel();
        }

        private void pbDodajMaterial_Click(object sender, EventArgs e)
        {
            // Ustvari novo okno za dodajanje materiala
            using (var dodajMaterialForm = new DodajMaterial())
            {
                // Odpri okno in preveri, ali je uporabnik potrdil dodajanje
                if (dodajMaterialForm.ShowDialog() == DialogResult.OK)
                {
                    // Pridobi ime materiala iz okna
                    string material = dodajMaterialForm.MaterialName;

                    // Dodaj material v RichTextBox
                    rtbmaterial.AppendText("• " + material + Environment.NewLine);

                    // Dodaj material v seznam
                    materialiList.Add(material);
                }
            }
        }

        #endregion events

    }
}
