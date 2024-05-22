using CourseProvider.Infrastructure.Models;
using CourseProvider.Infrastructure.Services;
using HotChocolate;

namespace CourseProvider.Infrastructure.GraphQL.Queries;

public class CourseQuery(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("getCourses")]
    public async Task<IEnumerable<Course>> GetCourseAsync()
    {
        return await _courseService.GetCoursesAsync();
    }

    [GraphQLName("getCoursesById")]
    public async Task<Course> GetCourseByIdAsync(string id)
    {
        return await _courseService.GetCourseByIdAsync(id);
    }
}
