using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EliteRecruit.Data;
using EliteRecruit.ViewModels;
using EliteRecruit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace EliteRecruit.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly EliteRecruitContext _context;

        public StudentsController(EliteRecruitContext context)
        {
            _context = context;
        }

        // GET: Students

        public async Task<IActionResult> Index(StudentViewModel studentViewModel, string SchoolY, string searchString, string majorString)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'DbContext.Student' is null.");
            }
            IQueryable<string> YearQuery = from m in _context.Student
                                            orderby m.SchoolYear
                                            select m.SchoolYear;
            IQueryable<string> MajorQuery = from m in _context.Student
                                           orderby m.Major
                                           select m.Major;


            var students = from s in _context.Student
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) ||
                                               s.LastName.Contains(searchString) ||
                                               s.School.Contains(searchString));
            }

            var graduationYearOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "2027" },
                new SelectListItem { Value = "2", Text = "2026" },
                new SelectListItem { Value = "3", Text = "2025" },
                new SelectListItem { Value = "4", Text = "2024" },
                new SelectListItem { Value = "5", Text = "2026G" }
            };

            if (!string.IsNullOrEmpty(SchoolY))
            {
                students = students.Where(x => x.SchoolYear == SchoolY);
            }

            if (!string.IsNullOrEmpty(majorString))
            {
                students = students.Where(x => x.Major == majorString);
            }

            studentViewModel = new StudentViewModel
            {
                SchoolYear2 = new SelectList(await YearQuery.Distinct().ToListAsync()),
                Major2 = new SelectList(await MajorQuery.Distinct().ToArrayAsync()),
                Students = await students.ToListAsync(),
                GraduationYearOptions = graduationYearOptions
            };

            return View(studentViewModel);
        }



        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            StudentViewModel studentViewModel;
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            else
            {
                studentViewModel = new(student);
            }


            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            StudentViewModel studentViewModel = new();
            return View(studentViewModel);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber")] StudentViewModel studentViewModel)
        {

            if (ModelState.IsValid)
            {
                Student student = new()
                {
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                    School = studentViewModel.School,
                    GPA = studentViewModel.GPA,
                    Major = studentViewModel.Major,
                    SchoolYear = studentViewModel.SchoolYear,
                    Email = studentViewModel.Email,
                    PhoneNumber = studentViewModel.PhoneNumber
                };
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            StudentViewModel studentViewModel;

            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                studentViewModel = new(student);
            }

            return View(studentViewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber")] StudentViewModel studentViewModel)
        {

            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Student student = await _context.Student.FindAsync(studentViewModel.Id);
                    if (student == null)
                    {
                        return NotFound();
                    }

                    student.FirstName = studentViewModel.FirstName;
                    student.LastName = studentViewModel.LastName;
                    student.School = studentViewModel.School;
                    student.GPA = studentViewModel.GPA;
                    student.Major = studentViewModel.Major;
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            StudentViewModel studentViewModel;

            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            else
            {
                studentViewModel = new(student);
            }


            return View(studentViewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
