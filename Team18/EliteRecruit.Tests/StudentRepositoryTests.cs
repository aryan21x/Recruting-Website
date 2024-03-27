
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
        StudentViewModel studentViewModel = new StudentViewModel();

        public StudentRepositoryTests(SharedDatabaseFixture fixture)
        {
            _fixture = fixture;
            _repository = new StudentRepository(_fixture.CreateContext());
        }

        [Fact]

        public async Task Get_Students_FilterBy_Default()
        {
            Assert.True(true);
            // Arrange.

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
        
    }
}
