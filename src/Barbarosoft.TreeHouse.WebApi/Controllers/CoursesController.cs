using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Service;
using Barbarosoft.TreeHouse.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Barbarosoft.TreeHouse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCourseRequest courseRequest)
    {
        if (!IsBodyValid(courseRequest))
        {
            return BadRequest();
        }
        var course = MapCreateCourseDto(courseRequest);
        await _courseService.Create(course);
        return Created(new Uri($"{Request.Path}", UriKind.Relative), course);
    }

    private CourseEntity MapCreateCourseDto(CreateCourseRequest courseRequest)
    {
        var course = new CourseEntity
        {
            Name = courseRequest.Name!,
            CategoryId = courseRequest.CategoryId
        };

        return course;
    }

    private bool IsBodyValid(CreateCourseRequest courseRequest)
    {
        if (courseRequest.Name.IsNullOrEmpty())
        {
            return false;
        }
        else if (courseRequest.CategoryId <= 0)
        {
            return false;
        }
        return true;
    }
}


