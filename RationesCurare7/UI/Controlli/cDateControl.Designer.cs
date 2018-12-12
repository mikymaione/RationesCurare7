namespace RationesCurare7.UI.Controlli
{
    partial class cDateControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cTime1 = new RationesCurare7.UI.Controlli.cTime();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(215, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // cTime1
            // 
            this.cTime1.Location = new System.Drawing.Point(221, 0);
            this.cTime1.Name = "cTime1";            
            this.cTime1.TabIndex = 1;
            // 
            // cDateControl
            //             
            this.Controls.Add(this.cTime1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "cDateControl";            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private cTime cTime1;
    }
}
