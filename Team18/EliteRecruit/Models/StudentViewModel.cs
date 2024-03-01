using EliteRecruit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EliteRecruit.Models;
public class StudentViewModel
{
    public List<Student>? Students { get; set; }
    public string? SearchString { get; set; }
}