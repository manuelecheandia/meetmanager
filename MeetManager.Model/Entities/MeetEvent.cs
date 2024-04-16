using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManager.Model
{
    public class MeetEvent
    {
        public  int Id { get; set; }
        public int MeetId { get; set; }
        public int EventId { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
