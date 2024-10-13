namespace Stantehnika.APP
{
    partial class DodajOpombo
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbOpomba = new System.Windows.Forms.TextBox();
            this.btnVnesiOpombo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 25);
            this.label1.TabIndex = 22;
            this.label1.Text = "Vnos opombe:";
            // 
            // tbOpomba
            // 
            this.tbOpomba.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbOpomba.Location = new System.Drawing.Point(163, 32);
            this.tbOpomba.Name = "tbOpomba";
            this.tbOpomba.Size = new System.Drawing.Size(623, 33);
            this.tbOpomba.TabIndex = 21;
            // 
            // btnVnesiOpombo
            // 
            this.btnVnesiOpombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVnesiOpombo.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVnesiOpombo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVnesiOpombo.FlatAppearance.BorderSize = 0;
            this.btnVnesiOpombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVnesiOpombo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnVnesiOpombo.ForeColor = System.Drawing.Color.White;
            this.btnVnesiOpombo.Location = new System.Drawing.Point(347, 72);
            this.btnVnesiOpombo.Name = "btnVnesiOpombo";
            this.btnVnesiOpombo.Size = new System.Drawing.Size(258, 34);
            this.btnVnesiOpombo.TabIndex = 23;
            this.btnVnesiOpombo.Text = "Vnesi opombo";
            this.btnVnesiOpombo.UseVisualStyleBackColor = false;
            this.btnVnesiOpombo.Click += new System.EventHandler(this.btnVnesiOpombo_Click);
            // 
            // DodajOpombo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 124);
            this.Controls.Add(this.btnVnesiOpombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOpomba);
            this.MaximumSize = new System.Drawing.Size(816, 163);
            this.MinimumSize = new System.Drawing.Size(816, 163);
            this.Name = "DodajOpombo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DodajOpombo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOpomba;
        private System.Windows.Forms.Button btnVnesiOpombo;
    }
}