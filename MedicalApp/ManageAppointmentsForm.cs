using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicalApp
{
    public partial class ManageAppointmentsForm : Form
    {
        private DataSet _ds;
        private SqlDataAdapter _adapter;

        public ManageAppointmentsForm()
        {
            InitializeComponent();
            Text = "Manage Appointments";
        }

        private void ManageAppointmentsForm_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private string BuildWhereClause(out SqlParameter[] parameters)
        {
            string where = "WHERE 1=1";
            var list = new System.Collections.Generic.List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                where += " AND (p.FullName LIKE @q OR d.FullName LIKE @q)";
                list.Add(new SqlParameter("@q", $"%{txtSearch.Text.Trim()}%"));
            }

            if (chkDateRange.Checked)
            {
                where += " AND a.AppointmentDate >= @from AND a.AppointmentDate < DATEADD(DAY, 1, @to)";
                list.Add(new SqlParameter("@from", dtFrom.Value.Date));
                list.Add(new SqlParameter("@to", dtTo.Value.Date));
            }

            parameters = list.ToArray();
            return where;
        }

        private void LoadAppointments()
        {
            SqlConnection conn = null;
            try
            {
                conn = DbHelper.GetConnection();

                var baseSql = @"
SELECT a.AppointmentID, 
       d.FullName AS Doctor, d.Specialty,
       p.FullName AS Patient, p.Email,
       a.AppointmentDate, a.Notes, a.DoctorID, a.PatientID
FROM dbo.Appointments a
JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
JOIN dbo.Patients p ON a.PatientID = p.PatientID
";

                var where = BuildWhereClause(out var pars);
                var sql = baseSql + " " + where + " ORDER BY a.AppointmentDate DESC";

                _adapter = new SqlDataAdapter(sql, conn);
                foreach (var p in pars) _adapter.SelectCommand.Parameters.Add(p);

                _ds = new DataSet();
                _adapter.Fill(_ds, "AppointmentsView");

                dgvAppts.DataSource = _ds.Tables["AppointmentsView"];
                dgvAppts.Columns["DoctorID"].Visible = false;
                dgvAppts.Columns["PatientID"].Visible = false;
                dgvAppts.Columns["AppointmentID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn?.Dispose();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadAppointments();
        private void TxtSearch_TextChanged(object sender, EventArgs e) => LoadAppointments();

        private int? GetSelectedAppointmentId()
        {
            if (dgvAppts.CurrentRow == null) return null;
            var row = dgvAppts.CurrentRow;
            var idObj = row.Cells["AppointmentID"].Value;
            if (idObj == null || idObj == DBNull.Value) return null;
            return Convert.ToInt32(idObj);
        }

        private void BtnUpdateDate_Click(object sender, EventArgs e)
        {
            var apptId = GetSelectedAppointmentId();
            if (apptId == null)
            {
                MessageBox.Show("Select an appointment first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Optional: prevent past date updates
            if (dtpNewDate.Value < DateTime.Now.AddMinutes(-1))
            {
                MessageBox.Show("New date/time must be in the future.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = null;
            SqlCommand cmdConflict = null;
            SqlCommand cmd = null;
            try
            {
                conn = DbHelper.GetConnection();
                conn.Open();

                // Find the selected doctor for conflict check
                var doctorId = Convert.ToInt32(dgvAppts.CurrentRow.Cells["DoctorID"].Value);

                // Conflict check: same doctor, same new datetime, different appointment
                cmdConflict = DbHelper.CreateCommand(conn, @"
SELECT COUNT(1) FROM dbo.Appointments
WHERE DoctorID=@DoctorID AND AppointmentDate=@NewDate AND AppointmentID<>@AppointmentID");
                DbHelper.AddParam(cmdConflict, "@DoctorID", doctorId, SqlDbType.Int);
                DbHelper.AddParam(cmdConflict, "@NewDate", dtpNewDate.Value, SqlDbType.DateTime);
                DbHelper.AddParam(cmdConflict, "@AppointmentID", apptId.Value, SqlDbType.Int);

                var conflict = (int)cmdConflict.ExecuteScalar() > 0;
                if (conflict)
                {
                    MessageBox.Show("Another appointment already exists for this doctor at the chosen time.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmd = DbHelper.CreateCommand(conn, @"
UPDATE dbo.Appointments 
SET AppointmentDate=@NewDate 
WHERE AppointmentID=@AppointmentID");
                DbHelper.AddParam(cmd, "@NewDate", dtpNewDate.Value, SqlDbType.DateTime);
                DbHelper.AddParam(cmd, "@AppointmentID", apptId.Value, SqlDbType.Int);

                var rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    MessageBox.Show("Appointment updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show("No changes made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd?.Dispose();
                cmdConflict?.Dispose();
                if (conn?.State == ConnectionState.Open) conn.Close();
                conn?.Dispose();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var apptId = GetSelectedAppointmentId();
            if (apptId == null)
            {
                MessageBox.Show("Select an appointment first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Delete this appointment?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = DbHelper.GetConnection();
                conn.Open();

                cmd = DbHelper.CreateCommand(conn, "DELETE FROM dbo.Appointments WHERE AppointmentID=@AppointmentID");
                DbHelper.AddParam(cmd, "@AppointmentID", apptId.Value, SqlDbType.Int);

                var rows = cmd.ExecuteNonQuery();
                if (rows == 1)
                {
                    MessageBox.Show("Appointment deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show("Appointment not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd?.Dispose();
                if (conn?.State == ConnectionState.Open) conn.Close();
                conn?.Dispose();
            }
        }
    }
}