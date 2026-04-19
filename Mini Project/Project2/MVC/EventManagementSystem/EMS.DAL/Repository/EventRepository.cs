using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EMS.DAL.Data;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL.Repository
{
    public class EventRepository : GenericRepository<EventDetails>, IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDetails>> GetActiveEvents()
        {
            return await _context.Events
                .Where(e => e.Status == "Active")
                .ToListAsync();
        }
    }
}