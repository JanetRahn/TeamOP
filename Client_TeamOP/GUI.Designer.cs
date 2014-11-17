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
            this.LEFT = new System.Windows.Forms.Button();
            this.UP = new System.Windows.Forms.Button();
            this.RIGHT = new System.Windows.Forms.Button();
            this.DOWN = new System.Windows.Forms.Button();
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
            this.MapWindow.Size = new System.Drawing.Size(380, 369);
            this.MapWindow.TabIndex = 1;
            this.MapWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.MapWindow_Paint_1);
            // 
            // ChatWindow
            // 
            this.ChatWindow.Location = new System.Drawing.Point(25, 387);
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.Size = new System.Drawing.Size(380, 71);
            this.ChatWindow.TabIndex = 2;
            this.ChatWindow.Text = "";
            // 
            // LEFT
            // 
            this.LEFT.Location = new System.Drawing.Point(435, 464);
            this.LEFT.Name = "LEFT";
            this.LEFT.Size = new System.Drawing.Size(28, 26);
            this.LEFT.TabIndex = 4;
            this.LEFT.Text = "A";
            this.LEFT.UseVisualStyleBackColor = true;
            this.LEFT.Click += new System.EventHandler(this.LEFT_Click);
            // 
            // UP
            // 
            this.UP.Location = new System.Drawing.Point(466, 432);
            this.UP.Name = "UP";
            this.UP.Size = new System.Drawing.Size(28, 26);
            this.UP.TabIndex = 5;
            this.UP.Text = "W";
            this.UP.UseVisualStyleBackColor = true;
            this.UP.Click += new System.EventHandler(this.UP_Click);
            // 
            // RIGHT
            // 
            this.RIGHT.Location = new System.Drawing.Point(500, 464);
            this.RIGHT.Name = "RIGHT";
            this.RIGHT.Size = new System.Drawing.Size(28, 26);
            this.RIGHT.TabIndex = 6;
            this.RIGHT.Text = "S";
            this.RIGHT.UseVisualStyleBackColor = true;
            this.RIGHT.Click += new System.EventHandler(this.RIGHT_Click);
            // 
            // DOWN
            // 
            this.DOWN.Location = new System.Drawing.Point(466, 464);
            this.DOWN.Name = "DOWN";
            this.DOWN.Size = new System.Drawing.Size(28, 26);
            this.DOWN.TabIndex = 7;
            this.DOWN.Text = "S";
            this.DOWN.UseVisualStyleBackColor = true;
            this.DOWN.Click += new System.EventHandler(this.DOWN_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 502);
            this.Controls.Add(this.DOWN);
            this.Controls.Add(this.RIGHT);
            this.Controls.Add(this.UP);
            this.Controls.Add(this.LEFT);
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
        private System.Windows.Forms.Button LEFT;
        private System.Windows.Forms.Button UP;
        private System.Windows.Forms.Button RIGHT;
        private System.Windows.Forms.Button DOWN;

    }
}

