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
    public async Task<ActionResult> Create([FromBody] CreateCourseDto courseDto)
    {
        if (!IsBodyValid(courseDto))
        {
            return BadRequest();
        }
        var course = MapCreateCourseDto(courseDto);
        await _courseService.Create(course);
        return Created(new Uri($"{Request.Path}", UriKind.Relative), course);
    }

    private CourseEntity MapCreateCourseDto(CreateCourseDto createCourseDto)
    {
        var course = new CourseEntity
        {
            Name = createCourseDto.Name!,
            CategoryId = createCourseDto.CategoryId
        };

        return course;
    }

    private bool IsBodyValid(CreateCourseDto createCourseDto)
    {
        if (createCourseDto.Name.IsNullOrEmpty())
        {
            return false;
        }
        else if (createCourseDto.CategoryId <= 0)
        {
            return false;
        }
        return true;
    }
}


