using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;
namespace swingvy.Repositories
{
    public interface ICalendarRepository
    {
        void AddCalendar(calendar calendar);
        // 其他方法
    }
    public class CalendarRepository
    {
        private readonly swingvyContext _context;
        public CalendarRepository(swingvyContext context)
        {
            _context = context;
        }
        public async Task AddCalendar(calendar calendar)
        {
            _context.calendar.Add(calendar);
            await _context.SaveChangesAsync();
        }
    }
}
