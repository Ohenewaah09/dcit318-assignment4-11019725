using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicalApp
{
    public partial class DoctorListForm : Form
    {
        public DoctorListForm()
        {
            InitializeComponent();
            Text = "Doctors";
        }

        private void DoctorListForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                conn = DbHelper.GetConnection();
                conn.Open();

                var sql = @"SELECT DoctorID, FullName, Specialty, 
                                   CASE WHEN Availability = 1 THEN 'Available' ELSE 'Unavailable' END AS Availability
                            FROM dbo.Doctors
                            ORDER BY FullName";
                cmd = DbHelper.CreateCommand(conn, sql);
                rdr = cmd.ExecuteReader();

                // Bind via DataReader -> DataTable.Load(reader)
                var table = new DataTable();
                table.Load(rdr);
                dgvDoctors.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                rdr?.Close();
                cmd?.Dispose();
                if (conn?.State == ConnectionState.Open) conn.Close();
                conn?.Dispose();
            }
        }
    }
}