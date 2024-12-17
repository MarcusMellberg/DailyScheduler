namespace DailyScheduler
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(EditEventPage), typeof(EditEventPage));
            Routing.RegisterRoute(nameof(UpcomingEventsPage), typeof(UpcomingEventsPage));
            Routing.RegisterRoute(nameof(PastEventsPage), typeof(PastEventsPage));
        }
    }
}

