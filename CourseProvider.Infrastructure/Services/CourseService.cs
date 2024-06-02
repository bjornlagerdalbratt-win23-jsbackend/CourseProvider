using CourseProvider.Infrastructure.Data.Contexts;
using CourseProvider.Infrastructure.Data.Entities;
using CourseProvider.Infrastructure.Factories;
using CourseProvider.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProvider.Infrastructure.Services;

//ett kontrakt på CRUD - det jag behöver skapa för att få det till att fungera
public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseCreateRequest request);
    Task<Course> GetCourseByIdAsync(string id);
    Task<IEnumerable<Course>> GetCoursesAsync(); //ger en array med courses
    Task<Course> UpdateCourseAsync(CourseUpdateRequest request);
    Task<bool> DeleteCourseAsync(string id);
}


public class CourseService(IDbContextFactory<DataContext> contextFactory) : ICourseService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;




    //måste omvanlda från en request till en Entitet, detta görs i factory
    public async Task<Course> CreateCourseAsync(CourseCreateRequest request)
    {
        //Get access to db
        await using var context = _contextFactory.CreateDbContext();

        //Omvandla/populera. variabel courseEtntiyt, skicka request till CourseFactory create
        var courseEntity = CourseFactory.Create(request);

        //lägga till i db
        context.Courses.Add(courseEntity);

        //spara ändringar i db
        await context.SaveChangesAsync();

        //returnera den nya entiteten
        return CourseFactory.Create(courseEntity);
    }


    public async Task<Course> GetCourseByIdAsync(string id)
    {
        //get access to DB
        await using var context = _contextFactory.CreateDbContext();
        //search
        var courseEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        //return entity if true
        return courseEntity == null ? null! : CourseFactory.Create(courseEntity);

    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        //get access to DB
        await using var context = _contextFactory.CreateDbContext();

        //Get courses from courses in context, to a list
        var courseEntities = await context.Courses.ToListAsync();

        //return entities with select through courseFactory
        return courseEntities.Select(CourseFactory.Create);
    }

    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        try
        {
            await using var context = _contextFactory.CreateDbContext();

            var existingCourse = await context.Courses
                .Include(c => c.Content)
                .ThenInclude(c => c.ProgramDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existingCourse == null) return null!;

            // Update course details
            existingCourse.ImageUri = request.ImageUri;
            existingCourse.ImageHeaderUri = request.ImageHeaderUri;
            existingCourse.ImageAuthor = request.ImageAuthor;
            existingCourse.IsBestSeller = request.IsBestSeller;
            existingCourse.IsDigital = request.IsDigital;
            existingCourse.Categories = request.Categories;
            existingCourse.Title = request.Title;
            existingCourse.Ingress = request.Ingress;
            existingCourse.StarRating = request.StarRating;
            existingCourse.Reviews = request.Reviews;
            existingCourse.LikesInProcent = request.LikesInProcent;
            existingCourse.Likes = request.Likes;
            existingCourse.Hours = request.Hours;

            // Update authors
            if (request.Authors != null)
            {
                existingCourse.Authors = request.Authors.Select(a => new AuthorEntity
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                }).ToList();
            }

            // Update prices
            if (request.Prices != null)
            {
                existingCourse.Prices = new PricesEntity
                {
                    Currency = request.Prices.Currency,
                    Price = request.Prices.Price,
                    Discount = request.Prices.Discount,
                };
            }

            // Update content
            if (request.Content != null)
            {
                if (existingCourse.Content == null)
                {
                    existingCourse.Content = new ContentEntity();
                }

                existingCourse.Content.Description = request.Content.Description;
                existingCourse.Content.Includes = request.Content.Includes;
                existingCourse.Content.Learnings = request.Content.Learnings;

                if (request.Content.ProgramDetails != null)
                {
                    {
                        existingCourse.Content.ProgramDetails = request.Content.ProgramDetails.Select(pd => new ProgramDetailItemEntity
                        {
                            Id = pd.Id,
                            ItemTitle = pd.ItemTitle,
                            ItemDescription = pd.ItemDescription,
                        }).ToList();
                    }
                }

                await context.SaveChangesAsync();

                var updatedCourse = CourseFactory.Create(existingCourse);
                request.Content = null;
                return updatedCourse;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    public async Task<bool> DeleteCourseAsync(string id)
    {
        //get access to db
        await using var context = _contextFactory.CreateDbContext();

        //sök efter courseEntitys Id
        var courseEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        //returnera false om entiteten är null
        if (courseEntity == null) return false;

        //ta bort id ur context/db
        context.Courses.Remove(courseEntity);

        //spara ändringar
        await context.SaveChangesAsync();

        //returnera true
        return true;

    }
}
