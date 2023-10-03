using swingvy.Enums;

namespace swingvy.Models
{
    public class LeaveOrderPerson
    {
        public int leaveOrder_id { get; set; }
        public int member_id { get; set; }
        public string? name { get; set; }
        public LeaveType type { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? applyTime { get; set; }
        public string? reason { get; set; }
    }
}
