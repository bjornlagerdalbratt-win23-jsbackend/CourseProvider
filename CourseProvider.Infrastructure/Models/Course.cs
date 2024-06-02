namespace CourseProvider.Infrastructure.Models;

public class Course
{
    public string Id { get; set; } = null!;
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public string? ImageAuthor { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsDigital { get; set; }
    public string[]? Categories { get; set; }
    public string? Title { get; set; }
    public string? Ingress { get; set; }
    public decimal StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? LikesInProcent { get; set; }
    public string? Likes { get; set; }
    public string? Hours { get; set; }
    public virtual List<Author>? Authors { get; set; }
    public virtual Prices? Prices { get; set; }
    public virtual Content? Content { get; set; }

}

public class Author
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
public class Prices
{
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}

public class Content
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public string[]? Learnings { get; set; }
    public virtual List<ProgramDetailItem>? ProgramDetails { get; set; }
}

public class ProgramDetailItem
{
    public int Id { get; set; }
    public string? ItemTitle { get; set; }
    public string? ItemDescription { get; set; }
}
