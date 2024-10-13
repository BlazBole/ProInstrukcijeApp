namespace Stantehnika.APP
{
    partial class PravnaOsebaVnos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PravnaOsebaVnos));
            this.btnVnesiStranko = new System.Windows.Forms.Button();
            this.tbUlica = new System.Windows.Forms.TextBox();
            this.tbDavcnaStevilka = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNazivPodjetja = new System.Windows.Forms.TextBox();
            this.tbEnaslov = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tbPostnaStevilka = new System.Windows.Forms.TextBox();
            this.tbKraj = new System.Windows.Forms.TextBox();
            this.tbHisnaStevilka = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVnesiStranko
            // 
            this.btnVnesiStranko.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVnesiStranko.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVnesiStranko.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVnesiStranko.FlatAppearance.BorderSize = 0;
            this.btnVnesiStranko.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVnesiStranko.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnVnesiStranko.ForeColor = System.Drawing.Color.White;
            this.btnVnesiStranko.Location = new System.Drawing.Point(64, 410);
            this.btnVnesiStranko.Name = "btnVnesiStranko";
            this.btnVnesiStranko.Size = new System.Drawing.Size(343, 34);
            this.btnVnesiStranko.TabIndex = 18;
            this.btnVnesiStranko.Text = "Vnesi novo stranko";
            this.btnVnesiStranko.UseVisualStyleBackColor = false;
            this.btnVnesiStranko.Click += new System.EventHandler(this.btnVnesiStranko_Click);
            // 
            // tbUlica
            // 
            this.tbUlica.AccessibleDescription = "";
            this.tbUlica.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbUlica.ForeColor = System.Drawing.Color.DimGray;
            this.tbUlica.Location = new System.Drawing.Point(6, 30);
            this.tbUlica.Name = "tbUlica";
            this.tbUlica.Size = new System.Drawing.Size(254, 33);
            this.tbUlica.TabIndex = 15;
            this.tbUlica.Tag = "";
            this.tbUlica.Text = "ulica";
            this.tbUlica.Click += new System.EventHandler(this.tbUlica_Click);
            this.tbUlica.Leave += new System.EventHandler(this.tbUlica_Leave);
            // 
            // tbDavcnaStevilka
            // 
            this.tbDavcnaStevilka.AccessibleDescription = "";
            this.tbDavcnaStevilka.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbDavcnaStevilka.ForeColor = System.Drawing.Color.DimGray;
            this.tbDavcnaStevilka.Location = new System.Drawing.Point(36, 130);
            this.tbDavcnaStevilka.Name = "tbDavcnaStevilka";
            this.tbDavcnaStevilka.Size = new System.Drawing.Size(394, 33);
            this.tbDavcnaStevilka.TabIndex = 13;
            this.tbDavcnaStevilka.Tag = "";
            this.tbDavcnaStevilka.Text = "davčna številka";
            this.tbDavcnaStevilka.Click += new System.EventHandler(this.tbDavcnaStevilka_Click);
            this.tbDavcnaStevilka.Leave += new System.EventHandler(this.tbDavcnaStevilka_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(34, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 30);
            this.label1.TabIndex = 12;
            this.label1.Text = "Vnos stranke";
            // 
            // tbNazivPodjetja
            // 
            this.tbNazivPodjetja.AccessibleDescription = "";
            this.tbNazivPodjetja.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbNazivPodjetja.ForeColor = System.Drawing.Color.DimGray;
            this.tbNazivPodjetja.Location = new System.Drawing.Point(36, 80);
            this.tbNazivPodjetja.Name = "tbNazivPodjetja";
            this.tbNazivPodjetja.Size = new System.Drawing.Size(394, 33);
            this.tbNazivPodjetja.TabIndex = 11;
            this.tbNazivPodjetja.Tag = "";
            this.tbNazivPodjetja.Text = "naziv podjetja";
            this.tbNazivPodjetja.Click += new System.EventHandler(this.tbNazivPodjetja_Click);
            this.tbNazivPodjetja.Leave += new System.EventHandler(this.tbNazivPodjetja_Leave);
            // 
            // tbEnaslov
            // 
            this.tbEnaslov.AccessibleDescription = "";
            this.tbEnaslov.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbEnaslov.ForeColor = System.Drawing.Color.DimGray;
            this.tbEnaslov.Location = new System.Drawing.Point(36, 330);
            this.tbEnaslov.Name = "tbEnaslov";
            this.tbEnaslov.Size = new System.Drawing.Size(394, 33);
            this.tbEnaslov.TabIndex = 20;
            this.tbEnaslov.Tag = "";
            this.tbEnaslov.Text = "e-naslov";
            this.tbEnaslov.Click += new System.EventHandler(this.tbEnaslov_Click);
            this.tbEnaslov.Leave += new System.EventHandler(this.tbEnaslov_Leave);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tbPostnaStevilka);
            this.groupBox7.Controls.Add(this.tbKraj);
            this.groupBox7.Controls.Add(this.tbHisnaStevilka);
            this.groupBox7.Controls.Add(this.tbUlica);
            this.groupBox7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox7.Location = new System.Drawing.Point(30, 180);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(406, 130);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "sedež podjetja";
            // 
            // tbPostnaStevilka
            // 
            this.tbPostnaStevilka.AccessibleDescription = "";
            this.tbPostnaStevilka.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPostnaStevilka.ForeColor = System.Drawing.Color.DimGray;
            this.tbPostnaStevilka.Location = new System.Drawing.Point(264, 80);
            this.tbPostnaStevilka.Name = "tbPostnaStevilka";
            this.tbPostnaStevilka.Size = new System.Drawing.Size(136, 33);
            this.tbPostnaStevilka.TabIndex = 18;
            this.tbPostnaStevilka.Tag = "";
            this.tbPostnaStevilka.Text = "poštna številka";
            this.tbPostnaStevilka.Click += new System.EventHandler(this.tbPostnaStevilka_Click);
            this.tbPostnaStevilka.Leave += new System.EventHandler(this.tbPostnaStevilka_Leave);
            // 
            // tbKraj
            // 
            this.tbKraj.AccessibleDescription = "";
            this.tbKraj.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbKraj.ForeColor = System.Drawing.Color.DimGray;
            this.tbKraj.Location = new System.Drawing.Point(6, 80);
            this.tbKraj.Name = "tbKraj";
            this.tbKraj.Size = new System.Drawing.Size(254, 33);
            this.tbKraj.TabIndex = 17;
            this.tbKraj.Tag = "";
            this.tbKraj.Text = "kraj";
            this.tbKraj.Click += new System.EventHandler(this.tbKraj_Click);
            this.tbKraj.Leave += new System.EventHandler(this.tbKraj_Leave);
            // 
            // tbHisnaStevilka
            // 
            this.tbHisnaStevilka.AccessibleDescription = "";
            this.tbHisnaStevilka.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbHisnaStevilka.ForeColor = System.Drawing.Color.DimGray;
            this.tbHisnaStevilka.Location = new System.Drawing.Point(264, 30);
            this.tbHisnaStevilka.Name = "tbHisnaStevilka";
            this.tbHisnaStevilka.Size = new System.Drawing.Size(136, 33);
            this.tbHisnaStevilka.TabIndex = 16;
            this.tbHisnaStevilka.Tag = "";
            this.tbHisnaStevilka.Text = "hišna številka";
            this.tbHisnaStevilka.Click += new System.EventHandler(this.tbHisnaStevilka_Click);
            this.tbHisnaStevilka.Leave += new System.EventHandler(this.tbHisnaStevilka_Leave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(389, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // PravnaOsebaVnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 557);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.tbEnaslov);
            this.Controls.Add(this.btnVnesiStranko);
            this.Controls.Add(this.tbDavcnaStevilka);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNazivPodjetja);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "PravnaOsebaVnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PravnaOsebaVnos";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVnesiStranko;
        private System.Windows.Forms.TextBox tbUlica;
        private System.Windows.Forms.TextBox tbDavcnaStevilka;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNazivPodjetja;
        private System.Windows.Forms.TextBox tbEnaslov;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbHisnaStevilka;
        private System.Windows.Forms.TextBox tbPostnaStevilka;
        private System.Windows.Forms.TextBox tbKraj;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}