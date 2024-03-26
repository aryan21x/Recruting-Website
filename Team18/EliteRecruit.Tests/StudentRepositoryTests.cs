
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

            StudentViewModel studentViewModel = new StudentViewModel();

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
    }
}
