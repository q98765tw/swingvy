using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using System.Security.Cryptography;
using System.Text;


namespace swingvy.Repositories
{
    public class HomeRepository
    {
        private readonly swingvyContext _swingvyContext;

        public HomeRepository(swingvyContext swingvyContext)
        {
            _swingvyContext = swingvyContext;
        }

        public object GetUserName(int userId)
        {
            var user = _swingvyContext.memberData.FirstOrDefault(a => a.member_id == userId);

            if (user != null)
            {
                return new { Name = user.name };
            }

            return null;
        }

        public worktime GetWorkTimeRecord(int userId, DateTime today)
        {
            return _swingvyContext.worktime
                .FirstOrDefault(wt => wt.member_id == userId && wt.startTime >= today);
        }

        public void UpdateWorkTimeRecordForNoState(int userId)
        {
            var workTimeRecord = _swingvyContext.worktime.FirstOrDefault(n => n.member_id == userId);
            if (workTimeRecord != null)
            {
                workTimeRecord.endTime = null;
                workTimeRecord.startTime = null;
                workTimeRecord.state = (int)WorkState.No;
                _swingvyContext.SaveChanges();
            }
        }

        public List<WorkTimeRecordViewModel> GetWorkTimeRecordsViewModel(int userId)
        {
            return _swingvyContext.worktime
                .Where(wt => wt.member_id == userId)
                .Select(wt => new WorkTimeRecordViewModel
                {
                    Start = wt.startTime.ToString(),
                    State = ((WorkState)wt.state).ToString()
                })
                .ToList();
        }

        public List<RecentActivitiesViewModel> GetRecentActivities(DateTime today, DateTime sevenDaysLater)
        {
            return (from w in _swingvyContext.calendar
                    join m in _swingvyContext.memberData
                    on w.member_id equals m.member_id
                    where w.startTime >= today && w.startTime <= sevenDaysLater
                    select new RecentActivitiesViewModel
                    {
                        MemberName = m.name,
                        mem_Act = w.name,
                        Time = w.startTime
                    }).ToList();
        }

        public void ClockInToWork(int userId)
        {
            var workTimeRecord = _swingvyContext.worktime.FirstOrDefault(w => w.member_id == userId);
            workTimeRecord.startTime = DateTime.Now;
            workTimeRecord.state = (int)WorkState.Ing;
            _swingvyContext.SaveChanges();
        }

        public void ClockOut(int userId)
        {
            var workTimeRecord = _swingvyContext.worktime.FirstOrDefault(w => w.member_id == userId);
            workTimeRecord.endTime = DateTime.Now;
            workTimeRecord.state = (int)WorkState.Yes;
            _swingvyContext.SaveChanges();
        }
    }
}
