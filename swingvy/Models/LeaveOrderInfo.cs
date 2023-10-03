namespace swingvy.Models
{
    public class leaveOrderInfo
    {
        public swingvy.Enums.LeaveType type { get; set; }
        public System.DateTime? startTime { get; set; }
        public System.DateTime? endTime { get; set; }
        public swingvy.Enums.LeaveState state { get; set; }
        public string? head_name { get; set; }
    }
}
