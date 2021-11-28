using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDangKiMonHoc
{
    
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            GetIDStudent();
            LoadStudents();
        }
        public static Guna.UI2.WinForms.Guna2DateTimePicker date1;
        public static Guna.UI2.WinForms.Guna2DateTimePicker date2;

        #region Xử lí Department
        private void LoadDepartments()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Department";
                dgvDepartment.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvDepartment.Rows.Add(dr["DepartmentName"], dr["PhoneNumber"], dr["Email"], dr["Fax"]);

                }
                dr.Close();
                connection.Close();
            }
        }

        private void btnAddDepart_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Department(DepartmentName,PhoneNumber,Email,Fax) values(@name,@phone,@email,@fax)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbDepartment.Text);
                command.Parameters.AddWithValue("@phone", tbPhone.Text);
                command.Parameters.AddWithValue("@email", tbEmail.Text);
                command.Parameters.AddWithValue("@fax", tbFax.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    LoadDepartments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbDepartment.Text = dgvDepartment.CurrentRow.Cells[0].Value.ToString();
            tbEmail.Text = dgvDepartment.CurrentRow.Cells[2].Value.ToString();
            tbPhone.Text = dgvDepartment.CurrentRow.Cells[1].Value.ToString();
            tbFax.Text = dgvDepartment.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdateDepartment_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update Department set DepartmentName = @name,PhoneNumber = @phone,Email = @email,Fax = @fax where DepartmentName = @name";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbDepartment.Text);
                command.Parameters.AddWithValue("@phone", tbPhone.Text);
                command.Parameters.AddWithValue("@email", tbEmail.Text);
                command.Parameters.AddWithValue("@fax", tbFax.Text);

                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Department", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadDepartments();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void dgvDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from Department where DepartmentName = @name";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@name", tbDepartment.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Department", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadDepartments();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region Xử lí Participant
        private void GetDepartment()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select DepartmentName from Department";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbDepartment.DataSource = dt;
                cbDepartment.DisplayMember = "DepartmentName";
                connection.Close();
            }
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Participant(ID,ParticipantName,Gender,Username,Password_Participant,Adress ,DepartmentName) values(@id,@name,@gender,@username,@password,@address,@department)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", tbIDParticipant.Text);
                command.Parameters.AddWithValue("@name", tbNameParticipant.Text);
                command.Parameters.AddWithValue("@gender", cbGender.SelectedItem);
                command.Parameters.AddWithValue("@username", tbUsername.Text);
                command.Parameters.AddWithValue("@password", tbPassword.Text);
                command.Parameters.AddWithValue("@address", tbAddress.Text);
                command.Parameters.AddWithValue("@department", cbDepartment.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    ClearText();
                    LoadParticipants();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void LoadParticipants()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Participant";
                dgvParticipant.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvParticipant.Rows.Add(dr["ID"], dr["ParticipantName"], dr["Gender"], dr["Username"], dr["Password_Participant"], dr["Adress"], dr["DepartmentName"]);
                }
                dr.Close();
                connection.Close();
            }
        }
        private void dgvParticipant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbIDParticipant.Text = dgvParticipant.CurrentRow.Cells[0].Value.ToString();
            tbNameParticipant.Text = dgvParticipant.CurrentRow.Cells[1].Value.ToString();
            cbGender.SelectedItem = dgvParticipant.CurrentRow.Cells[2].Value.ToString();
            tbUsername.Text = dgvParticipant.CurrentRow.Cells[3].Value.ToString();
            tbPassword.Text = dgvParticipant.CurrentRow.Cells[4].Value.ToString();
            tbAddress.Text = dgvParticipant.CurrentRow.Cells[5].Value.ToString();
            cbDepartment.Text = dgvParticipant.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnUpdateParticipant_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update Participant set ID = @id,ParticipantName = @name,Gender = @gender,Username = @username,Password_Participant = @password,Adress = @address,DepartmentName = @department where ID = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", tbIDParticipant.Text);
                command.Parameters.AddWithValue("@name", tbNameParticipant.Text);
                command.Parameters.AddWithValue("@gender", cbGender.SelectedItem);
                command.Parameters.AddWithValue("@username", tbUsername.Text);
                command.Parameters.AddWithValue("@password", tbPassword.Text);
                command.Parameters.AddWithValue("@address", tbAddress.Text);
                command.Parameters.AddWithValue("@department", cbDepartment.Text);

                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Participant", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadParticipants();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ClearText()
        {
            tbIDParticipant.Clear();
            tbNameParticipant.Clear();
            cbGender.SelectedIndex = -1;
            tbUsername.Clear();
            tbPassword.Clear();
            tbAddress.Clear();
            cbDepartment.SelectedIndex = -1;
        }

        private void dgvParticipant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from Participant where ID = @id";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", tbIDParticipant.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Participant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadParticipants();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region Xử lí Student
        private void GetIDStudent()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select ID from Participant,Student where ID = StudentID";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbStudentID.DataSource = dt;
                cbStudentID.DisplayMember = "ID";
                connection.Close();
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Student(StudentID,Credits,StudyStatus) values(@id,@credits,@status)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbStudentID.Text);
                command.Parameters.AddWithValue("@credits", tbCredits.Text);
                command.Parameters.AddWithValue("@status", tbStudyStatus.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    cbStudentID.SelectedIndex = -1;
                    tbStudyStatus.Clear();
                    tbCredits.Clear();
                    LoadStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void LoadStudents()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Student";
                dgvStudent.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvStudent.Rows.Add(dr["StudentID"], dr["Credits"], dr["StudyStatus"]);

                }
                dr.Close();
                connection.Close();
            }
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update Student set StudentID = @id,Credits = @credits,StudyStatus = @status where StudentID = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbStudentID.Text);
                command.Parameters.AddWithValue("@credits", tbCredits.Text);
                command.Parameters.AddWithValue("@status", tbStudyStatus.Text);
                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadStudents();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbStudentID.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString();
            tbStudyStatus.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString();
            tbCredits.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
        }

        private void dgvStudent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from Student where StudentID = @id";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", cbStudentID.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadStudents();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion


        private void GetID(ComboBox cb)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select distinct ID from Participant";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cb.DataSource = dt;
                cb.DisplayMember = "ID";
                connection.Close();
            }
        }

        #region Xử lí Participant Email
        private void LoadParticipantEmail()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                dgvParEmail.Rows.Clear();
                string query = "select ID,Email from ParticipantEmail";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvParEmail.Rows.Add(dr["ID"], dr["Email"]);
                }
                dr.Close();
                connection.Close();
            }
        }

        private void btnAddParEmail_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into ParticipantEmail(ID,Email) values(@id,@email)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbID.Text.ToString());
                command.Parameters.AddWithValue("@email", tbEmailParticipant.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    cbID.SelectedIndex = -1;
                    tbEmailParticipant.Clear();
                    LoadParticipantEmail();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnUpdateParEmail_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update ParticipantEmail set ID = @id,Email = @email where Email = @email1";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbID.Text.ToString());
                command.Parameters.AddWithValue("@email", tbEmailParticipant.Text);
                command.Parameters.AddWithValue("@email1", dgvParEmail.CurrentRow.Cells[1].Value.ToString());
                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Participant Email", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadParticipantEmail();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvParEmail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbID.Text = dgvParEmail.CurrentRow.Cells[0].Value.ToString();
            tbEmailParticipant.Text = dgvParEmail.CurrentRow.Cells[1].Value.ToString();
        }

        private void dgvParEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from ParticipantEmail where Email = @email";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@email", tbEmailParticipant.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Participant Email", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadParticipantEmail();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        #endregion

        #region Xử lí Participant Phone
        private void LoadParticipantPhone()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                dgvParPhone.Rows.Clear();
                string query = "select ID,PhoneNumber from ParticipantPhoneNumber";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvParPhone.Rows.Add(dr["ID"], dr["PhoneNumber"]);
                }
                dr.Close();
                connection.Close();
            }
        }

        private void btnAddParPhone_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into ParticipantPhoneNumber(ID,PhoneNumber) values(@id,@phone)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbID1.Text.ToString());
                command.Parameters.AddWithValue("@phone", tbPhonePar.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    cbID1.SelectedIndex = -1;
                    tbPhonePar.Clear();
                    LoadParticipantPhone();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnUpdateParPhone_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update ParticipantPhoneNumber set ID = @id,PhoneNumber = @phone where PhoneNumber = @phone1";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbID1.Text.ToString());
                command.Parameters.AddWithValue("@phone", tbPhonePar.Text);
                command.Parameters.AddWithValue("@phone1", dgvParPhone.CurrentRow.Cells[1].Value.ToString());
                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Participant Phone", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadParticipantPhone();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvParPhone_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbID1.Text = dgvParPhone.CurrentRow.Cells[0].Value.ToString();
            tbPhonePar.Text = dgvParPhone.CurrentRow.Cells[1].Value.ToString();
        }

        private void dgvParPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from ParticipantPhoneNumber where PhoneNumber = @phone";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@phone", tbPhonePar.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Participant Phone", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            cbID1.SelectedIndex = -1;
                            tbPhonePar.Clear();
                            LoadParticipantPhone();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        #endregion

        #region Xử lí Teacher
        private void GetIDTeacher()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select ID from Participant,Teacher where ID = TeacherID";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbTeacherID.DataSource = dt;
                cbTeacherID.DisplayMember = "ID";
                connection.Close();
            }
        }

        private void LoadTeachers()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Teacher";
                dgvTeacher.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvTeacher.Rows.Add(dr["TeacherID"], dr["Degree"]);

                }
                dr.Close();
                connection.Close();
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Teacher(TeacherID,Degree) values(@TeacherID,@Degree)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TeacherID", cbTeacherID.Text.ToString());
                command.Parameters.AddWithValue("@Degree", tbDegree.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                    cbTeacherID.SelectedIndex = -1;
                    tbDegree.Clear();
                    LoadTeachers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnUpdateTeacher_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "update Teacher set TeacherID = @id,Degree = @degree where TeacherID = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", cbTeacherID.Text);
                command.Parameters.AddWithValue("@degree", tbDegree.Text);

                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Teacher", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadTeachers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvTeacher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "delete from Teacher where TeacherID = @id";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", cbTeacherID.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Teacher", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadTeachers();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void dgvTeacher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbTeacherID.Text = dgvTeacher.CurrentRow.Cells[0].Value.ToString();
            tbDegree.Text = dgvTeacher.CurrentRow.Cells[1].Value.ToString();
        }
        #endregion

        #region Xử lí Course
        private void LoadCourses()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Subject";
                dgvCourse.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvCourse.Rows.Add(dr["SubjectName"], dr["SubjectID"], dr["Credits"], dr["Semester"], dr["DepartmentName"], dr["PreviousSubjectID"]);
                }
                dr.Close();
                connection.Close();
            }
        }

        private void dgvCourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbSubjectName.Text = dgvCourse.CurrentRow.Cells[0].Value.ToString();
            tbSubject.Text = dgvCourse.CurrentRow.Cells[1].Value.ToString();
            tbSemester.Text = dgvCourse.CurrentRow.Cells[3].Value.ToString();
            tbCreditCourse.Text = dgvCourse.CurrentRow.Cells[2].Value.ToString();
            cbDepartmentCourse.Text = dgvCourse.CurrentRow.Cells[4].Value.ToString();
            tbIDPre.Text = dgvCourse.CurrentRow.Cells[5].Value.ToString();
        }

        private void GetDepartment1()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select DepartmentName from Department";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbDepartmentCourse.DataSource = dt;
                cbDepartmentCourse.DisplayMember = "DepartmentName";
                connection.Close();
            }
        }

        private void dgvCourse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "delete from Subject where SubjectID = @id";

                    SqlCommand command = new SqlCommand(query, connection);


                    command.Parameters.AddWithValue("@id", tbSubject.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Subject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadCourses();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Subject(SubjectName,SubjectID,Credits,Semester,DepartmentName,PreviousSubjectID) values(@name,@id,@credits,@semester,@department,@idpre)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbSubjectName.Text);
                command.Parameters.AddWithValue("@id", tbSubject.Text);
                command.Parameters.AddWithValue("@credits", tbCreditCourse.Text);
                command.Parameters.AddWithValue("@semester", tbSemester.Text);
                command.Parameters.AddWithValue("@department", cbDepartmentCourse.Text);
                command.Parameters.AddWithValue("@idpre", tbIDPre.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");

                    tbSubjectName.Clear();
                    tbSubject.Clear();
                    tbSemester.Clear();
                    tbCreditCourse.Clear();
                    cbDepartmentCourse.SelectedIndex = -1;
                    tbIDPre.Clear();

                    LoadCourses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnUpdateCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();

                string query = "update Subject set SubjectName = @name,SubjectID = @id,Credits = @credits,Semester = @semester,DepartmentName = @department,PreviousSubjectID = @idpre where SubjectID = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbSubjectName.Text);
                command.Parameters.AddWithValue("@id", tbSubject.Text);
                command.Parameters.AddWithValue("@credits", tbCreditCourse.Text);
                command.Parameters.AddWithValue("@semester", tbSemester.Text);
                command.Parameters.AddWithValue("@department", cbDepartmentCourse.Text);
                command.Parameters.AddWithValue("@idpre", tbIDPre.Text);

                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Subject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadCourses();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        #endregion

        #region Xử lí Class

        private void GetIDTeacher1()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select ID from Participant,Teacher where ID = TeacherID";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbTeacherClass.DataSource = dt;
                cbTeacherClass.DisplayMember = "ID";
                connection.Close();
            }
        }

        private void GetSubject()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select SubjectID from Subject";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter ada = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                cbSubject.DataSource = dt;
                cbSubject.DisplayMember = "SubjectID";
                connection.Close();
            }
        }
        private void dgvClass_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbClassName.Text = dgvClass.CurrentRow.Cells[0].Value.ToString();
            tbClassRoom.Text = dgvClass.CurrentRow.Cells[1].Value.ToString();
            cbTeacherClass.Text = dgvClass.CurrentRow.Cells[2].Value.ToString();
            cbSubject.Text = dgvClass.CurrentRow.Cells[3].Value.ToString();
            tbDay.Text = dgvClass.CurrentRow.Cells[4].Value.ToString();
            tbStartWeek.Text = dgvClass.CurrentRow.Cells[5].Value.ToString();
            tbEndWeek.Text = dgvClass.CurrentRow.Cells[6].Value.ToString();
            tbStartTime.Text = dgvClass.CurrentRow.Cells[7].Value.ToString();
            tbEndTime.Text = dgvClass.CurrentRow.Cells[8].Value.ToString();
            tbMaximumStudent.Text = dgvClass.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();
                string query = "insert into Class(ClassName, ClassRoom, TeacherID, SubjectID, DayInWeek, StartWeek, EndWeek, StartTimeInDay, EndTimeInDay,MaximumStudents) " +
                    "values(@name, @room, @idTeacher, @idSubject, @day, @start, @end, @starttime, @endtime,@max)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbClassName.Text);
                command.Parameters.AddWithValue("@room", tbClassRoom.Text);
                command.Parameters.AddWithValue("@idTeacher", cbTeacherClass.Text);
                command.Parameters.AddWithValue("@idSubject", cbSubject.Text);
                command.Parameters.AddWithValue("@day", tbDay.Text);
                command.Parameters.AddWithValue("@start", tbStartWeek.Text);
                command.Parameters.AddWithValue("@end", tbEndWeek.Text);
                command.Parameters.AddWithValue("@starttime", tbStartTime.Text);
                command.Parameters.AddWithValue("@endtime", tbEndTime.Text);
                command.Parameters.AddWithValue("@max", tbMaximumStudent.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");

                    tbClassName.Clear();
                    tbClassRoom.Clear();
                    cbTeacherClass.SelectedIndex = -1;
                    cbSubject.SelectedIndex = -1;
                    tbDay.Clear();
                    tbStartWeek.Clear();
                    tbEndWeek.Clear();
                    tbStartTime.Clear();
                    tbEndTime.Clear();
                    tbMaximumStudent.Clear();

                    LoadClass();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnUpdateClass_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                connection.Open();

                string query = "update Class set ClassName = @name, ClassRoom = @room, TeacherID = @idTeacher, SubjectID = @idSubject, DayInWeek = @day, StartWeek = @start, EndWeek = @end, " +
                    "StartTimeInDay = @starttime, EndTimeInDay = @endtime,MaximumStudents = @max where ClassName = @name";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", tbClassName.Text);
                command.Parameters.AddWithValue("@room", tbClassRoom.Text);
                command.Parameters.AddWithValue("@idTeacher", cbTeacherClass.Text);
                command.Parameters.AddWithValue("@idSubject", cbSubject.Text);
                command.Parameters.AddWithValue("@day", tbDay.Text);
                command.Parameters.AddWithValue("@start", tbStartWeek.Text);
                command.Parameters.AddWithValue("@end", tbEndWeek.Text);
                command.Parameters.AddWithValue("@starttime", tbStartTime.Text);
                command.Parameters.AddWithValue("@endtime", tbEndTime.Text);
                command.Parameters.AddWithValue("@max", tbMaximumStudent.Text);

                try
                {
                    if (MessageBox.Show("Do you want update?", "Update Class", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Update successfully");
                        LoadClass();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void dgvClass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "delete from Class where ClassName = @name";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@name", tbClassName.Text);

                    try
                    {
                        if (MessageBox.Show("Do you want delete?", "Delete Class", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Delete successfully");
                            LoadClass();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void LoadClass()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
            {
                string query = "select * from Class";
                dgvClass.Rows.Clear();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dgvClass.Rows.Add(dr["ClassName"], dr["ClassRoom"], dr["TeacherID"], dr["SubjectID"], dr["DayInWeek"], dr["StartWeek"], dr["EndWeek"], dr["StartTimeInDay"], dr["EndTimeInDay"], dr["MaximumStudents"]);
                }
                dr.Close();
                connection.Close();
            }
        }
        #endregion


        private void guna2TabControl1_Click_1(object sender, EventArgs e)
        {
            string text = guna2TabControl1.SelectedTab.Text;
            
            if (text == "Student")
            {
                GetIDStudent();
                LoadStudents();
            }
            else if (text == "Department")
            {
                LoadDepartments();
            }
            else if (text == "Participant")
            {
                GetDepartment();
                LoadParticipants();
            }
            else if (text == "Participant Email")
            {
                GetID(cbID);
                LoadParticipantEmail();
            }
            else if (text == "Participant Number")
            {
                GetID(cbID1);
                LoadParticipantPhone();
            }
            else if (text == "Teacher")
            {
                GetIDTeacher();
                LoadTeachers();
            }
            else if (text == "Course")
            {
                GetDepartment1();
                LoadCourses();
            }
            else if (text == "Class")
            {
                GetSubject();
                GetIDTeacher1();
                LoadClass();
            }
        }

        private void tbEndTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            this.Hide();
            f.Show();
        }

        private void tbNameParticipant_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
