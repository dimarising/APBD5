﻿using CW.DTO.Requests;
using CW.DTO.Responses;
using CW.ModelsEF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW.Services
{
    public class ModificStudentsDbService : StudentDbService
    {
        private readonly s16541Context _context;

        public ModificStudentsDbService(s16541Context context)
        {
            _context = context;
        }

        public Student DeleteStudent(Student student)
        {
            var res = _context.Student.FirstOrDefault(s => s.IndexNumber == student.IndexNumber);
            if(res == null)
            {
                return null;
            }
            _context.Student.Remove(res);
            _context.SaveChanges();
            return res;
        }

        public List<Student> GetStudents()
        {
            return _context.Student.ToList();
        }

        public Student InsertStudent(Student student)
        {
            var res = _context.Student.FirstOrDefault(s => s.IndexNumber == student.IndexNumber);
            if (res == null)
            {
                _context.Student.Add(student);
                _context.SaveChanges();
                return student;
            }
            return null;
        }

        public Student UpdateStudent(Student student)
        {
            _context.Attach(student);
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return student;
        }
    }
}
