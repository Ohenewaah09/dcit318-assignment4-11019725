namespace MedicalApp
{
    partial class MainForm
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
            this.btnDoctors = new System.Windows.Forms.Button();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnManage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDoctors
            // 
            this.btnDoctors.Location = new System.Drawing.Point(30, 30);
            this.btnDoctors.Name = "btnDoctors";
            this.btnDoctors.Size = new System.Drawing.Size(180, 23);
            this.btnDoctors.TabIndex = 0;
            this.btnDoctors.Text = "View Doctors";
            this.btnDoctors.UseVisualStyleBackColor = true;
            this.btnDoctors.Click += new System.EventHandler(this.BtnDoctors_Click);
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(30, 75);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(180, 23);
            this.btnBook.TabIndex = 1;
            this.btnBook.Text = "Book Appointment";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.BtnBook_Click);
            // 
            // btnManage
            // 
            this.btnManage.Location = new System.Drawing.Point(30, 120);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(180, 23);
            this.btnManage.TabIndex = 2;
            this.btnManage.Text = "Manage Appointments";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.BtnManage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 220);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.btnDoctors);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medical Appointment System - Main";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnDoctors;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnManage;
    }
}