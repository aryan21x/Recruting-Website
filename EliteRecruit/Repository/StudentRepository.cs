using EliteRecruit.Data;
using EliteRecruit.Interfaces;
using EliteRecruit.Models;
using EliteRecruit.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static EliteRecruit.Helpers.Enums;

namespace EliteRecruit.Repository
{
    public class StudentRepository(EliteRecruitContext context) : IStudentRepository
    {
        private EliteRecruitContext _context = context;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context = null;
        }


        public async Task<IList<Student>> GetStudents(string filterBy, SortByParameter sortBy, StudentViewModel studentViewModel)
        {
            var students = from s in _context.Student
                           select s;

            if (string.IsNullOrEmpty(filterBy) == false)
            {
                students = students.Where(s => s.FirstName.Contains(filterBy) || s.LastName.Contains(filterBy));
            }

            if (!string.IsNullOrEmpty(studentViewModel.majorString))
            {
                students = students.Where(x => x.Major == studentViewModel.majorString);
            }

            if (!string.IsNullOrEmpty(studentViewModel.SchoolYearString))
            {
                students = students.Where(x => x.SchoolYear == studentViewModel.SchoolYearString);
            }

            IQueryable<string> MajorQuery = from m in _context.Student
                                            orderby m.Major
                                            select m.Major;
            studentViewModel.MajorList = new SelectList(await MajorQuery.Distinct().ToArrayAsync());

            IQueryable<string> GraduationYearQuery = from m in _context.Student
                                                     orderby m.SchoolYear
                                                     select m.SchoolYear;

            studentViewModel.SchoolYearList = new SelectList(await GraduationYearQuery.Distinct().ToListAsync());

            students = sortBy switch
            {
                SortByParameter.FirstNameDESC => students.OrderByDescending(o => o.FirstName).ThenByDescending(o => o.LastName),
                SortByParameter.LastNameASC => students.OrderBy(o => o.LastName).ThenBy(o => o.FirstName),
                SortByParameter.LastNameDESC => students.OrderByDescending(o => o.LastName).ThenByDescending(o => o.FirstName),
                SortByParameter.GPAASC => students.OrderBy(o => o.GPA).ThenBy(o => o.FirstName).ThenBy(o => o.LastName),
                SortByParameter.GPADSC => students.OrderByDescending(o => o.GPA).ThenBy(o => o.FirstName).ThenBy(o => o.LastName),


                /*SortByParameter.GraduationDateASC => students.OrderBy(o => o.GraduationYear).ThenBy(o => o.FirstName).ThenBy(o => o.LastName),
                SortByParameter.GraduationDateDESC => students.OrderByDescending(o => o.GraduationYear).ThenByDescending(o => o.FirstName).ThenByDescending(o => o.LastName),
                _ => students.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ThenBy(o => o.GraduationYear),*/
                _ => students.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ThenBy(o => o.GPA),
            };

            return await students.ToListAsync();
        }

        public async Task<Student> GetStudentByID(int studentId)
        {
            return await _context.Student.FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task<Student> InsertStudent(StudentViewModel studentViewModel)
        {
            Student student = new()
            {

                FirstName = studentViewModel.FirstName.Trim(),
                LastName = studentViewModel.LastName.Trim(),
                School = studentViewModel.School.Trim(),
                GPA = studentViewModel.GPA,
                Major = studentViewModel.Major.Trim(),
                SchoolYear = studentViewModel.SchoolYear,
                Email = studentViewModel.Email,
                PhoneNumber = studentViewModel.PhoneNumber
            };

            _context.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task DeleteStudent(int studentID)
        {
            var student = _context.Student.SingleOrDefault(s => s.Id == studentID);

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> UpdateStudent(StudentViewModel studentViewModel)
        {
            Student student;

            try
            {
                student = await _context.Student.FindAsync(studentViewModel.Id);

                student.FirstName = studentViewModel.FirstName.Trim();
                student.LastName = studentViewModel.LastName.Trim();
                student.School = studentViewModel.School.Trim();
                student.GPA = studentViewModel.GPA;
                student.Major = studentViewModel.Major.Trim();
                student.SchoolYear = studentViewModel.SchoolYear;
                student.Email = studentViewModel.Email;
                student.PhoneNumber = studentViewModel.PhoneNumber;

                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(studentViewModel.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return student;
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
