using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DailyScheduler.Services;
using DailyScheduler.Models;
using System.Diagnostics;
using System.Globalization;
using DailyScheduler.Helpers;

namespace DailyScheduler.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly JsonDataService _jsonDataService;

        public MainViewModel(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
            TodaysDate = GetFormattedTodayDate();
        }

        [ObservableProperty]
        ObservableCollection<Event> todaysEvents = new ObservableCollection<Event>();

        [ObservableProperty]
        ObservableCollection<Event> thisWeeksEvents = new ObservableCollection<Event>();

        [ObservableProperty]
        ObservableCollection<User> users = new ObservableCollection<User>();

        [ObservableProperty]
        string todaysDate = string.Empty;

        [RelayCommand]
        private async Task LoadEventsAsync()
        {
            try
            {
                var events = await _jsonDataService.LoadEventsAsync();
                if (events != null)
                {
                    TodaysEvents.Clear();
                    ThisWeeksEvents.Clear();

                    foreach (var e in events)
                    {
                        if (e.StartDate.Date == DateTime.Today)
                        {
                            TodaysEvents.Add(e);
                        }
                        else if (e.StartDate.Date > DateTime.Today && e.StartDate.Date <= DateTime.Today.AddDays(7))
                        {
                            ThisWeeksEvents.Add(e);
                        }
                    }

                    var sortedEvents = ThisWeeksEvents.OrderBy(e => e.StartDate).ToList();

                    ThisWeeksEvents.Clear();
                    foreach (var e in sortedEvents)
                    {
                        ThisWeeksEvents.Add(e);
                    }

                    Debug.WriteLine($"Events loaded successfully. Total events: {events.Count}");
                }
                else
                {
                    Debug.WriteLine("Failed to load events");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading events: {ex.Message}");
            }
        }



        [RelayCommand]
        async Task LoadUsersAsync()
        {
            if (Users.Any())
            {
                return;
            }

            try
            {
                var users = await UserHelper.LoadUsersAsync(_jsonDataService);
                if (users != null)
                {
                    Users.Clear();
                    foreach (var user in users)
                    {
                        Users.Add(user);
                    }
                    Debug.WriteLine($"Users loaded successfully. Total users: {users.Count}");
                }
                else
                {
                    Debug.WriteLine("Failed to load users");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        [RelayCommand]
        async Task GotoDetail(Event eventDetail)
        {
            if (eventDetail == null) return;

            await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                true,
                new Dictionary<string, object>
                {
                    { "Event", eventDetail }
                });
        }

        [RelayCommand]
        private async Task NavigateToAddEventAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage));
        }

        private string GetFormattedTodayDate()
        {
            var today = DateTime.Now;
            var culture = new CultureInfo("sv-SE");
            return today.ToString("dddd d MMMM", culture);
        }
    }
}
