using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StudentManagementSystem.Business_Logic
{
    public class StudentService
    {
        private IConfiguration configuration;
        private string connectionString;
        private readonly HttpClient client;

        public StudentService()
        {
            client = new HttpClient();
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); // Default configuration
            connectionString = configuration.GetSection("APIEndPoint").Value;
            client.BaseAddress = new Uri(connectionString);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<Students> GetStudents()
        {
            List<Students> students = new List<Students>();
            try
            {
                //HTTP GET
                HttpResponseMessage studentsResponse = client.GetAsync("api/Students").Result;
                if (studentsResponse.IsSuccessStatusCode)
                {
                    students = JsonConvert.DeserializeObject<List<Students>>(studentsResponse.Content.ReadAsStringAsync().Result);
                } 
                 
                return students;                
             }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
