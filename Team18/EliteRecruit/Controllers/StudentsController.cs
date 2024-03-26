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

        // GET: Students


        public async Task<IActionResult> Index(StudentViewModel studentViewModel)
        {

            MaintainViewState(ref studentViewModel);

            studentViewModel.Students = await _studentRepository.GetStudents(studentViewModel.FilterBy, studentViewModel.SortBy, studentViewModel);

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
                var updatedStudent = await _studentRepository.UpdateStudent(studentViewModel);

                //catch (DbUpdateConcurrencyException)
                if (updatedStudent == null)
                {
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

            if (studentViewModel.SortBy == SortByParameter.GPAASC)
            {
                studentViewModel.SortByGPA = SortByParameter.GPADSC;
            }
            else
            {
                studentViewModel.SortByGPA = SortByParameter.GPAASC;
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
