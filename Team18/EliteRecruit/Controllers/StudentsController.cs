using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EliteRecruit.Data;
using EliteRecruit.Models;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> Index(string SchoolY, string searchString)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'DbContext.Student' is null.");
            }
            IQueryable<string> YearQuery = from m in _context.Student
                                            orderby m.SchoolYear
                                            select m.SchoolYear;


            var students = from s in _context.Student
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) ||
                                               s.LastName.Contains(searchString) ||
                                               s.Major.Contains(searchString) ||
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


            var studentViewModel = new StudentViewModel
            {
                SchoolYear = new SelectList(await YearQuery.Distinct().ToListAsync()),
                Students = await students.ToListAsync(),
                GraduationYearOptions = graduationYearOptions
            };

            return View(studentViewModel);
        }



        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,School,GPA,Major,Minor,SchoolYear,Email,PhoneNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,School,GPA,Major,Minor,SchoolYear,Email,PhoneNumber")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
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
