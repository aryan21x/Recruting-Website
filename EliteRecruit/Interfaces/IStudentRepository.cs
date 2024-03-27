using EliteRecruit.Models;
using EliteRecruit.ViewModels;
using static EliteRecruit.Helpers.Enums;

namespace EliteRecruit.Interfaces
{
    public interface IStudentRepository : IDisposable
    {
        Task<IList<Student>> GetStudents(string filterBy, SortByParameter sortBy, StudentViewModel studentViewModel);
        Task<Student> GetStudentByID(int studentId);
        Task<Student> InsertStudent(StudentViewModel studentViewModel);
        Task<Student> UpdateStudent(StudentViewModel studentViewModel);
        Task DeleteStudent(int studentId);
    }
}