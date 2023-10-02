using swingvy.Enums;
using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;

namespace swingvy.Repositories
{
    public class MemberDataRepository
    {
        private readonly swingvyContext _context;
        public MemberDataRepository(swingvyContext context) {
            _context = context;
        }
        public void AddMemberData(memberData user)
        {
            _context.memberData.Add(user);
            _context.SaveChanges();
        }
        public memberData FindHead(int type,int position)
        {
            return _context.memberData.FirstOrDefault(m => m.type == (Department)type && m.position == Position.Manager);
        }
    }
}
