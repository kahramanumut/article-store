using Domain.Review;
using Microsoft.EntityFrameworkCore;

public class ReviewDbContext : DbContext
{
    //Migration and DB update
    //dotnet ef migrations add MigrationName -s ../Review.WebApi/ -o EFCore/Migrations --namespace ReviewMig
    //dotnet ef database update -s ../Review.WebApi/

    public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
    {

    }

    public DbSet<Review> Reviews { get; set; }

}
