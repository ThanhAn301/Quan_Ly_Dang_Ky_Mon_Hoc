using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyDangKiMonHoc
{
    public class Student
    {
        DBConnect connect = new DBConnect();
        //create a function to add a new students to the database

        public bool InsertStudent(string studentID, int credits, string studyStatus)
        {
            SqlConnection connection = connect.Getconnection;

            SqlCommand command = new SqlCommand("insert into Student(StudentID,Credits,StudyStatus) values(@studentID,@credits,@studyStatus",connection);

            //@fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.AddWithValue("@studentID", studentID);
            command.Parameters.AddWithValue("@credits", credits);
            command.Parameters.AddWithValue("@studyStatus", studyStatus);

            connection.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }

        }
        // to get student table
        public DataTable getStudentlist(SqlCommand command)
        {
            SqlConnection connection = connect.Getconnection;

            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //create a function search for student (first name, last name, address)
        public DataTable searchStudent(string searchdata)
        {
            SqlConnection connection = connect.Getconnection;
            SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE CONCAT(`StdFirstName`,`StdLastName`,`Address`) LIKE '%" + searchdata + "%'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //create a function edit for student
        //public bool updateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        //{
        //    SqlCommand command = new SqlCommand("UPDATE `student` SET `StdFirstName`=@fn,`StdLastName`=@ln,`Birthdate`=@bd,`Gender`=@gd,`Phone`=@ph,`Address`=@adr,`Photo`=@img WHERE  `StdId`= @id", connect.getconnection);

        //    //@id,@fn, @ln, @bd, @gd, @ph, @adr, @img
        //    //command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        //    //command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
        //    //command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
        //    //command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
        //    //command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
        //    //command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
        //    //command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
        //    //command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

        //    //connect.openConnect();
        //    //if (command.ExecuteNonQuery() == 1)
        //    //{
        //    //    connect.closeConnect();
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    connect.closeConnect();
        //    //    return false;
        //    //}

        //}
        //Create a function to delete data
        //we need only id 
        //public bool deleteStudent(int id)
        //{
        //    //SqlCommand command = new SqlCommand("DELETE FROM `student` WHERE `StdId`=@id", connect.getconnection);

        //    ////@id
        //    //command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

        //    //connect.openConnect();
        //    //if (command.ExecuteNonQuery() == 1)
        //    //{
        //    //    connect.closeConnect();
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    connect.closeConnect();
        //    //    return false;
        //    //}

        //}
        // create a function for any command in studentDb
        //public DataTable getList(SqlCommand command)
        //{
        //    //command.Connection = connect.getconnection;
        //    //SqlDataAdapter adapter = new SqlDataAdapter(command);
        //    //DataTable table = new DataTable();
        //    //adapter.Fill(table);
        //    //return table;
        //}
    }
}

