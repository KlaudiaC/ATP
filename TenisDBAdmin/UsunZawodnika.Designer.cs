namespace TenisDBUser
{
    partial class UsunZawodnika
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
            this.zawComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.anulujButton = new System.Windows.Forms.Button();
            this.usunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zawComboBox
            // 
            this.zawComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zawComboBox.FormattingEnabled = true;
            this.zawComboBox.Location = new System.Drawing.Point(71, 7);
            this.zawComboBox.Name = "zawComboBox";
            this.zawComboBox.Size = new System.Drawing.Size(201, 21);
            this.zawComboBox.TabIndex = 0;
            this.zawComboBox.SelectedIndexChanged += new System.EventHandler(this.zawComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nazwisko";
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(12, 226);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 2;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // usunButton
            // 
            this.usunButton.Location = new System.Drawing.Point(197, 226);
            this.usunButton.Name = "usunButton";
            this.usunButton.Size = new System.Drawing.Size(75, 23);
            this.usunButton.TabIndex = 3;
            this.usunButton.Text = "Usuń";
            this.usunButton.UseVisualStyleBackColor = true;
            this.usunButton.Click += new System.EventHandler(this.usunButton_Click);
            // 
            // UsunZawodnika
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.usunButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zawComboBox);
            this.Name = "UsunZawodnika";
            this.Text = "UsunZawodnika";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox zawComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button usunButton;
    }
}