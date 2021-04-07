using System;
using System.Collections.Generic;
using System.Text;

namespace Enitities.Concrete
{
    public class Meeting
    {
        public string Subject { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Status { get; set; }
    }
}
