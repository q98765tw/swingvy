using swingvy.Models;

namespace swingvy.Repositories
{
    public class CalendarPageRepository
    {
        private readonly swingvyContext _swingvyContext;

        public CalendarPageRepository(swingvyContext swingvyContext)
        {
            _swingvyContext = swingvyContext;
        }

        public List<CalendarViewModel> GetCalendarData()
        {
            return (from w in _swingvyContext.calendar
                    join m in _swingvyContext.memberData on w.member_id equals m.member_id
                    select new CalendarViewModel
                    {
                        MemberName = m.name,
                        MemAct = w.name,
                        StTime = Convert.ToDateTime(w.startTime).ToString("yyyy-MM-dd"),
                        EdTime = Convert.ToDateTime(w.endTime).ToString("yyyy-MM-dd"),
                    }).ToList();
        }
    }
}
