using BulletinBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.DAL
{
    public class BulletinBoardDbContext : DbContext
    {
        public BulletinBoardDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }

    }
}
