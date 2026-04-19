using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EMS.DAL.Models;

namespace EMS.DAL.Repository
{
    public interface IEventRepository : IGenericRepository<EventDetails>
    {
        Task<IEnumerable<EventDetails>> GetActiveEvents();
    }
}