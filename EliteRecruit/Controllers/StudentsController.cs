using EliteRecruit.Data;
using EliteRecruit.Interfaces;
using EliteRecruit.Models;
using EliteRecruit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using static EliteRecruit.Helpers.Enums;


namespace EliteRecruit.Controllers
{
    [Authorize]
    public class StudentsController(IStudentRepository studentRepository) : Controller
    {
        private readonly IStudentRepository _studentRepository = studentRepository;

        public async Task<IActionResult> Index(StudentViewModel studentViewModel)
        {
       
            MaintainViewState(ref studentViewModel);
            await _studentRepository.UpdateStudentsImagePath();
            studentViewModel.Students = await _studentRepository.GetStudents(studentViewModel.FilterBy, studentViewModel.SortBy, studentViewModel);

            return View(studentViewModel);
        }



        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id, string filterBy, SortByParameter sortBy, string MajorString, string schoolYearString)
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
                    SortBy = sortBy,
                    majorString = MajorString,
                    SchoolYearString = schoolYearString
                };
            }
            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create(string filterBy, SortByParameter sortBy,string MajorString, string schoolYearString)
        {
            //StudentViewModel studentViewModel = new();
            StudentViewModel studentViewModel = new()
            {
                FilterBy = filterBy,
                SortBy = sortBy,
                majorString = MajorString,
                SchoolYearString = schoolYearString
            };
            return View(studentViewModel);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber,ImagePath,Image,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.InsertStudent(studentViewModel);
                return RedirectToAction(nameof(Index), studentViewModel);
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id, string filterBy, SortByParameter sortBy, string MajorString, string schoolYearString)
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
                    SortBy = sortBy,
                    majorString = MajorString,
                    SchoolYearString = schoolYearString
                };
            }

            return View(studentViewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber,ImagePath,Image,ClearImagePath,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {

            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedStudent = await _studentRepository.UpdateStudent(studentViewModel);

                if (updatedStudent == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index), studentViewModel);
            }
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, string filterBy, SortByParameter sortBy, string MajorString, string schoolYearString)
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
                    SortBy = sortBy,
                    majorString = MajorString,
                    SchoolYearString = schoolYearString
                };
            }


            return View(studentViewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Id,FirstName,LastName,School,GPA,Major,SchoolYear,Email,PhoneNumber,ImagePath,Image,ClearImagePath,FilterBy,SortBy")] StudentViewModel studentViewModel)
        {
            await _studentRepository.DeleteStudent(studentViewModel.Id);
            return RedirectToAction(nameof(Index), studentViewModel);
        }

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

            if (studentViewModel.SortBy == SortByParameter.GPAASC)
            {
                studentViewModel.SortByGPA = SortByParameter.GPADSC;
            }
            else
            {
                studentViewModel.SortByGPA = SortByParameter.GPAASC;
            }
        }
    }
}
