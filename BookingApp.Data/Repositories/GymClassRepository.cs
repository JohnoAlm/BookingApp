using BookingApp.Core.Entitis;
using BookingApp.Core.Repositories;
using BookingApp.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext db;

        public GymClassRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<GymClass>> GetAsync()
        {
            return await db.GymClasses.ToListAsync();
        } 
        
        public async Task<IEnumerable<GymClass>> GetWithAttendinAsync()
        {
            return await db.GymClasses.Include(g => g.AttendingMembers).ToListAsync();
        }   
        
        public async Task<IEnumerable<GymClass>> GetHistoryAsync()
        {
            return await db.GymClasses
                .Include(g => g.AttendingMembers)
                .IgnoreQueryFilters()
                .Where(g => g.StartDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<GymClass?> GetAsync(int? id)
        {
           return  await db.GymClasses.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Add(GymClass gymclass)
        {
            db.Add(gymclass);
        }
    }
}
