using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobileService.Entities.Models;

namespace MobileService.DataAccess
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CollectionModel> Collections { get; set; }
        public DbSet<FlashcardModel> Flashcards { get; set; }
        public DbSet<FlashcardProgressModel> FlashcardProgresses { get; set; }
        public DbSet<StatsUserModel> StatsUserModels { get; set; }
    }
}
