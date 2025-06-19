using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers;

//https://localhost:####/api/students
[Route("api/[controller]")]
[ApiController]

public class StudentsController : Controller
{
    // GET: https://localhost:####/api/students
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        string[] studentNames = ["John", "Jane", "Mark" ];
        return Ok(studentNames);
    }
}