using OfficeOpenXml;
using Stantehnika.Dal;
using Stantehnika.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using ceTe.DynamicPDF.HtmlConverter;
using Apitron.PDF.Kit.FixedLayout.Content;
using Apitron.PDF.Kit.FixedLayout.ContentElements;
using Apitron.PDF.Kit;
using System.Diagnostics;
using Aspose.Words.Bibliography;
using ceTe.DynamicPDF.LayoutEngine;
using ceTe.DynamicPDF;




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
            NastaviTabeloMateriala();

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
            dgvPostavke.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14);

            dgvPostavke.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
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

        private void NastaviTabeloMateriala()
        {
            dgvMaterial.Columns.Clear();

            dgvMaterial.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMaterial.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14);

            dgvMaterial.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgvMaterial.EnableHeadersVisualStyles = false;

            dgvMaterial.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Naziv",
                Name = "Naziv",
                DataPropertyName = "Naziv",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Onemogočite dodajanje vrstic, če je potrebno
            dgvMaterial.AllowUserToAddRows = false;

            // Samodejno prilagajanje višin vrstic
            dgvMaterial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Dodajte obravnavo dogodkov, če je potrebno
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

        public void DodajVrsticoMaterial()
        {
            int newRowIndex = dgvMaterial.Rows.Add(); // Dodajte novo vrstico

        }

        public void OdstraniVrsticoMaterial()
        {
            if (dgvMaterial.SelectedRows.Count > 0)
            {
                // Odstranite izbrano vrstico
                dgvMaterial.Rows.RemoveAt(dgvMaterial.SelectedRows[0].Index);
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
                    writer.WriteStartElement("Material"); // Začne element Material

                    // Preveri, ali obstajajo vrstice v dgvMaterial
                    for (int i = 0; i < dgvMaterial.Rows.Count; i++)
                    {
                        // Preveri, ali vrstica ni nova (ne vsebuje podatkov)
                        if (!dgvMaterial.Rows[i].IsNewRow)
                        {
                            writer.WriteStartElement("MaterialItem"); // Začne element MaterialItem

                            // Preveri, ali celice vsebujejo vrednosti
                            if (dgvMaterial.Rows[i].Cells["Naziv"].Value != null)
                            {
                                writer.WriteElementString("Naziv", dgvMaterial.Rows[i].Cells["Naziv"].Value.ToString());
                            }

                            // Dodajte morebitne druge celice, ki jih želite shraniti v XML
                            // Če imate več stolpcev, jih lahko dodate tukaj
                            // Na primer:
                            // if (dgvMaterial.Rows[i].Cells["DrugStolpec"].Value != null)
                            // {
                            //     writer.WriteElementString("DrugStolpec", dgvMaterial.Rows[i].Cells["DrugStolpec"].Value.ToString());
                            // }

                            writer.WriteEndElement(); // Končaj element MaterialItem
                        }
                    }

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

        public void ReplaceText(string inputFilePath, string oldText, string newText)
        {
            string outputFileName = "racunStantehnika.pdf"; // Specify the output file name

            using (Stream inputStream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read))
            {
                using (FixedDocument doc = new FixedDocument(inputStream))
                {
                    // Enumerate content elements found on each page
                    foreach (var page in doc.Pages)
                    {
                        foreach (IContentElement element in page.Elements)
                        {
                            // Handle the text element case
                            if (element.ElementType == ElementType.Text)
                            {
                                TextContentElement textElement = element as TextContentElement;
                                if (textElement != null)
                                {
                                    // Go through all the text segments and replace
                                    foreach (TextSegment textSegment in textElement.Segments)
                                    {
                                        if (textSegment.Text.Contains(oldText))
                                        {
                                            // Create a new text object with the new text
                                            TextObject newTextObject =
                                                new TextObject(textSegment.FontName, textSegment.FontSize);
                                            newTextObject.AppendText(newText); // Add the new text
                                            textSegment.ReplaceText(0, textSegment.Text.Length, newTextObject);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Save the modified file
                    using (Stream outputStream = File.Create(outputFileName))
                    {
                        doc.Save(outputStream);
                    }
                }
            }

            // Open the modified PDF file
            Process.Start(outputFileName);
        }

        public void IzberiPotZaShranjenRacun()
        {
            string originalPath = @"racunStantehnika.pdf"; 

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf"; 
                saveFileDialog.Title = "Shrani račun kot";
                saveFileDialog.FileName = "racunStantehnika.pdf"; 

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Kopiraj datoteko na izbrano mesto
                    try
                    {
                        File.Copy(originalPath, saveFileDialog.FileName, true); 
                        MessageBox.Show("Račun uspešno shranjen.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Napaka pri shranjevanju računa: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

        public void shraniPodatkeVracun()
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

            int racunGlavaID = racunManager.DodajRacunGlava(tbStevikaRacuna.Text, lblKraj.Text, dtpDatum.Value, dtpDatumOpravljeno.Value, dtpDatumZapade.Value, strankaID);

            List<Stantehnika.Model.RacunPostavka> postavke = PridobiPostavkeIzDataGridView();

            racunManager.DodajPostavkeZaRacun(racunGlavaID, postavke);

            // Shranjevanje materialov v bazo
            foreach (var material in materialiList)
            {
                racunManager.DodajRacunMaterial(material, racunGlavaID);
            }
        }
        public void generirajRacun()
        {
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
                ReplaceText("C:\\Users\\bole\\source\\repos\\Stantehnika.APP\\Stantehnika.APP\\bin\\Debug\\racun.pdf", "Created with the DynamicPDF Essentials Edition.", "");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka! {ex.Message}");
            }
        }

        private void btnKoncajRacun_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Ste prepričani, da želite zaključiti račun?", "Potrditev", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                shraniPodatkeVracun();    
                generirajRacun();        
                IzberiPotZaShranjenRacun();

                this.Close();
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


        private void pbDodajvrsticoMaterial_Click(object sender, EventArgs e)
        {
            DodajVrsticoMaterial();
        }

        private void pbOdstraniVrsticoMaterial_Click(object sender, EventArgs e)
        {
            OdstraniVrsticoMaterial();
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

        private void lblShraniRacun_Click(object sender, EventArgs e)
        {
            generirajRacun();
            IzberiPotZaShranjenRacun();
        }

        private void btnGenerirajRacun_Click(object sender, EventArgs e)
        {
            generirajRacun();
        }

        #endregion events
    }
}
