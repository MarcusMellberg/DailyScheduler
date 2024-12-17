using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyScheduler.Models;
using DailyScheduler.Services;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DailyScheduler.ViewModels
{
    [QueryProperty(nameof(EventDetail), "Event")]
    public partial class DetailsViewModel : BaseViewModel
    {
        private readonly JsonDataService _jsonDataService;

        public DetailsViewModel(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
            FormattedEventDates = string.Empty;
        }

        [ObservableProperty]
        Event eventDetail;

        [ObservableProperty]
        string formattedEventDates;

        [ObservableProperty]
        string startTime;

        [ObservableProperty]
        string endTime; 

        partial void OnEventDetailChanged(Event value)
        {
            FormattedEventDates = GetFormattedEventDates(value);
            if (value != null) 
            {
                StartTime = value.StartTime?.ToString(@"hh\:mm");
                EndTime = value.EndTime?.ToString(@"hh\:mm");
            }
        }

        private string GetFormattedEventDates(Event value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var culture = new CultureInfo("sv-SE");
            var textInfo = culture.TextInfo;

            string startDateString = textInfo.ToTitleCase(value.StartDate.ToString("dddd d MMMM", culture));
            string? endDateString = value.EndDate?.ToString("dddd d MMMM", culture);

            if (value.StartDate.Date == value.EndDate?.Date)
            {
                return startDateString;
            }

            return string.IsNullOrEmpty(endDateString) ? startDateString : $"{startDateString} - {endDateString}";
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }


        [RelayCommand]
        async Task ShowDeleteAlertAsync()
        {
            bool isConfirmed = await Shell.Current.DisplayAlert("Bekräfta radering", "Vill du verkligen ta bort detta event?", "Ja", "Nej");
            if (isConfirmed)
            {
                await _jsonDataService.DeleteEventAsync(EventDetail);

                await Shell.Current.GoToAsync("//MainPage");
            }
        }




        [RelayCommand]
        async Task EditEventAsync()
        {
            if (EventDetail == null) return;

            await Shell.Current.GoToAsync($"{nameof(EditEventPage)}",
                true,
                new Dictionary<string, object>
                {
            { "Event", EventDetail }
                });
        }
    }
}
