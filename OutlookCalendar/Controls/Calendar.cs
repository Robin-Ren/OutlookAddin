﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using OutlookCalendar.Model;

namespace OutlookCalendar.Controls
{
    public class Calendar : Control
    {
        static Calendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));

            CommandManager.RegisterClassCommandBinding(typeof(Calendar), new CommandBinding(NextDay, new ExecutedRoutedEventHandler(OnExecutedNextDay), new CanExecuteRoutedEventHandler(OnCanExecuteNextDay)));
            CommandManager.RegisterClassCommandBinding(typeof(Calendar), new CommandBinding(PreviousDay, new ExecutedRoutedEventHandler(OnExecutedPreviousDay), new CanExecuteRoutedEventHandler(OnCanExecutePreviousDay)));
        }

        #region AddAppointment

        public static readonly RoutedEvent AddAppointmentEvent =
            CalendarTimeslotItem.AddAppointmentEvent.AddOwner(typeof(CalendarDay));

        public event RoutedEventHandler AddAppointment
        {
            add
            {
                AddHandler(AddAppointmentEvent, value);
            }
            remove
            {
                RemoveHandler(AddAppointmentEvent, value);
            }
        }

        #endregion

        #region Appointments

        public static readonly DependencyProperty AppointmentsProperty =
            DependencyProperty.Register("Appointments", typeof(IEnumerable<Appointment>), typeof(Calendar),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(Calendar.OnAppointmentsChanged)));

        public IEnumerable<Appointment> Appointments
        {
            get { return (IEnumerable<Appointment>)GetValue(AppointmentsProperty); }
            set { SetValue(AppointmentsProperty, value); }
        }

        private static void OnAppointmentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Calendar)d).OnAppointmentsChanged(e);
        }

        protected virtual void OnAppointmentsChanged(DependencyPropertyChangedEventArgs e)
        {
            FilterAppointments();
        }

        #endregion

        #region CurrentDate

        /// <summary>
        /// CurrentDate Dependency Property
        /// </summary>
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(Calendar),
                new FrameworkPropertyMetadata((DateTime)DateTime.Now,
                    new PropertyChangedCallback(OnCurrentDateChanged)));

        /// <summary>
        /// Gets or sets the CurrentDate property.  This dependency property 
        /// indicates ....
        /// </summary>
        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        /// <summary>
        /// Handles changes to the CurrentDate property.
        /// </summary>
        private static void OnCurrentDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Calendar)d).OnCurrentDateChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CurrentDate property.
        /// </summary>
        protected virtual void OnCurrentDateChanged(DependencyPropertyChangedEventArgs e)
        {
            FilterAppointments();
        }

        #endregion

        #region Current Week
        /// <summary>
        /// CurrentWeek Dependency Property
        /// </summary>
        public static readonly DependencyProperty CurrentWeekProperty =
            DependencyProperty.Register("CurrentWeek", typeof(CurrentWeek), typeof(Calendar),
                new FrameworkPropertyMetadata(new CurrentWeek(),
                    new PropertyChangedCallback(OnCurrentWeekChanged)));

        /// <summary>
        /// Gets or sets the CurrentWeek property.  This dependency property 
        /// indicates ....
        /// </summary>
        public CurrentWeek CurrentWeek
        {
            get { return (CurrentWeek)GetValue(CurrentWeekProperty); }
            set { SetValue(CurrentWeekProperty, value); }
        }

        /// <summary>
        /// Handles changes to the CurrentWeek property.
        /// </summary>
        private static void OnCurrentWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Calendar)d).OnCurrentWeekChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CurrentWeek property.
        /// </summary>
        protected virtual void OnCurrentWeekChanged(DependencyPropertyChangedEventArgs e)
        {
            FilterAppointments();
        }
        #endregion

        private void FilterAppointments()
        {
            CalendarDay day1 = this.GetTemplateChild("day1") as CalendarDay;
            day1.ItemsSource = Appointments.ByDate(CurrentWeek.Day1);
            day1.Day = CurrentWeek.Day1;
            TextBlock dayHeader1 = this.GetTemplateChild("dayHeader1") as TextBlock;
            dayHeader1.Text = CurrentDate.ToShortDateString();

            CalendarDay day2 = this.GetTemplateChild("day2") as CalendarDay;
            day2.ItemsSource = Appointments.ByDate(CurrentWeek.Day2);
            day2.Day = CurrentWeek.Day2;
            TextBlock dayHeader2 = this.GetTemplateChild("dayHeader2") as TextBlock;
            dayHeader2.Text = CurrentWeek.Day2.ToShortDateString();

            CalendarDay day3 = this.GetTemplateChild("day3") as CalendarDay;
            day3.ItemsSource = Appointments.ByDate(CurrentWeek.Day3);
            day3.Day = CurrentWeek.Day3;
            TextBlock dayHeader3 = this.GetTemplateChild("dayHeader3") as TextBlock;
            dayHeader3.Text = CurrentWeek.Day3.ToShortDateString();

            CalendarDay day4 = this.GetTemplateChild("day4") as CalendarDay;
            day4.ItemsSource = Appointments.ByDate(CurrentWeek.Day4);
            day4.Day = CurrentWeek.Day4;
            TextBlock dayHeader4 = this.GetTemplateChild("dayHeader4") as TextBlock;
            dayHeader4.Text = CurrentWeek.Day4.ToShortDateString();

            CalendarDay day5 = this.GetTemplateChild("day5") as CalendarDay;
            day5.ItemsSource = Appointments.ByDate(CurrentWeek.Day5);
            day5.Day = CurrentWeek.Day5;
            TextBlock dayHeader5 = this.GetTemplateChild("dayHeader5") as TextBlock;
            dayHeader5.Text = CurrentWeek.Day5.ToShortDateString();

            CalendarDay day6 = this.GetTemplateChild("day6") as CalendarDay;
            day6.ItemsSource = Appointments.ByDate(CurrentWeek.Day6);
            day6.Day = CurrentWeek.Day6;
            TextBlock dayHeader6 = this.GetTemplateChild("dayHeader6") as TextBlock;
            dayHeader6.Text = CurrentWeek.Day6.ToShortDateString();

            CalendarDay day7 = this.GetTemplateChild("day7") as CalendarDay;
            day7.ItemsSource = Appointments.ByDate(CurrentWeek.Day7);
            day7.Day = CurrentWeek.Day7;
            TextBlock dayHeader7 = this.GetTemplateChild("dayHeader7") as TextBlock;
            dayHeader7.Text = CurrentWeek.Day7.ToShortDateString();
        }

        #region NextDay/PreviousDay

        public static readonly RoutedCommand NextDay = new RoutedCommand("NextDay", typeof(Calendar));
        public static readonly RoutedCommand PreviousDay = new RoutedCommand("PreviousDay", typeof(Calendar));

        private static void OnCanExecuteNextDay(object sender, CanExecuteRoutedEventArgs e)
        {
            ((Calendar)sender).OnCanExecuteNextDay(e);
        }

        private static void OnExecutedNextDay(object sender, ExecutedRoutedEventArgs e)
        {
            ((Calendar)sender).OnExecutedNextDay(e);
        }

        protected virtual void OnCanExecuteNextDay(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = false;
        }

        protected virtual void OnExecutedNextDay(ExecutedRoutedEventArgs e)
        {
            ClearCheckedCalendarTimeslots();

            CurrentDate += TimeSpan.FromDays(7);
            CurrentWeek = new CurrentWeek(CurrentDate);
            e.Handled = true;
        }

        private static void OnCanExecutePreviousDay(object sender, CanExecuteRoutedEventArgs e)
        {
            ((Calendar)sender).OnCanExecutePreviousDay(e);
        }

        private static void OnExecutedPreviousDay(object sender, ExecutedRoutedEventArgs e)
        {
            ((Calendar)sender).OnExecutedPreviousDay(e);
        }

        protected virtual void OnCanExecutePreviousDay(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = false;
        }

        protected virtual void OnExecutedPreviousDay(ExecutedRoutedEventArgs e)
        {
            ClearCheckedCalendarTimeslots();

            CurrentDate -= TimeSpan.FromDays(7);
            CurrentWeek = new CurrentWeek(CurrentDate);
            e.Handled = true;
        }

        private void ClearCheckedCalendarTimeslots()
        {
            var checkedTimeslots = DependencyObjectHelper
                .FindVisualChildren<CalendarTimeslotItem>(this)
                .Where(x => x.IsChecked == true)
                .OrderBy(x => x.TimeslotDate)
                .ThenBy(x => x.TimeslotStart)
                .ToList();

            if (checkedTimeslots != null)
            {
                foreach (var timeslot in checkedTimeslots)
                {
                    timeslot.IsChecked = false;
                }
            }
        }
        #endregion
    }
}
