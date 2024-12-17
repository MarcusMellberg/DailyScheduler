using DailyScheduler.Models;
using DailyScheduler.Services;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DailyScheduler.Helpers;
using System.Threading.Tasks;
using System;

namespace DailyScheduler.ViewModels
{
    public partial class AddEventViewModel : BaseViewModel
    {
        private readonly JsonDataService _jsonDataService;

        public AddEventViewModel(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
            Users = new ObservableCollection<User>();
            EventDetail = new Event()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            DayOnly = EventDetail.DayOnly;
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
        private async Task SaveEventAsync()
        {
            try
            {
                CleanEventForDayOnly();

                await _jsonDataService.SaveEventAsync(EventDetail);

                Debug.WriteLine("Event saved successfully!");
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving event: {ex.Message}");
            }
        }

        private void CleanEventForDayOnly()
        {
            if (DayOnly)
            {
                EventDetail.EndDate = null;
                EventDetail.StartTime = null;
                EventDetail.EndTime = null;
            }
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            // Navigera direkt till MainPage
            await Shell.Current.GoToAsync("//MainPage");
        }

        public bool StartTimeVisibility => !DayOnly;
        public bool EndDateVisibility => !DayOnly;
        public bool EndTimeVisibility => !DayOnly;
    }
}
