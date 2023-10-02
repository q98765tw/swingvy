using swingvy.Models;
using System.Diagnostics.Metrics;
using System.Linq;

namespace swingvy.Repositories 
{ 
    public class MemberRepository
    {
        private readonly swingvyContext _context;

        public MemberRepository(swingvyContext context)
        {
            _context = context;
        }

        public member GetUserByAccount(string account)
        {
            return _context.member.FirstOrDefault(m => m.account == account);
        }
        public member GetUserByAccountPassword(string account,string password)
        {
            return _context.member.FirstOrDefault(m => m.account == account && m.password == password);
        }
        public void AddUser(member user)
        {
            _context.member.Add(user);
            _context.SaveChanges();
        }

    // 其他與數據存取相關的方法
    }
}
