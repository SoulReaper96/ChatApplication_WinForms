namespace Chat_Application_WinForms
{
    partial class AddUserForm
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
            Username_tb = new TextBox();
            AddUser_btn = new Button();
            SuspendLayout();
            // 
            // Username_tb
            // 
            Username_tb.Font = new Font("Arial", 14F, FontStyle.Bold);
            Username_tb.Location = new Point(81, 60);
            Username_tb.Name = "Username_tb";
            Username_tb.Size = new Size(275, 29);
            Username_tb.TabIndex = 0;
            // 
            // AddUser_btn
            // 
            AddUser_btn.FlatStyle = FlatStyle.Flat;
            AddUser_btn.Location = new Point(167, 95);
            AddUser_btn.Name = "AddUser_btn";
            AddUser_btn.Size = new Size(100, 40);
            AddUser_btn.TabIndex = 1;
            AddUser_btn.Text = "ADD USER";
            AddUser_btn.UseVisualStyleBackColor = true;
            AddUser_btn.Click += AddUser_btn_Click;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(468, 205);
            Controls.Add(AddUser_btn);
            Controls.Add(Username_tb);
            Font = new Font("Arial", 10F);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "AddUserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddUserForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Username_tb;
        private Button AddUser_btn;
    }
}