using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EliteRecruit.Data;
using EliteRecruit.Interfaces;
using EliteRecruit.ViewModels;
using EliteRecruit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static EliteRecruit.Helpers.Enums;

namespace EliteRecruit.Controllers
{
    [Authorize]
    public class StudentsController(IStudentRepository studentRepository) : Controller
    {
        private readonly IStudentRepository _studentRepository = studentRepository;

        /*public StudentsController(EliteRecruitContext context)
        {
            _context = context;
        }*/

        // GET: Students

        public async Task<IActionResult> Index(StudentViewModel studentViewModel)
        {
            /*if (_context.Student == null)
            {
                return Problem("Entity set 'DbContext.Student' is null.");
            }

            //  queries for GraduationYear and Major

            IQueryable<string> GraduationYearQuery = from m in _context.Student
                                            orderby m.SchoolYear
                                            select m.SchoolYear;
            IQueryable<string> MajorQuery = from m in _context.Student
                                           orderby m.Major
                                           select m.Major;

            var students = from s in _context.Student
                           select s;

            // searching by firstName, lastName and school

            if (!string.IsNullOrEmpty(studentViewModel.searchString))
            {
                students = students.Where(s => s.FirstName.Contains(studentViewModel.searchString) ||
                                               s.LastName.Contains(studentViewModel.searchString) ||
                                               s.School.Contains(studentViewModel.searchString));
            }

            // filter by schoolYear and major

            if (!string.IsNullOrEmpty(studentViewModel.SchoolYearString))
            {
                students = students.Where(x => x.SchoolYear == studentViewModel.SchoolYearString);
            }

            if (!string.IsNullOrEmpty(studentViewModel.majorString))
            {
                students = students.Where(x => x.Major == studentViewModel.majorString);
            }


            studentViewModel = new StudentViewModel
            {
                SchoolYearList = new SelectList(await GraduationYearQuery.Distinct().ToListAsync()),
                MajorList = new SelectList(await MajorQuery.Distinct().ToArrayAsync()),
                Students = await students.ToListAsync(),
                GraduationYearOptions = studentViewModel.GraduationYearOptions
            };*/
            MaintainViewState(ref studentViewModel);

            studentViewModel.Students = await _studentRepository.GetStudents(studentViewModel.FilterBy, studentViewModel.SortBy);

            return View(studentViewModel);
        }



        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id, string filterBy, SortByParameter sortBy)
        {
            StudentViewModel studentViewModel;
            if (id == null)
            {
                return NotFound();
            }

            //var student = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
            var student = await _studentRepository.GetStudentByID((int)id);

            if (student == null)
            {
                return NotFound();
            }

            else
            {
                studentViewModel = new(student)
                {
                    FilterBy = filterBy,
                    SortBy = sortBy
                };
            }


            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create(string filterBy, SortByParameter sortBy)
        {
            //StudentViewModel studentViewModel = new();
            StudentViewModel studentViewModel = new()
            {
                FilterBy = filterBy,
                SortBy = sortBy
            };
            return View(studentViewModel);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {

            if (ModelState.IsValid)
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
                //_context.Add(student);
                //await _context.SaveChangesAsync();
                await _studentRepository.InsertStudent(studentViewModel);
                return RedirectToAction(nameof(Index), studentViewModel);
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id, string filterBy, SortByParameter sortBy)
        {
            StudentViewModel studentViewModel;

            if (id == null)
            {
                return NotFound();
            }

            //var student = await _context.Student.FindAsync(id);
            var student = await _studentRepository.GetStudentByID((int)id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                studentViewModel = new(student)
                {
                    FilterBy = filterBy,
                    SortBy = sortBy
                };
            }

            return View(studentViewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {

            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*try
                {
                    Student student = await _context.Student.FindAsync(studentViewModel.Id);
                    if (student == null)
                    {
                        return NotFound();
                    }

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
                }*/

                var updatedStudent = await _studentRepository.UpdateStudent(studentViewModel);

                //catch (DbUpdateConcurrencyException)
                if (updatedStudent == null)
                {
                    /*if (!StudentExists(studentViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                    return NotFound();
                }
                return RedirectToAction(nameof(Index), studentViewModel);
            }
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, string filterBy, SortByParameter sortBy)
        {
            StudentViewModel studentViewModel;

            if (id == null)
            {
                return NotFound();
            }

            //var student = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
            var student = await _studentRepository.GetStudentByID((int)id);

            if (student == null)
            {
                return NotFound();
            }

            else
            {
                studentViewModel = new(student)
                {
                    FilterBy = filterBy,
                    SortBy = sortBy
                };
            }


            return View(studentViewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Id,FirstName,LastName,GraduationDate,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {
            /*var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();*/
            await _studentRepository.DeleteStudent(studentViewModel.Id);
            return RedirectToAction(nameof(Index), studentViewModel);
        }

        //private bool StudentExists(int id)
        private static void MaintainViewState(ref StudentViewModel studentViewModel)
        {
            //return _context.Student.Any(e => e.Id == id);
            // Swap First Name sort order.
            if (studentViewModel.SortBy == SortByParameter.FirstNameASC)
            {
                studentViewModel.SortByFirstName = SortByParameter.FirstNameDESC;
            }
            else
            {
                studentViewModel.SortByFirstName = SortByParameter.FirstNameASC;
            }

            // Swap Last Name sort order.
            if (studentViewModel.SortBy == SortByParameter.LastNameASC)
            {
                studentViewModel.SortByLastName = SortByParameter.LastNameDESC;
            }
            else
            {
                studentViewModel.SortByLastName = SortByParameter.LastNameASC;
            }

            // Swap Graduation Date sort order.
            /*if (studentViewModel.SortBy == SortByParameter.GraduationDateASC)
            {
                studentViewModel.SortByGraduationDate = SortByParameter.GraduationDateDESC;
            }
            else
            {
                studentViewModel.SortByGraduationDate = SortByParameter.GraduationDateASC;
            }*/
        }
    }
}
