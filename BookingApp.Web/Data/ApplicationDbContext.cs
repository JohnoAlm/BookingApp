using BookingApp.Core.Entitis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<GymClass> GymClasses => Set<GymClass>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}