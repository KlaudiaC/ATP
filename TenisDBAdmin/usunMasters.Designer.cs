namespace TenisDBUser
{
    partial class usunMasters
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
            this.usunButton = new System.Windows.Forms.Button();
            this.anulujButton = new System.Windows.Forms.Button();
            this.myLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nazwaComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // usunButton
            // 
            this.usunButton.Location = new System.Drawing.Point(391, 320);
            this.usunButton.Name = "usunButton";
            this.usunButton.Size = new System.Drawing.Size(75, 31);
            this.usunButton.TabIndex = 9;
            this.usunButton.Text = "Usuń";
            this.usunButton.UseVisualStyleBackColor = true;
            this.usunButton.Click += new System.EventHandler(this.usunButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(12, 320);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 31);
            this.anulujButton.TabIndex = 8;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(224, 58);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(0, 17);
            this.myLabel.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nazwa turnieju";
            // 
            // nazwaComboBox
            // 
            this.nazwaComboBox.FormattingEnabled = true;
            this.nazwaComboBox.Location = new System.Drawing.Point(132, 31);
            this.nazwaComboBox.Name = "nazwaComboBox";
            this.nazwaComboBox.Size = new System.Drawing.Size(121, 24);
            this.nazwaComboBox.TabIndex = 5;
            this.nazwaComboBox.SelectedIndexChanged += new System.EventHandler(this.nazwaComboBox_SelectedIndexChanged);
            // 
            // usunMasters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 363);
            this.Controls.Add(this.usunButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.myLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nazwaComboBox);
            this.Name = "usunMasters";
            this.Text = "usunMasters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button usunButton;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Label myLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox nazwaComboBox;
    }
}