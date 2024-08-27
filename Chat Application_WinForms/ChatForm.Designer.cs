namespace Chat_Application_WinForms
{
    partial class ChatApplication
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            FileToolMenuItem = new ToolStripMenuItem();
            ConnectToolItem = new ToolStripMenuItem();
            DisconnectToolItem = new ToolStripMenuItem();
            AddUserToolItem = new ToolStripMenuItem();
            HelpToolMenuItem = new ToolStripMenuItem();
            AboutToolItem = new ToolStripMenuItem();
            panel1 = new Panel();
            DisconnectFrom_btn = new Button();
            ConnectTo_btn = new Button();
            PortNum_tb = new TextBox();
            label2 = new Label();
            ServerAddr_tb = new TextBox();
            label1 = new Label();
            panel2 = new Panel();
            ServerMessage_tb = new TextBox();
            ServerMessage_btn = new Button();
            InputMessage_tb = new TextBox();
            ChatMessages_rtb = new RichTextBox();
            SendMessage_btn = new Button();
            Users_lstbox = new ListBox();
            statusStrip1 = new StatusStrip();
            Connection_pbar = new ToolStripProgressBar();
            ConnectionStatus_lbl = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Gainsboro;
            menuStrip1.Font = new Font("Arial", 11F, FontStyle.Bold);
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileToolMenuItem, HelpToolMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(784, 26);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileToolMenuItem
            // 
            FileToolMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ConnectToolItem, DisconnectToolItem, AddUserToolItem });
            FileToolMenuItem.Name = "FileToolMenuItem";
            FileToolMenuItem.Size = new Size(46, 22);
            FileToolMenuItem.Text = "File";
            // 
            // ConnectToolItem
            // 
            ConnectToolItem.Name = "ConnectToolItem";
            ConnectToolItem.Size = new Size(176, 22);
            ConnectToolItem.Text = "Connect";
            // 
            // DisconnectToolItem
            // 
            DisconnectToolItem.Name = "DisconnectToolItem";
            DisconnectToolItem.Size = new Size(176, 22);
            DisconnectToolItem.Text = "Disconnect";
            // 
            // AddUserToolItem
            // 
            AddUserToolItem.Name = "AddUserToolItem";
            AddUserToolItem.Size = new Size(176, 22);
            AddUserToolItem.Text = "Add New User";
            AddUserToolItem.Click += AddUserToolItem_Click;
            // 
            // HelpToolMenuItem
            // 
            HelpToolMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AboutToolItem });
            HelpToolMenuItem.Name = "HelpToolMenuItem";
            HelpToolMenuItem.Size = new Size(53, 22);
            HelpToolMenuItem.Text = "Help";
            // 
            // AboutToolItem
            // 
            AboutToolItem.Name = "AboutToolItem";
            AboutToolItem.Size = new Size(117, 22);
            AboutToolItem.Text = "About";
            // 
            // panel1
            // 
            panel1.Controls.Add(DisconnectFrom_btn);
            panel1.Controls.Add(ConnectTo_btn);
            panel1.Controls.Add(PortNum_tb);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(ServerAddr_tb);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 69);
            panel1.TabIndex = 1;
            // 
            // DisconnectFrom_btn
            // 
            DisconnectFrom_btn.Location = new Point(609, 31);
            DisconnectFrom_btn.Name = "DisconnectFrom_btn";
            DisconnectFrom_btn.Size = new Size(130, 23);
            DisconnectFrom_btn.TabIndex = 5;
            DisconnectFrom_btn.Text = "Disconnect";
            DisconnectFrom_btn.UseVisualStyleBackColor = true;
            DisconnectFrom_btn.Click += DisconnectFrom_btn_Click;
            // 
            // ConnectTo_btn
            // 
            ConnectTo_btn.Location = new Point(473, 31);
            ConnectTo_btn.Name = "ConnectTo_btn";
            ConnectTo_btn.Size = new Size(130, 23);
            ConnectTo_btn.TabIndex = 4;
            ConnectTo_btn.Text = "Connect";
            ConnectTo_btn.UseVisualStyleBackColor = true;
            ConnectTo_btn.Click += ConnectTo_btn_Click;
            // 
            // PortNum_tb
            // 
            PortNum_tb.Location = new Point(247, 31);
            PortNum_tb.Name = "PortNum_tb";
            PortNum_tb.Size = new Size(220, 23);
            PortNum_tb.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(247, 12);
            label2.Name = "label2";
            label2.Size = new Size(37, 16);
            label2.TabIndex = 2;
            label2.Text = "Port:";
            // 
            // ServerAddr_tb
            // 
            ServerAddr_tb.Location = new Point(21, 31);
            ServerAddr_tb.Name = "ServerAddr_tb";
            ServerAddr_tb.Size = new Size(220, 23);
            ServerAddr_tb.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 12);
            label1.Name = "label1";
            label1.Size = new Size(53, 16);
            label1.TabIndex = 0;
            label1.Text = "Server:";
            // 
            // panel2
            // 
            panel2.Controls.Add(ServerMessage_tb);
            panel2.Controls.Add(ServerMessage_btn);
            panel2.Controls.Add(InputMessage_tb);
            panel2.Controls.Add(ChatMessages_rtb);
            panel2.Controls.Add(SendMessage_btn);
            panel2.Controls.Add(Users_lstbox);
            panel2.Controls.Add(statusStrip1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 95);
            panel2.Name = "panel2";
            panel2.Size = new Size(784, 466);
            panel2.TabIndex = 6;
            // 
            // ServerMessage_tb
            // 
            ServerMessage_tb.BorderStyle = BorderStyle.FixedSingle;
            ServerMessage_tb.Dock = DockStyle.Bottom;
            ServerMessage_tb.Font = new Font("Arial", 11F);
            ServerMessage_tb.Location = new Point(169, 350);
            ServerMessage_tb.Name = "ServerMessage_tb";
            ServerMessage_tb.Size = new Size(615, 24);
            ServerMessage_tb.TabIndex = 8;
            // 
            // ServerMessage_btn
            // 
            ServerMessage_btn.Dock = DockStyle.Bottom;
            ServerMessage_btn.Location = new Point(169, 374);
            ServerMessage_btn.Name = "ServerMessage_btn";
            ServerMessage_btn.Size = new Size(615, 23);
            ServerMessage_btn.TabIndex = 9;
            ServerMessage_btn.Text = "Send Server Message";
            ServerMessage_btn.UseVisualStyleBackColor = true;
            ServerMessage_btn.Click += ServerMessage_btn_Click;
            // 
            // InputMessage_tb
            // 
            InputMessage_tb.BorderStyle = BorderStyle.FixedSingle;
            InputMessage_tb.Dock = DockStyle.Bottom;
            InputMessage_tb.Font = new Font("Arial", 11F);
            InputMessage_tb.Location = new Point(169, 397);
            InputMessage_tb.Name = "InputMessage_tb";
            InputMessage_tb.Size = new Size(615, 24);
            InputMessage_tb.TabIndex = 6;
            // 
            // ChatMessages_rtb
            // 
            ChatMessages_rtb.Dock = DockStyle.Fill;
            ChatMessages_rtb.Location = new Point(169, 0);
            ChatMessages_rtb.Name = "ChatMessages_rtb";
            ChatMessages_rtb.Size = new Size(615, 421);
            ChatMessages_rtb.TabIndex = 1;
            ChatMessages_rtb.Text = "";
            // 
            // SendMessage_btn
            // 
            SendMessage_btn.Dock = DockStyle.Bottom;
            SendMessage_btn.Location = new Point(169, 421);
            SendMessage_btn.Name = "SendMessage_btn";
            SendMessage_btn.Size = new Size(615, 23);
            SendMessage_btn.TabIndex = 6;
            SendMessage_btn.Text = "Send Message";
            SendMessage_btn.UseVisualStyleBackColor = true;
            SendMessage_btn.Click += SendMessage_btn_Click;
            // 
            // Users_lstbox
            // 
            Users_lstbox.Dock = DockStyle.Left;
            Users_lstbox.FormattingEnabled = true;
            Users_lstbox.Location = new Point(0, 0);
            Users_lstbox.Name = "Users_lstbox";
            Users_lstbox.Size = new Size(169, 444);
            Users_lstbox.TabIndex = 0;
            Users_lstbox.SelectedIndexChanged += Users_lstbox_SelectedIndexChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { Connection_pbar, ConnectionStatus_lbl });
            statusStrip1.Location = new Point(0, 444);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(784, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // Connection_pbar
            // 
            Connection_pbar.Name = "Connection_pbar";
            Connection_pbar.Size = new Size(100, 16);
            // 
            // ConnectionStatus_lbl
            // 
            ConnectionStatus_lbl.Name = "ConnectionStatus_lbl";
            ConnectionStatus_lbl.Size = new Size(145, 17);
            ConnectionStatus_lbl.Text = "Connection Status: Online";
            // 
            // ChatApplication
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(784, 561);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            Font = new Font("Arial", 10F);
            MainMenuStrip = menuStrip1;
            Name = "ChatApplication";
            Text = "Chat Application";
            FormClosing += ChatApplication_FormClosing;
            Load += ChatApplication_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileToolMenuItem;
        private ToolStripMenuItem ConnectToolItem;
        private ToolStripMenuItem DisconnectToolItem;
        private ToolStripMenuItem HelpToolMenuItem;
        private ToolStripMenuItem AboutToolItem;
        private Panel panel1;
        private TextBox ServerAddr_tb;
        private Label label1;
        private TextBox PortNum_tb;
        private Label label2;
        private Button DisconnectFrom_btn;
        private Button ConnectTo_btn;
        private Panel panel2;
        private RichTextBox ChatMessages_rtb;
        private ListBox Users_lstbox;
        private TextBox InputMessage_tb;
        private Button SendMessage_btn;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel ConnectionStatus_lbl;
        private ToolStripProgressBar Connection_pbar;
        private ToolStripMenuItem AddUserToolItem;
        private TextBox ServerMessage_tb;
        private Button ServerMessage_btn;
    }
}
