using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyScheduler.Models;
using DailyScheduler.Services;
using DailyScheduler.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DailyScheduler.ViewModels
{
    public partial class PastEventsViewModel : BaseViewModel
    {
        private readonly JsonDataService _jsonDataService;

        public PastEventsViewModel(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
        }

        [ObservableProperty]
        string key;

        [ObservableProperty]
        ObservableCollection<GroupingHelper<string, Event>> groupedEvents = new ObservableCollection<GroupingHelper<string, Event>>();

        [RelayCommand]
        public async Task LoadEventsAsync()
        {
            try
            {
                var events = await _jsonDataService.LoadEventsAsync();
                if (events != null)
                {
                    GroupedEvents.Clear();

                    var groupedEvents = from evt in events
                                        where evt.StartDate.Date < DateTime.Today
                                        orderby evt.StartDate
                                        group evt by evt.StartDate.ToString("dddd, d MMMM", new System.Globalization.CultureInfo("sv-SE")) into eventGroup
                                        select new GroupingHelper<string, Event>(eventGroup.Key, eventGroup);

                    foreach (var group in groupedEvents)
                    {
                        GroupedEvents.Add(group);
                    }

                    if (GroupedEvents.Any())
                    {
                        Key = GroupedEvents.First().Key;
                    }

                    System.Diagnostics.Debug.WriteLine($"Events loaded successfully. Total events: {events.Count}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to load events");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading events: {ex.Message}");
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
    }
}
