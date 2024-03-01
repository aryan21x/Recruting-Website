using EliteRecruit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EliteRecruit.Models;
public class StudentViewModel
{
    public List<Student>? Students { get; set; }
    public string? SearchString { get; set; }

    public SelectList? SchoolYear { get; set; }
    public string? SchoolY { get; set; }
    public IEnumerable<SelectListItem> GraduationYearOptions { get; set; }

    public SelectList? Major { get; set; }
    public String? majorString { get; set; }

}

