namespace Client_TeamOP
{
   public partial class GUI
    {
        private System.ComponentModel.IContainer components = null;

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
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.MapWindow = new System.Windows.Forms.Panel();
            this.ChatWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ChatInput
            // 
            this.ChatInput.Location = new System.Drawing.Point(25, 464);
            this.ChatInput.Multiline = true;
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(380, 26);
            this.ChatInput.TabIndex = 0;
            // 
            // MapWindow
            // 
            this.MapWindow.Location = new System.Drawing.Point(25, 12);
            this.MapWindow.Name = "MapWindow";
            this.MapWindow.Size = new System.Drawing.Size(380, 369);
            this.MapWindow.TabIndex = 1;
            this.MapWindow.Click += new System.EventHandler(this.MapWindow_Click);
            this.MapWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.MapWindow_Paint_1);
            // 
            // ChatWindow
            // 
            this.ChatWindow.Location = new System.Drawing.Point(25, 387);
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.ReadOnly = true;
            this.ChatWindow.Size = new System.Drawing.Size(380, 71);
            this.ChatWindow.TabIndex = 2;
            this.ChatWindow.Text = "";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 502);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.MapWindow);
            this.Controls.Add(this.ChatInput);
            this.Name = "GUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.Panel MapWindow;
        private System.Windows.Forms.RichTextBox ChatWindow;

    }
}

