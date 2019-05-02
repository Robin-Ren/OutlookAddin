using System;
using System.Windows;
using OutlookCalendar.Model;

namespace OutlookCalendar
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Appointments appointments = new Appointments();

        public Window1()
        {
            InitializeComponent();

            DataContext = appointments;
        }

        private void Calendar_AddAppointment(object sender, RoutedEventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Subject = "Subject?";
            appointment.StartTime = new DateTime(2008, 10, 22, 16, 00, 00);
            appointment.EndTime = new DateTime(2008, 10, 22, 17, 00, 00);

            AddAppointmentWindow aaw = new AddAppointmentWindow();
            aaw.DataContext = appointment;
            aaw.ShowDialog();

            appointments.Add(appointment);
        }
    }
}
