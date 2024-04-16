using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFrontEnd
{
    internal class MeetEventsVM
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Discipline { get; set; }
        public string? Division { get; set; }
        public string? Gender { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
