using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.Data_Access
{
    public interface IStudentReporsitory
    {
        List<Students> GetStudent();
    }
}
