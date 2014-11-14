namespace Client_TeamOP
{
   public partial class GUI
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
            this.Chat = new System.Windows.Forms.TextBox();
            this.MapWindow = new System.Windows.Forms.Panel();
            this.ChatWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Chat
            // 
            this.Chat.Location = new System.Drawing.Point(25, 464);
            this.Chat.Multiline = true;
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(380, 26);
            this.Chat.TabIndex = 0;
            // 
            // MapWindow
            // 
            this.MapWindow.Location = new System.Drawing.Point(25, 12);
            this.MapWindow.Name = "MapWindow";
            this.MapWindow.Size = new System.Drawing.Size(380, 398);
            this.MapWindow.TabIndex = 1;
            // 
            // ChatWindow
            // 
            this.ChatWindow.Location = new System.Drawing.Point(25, 416);
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.Size = new System.Drawing.Size(380, 42);
            this.ChatWindow.TabIndex = 2;
            this.ChatWindow.Text = "";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 502);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.MapWindow);
            this.Controls.Add(this.Chat);
            this.Name = "GUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Chat;
        private System.Windows.Forms.Panel MapWindow;
        private System.Windows.Forms.RichTextBox ChatWindow;

    }
}

