using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace OutlookCalendar.Model
{
    public class Appointments : ObservableCollection<Appointment>
    {
        public Appointments()
        {
            Add(new Appointment() { Subject = "Dummy Appointment #1", StartTime = new DateTime(2019, 5, 1, 12, 00, 00), EndTime = new DateTime(2019, 5, 1, 14, 00, 00) });
            Add(new Appointment() { Subject = "Dummy Appointment #2", StartTime = new DateTime(2019, 5, 2, 11, 30, 00), EndTime = new DateTime(2019, 5, 2, 12, 00, 00) });
            Add(new Appointment() { Subject = "Dummy Appointment #3", StartTime = new DateTime(2019, 5, 3, 16, 00, 00), EndTime = new DateTime(2019, 5, 3, 17, 30, 00) });
        }
    }
}
