using CourseProvider.Infrastructure.Data.Entities;
using CourseProvider.Infrastructure.Models;

namespace CourseProvider.Infrastructure.Factories;

//Med denna factory, omvanldar vi requesten till en entitet
public static class CourseFactory
{
    //Omvandlar/populerar en CourseCreateRequest -> CourseEntity
    public static CourseEntity Create(CourseCreateRequest request)
    {
        return new CourseEntity
        {
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            ImageAuthor = request.ImageAuthor,
            IsBestSeller = request.IsBestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Ingress = request.Ingress,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            LikesInProcent = request.LikesInProcent,
            Likes = request.Likes,
            Hours = request.Hours,
            //Loopar igenom och lägger i en lista
            Authors = request.Authors?.Select(a => new AuthorEntity
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
            }).ToList(),
            Prices = request.Prices == null ? null : new PricesEntity
            {
                Currency = request.Prices.Currency,
                Price = request.Prices.Price,
                Discount = request.Prices.Discount,
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                Learnings = request.Content.Learnings,
                ProgramDetails = request.Content.ProgramDetails?.Select(pd => new ProgramDetailItemEntity
                {
                    Id = pd.Id,
                    ItemTitle = pd.ItemTitle,
                    ItemDescription = pd.ItemDescription,
                }).ToList()
            }
        };
    }
    //CourseUpdateRequest -> CourseEntity
    public static CourseEntity Create(CourseUpdateRequest request)
    {
        return new CourseEntity
        {
            Id = request.Id,
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            ImageAuthor = request.ImageAuthor,
            IsBestSeller = request.IsBestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Ingress = request.Ingress,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            LikesInProcent = request.LikesInProcent,
            Likes = request.Likes,
            Hours = request.Hours,
            Authors = request.Authors?.Select(a => new AuthorEntity
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
            }).ToList(),
            Prices = request.Prices == null ? null : new PricesEntity
            {
                Currency = request.Prices.Currency,
                Price = request.Prices.Price,
                Discount = request.Prices.Discount,
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                Learnings = request.Content.Learnings,
                ProgramDetails = request.Content.ProgramDetails?.Select(pd => new ProgramDetailItemEntity
                {
                    Id = pd.Id,
                    ItemTitle = pd.ItemTitle,
                    ItemDescription = pd.ItemDescription,
                }).ToList()
            }
        };
    }


    //CourseEntity -> Course
    public static Course Create(CourseEntity entity)
    {
        return new Course
        {
            Id = entity.Id,
            ImageUri = entity.ImageUri,
            ImageHeaderUri = entity.ImageHeaderUri,
            ImageAuthor = entity.ImageAuthor,
            IsBestSeller = entity.IsBestSeller,
            IsDigital = entity.IsDigital,
            Categories = entity.Categories,
            Title = entity.Title,
            Ingress = entity.Ingress,
            StarRating = entity.StarRating,
            Reviews = entity.Reviews,
            LikesInProcent = entity.LikesInProcent,
            Likes = entity.Likes,
            Hours = entity.Hours,
            Authors = entity.Authors?.Select(a => new Author
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
            }).ToList(),
            Prices = entity.Prices == null ? null : new Prices
            {
                Currency = entity.Prices.Currency,
                Price = entity.Prices.Price,
                Discount = entity.Prices.Discount,
            },
            Content = entity.Content == null ? null : new Content
            {
                Description = entity.Content.Description,
                Includes = entity.Content.Includes,
                Learnings = entity.Content.Learnings,
                ProgramDetails = entity.Content.ProgramDetails?.Select(pd => new ProgramDetailItem
                {
                    Id = pd.Id,
                    ItemTitle = pd.ItemTitle,
                    ItemDescription = pd.ItemDescription,
                }).ToList()
            }
        };
    }
}
