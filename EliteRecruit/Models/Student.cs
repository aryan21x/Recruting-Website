﻿namespace EliteRecruit.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public decimal GPA { get; set; }
        public string Major { get; set; }
        public string SchoolYear { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int GraduationYear
        {
            get
            {
                if (int.TryParse(SchoolYear, out int year))
                {
                    if (year > 4) // graduate 
                    {
                        return DateTime.Now.Year + 2;
                    }
                    return DateTime.Now.Year + (4 - year);
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}