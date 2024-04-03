
using EliteRecruit.Models;
using EliteRecruit.Repository;
using EliteRecruit.Tests.Fixture;
using EliteRecruit.Tests.Helpers;
using static EliteRecruit.Helpers.Enums;
using  EliteRecruit.ViewModels;

namespace EliteRecruit.Tests
{
    [Collection("DisablesParallelExecution")]
    public class StudentRepositoryTests : IClassFixture<SharedDatabaseFixture>
    {
        private readonly SharedDatabaseFixture _fixture;
        private readonly StudentRepository _repository;
        StudentViewModel studentViewModel = new();

        public StudentRepositoryTests(SharedDatabaseFixture fixture)
        {
            _fixture = fixture;
            _repository = new StudentRepository(_fixture.CreateContext());
        }

        [Fact]

        public async Task Get_Students_FilterBy_Default()
        {
            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.LastNameASC, studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_2, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_3, s.LastName));
        }

        [Fact]
        public async Task Get_Students_FilterBy_None()
        {
            
            // Arrange.
            var searchString = "Notpresent";

            // Act.
            IList<Student> students = await _repository.GetStudents(searchString, SortByParameter.LastNameASC, studentViewModel);

            // Assert.
            Assert.Equal(0, students.Count);
        }
        

        [Fact]
        public async Task Get_Students_FilterBy_Many()
        {
            // Arrange.
            var searchString = "Student";

            // Act.
            IList<Student> students = await _repository.GetStudents(searchString, SortByParameter.LastNameASC, studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_2, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_3, s.LastName));
        }

        
        [Fact]
        public async Task Get_Students_FilterBy_Single()
        {
            // Arrange.
            var searchString = Constants.LAST_NAME_1;

            // Act.
            IList<Student> students = await _repository.GetStudents(searchString, SortByParameter.LastNameASC, studentViewModel);

            // Assert.
            Assert.Single(students);

            // The number of inspectors should match the number of Students in the list.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName));
        }

        
        [Fact]
        public async Task Get_Students_SortBy_FirstName_DESC()
        {
            // Arrange.


            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.FirstNameDESC,studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list in the right order.
            // NOTE: The Act sorts by FirstNameDESC which the logic will sort the Last Name Descending in the implementation.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_3, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_2, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName));
        }

        
        [Fact]
        public async Task Get_Students_SortBy_LastName_ASC()
        {
            // Arrange.


            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.LastNameASC,studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list in the right order.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_2, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_3, s.LastName));
        }

        [Fact]
        public async Task Get_Students_SortBy_LastName_DESC()
        {
            // Arrange.


            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.LastNameDESC,studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list in the right order.
            Assert.Collection(students,
                s => Assert.Equal(Constants.LAST_NAME_3, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_2, s.LastName),
                s => Assert.Equal(Constants.LAST_NAME_1, s.LastName));
        }

        
        [Fact]
        public async Task Get_Students_SortBy_GraduationDate_ASC()
        {
            // Arrange.


            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.GPAASC,studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list in the right order.
            Assert.Collection(students,
                s => Assert.Equal(Constants.GPA_2, s.GPA),
                s => Assert.Equal(Constants.GPA_3, s.GPA),
                s => Assert.Equal(Constants.GPA_1, s.GPA));
        }

        
        [Fact]
        public async Task Get_Students_SortBy_GraduationDate_DESC()
        {
            // Arrange.


            // Act.
            IList<Student> students = await _repository.GetStudents(string.Empty, SortByParameter.GPADSC,studentViewModel);

            // Assert.
            Assert.Equal(3, students.Count);

            // The number of inspectors should match the number of Students in the list in the right order.
            Assert.Collection(students,
                s => Assert.Equal(Constants.GPA_1, s.GPA),
                s => Assert.Equal(Constants.GPA_3, s.GPA),
                s => Assert.Equal(Constants.GPA_2, s.GPA));
        }

        [Fact]
        public async Task Get_Student_ById()
        {
            // Arrange.
            int studentId = 1;

            // Act.
            Student student = await _repository.GetStudentByID(studentId);

            // Assert.
            Assert.Equal(Constants.LAST_NAME_1, student.LastName);
        }

        [Fact]
        public async Task Get_Student_ById_NotFound()
        {
            // Arrange.
            int studentId = -1;

            // Act.
            Student student = await _repository.GetStudentByID(studentId);

            // Assert.
            Assert.Null(student);
        }

        [Fact]
        public async Task Get_Student_ById_After_Insert()
        {
            // Arrange.
            StudentViewModel viewModel = new()
            {
                FirstName = "Test",
                LastName = "GetById",
                School = "TestSchool",
                GPA = 3.5M,
                Major = "Physics",
                SchoolYear = "Junior",
                Email = "testemail@gmail.com",
                PhoneNumber = "1234567891"
            };

            // Act.
            Student newStudent = await _repository.InsertStudent(viewModel);
            Student sudent = await _repository.GetStudentByID(newStudent.Id);

            // Assert.
            Assert.Same(newStudent, sudent);
            Assert.Equal(sudent.LastName, viewModel.LastName);

            // Cleanup.
            await _repository.DeleteStudent(newStudent.Id);
        }

        
        [Fact]
        public async Task Insert_Student()
        {
            // Arrange.
            StudentViewModel viewModel = new()
            {
                FirstName = "Test",
                LastName = "Insert",
                GPA = 3.5M,
                School = "TestSchool",
                Major = "Physics",
                SchoolYear = "Junior",
                Email = "testemail@gmail.com",
                PhoneNumber = "1234567891"
            };

            // Act.
            Student newStudent = await _repository.InsertStudent(viewModel);
            Student student = await _repository.GetStudentByID(newStudent.Id);

            // Assert.
            Assert.Same(newStudent, student);
            Assert.Equal(student.LastName, viewModel.LastName);
            Assert.Equal(student.GPA, viewModel.GPA);

            // Cleanup.
            await _repository.DeleteStudent(newStudent.Id);
        }
        
        [Fact]
        public async Task Update_Student()
        {
            // Arrange.
            string tempLastName = "Update_Update";

            StudentViewModel viewModel = new()
            {
                FirstName = "Test",
                LastName = "Update",
                GPA = 3.1M,
                School = "TestSchool",
                Major = "Physics",
                SchoolYear = "Junior",
                Email = "testemail@gmail.com",
                PhoneNumber = "1234567891"
            };

            // Act.
            Student newStudent = await _repository.InsertStudent(viewModel);

            viewModel.Id = newStudent.Id;
            viewModel.FirstName = newStudent.FirstName;
            viewModel.LastName = tempLastName;
            viewModel.GPA = 3.1M;

            Student student = await _repository.UpdateStudent(viewModel);

            // Assert.
            Assert.IsAssignableFrom<Student>(student);
            Assert.Equal(student.LastName, tempLastName);

            // Cleanup.
            await _repository.DeleteStudent(newStudent.Id);
        }
        
        [Fact]
        public async Task Delete_Student()
        {
            // Arrange.
            StudentViewModel viewModel = new()
            {
                FirstName = "Test",
                LastName = "Delete",
                GPA = 3.9M,
                School = "TestSchool",
                Major = "Physics",
                SchoolYear = "Junior",
                Email = "testemail@gmail.com",
                PhoneNumber = "1234567891"
            };

            // Act.
            Student newStudent = await _repository.InsertStudent(viewModel);

            int id = newStudent.Id;
            await _repository.DeleteStudent(id);

            Student student = await _repository.GetStudentByID(id);

            // Assert.
            Assert.Null(student);
        }

    }
}
