using EliteRecruit.Models;
using EliteRecruit.ViewModels;
using static EliteRecruit.Helpers.Enums;

namespace EliteRecruit.Interfaces
{
    public interface IStudentRepository : IDisposable
    {
        Task<IList<Student>> GetStudents(string filterBy, SortByParameter sortBy);
        Task<Student> GetStudentByID(int studentId);
        //Task<Student> GetStudentByName(string name);
        //Task<Student> GetStudentByEmail(string email);
        Task<Student> InsertStudent(StudentViewModel studentViewModel);
        Task<Student> UpdateStudent(StudentViewModel studentViewModel);
        Task DeleteStudent(int studentId);

        Task<Student> GetStudentByStudentId(int studentId); 


    }
}