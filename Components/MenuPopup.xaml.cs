using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace DailyScheduler.Components
{
    public partial class MenuPopup : Popup
    {
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToAddEventCommand { get; }
        public ICommand NavigateToUpcomingEventsCommand { get; }
        public ICommand NavigateToPastEventsCommand { get; }

        public MenuPopup()
        {
            InitializeComponent();
            NavigateToHomeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//MainPage");
                Close();
            });
            NavigateToAddEventCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AddEventPage));
                Close();
            });
            NavigateToUpcomingEventsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(UpcomingEventsPage));
                Close();
            });
            NavigateToPastEventsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PastEventsPage));
                Close();
            });
            BindingContext = this;
        }
    }
}
