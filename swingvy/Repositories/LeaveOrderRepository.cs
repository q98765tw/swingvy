using swingvy.Models;

namespace swingvy.Repositories
{
    public class LeaveOrderRepository
    {
        private readonly swingvyContext _context;
        public LeaveOrderRepository(swingvyContext context) {
            _context = context;
        }
        public List<leaveOrderInfo> GetLeaveOrderData(int memberId)
        {
            var query = from leaveOrder in _context.leaveOrder
                        join md1 in _context.memberData on leaveOrder.member_id equals md1.member_id
                        join md2 in _context.memberData on md1.head equals md2.member_id
                        where leaveOrder.member_id == memberId
                        orderby leaveOrder.startTime ascending
                        select new leaveOrderInfo
                        {
                            type = leaveOrder.type,
                            startTime = leaveOrder.startTime,
                            endTime = leaveOrder.endTime,
                            state = leaveOrder.state,
                            head_name = md2.name
                        };

            return query.ToList();
        }
        public async Task AddLeaveOrder(leaveOrder leaveOrder)
        {
            _context.leaveOrder.Add(leaveOrder);
            await _context.SaveChangesAsync();
        }
    }
}
