using Enitities.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Business.concrete
{
    public class ScheduleManager
    {
        public ObservableCollection<Meeting> Events { get; set; }

        public ScheduleManager()
        {
            Events = new ObservableCollection<Meeting>();
        }

        public void Add(Meeting meeting)
        {
            Events.Add(meeting);
        }
    }
}
