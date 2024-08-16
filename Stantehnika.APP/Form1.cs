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
        private RacunManager racunManager;
        public Form1()
        {
            InitializeComponent();
            racunManager = new RacunManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<RacunGlava> racuni = racunManager.GetAllRacuni();

            // Nastavi podatke v DataGridView
            dataGridView1.DataSource = racuni;
        }
    }
}
