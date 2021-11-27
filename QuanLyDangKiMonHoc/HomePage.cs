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
    public partial class HomePage : Form
    {
        private int sum = 0;
        public HomePage(string idStudent)
        {
            this.idStudent = idStudent;
            InitializeComponent();
            
        }

        private string idStudent;
        private void LoadSubject()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select SubjectID,SubjectName,Credits,Semester,DepartmentName,PreviousSubjectID from Subject";
                
                dataGridViewSubject.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    dataGridViewSubject.Rows.Add(dr["SubjectID"], dr["SubjectName"], dr["Credits"], dr["Semester"], dr["DepartmentName"], dr["PreviousSubjectID"]);
                }
                dr.Close();
                connection.Close();
            }
        }

        public void LoadSubjectRegistered()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select ClassRoom,C.ClassName,S.SubjectID,S.SubjectName,S.Credits,TeacherID,DayInWeek from Subject as S,Class as C,Study " +
                    "where C.SubjectID = S.SubjectID and Study.ClassName = C.ClassName and Study.StudentID = '" + idStudent + "'";

                dataGridViewRegistered.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    dataGridViewRegistered.Rows.Add(dr["ClassRoom"], dr["ClassName"],dr["SubjectID"], dr["SubjectName"], dr["Credits"], dr["TeacherID"], dr["DayInWeek"]);
                }
                dr.Close();
                connection.Close();
            }
        }
        private void dataGridViewSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (CheckMonTrung(dataGridViewSubject.CurrentRow.Cells[0].Value.ToString()))
            //{
            //    MessageBox.Show("Bạn đã chọn môn này rồi");
            //    return;
            //}
            Form formBackground = new Form();

            try
            {
                ClassForm classForm = new ClassForm(idStudent,this, false, " ");
                //classForm.Show();

                formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.WindowState = FormWindowState.Maximized;
                //formBackground.TopMost = true;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();

                classForm.Owner = formBackground;
                classForm.SearchClass(dataGridViewSubject.CurrentRow.Cells[0].Value.ToString());
                classForm.ShowDialog();

                formBackground.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }





        private void SearchSubject()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.SearchSubject(@Name)", connection);
            command.Parameters.AddWithValue("@Name", textBox_search.Text);
            command.ExecuteNonQuery();
            dataGridViewSubject.Rows.Clear();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                dataGridViewSubject.Rows.Add(dr["SubjectID"], dr["SubjectName"], dr["Credits"], dr["Semester"], dr["DepartmentName"], dr["PreviousSubjectID"]);
            }
            dr.Close();

            connection.Close();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            SearchSubject();
        }

        private bool CheckMonTrung(string idSubject)
        {
            foreach(DataGridViewRow dr in dataGridViewRegistered.Rows)
            {
                if (dr.Cells[2].Value.ToString() == idSubject)
                    return true;
            }

            return false;
        }

        private void dataGridViewRegistered_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridViewRegistered.Columns[e.ColumnIndex].Name;
            if (name == "Delete")
            {
                if (MessageBox.Show("Bạn muốn xóa môn này phải không", "Xóa môn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                    {
                        connection.Open();
                        var command = new SqlCommand("DeleteSuject", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SelectStudentId", idStudent);
                        command.Parameters.AddWithValue("@SelectClassName", dataGridViewRegistered.CurrentRow.Cells[1].Value.ToString());
                        //string query = "delete from Study where ClassName = '" + dataGridViewRegistered.CurrentRow.Cells[1].Value.ToString() + "'";
                        //SqlCommand command = new SqlCommand(query, connection);
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Xóa môn thành công");
                            LoadSubjectRegistered();
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
            else if(name == "Change")
            {
                if(MessageBox.Show("Bạn muốn thay đổi lớp không", "Thay đổi lớp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form formBackground = new Form();

                    try
                    {
                        ClassForm classForm = new ClassForm(idStudent, this, true, dataGridViewRegistered.CurrentRow.Cells[1].Value.ToString());
                        formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();

                        classForm.Owner = formBackground;
                        classForm.SearchClass(dataGridViewRegistered.CurrentRow.Cells[2].Value.ToString());
                        classForm.ShowDialog();

                        formBackground.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        formBackground.Dispose();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn hủy danh sách này phải không", "Xóa danh sách", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from Study";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công");
                        dataGridViewRegistered.Rows.Clear();
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

        private void CountCredits()
        {
            int sum1 = 0;

            foreach (DataGridViewRow dr in dataGridViewRegistered.Rows)
            {
                sum1 += int.Parse(dr.Cells[4].Value.ToString());
            }

            sum = sum1;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            LoadSubject();
            LoadSubjectRegistered();
            CountCredits();
            countCredit.Text = sum.ToString();
        }

        private void dataGridViewRegistered_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CountCredits();
            countCredit.Text = sum.ToString();
        }

        private void dataGridViewRegistered_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountCredits();
            countCredit.Text = sum.ToString();
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Hide();
        }

        private void dataGridViewRegistered_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
