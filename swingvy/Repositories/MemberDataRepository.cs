﻿using swingvy.Enums;
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
        }
        public memberData? GetUserById(int id)
        {
            return _context.memberData.FirstOrDefault(m => m.member_id == id);
        }
        public memberData? FindHead(Department type,Position position)
        {
            return _context.memberData.FirstOrDefault(m => m.type == type && m.position == position);
        }
        public void save() 
        {
            _context.SaveChanges();
        }
    }
}
