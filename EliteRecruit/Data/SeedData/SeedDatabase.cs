using EliteRecruit.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EliteRecruit.Data.SeedData
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new EliteRecruitContext(serviceProvider.GetRequiredService<DbContextOptions<EliteRecruitContext>>());

            // Look for any Students.
            if (context.Student.Any())
            {
                return; // Database has been seeded
            }

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "EliteRecruit.Data.SeedData.seedData.csv";

            // Retrieve the resource stream
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource '{resourceName}' not found.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    // Skip the header row
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var student = new Student
                        {
                            FirstName = values[1],
                            LastName = values[2],
                            School = values[3],
                            GPA = Convert.ToDecimal(values[4]),
                            Major = values[5],
                            SchoolYear = values[6],
                            Email = values[7],
                            PhoneNumber = values[8]
                        };

                        context.Student.Add(student);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
