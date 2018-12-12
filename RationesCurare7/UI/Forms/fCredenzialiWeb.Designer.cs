namespace RationesCurare7.UI.Forms
{
    partial class fCredenzialiWeb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCredenzialiWeb));
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.eEmail = new System.Windows.Forms.TextBox();
            this.ePassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // bOK
            // 
            this.bOK.Image = global::RationesCurare7.Properties.Resources.accept;
            this.bOK.Location = new System.Drawing.Point(15, 109);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(50, 25);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "Ok";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // eEmail
            // 
            this.eEmail.Location = new System.Drawing.Point(15, 25);
            this.eEmail.MaxLength = 80;
            this.eEmail.Name = "eEmail";
            this.eEmail.Size = new System.Drawing.Size(224, 20);
            this.eEmail.TabIndex = 0;
            // 
            // ePassword
            // 
            this.ePassword.Location = new System.Drawing.Point(15, 73);
            this.ePassword.MaxLength = 250;
            this.ePassword.Name = "ePassword";
            this.ePassword.Size = new System.Drawing.Size(224, 20);
            this.ePassword.TabIndex = 1;
            this.ePassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // fCredenzialiWeb
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 146);
            this.Controls.Add(this.ePassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eEmail);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fCredenzialiWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credenziali Web";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.TextBox eEmail;
        private System.Windows.Forms.TextBox ePassword;
        private System.Windows.Forms.Label label2;
    }
}