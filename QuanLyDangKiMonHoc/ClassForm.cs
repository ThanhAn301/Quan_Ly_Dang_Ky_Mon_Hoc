using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyDangKiMonHoc
{   
    public partial class ClassForm : Form
    {
        public ClassForm(string idStudent,HomePage homePage, bool isChange, string beforeClass)
        {
            HomePage = homePage;
            this.idStudent = idStudent;
            this.isChange = isChange;
            this.beforeClass = beforeClass;
            InitializeComponent();
        }
        
        private string idStudent;
        private HomePage HomePage;
        private bool isChange;
        private string beforeClass;
        private void dataGridViewClass_Click(object sender, EventArgs e)
        {
            txtClass.Text = dataGridViewClass.CurrentRow.Cells[0].Value.ToString();
            txtRoom.Text = dataGridViewClass.CurrentRow.Cells[1].Value.ToString();
            txtTeacher.Text = dataGridViewClass.CurrentRow.Cells[2].Value.ToString();
            txtCourse.Text = dataGridViewClass.CurrentRow.Cells[3].Value.ToString();
            txtTime.Text = dataGridViewClass.CurrentRow.Cells[4].Value.ToString();
            txtStart.Text = dataGridViewClass.CurrentRow.Cells[5].Value.ToString();
            txtEnd.Text = dataGridViewClass.CurrentRow.Cells[6].Value.ToString();
            txtTimeStart.Text = dataGridViewClass.CurrentRow.Cells[7].Value.ToString();
            txtTimeEnd.Text = dataGridViewClass.CurrentRow.Cells[8].Value.ToString();
            NumStudent();
        }


        public void SearchClass(string idSubject)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                dataGridViewClass.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.VIEWCLASS(@subjectID)", connection);
                command.Parameters.AddWithValue("@subjectID", idSubject);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    dataGridViewClass.Rows.Add(dr["ClassName"], dr["ClassRoom"], dr["TeacherName"], dr["SubjectID"], dr["DayInWeek"], dr["StartWeek"], dr["EndWeek"], dr["StartTimeInDay"], dr["EndTimeInDay"]);
                }
                dr.Close();
                connection.Close();
            }
        }

        public void NumStudent()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("dbo.numStudent", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@classname", SqlDbType.VarChar).Value = dataGridViewClass.CurrentRow.Cells[0].Value.ToString();
                SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.VarChar);
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);
                cmd.ExecuteNonQuery();
                txtCount.Text = resultParam.Value.ToString();
                connection.Close();
                
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!isChange)
            {
                if (MessageBox.Show("Bạn muốn chọn lớp này phải không", "Chọn lớp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                    {
                        connection.Open();
                        var command = new SqlCommand("DKYHOC", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@studentID", idStudent);
                        command.Parameters.AddWithValue("@classname", dataGridViewClass.CurrentRow.Cells[0].Value.ToString());
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Chọn môn thành công");
                            HomePage.LoadSubjectRegistered();
                            this.Dispose();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            connection.Close();
                        }

                    }
                }
            }
            else
            {
                if (MessageBox.Show("Bạn muốn chọn lớp này phải không", "Chọn lớp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                    {
                        connection.Open();
                        var command = new SqlCommand("ChangeClassName", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", idStudent);
                        command.Parameters.AddWithValue("@ClassNameBefore", beforeClass);
                        command.Parameters.AddWithValue("@ClassNameAfter", dataGridViewClass.CurrentRow.Cells[0].Value.ToString());
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Chọn môn thành công");
                            HomePage.LoadSubjectRegistered();
                            this.Dispose();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            connection.Close();
                        }

                    }
                }
            }
        }

        private void txtEnd_TextChanged(object sender, EventArgs e)
        {
 
        }

    }
}
