using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Stantehnika.APP.UsersControls;
using Stantehnika.Dal;
using Stantehnika.Model;

namespace Stantehnika.APP
{
    public partial class Form1 : Form
    {
        #region private members
        private RacunManager racunManager;
        #endregion

        #region constructor
        public Form1()
        {
            InitializeComponent();
            ShowUC_Home();
        }
        #endregion constructor

        #region methods
        private void ShowUC_Home()
        {
            UC_Domov homeControl = new UC_Domov();
            homeControl.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(homeControl);
        }
        #endregion methods

        #region events
        private void btnNovRacun_Click(object sender, EventArgs e)
        {
            
        }
        #endregion events
    }
}
