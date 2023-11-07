using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class EfcRedditContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<RedditPost> RedditPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Reddit.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
}