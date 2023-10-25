using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;
namespace swingvy.Repositories
{
    public interface ICalendarRepository
    {
        void AddCalendar(calendar calendar);
        // 其他方法
        Task Save();
    }
    public class CalendarRepository: ICalendarRepository
    {
        private readonly swingvyContext _context;
        public CalendarRepository(swingvyContext context)
        {
            _context = context;
        }
        public void AddCalendar(calendar calendar)
        {
            _context.calendar.Add(calendar);
        }
        public async Task Save() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
