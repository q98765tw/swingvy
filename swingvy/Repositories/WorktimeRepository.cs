using swingvy.Enums;
using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;

namespace swingvy.Repositories
{
    public class WorktimeRepository
    {
        private readonly swingvyContext _context;
        public WorktimeRepository(swingvyContext context)
        {
            _context = context;
        }

        public void AddWorkTime(worktime worktime)
        {
            _context.worktime.Add(worktime);
            
        }
        public async Task Save() {
            await _context.SaveChangesAsync();
        }
    }
}
