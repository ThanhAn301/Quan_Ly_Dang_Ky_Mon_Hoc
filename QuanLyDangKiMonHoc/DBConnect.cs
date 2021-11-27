using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyDangKiMonHoc
{
    public class DBConnect
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-DJCB51T\TEST;Initial Catalog=QLDKMH;Integrated Security=True");

        //to get connection
        public SqlConnection Getconnection
        {
            get
            {
                return connection;
            }
        }
    }
}
