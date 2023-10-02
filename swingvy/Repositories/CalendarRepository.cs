using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;
namespace swingvy.Repositories
{
    public class CalendarRepository
    {
        private readonly swingvyContext _context;
        public CalendarRepository(swingvyContext context)
        {
            _context = context;
        }
        public void AddCalendar(calendar calendar)
        {
            _context.calendar.Add(calendar);
            _context.SaveChanges();
        }
    }
}
