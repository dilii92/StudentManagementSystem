using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Data_Access;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagementSystem.Models
{
    public class StudentRepository:IStudentReporsitory
    {
        private IConfiguration configuration;
        private SqlConnection sqlConnection;
        private void Connection()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            sqlConnection = new SqlConnection(connectionString);
        }

        // ********** VIEW STUDENT DETAILS ********************
        public List<Students> GetStudent()
        {
            try
            {
                Connection();
                List<Students> studentlist = new List<Students>();

                SqlCommand cmd = new SqlCommand("GetStudentDetails", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sd.Fill(dt);
                sqlConnection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    studentlist.Add(
                        new Students
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = Convert.ToString(dr["Name"]),
                            City = Convert.ToString(dr["City"]),
                            Address = Convert.ToString(dr["Address"])
                        });
                }
                return studentlist;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
