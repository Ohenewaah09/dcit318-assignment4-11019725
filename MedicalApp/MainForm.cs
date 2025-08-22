using System;
using System.Windows.Forms;

namespace MedicalApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnDoctors_Click(object sender, EventArgs e)
        {
            using (var f = new DoctorListForm())
                f.ShowDialog(this);
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            using (var f = new AppointmentForm())
                f.ShowDialog(this);
        }

        private void BtnManage_Click(object sender, EventArgs e)
        {
            using (var f = new ManageAppointmentsForm())
                f.ShowDialog(this);
        }
    }
}