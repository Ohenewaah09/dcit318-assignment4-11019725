using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicalApp
{
    public partial class AppointmentForm : Form
    {
        public AppointmentForm()
        {
            InitializeComponent();
            Text = "Book Appointment";
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            LoadDoctors();
            LoadPatients();
        }

        private void LoadDoctors()
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                var cmd = DbHelper.CreateCommand(conn, "SELECT DoctorID, FullName FROM dbo.Doctors WHERE Availability = 1 ORDER BY FullName");
                var adapter = new SqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                cbDoctor.DisplayMember = "FullName";
                cbDoctor.ValueMember = "DoctorID";
                cbDoctor.DataSource = table;
            }
        }

        private void LoadPatients()
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                var cmd = DbHelper.CreateCommand(conn, "SELECT PatientID, FullName FROM dbo.Patients ORDER BY FullName");
                var adapter = new SqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                cbPatient.DisplayMember = "FullName";
                cbPatient.ValueMember = "PatientID";
                cbPatient.DataSource = table;
            }
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            if (cbDoctor.SelectedValue == null || cbPatient.SelectedValue == null)
            {
                MessageBox.Show("Please select both doctor and patient.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDate.Value < DateTime.Now)
            {
                MessageBox.Show("Appointment date must be in the future.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();

                    // Check for conflicting appointments
                    var conflictCmd = DbHelper.CreateCommand(conn,
                        "SELECT COUNT(*) FROM Appointments WHERE DoctorID = @DoctorID AND AppointmentDate = @AppointmentDate");
                    DbHelper.AddParam(conflictCmd, "@DoctorID", cbDoctor.SelectedValue, SqlDbType.Int);
                    DbHelper.AddParam(conflictCmd, "@AppointmentDate", dtpDate.Value, SqlDbType.DateTime);

                    var conflictCount = (int)conflictCmd.ExecuteScalar();
                    if (conflictCount > 0)
                    {
                        MessageBox.Show("This doctor already has an appointment at the selected time.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new appointment
                    var insertCmd = DbHelper.CreateCommand(conn,
                        "INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) VALUES (@DoctorID, @PatientID, @AppointmentDate, @Notes)");
                    DbHelper.AddParam(insertCmd, "@DoctorID", cbDoctor.SelectedValue, SqlDbType.Int);
                    DbHelper.AddParam(insertCmd, "@PatientID", cbPatient.SelectedValue, SqlDbType.Int);
                    DbHelper.AddParam(insertCmd, "@AppointmentDate", dtpDate.Value, SqlDbType.DateTime);
                    DbHelper.AddParam(insertCmd, "@Notes", txtNotes.Text, SqlDbType.NVarChar);

                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Appointment booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}