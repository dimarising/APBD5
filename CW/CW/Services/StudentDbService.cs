using CW.Models;
using System.Collections.Generic;

namespace CW.Services
{
    public interface StudentDbService
    {
        public List<Student> GetStudents();
        public Student UpdateStudent(Student student);
        public Student DeleteStudent(Student student);
        public Student InsertStudent(Student student);


    }
}
