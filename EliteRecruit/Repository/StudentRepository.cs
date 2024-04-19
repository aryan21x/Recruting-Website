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
                _ => students.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ThenBy(o => o.GPA),
            };

            return await students.ToListAsync();
        }

        public async Task<Student> GetStudentByID(int studentId)
        {
            return await _context.Student
                .Include(c => c.Comments.OrderByDescending(o => o.EnteredOn))
                .ThenInclude(u => u.ApplicationUser)
                .FirstOrDefaultAsync(s => s.Id == studentId);
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
                PhoneNumber = studentViewModel.PhoneNumber,
                ImagePath = studentViewModel.ImagePath
            };

            if (studentViewModel.Image != null)
            {

                // Generate a unique file name for the uploaded image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(studentViewModel.Image.FileName);

                // Define the directory where the image will be saved
                var uploadDirectory = Path.Combine("wwwroot", "StudentImages", fileName);

                var imagePath = Path.Combine("/StudentImages/", fileName);

                // Save the image file to the server
                using (var stream = new FileStream(uploadDirectory, FileMode.Create))
                {
                    await studentViewModel.Image.CopyToAsync(stream);
                }
                student.ImagePath = imagePath;
            }
            _context.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task DeleteStudent(int ID)
        {
            var student = _context.Student.Include(c => c.Comments).SingleOrDefault(s => s.Id == ID);
            _context.Comment.RemoveRange(student.Comments);

            var imagePath = student.ImagePath;
            var delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + imagePath);

            if (File.Exists(delete) && imagePath != "/StudentImages/default.jpg")
            {
                File.Delete(delete);
            }

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

                if (studentViewModel.ClearImagePath == true)
                {
                    var iPath = student.ImagePath;
                    var delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + iPath);

                    if (File.Exists(delete) && iPath != "/StudentImages/default.jpg")
                    {
                        File.Delete(delete);
                    }
                    student.ImagePath = "/StudentImages/default.jpg";
                }

                if (studentViewModel.Image != null)
                {
                    var iPath = student.ImagePath;
                    var delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + iPath);

                    if (File.Exists(delete) && iPath != "/StudentImages/default.jpg")
                    {
                        File.Delete(delete);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(studentViewModel.Image.FileName);

                    var uploadDirectory = Path.Combine("wwwroot", "StudentImages", fileName);

                    var imagePath = Path.Combine("/StudentImages/", fileName);

                    using (var stream = new FileStream(uploadDirectory, FileMode.Create))
                    {
                        await studentViewModel.Image.CopyToAsync(stream);
                    }
                    student.ImagePath = imagePath;
                }

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

        public async Task<Comment> GetCommentByID(int commentId)
        {
            return await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<Comment> InsertComment(CommentViewModel commentViewModel)
        {
            Student student = await _context.Student.FindAsync(commentViewModel.StudentId);

            Comment comment = new()
            {
                Student = student,
                ApplicationUser = commentViewModel.CommentEnteredBy,
                EnteredOn = commentViewModel.CommentEnteredOn,
                Text = commentViewModel.CommentText.Trim()
            };

            _context.Comment.Add(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task DeleteCommentByID(int commentId)
        {
            Comment comment = await _context.Comment.SingleOrDefaultAsync(c => c.Id == commentId);

            _context.Comment.Remove(comment);

            await _context.SaveChangesAsync();
        }

        public async Task<Comment> EditComment(CommentViewModel commentViewModel)
        {
            Comment comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentViewModel.Id);

            comment.Text = commentViewModel.CommentText;

            _context.Comment.Update(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task UpdateStudentsImagePath()
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Student SET ImagePath = '/StudentImages/default.jpg' WHERE ImagePath IS NULL");
        }

        public async Task<IList<Student>> GetTop5StudentsByGPA()
        {
            return await _context.Student.OrderByDescending(s => s.GPA).Take(7).ToListAsync();
        }

    }
}
