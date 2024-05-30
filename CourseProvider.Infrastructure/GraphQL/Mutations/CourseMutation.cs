using CourseProvider.Infrastructure.Models;
using CourseProvider.Infrastructure.Services;

namespace CourseProvider.Infrastructure.GraphQL.Mutations;

public class CourseMutation(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("createCourse")]
    public async Task<Course> CreateCourseAsync(CourseCreateRequest input)
    {
        return await _courseService.CreateCourseAsync(input);
    }

    [GraphQLName("updateCourse")]
    public async Task<Course> UpdateCourseAsync(CourseUpdateRequestInput request)
    {
        return await _courseService.UpdateCourseAsync(request);
    }

    [GraphQLName("deleteCourse")]
    public async Task<bool> DeleteCourseAsync(string id)
    {
        return await _courseService.DeleteCourseAsync(id);
    }
}
