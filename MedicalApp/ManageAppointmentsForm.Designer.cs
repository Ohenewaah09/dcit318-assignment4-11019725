namespace MedicalApp
{
    partial class ManageAppointmentsForm
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvAppts = new System.Windows.Forms.DataGridView();
            this.dtpNewDate = new System.Windows.Forms.DateTimePicker();
            this.btnUpdateDate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppts)).BeginInit();
            this.SuspendLayout();
           
            // txtSearch
          
            this.txtSearch.Location = new System.Drawing.Point(10, 10);
            this.txtSearch.Name = "txtSearch";
            //this.txtSearch.PlaceholderText = "Search patient/doctor...";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 0;
           
            // chkDateRange
          
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Location = new System.Drawing.Point(220, 12);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(110, 17);
            this.chkDateRange.TabIndex = 1;
            this.chkDateRange.Text = "Use Date Range";
            this.chkDateRange.UseVisualStyleBackColor = true;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "yyyy-MM-dd";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(340, 10);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(150, 20);
            this.dtFrom.TabIndex = 2;
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "yyyy-MM-dd";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(500, 10);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(150, 20);
            this.dtTo.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(660, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvAppts
            // 
            this.dgvAppts.AllowUserToAddRows = false;
            this.dgvAppts.AllowUserToDeleteRows = false;
            this.dgvAppts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppts.Location = new System.Drawing.Point(10, 45);
            this.dgvAppts.Name = "dgvAppts";
            this.dgvAppts.ReadOnly = true;
            this.dgvAppts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppts.Size = new System.Drawing.Size(750, 320);
            this.dgvAppts.TabIndex = 5;
            // 
            // dtpNewDate
            // 
            this.dtpNewDate.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpNewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNewDate.Location = new System.Drawing.Point(10, 375);
            this.dtpNewDate.Name = "dtpNewDate";
            this.dtpNewDate.Size = new System.Drawing.Size(200, 20);
            this.dtpNewDate.TabIndex = 6;
            // 
            // btnUpdateDate
            // 
            this.btnUpdateDate.Location = new System.Drawing.Point(220, 373);
            this.btnUpdateDate.Name = "btnUpdateDate";
            this.btnUpdateDate.Size = new System.Drawing.Size(120, 23);
            this.btnUpdateDate.TabIndex = 7;
            this.btnUpdateDate.Text = "Update Date";
            this.btnUpdateDate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(350, 373);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // ManageAppointmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 460);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdateDate);
            this.Controls.Add(this.dtpNewDate);
            this.Controls.Add(this.dgvAppts);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.chkDateRange);
            this.Controls.Add(this.txtSearch);
            this.Name = "ManageAppointmentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Appointments";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvAppts;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
        private System.Windows.Forms.Button btnUpdateDate;
        private System.Windows.Forms.Button btnDelete;
    }
}