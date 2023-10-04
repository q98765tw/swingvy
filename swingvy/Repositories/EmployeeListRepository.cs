using swingvy.Enums;
using swingvy.Models;
using System.Reflection;


namespace swingvy.Repositories
{
    public class EmployeeListRepository
    {
        private readonly swingvyContext _swingvyContext;

        public EmployeeListRepository(swingvyContext swingvyContext)
        {
            _swingvyContext = swingvyContext;
        }

        public List<EmployeeViewModel> GetEmployeeData()
        {
            return (from w in _swingvyContext.memberData
                    select new EmployeeViewModel
                    {
                        Name = w.name,
                        Depart = w.type,
                        Ption = w.position,
                        Mail = w.email,
                        Phone = w.phone
                    }).ToList();
        }
    }
}
