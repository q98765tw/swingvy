using swingvy.Enums;

namespace swingvy.Models
{
    public class MemberCenterViewModel
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public Department Depart { get; set; }
        public Position Position { get; set; }
        public string Photo { get; set; }
    }
}
