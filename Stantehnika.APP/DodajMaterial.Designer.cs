namespace Stantehnika.APP
{
    partial class DodajMaterial
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
            this.tbNazivMateriala = new System.Windows.Forms.TextBox();
            this.btnVnesiStranko = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbNazivMateriala
            // 
            this.tbNazivMateriala.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbNazivMateriala.Location = new System.Drawing.Point(198, 43);
            this.tbNazivMateriala.Name = "tbNazivMateriala";
            this.tbNazivMateriala.Size = new System.Drawing.Size(166, 33);
            this.tbNazivMateriala.TabIndex = 0;
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
            this.btnVnesiStranko.Location = new System.Drawing.Point(76, 102);
            this.btnVnesiStranko.Name = "btnVnesiStranko";
            this.btnVnesiStranko.Size = new System.Drawing.Size(258, 34);
            this.btnVnesiStranko.TabIndex = 19;
            this.btnVnesiStranko.Text = "Vnesi novo postavko za material";
            this.btnVnesiStranko.UseVisualStyleBackColor = false;
            this.btnVnesiStranko.Click += new System.EventHandler(this.btnVnesiStranko_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(41, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "Naziv materiala:";
            // 
            // DodajMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(407, 148);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVnesiStranko);
            this.Controls.Add(this.tbNazivMateriala);
            this.Name = "DodajMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DodajMaterial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNazivMateriala;
        private System.Windows.Forms.Button btnVnesiStranko;
        private System.Windows.Forms.Label label1;
    }
}