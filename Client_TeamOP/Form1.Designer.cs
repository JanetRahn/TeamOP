namespace Client_TeamOP
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogWindow = new System.Windows.Forms.RichTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.sendenB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogWindow
            // 
            this.LogWindow.Cursor = System.Windows.Forms.Cursors.Cross;
            this.LogWindow.Location = new System.Drawing.Point(691, 204);
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LogWindow.Size = new System.Drawing.Size(172, 230);
            this.LogWindow.TabIndex = 0;
            this.LogWindow.Text = "";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(691, 440);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(172, 20);
            this.maskedTextBox1.TabIndex = 1;
            this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected_1);
            // 
            // sendenB
            // 
            this.sendenB.Location = new System.Drawing.Point(691, 472);
            this.sendenB.Name = "sendenB";
            this.sendenB.Size = new System.Drawing.Size(172, 23);
            this.sendenB.TabIndex = 2;
            this.sendenB.Text = "Senden";
            this.sendenB.UseVisualStyleBackColor = true;
            this.sendenB.Click += new System.EventHandler(this.sendenB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 507);
            this.Controls.Add(this.sendenB);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.LogWindow);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogWindow;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button sendenB;


    }
}

