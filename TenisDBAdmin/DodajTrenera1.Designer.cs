namespace TenisDBUser
{
    partial class DodajTrenera1
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
            this.dodajButton = new System.Windows.Forms.Button();
            this.AnulujButton = new System.Windows.Forms.Button();
            this.imieTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.zawodnikTextbox = new System.Windows.Forms.TextBox();
            this.nazwiskoTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dodajButton
            // 
            this.dodajButton.Location = new System.Drawing.Point(263, 278);
            this.dodajButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dodajButton.Name = "dodajButton";
            this.dodajButton.Size = new System.Drawing.Size(100, 28);
            this.dodajButton.TabIndex = 0;
            this.dodajButton.Text = "Dodaj";
            this.dodajButton.UseVisualStyleBackColor = true;
            this.dodajButton.Click += new System.EventHandler(this.dodajButton_Click);
            // 
            // AnulujButton
            // 
            this.AnulujButton.Location = new System.Drawing.Point(16, 278);
            this.AnulujButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AnulujButton.Name = "AnulujButton";
            this.AnulujButton.Size = new System.Drawing.Size(100, 28);
            this.AnulujButton.TabIndex = 1;
            this.AnulujButton.Text = "Anuluj";
            this.AnulujButton.UseVisualStyleBackColor = true;
            this.AnulujButton.Click += new System.EventHandler(this.AnulujButton_Click);
            // 
            // imieTextbox
            // 
            this.imieTextbox.Location = new System.Drawing.Point(167, 20);
            this.imieTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imieTextbox.Name = "imieTextbox";
            this.imieTextbox.Size = new System.Drawing.Size(132, 22);
            this.imieTextbox.TabIndex = 2;
            this.imieTextbox.TextChanged += new System.EventHandler(this.imieTextbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Imię trenera*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nazwisko zawodnika";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nazwisko trenera*";
            // 
            // zawodnikTextbox
            // 
            this.zawodnikTextbox.Location = new System.Drawing.Point(167, 84);
            this.zawodnikTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zawodnikTextbox.Name = "zawodnikTextbox";
            this.zawodnikTextbox.Size = new System.Drawing.Size(132, 22);
            this.zawodnikTextbox.TabIndex = 6;
            // 
            // nazwiskoTextbox
            // 
            this.nazwiskoTextbox.Location = new System.Drawing.Point(167, 52);
            this.nazwiskoTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nazwiskoTextbox.Name = "nazwiskoTextbox";
            this.nazwiskoTextbox.Size = new System.Drawing.Size(132, 22);
            this.nazwiskoTextbox.TabIndex = 7;
            this.nazwiskoTextbox.TextChanged += new System.EventHandler(this.nazwiskoTextbox_TextChanged);
            // 
            // DodajTrenera1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 321);
            this.Controls.Add(this.nazwiskoTextbox);
            this.Controls.Add(this.zawodnikTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imieTextbox);
            this.Controls.Add(this.AnulujButton);
            this.Controls.Add(this.dodajButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DodajTrenera1";
            this.Text = "DodajTrenera1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button dodajButton;
        private System.Windows.Forms.Button AnulujButton;
        private System.Windows.Forms.TextBox imieTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox zawodnikTextbox;
        private System.Windows.Forms.TextBox nazwiskoTextbox;
    }
}