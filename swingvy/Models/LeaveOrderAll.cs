using swingvy.Enums;

namespace swingvy.Models
{
    public class LeaveOrderAll
    {
        public int leaveOrder_id { get; set; }
        public string name { get; set; }
        public DateTime? applyTime { get; set; }
        public LeaveType leaveType { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
    }

}
