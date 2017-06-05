namespace TenisDBUser
{
    partial class usunTrenera
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
            this.trenerComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.anulujButton = new System.Windows.Forms.Button();
            this.usunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trenerComboBox
            // 
            this.trenerComboBox.FormattingEnabled = true;
            this.trenerComboBox.Location = new System.Drawing.Point(85, 12);
            this.trenerComboBox.Name = "trenerComboBox";
            this.trenerComboBox.Size = new System.Drawing.Size(121, 24);
            this.trenerComboBox.TabIndex = 0;
            this.trenerComboBox.SelectedIndexChanged += new System.EventHandler(this.trenerComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nazwisko";
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(12, 298);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 33);
            this.anulujButton.TabIndex = 2;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // usunButton
            // 
            this.usunButton.Location = new System.Drawing.Point(270, 298);
            this.usunButton.Name = "usunButton";
            this.usunButton.Size = new System.Drawing.Size(75, 33);
            this.usunButton.TabIndex = 3;
            this.usunButton.Text = "Usuń";
            this.usunButton.UseVisualStyleBackColor = true;
            this.usunButton.Click += new System.EventHandler(this.usunButton_Click);
            // 
            // usunTrenera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 343);
            this.Controls.Add(this.usunButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trenerComboBox);
            this.Name = "usunTrenera";
            this.Text = "usunTrenera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox trenerComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button usunButton;
    }
}