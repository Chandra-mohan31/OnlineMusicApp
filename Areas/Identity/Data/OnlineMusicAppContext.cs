using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMusicApp.Areas.Identity.Data;
using OnlineMusicApp.Models;

namespace OnlineMusicApp.Data;

public class OnlineMusicAppContext : IdentityDbContext<OnlineMusicAppUser>
{
    public OnlineMusicAppContext(DbContextOptions<OnlineMusicAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<OnlineMusicApp.Models.MusicModel>? MusicModel { get; set; }
    public DbSet<OnlineMusicApp.Models.UserAlbum>? userAlbum { get; set; }

}
