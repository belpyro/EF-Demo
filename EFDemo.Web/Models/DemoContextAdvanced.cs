using Microsoft.EntityFrameworkCore;

namespace EFDemo.Web.Models;

public partial class EfDemoContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>().Property<DateTime>("UpdatedOn");
        modelBuilder.Entity<Track>().Property<bool>("IsDeleted");
        modelBuilder.Entity<Track>().HasQueryFilter(t => !EF.Property<bool>(t,"IsDeleted"));

        modelBuilder.Entity<Artist>().HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
    }
}