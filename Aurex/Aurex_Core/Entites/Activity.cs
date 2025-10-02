using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Entites
{
    public class Activity
    {

        public int Id { get; set; }
        public ActivityType Type { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
    public enum ActivityType
    {
        Call,
        Visit,
        Meeting
    }
}

