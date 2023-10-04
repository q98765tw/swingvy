using swingvy.Models;

namespace swingvy.Services
{
    public class EmployeeListService
    {
        private readonly swingvyContext _swingvyContext;

        public EmployeeListService(swingvyContext swingvyContext)
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
