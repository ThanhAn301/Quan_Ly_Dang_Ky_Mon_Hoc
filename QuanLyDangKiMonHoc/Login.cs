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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Check()
        {
            if(txtUsername.Text == "")
            {
                MessageBox.Show("Bạn vui lòng nhập tài khoản");
            }else if (txtPassWord.Text == "")
            {
                MessageBox.Show("Bạn vui lòng nhập mật khẩu");
            }
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            Check();

            if (txtUsername.Text != "" && txtPassWord.Text != "")
            {
                if (txtUsername.Text == "admin" && txtPassWord.Text == "admin123")
                {
                    Admin adminForm = new Admin();
                    this.Hide();
                    adminForm.ShowDialog();
                    this.Show();
                }
                else {
                    if (DateTime.Today > Admin.endDate1 || DateTime.Today < Admin.startDate1)
                    {
                        MessageBox.Show("Đã hết thời gian đăng ký!!!");
                        return;
                    }
                    else
                    {
                        using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True"))
                        {
                            string query = "select ID,Password_Participant from Participant,Student where StudentID = ID and ID = '" + txtUsername.Text + "' and Password_Participant = '" + txtPassWord.Text + "'";

                            connection.Open();
                            SqlDataAdapter ada = new SqlDataAdapter(query, connection);
                            DataTable dt = new DataTable();
                            ada.Fill(dt);

                            if (dt.Rows.Count != 1)
                            {
                                MessageBox.Show("Tài khoản hoặc mật khẩu bị sai");
                                return;
                            }
                            else
                            {
                                HomePage homePage = new HomePage(txtUsername.Text);
                                this.Hide();
                                homePage.ShowDialog();
                                this.Show();
                            }

                            connection.Close();

                        }
                    }
                }
            }
        }
    }
}
