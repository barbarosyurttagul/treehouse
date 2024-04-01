using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Service;
using Barbarosoft.TreeHouse.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

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
        var course = MapCreateCourseDto(courseRequest);
        var serviceResult = await _courseService.Create(course);

        if (!serviceResult.IsSuccess)
            return BadRequest(serviceResult.Message);

        return Created(new Uri($"{Request.Path}", UriKind.Relative), course);
    }

    private static CourseEntity MapCreateCourseDto(CreateCourseRequest courseRequest)
    {
        var course = new CourseEntity
        {
            Name = courseRequest.Name!,
            CategoryId = courseRequest.CategoryId
        };

        return course;
    }
}


