﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using swingvy.Enums;
namespace swingvy.Models
{
    public partial class leaveOrder
    {
        public int leaveOrder_id { get; set; }
        public int member_id { get; set; }
        public LeaveType type { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? applyTime { get; set; }
        public string reason { get; set; }
        public LeaveState state { get; set; }
        public int head { get; set; }

        public virtual member member { get; set; }
    }
}