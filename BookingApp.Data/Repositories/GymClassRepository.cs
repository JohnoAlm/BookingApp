using BookingApp.Core.Entitis;
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

        public Task<List<GymClass>> GetAsync()
        {
            return db.GymClasses.ToListAsync();
        }
    }
}
