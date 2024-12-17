using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyScheduler.Helpers;
using DailyScheduler.Models;
using DailyScheduler.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DailyScheduler.ViewModels
{
    [QueryProperty(nameof(EventDetail), "Event")]
    public partial class EditEventViewModel : BaseViewModel
    {
        private readonly JsonDataService _jsonDataService;

        public EditEventViewModel(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
            Users = new ObservableCollection<User>();
            EventDetail = new Event();
        }

        [ObservableProperty]
        ObservableCollection<User> users;

        [ObservableProperty]
        Event eventDetail;

        [ObservableProperty]
        bool dayOnly;

        [ObservableProperty]
        User selectedUser;

        partial void OnEventDetailChanged(Event value)
        {
            DayOnly = EventDetail.DayOnly;
            Debug.WriteLine("EventDetail changed.");
            OnPropertyChanged(nameof(StartTimeVisibility));
            OnPropertyChanged(nameof(EndDateVisibility));
            OnPropertyChanged(nameof(EndTimeVisibility));
        }

        partial void OnDayOnlyChanged(bool value)
        {
            Debug.WriteLine($"DayOnly changed: {value}");
            OnPropertyChanged(nameof(StartTimeVisibility));
            OnPropertyChanged(nameof(EndDateVisibility));
            OnPropertyChanged(nameof(EndTimeVisibility));
        }

        [RelayCommand]
        async Task LoadUsersAsync()
        {
            try
            {
                var users = await UserHelper.LoadUsersAsync(_jsonDataService);
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }


        [RelayCommand]
        void AddParticipant()
        {
            if (SelectedUser != null && !EventDetail.Participants.Contains(SelectedUser))
            {
                EventDetail.Participants.Add(SelectedUser);
                SelectedUser = null;
            }
        }

        [RelayCommand]
        void RemoveParticipant(User user)
        {
            if (user != null && EventDetail.Participants.Contains(user))
            {
                EventDetail.Participants.Remove(user);
            }
        }

        [RelayCommand]
        async Task SaveEventAsync()
        {
            try
            {
                await _jsonDataService.UpdateEventAsync(EventDetail);

                await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                    true,
                    new Dictionary<string, object>
                    {
                { "Event", EventDetail }
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving event: {ex.Message}");
            }
        }


        [RelayCommand]
        async Task CancelAsync()
        {
            if (EventDetail != null)
            {
                await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                    true,
                    new Dictionary<string, object>
                    {
                { "Event", EventDetail }
                    });
            }
        }

        public bool StartTimeVisibility => !DayOnly;
        public bool EndDateVisibility => !DayOnly;
        public bool EndTimeVisibility => !DayOnly;
    }
}
