namespace TenisDBUser
{
    partial class usunOlimp
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
            this.rokComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.myLabel = new System.Windows.Forms.Label();
            this.anulujButton = new System.Windows.Forms.Button();
            this.usunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rokComboBox
            // 
            this.rokComboBox.FormattingEnabled = true;
            this.rokComboBox.Location = new System.Drawing.Point(51, 16);
            this.rokComboBox.Name = "rokComboBox";
            this.rokComboBox.Size = new System.Drawing.Size(121, 24);
            this.rokComboBox.TabIndex = 0;
            this.rokComboBox.SelectedIndexChanged += new System.EventHandler(this.rokComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rok";
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(178, 19);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(0, 17);
            this.myLabel.TabIndex = 2;
            // 
            // anulujButton
            // 
            this.anulujButton.Location = new System.Drawing.Point(15, 368);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 31);
            this.anulujButton.TabIndex = 3;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // usunButton
            // 
            this.usunButton.Location = new System.Drawing.Point(336, 368);
            this.usunButton.Name = "usunButton";
            this.usunButton.Size = new System.Drawing.Size(75, 31);
            this.usunButton.TabIndex = 4;
            this.usunButton.Text = "Usuń";
            this.usunButton.UseVisualStyleBackColor = true;
            this.usunButton.Click += new System.EventHandler(this.usunButton_Click);
            // 
            // usunOlimp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 411);
            this.Controls.Add(this.usunButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.myLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rokComboBox);
            this.Name = "usunOlimp";
            this.Text = "usunOlimp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox rokComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label myLabel;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button usunButton;
    }
}