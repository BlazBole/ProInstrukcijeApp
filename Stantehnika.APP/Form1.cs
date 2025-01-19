using System;
using System.Drawing;
using System.Windows.Forms;
using Stantehnika.APP.UsersControls;

namespace Stantehnika.APP
{
    public partial class Form1 : Form
    {
        #region private members
        #endregion

        #region constructor
        public Form1()
        {
            InitializeComponent();
            ShowUC_Domov();
        }
        #endregion constructor

        #region methods
        private void ShowUC_Domov()
        {
            UC_Domov homeControl = new UC_Domov();
            homeControl.Dock = DockStyle.Fill;
            lblGlavaProfil.Controls.Clear();
            lblGlavaProfil.Controls.Add(homeControl);
        }

        private void ShowUC_Stranke()
        {
            UC_Stranke homeControl = new UC_Stranke();
            homeControl.Dock = DockStyle.Fill;
            lblGlavaProfil.Controls.Clear();
            lblGlavaProfil.Controls.Add(homeControl);
        }

        private void ShowUC_Profil()
        {
            UC_Profil homeControl = new UC_Profil();
            homeControl.Dock = DockStyle.Fill;
            lblGlavaProfil.Controls.Clear();
            lblGlavaProfil.Controls.Add(homeControl);
        }

        private void ShowUC_Statistika()
        {
            UC_Statistika homeControl = new UC_Statistika();
            homeControl.Dock = DockStyle.Fill;
            lblGlavaProfil.Controls.Clear();
            lblGlavaProfil.Controls.Add(homeControl);
        }

        private void ShowForm_VnosRacuna()
        {
            VnosRacuna form = new VnosRacuna();

            form.ShowDialog();
        }
        #endregion methods

        #region events
        private void gbGlavaDelo_Paint(object sender, PaintEventArgs e)
        {
            if (gbGlavaDelo == null) return;

            using (Pen whitePen = new Pen(Color.White, 2)) 
            using (Pen greenPen = new Pen(Color.DodgerBlue, 1))
            {
                Rectangle borderRect = new Rectangle(gbGlavaDelo.ClientRectangle.X, gbGlavaDelo.ClientRectangle.Y + 7,
                                                     gbGlavaDelo.ClientRectangle.Width - 1, gbGlavaDelo.ClientRectangle.Height - 8);

                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Right, borderRect.Top);
                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Left, borderRect.Bottom);
                e.Graphics.DrawLine(whitePen, borderRect.Right, borderRect.Top, borderRect.Right, borderRect.Bottom);
                e.Graphics.DrawLine(greenPen, borderRect.Left, borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }
        }

        private void gbGlavaStranke_Paint(object sender, PaintEventArgs e)
        {
            if (gbGlavaStranke == null) return;

            using (Pen whitePen = new Pen(Color.White, 2))
            using (Pen greenPen = new Pen(Color.DodgerBlue, 1))
            {
                Rectangle borderRect = new Rectangle(gbGlavaStranke.ClientRectangle.X, gbGlavaStranke.ClientRectangle.Y + 7,
                                                     gbGlavaStranke.ClientRectangle.Width - 1, gbGlavaStranke.ClientRectangle.Height - 8);

                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Right, borderRect.Top);
                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Left, borderRect.Bottom);
                e.Graphics.DrawLine(whitePen, borderRect.Right, borderRect.Top, borderRect.Right, borderRect.Bottom);
                e.Graphics.DrawLine(greenPen, borderRect.Left, borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }
        }

        private void gbGlavaStatistika_Paint(object sender, PaintEventArgs e)
        {
            if (gbGlavaStatistika == null) return;

            using (Pen whitePen = new Pen(Color.White, 2))
            using (Pen greenPen = new Pen(Color.DodgerBlue, 1))
            {
                Rectangle borderRect = new Rectangle(gbGlavaStatistika.ClientRectangle.X, gbGlavaStatistika.ClientRectangle.Y + 7,
                                                     gbGlavaStatistika.ClientRectangle.Width - 1, gbGlavaStatistika.ClientRectangle.Height - 8);

                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Right, borderRect.Top);
                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Left, borderRect.Bottom);
                e.Graphics.DrawLine(whitePen, borderRect.Right, borderRect.Top, borderRect.Right, borderRect.Bottom);
                e.Graphics.DrawLine(greenPen, borderRect.Left, borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }
        }

        private void gbGlavaProfil_Paint(object sender, PaintEventArgs e)
        {
            if (gbGlavaProfil == null) return;

            using (Pen whitePen = new Pen(Color.White, 2))
            using (Pen greenPen = new Pen(Color.DodgerBlue, 1))
            {
                Rectangle borderRect = new Rectangle(gbGlavaProfil.ClientRectangle.X, gbGlavaProfil.ClientRectangle.Y + 7,
                                                     gbGlavaProfil.ClientRectangle.Width - 1, gbGlavaProfil.ClientRectangle.Height - 8);

                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Right, borderRect.Top);
                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Left, borderRect.Bottom);
                e.Graphics.DrawLine(whitePen, borderRect.Right, borderRect.Top, borderRect.Right, borderRect.Bottom);
                e.Graphics.DrawLine(greenPen, borderRect.Left, borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }
        }

        private void gbGlavaNovRacun_Paint(object sender, PaintEventArgs e)
        {
            if (gbGlavaNovRacun == null) return;

            using (Pen whitePen = new Pen(Color.White, 2))
            using (Pen greenPen = new Pen(Color.DodgerBlue, 1))
            {
                Rectangle borderRect = new Rectangle(gbGlavaNovRacun.ClientRectangle.X, gbGlavaNovRacun.ClientRectangle.Y + 7,
                                                     gbGlavaNovRacun.ClientRectangle.Width - 1, gbGlavaNovRacun.ClientRectangle.Height - 8);

                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Right, borderRect.Top);
                e.Graphics.DrawLine(whitePen, borderRect.Left, borderRect.Top, borderRect.Left, borderRect.Bottom);
                e.Graphics.DrawLine(whitePen, borderRect.Right, borderRect.Top, borderRect.Right, borderRect.Bottom);
                e.Graphics.DrawLine(greenPen, borderRect.Left, borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }
        }

        private void lblGlavaDelo_Click(object sender, EventArgs e)
        {
            ShowUC_Domov();
            lblGlavaDelo.ForeColor = Color.DodgerBlue;
        }

        private void pbGlavaDelo_Click(object sender, EventArgs e)
        {
            ShowUC_Domov();
            lblGlavaDelo.ForeColor = Color.DodgerBlue;
        }

        private void lblGlavaStranke_Click(object sender, EventArgs e)
        {
            ShowUC_Stranke();
        }

        private void pbGlavaStranke_Click(object sender, EventArgs e)
        {
            ShowUC_Stranke();
        }

        private void lblGlavaPodjetje_Click(object sender, EventArgs e)
        {
            ShowUC_Profil();
        }

        private void pbGlavaProfil_Click(object sender, EventArgs e)
        {
            ShowUC_Profil();
        }

        private void lblGlavaStatistika_Click(object sender, EventArgs e)
        {
            ShowUC_Statistika();
        }

        private void pbGlavaStatistika_Click(object sender, EventArgs e)
        {
            ShowUC_Statistika();
        }

        private void lblGlavaNovRacun_Click(object sender, EventArgs e)
        {
            ShowForm_VnosRacuna();
        }

        private void pbglavaNovRacun_Click(object sender, EventArgs e)
        {
            ShowForm_VnosRacuna();
        }

        private void lblGlavaDelo_MouseHover(object sender, EventArgs e)
        {
            lblGlavaDelo.ForeColor = Color.DodgerBlue;
        }

        private void lblGlavaDelo_MouseLeave(object sender, EventArgs e)
        {
            lblGlavaDelo.ForeColor = Color.Black;
        }

        private void lblGlavaStranke_MouseHover(object sender, EventArgs e)
        {
            lblGlavaStranke.ForeColor = Color.DodgerBlue;
        }

        private void lblGlavaStranke_MouseLeave(object sender, EventArgs e)
        {
            lblGlavaStranke.ForeColor = Color.Black;
        }

        private void lblGlavaPodjetje_MouseHover(object sender, EventArgs e)
        {
            lblGlavaPodjetje.ForeColor = Color.DodgerBlue;
        }

        private void lblGlavaPodjetje_MouseLeave(object sender, EventArgs e)
        {
            lblGlavaPodjetje.ForeColor = Color.Black;
        }

        private void lblGlavaStatistika_MouseHover(object sender, EventArgs e)
        {
            lblGlavaStatistika.ForeColor = Color.DodgerBlue;
        }

        private void lblGlavaStatistika_MouseLeave(object sender, EventArgs e)
        {
            lblGlavaStatistika.ForeColor = Color.Black;
        }
        #endregion events
    }
}
