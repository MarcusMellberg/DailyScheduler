using DailyScheduler.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DailyScheduler.Models
{
    public class Event : BaseModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool DayOnly { get; set; }
        public ObservableCollection<User> Participants { get; set; } = new ObservableCollection<User>();

        public DateTime CombinedStartDateTime => DayOnly ? StartDate : StartDate.Add(StartTime ?? TimeSpan.Zero);
        public DateTime? CombinedEndDateTime => EndDate?.Add(EndTime ?? TimeSpan.Zero) ?? (EndDate ?? StartDate);

        public string StartFormattedTime => StartTime?.ToString(@"hh\:mm") ?? string.Empty;
        public string EndFormattedTime => EndTime?.ToString(@"hh\:mm") ?? string.Empty;

        public string FormattedEventDates => EndDate == null || StartDate.Date == EndDate?.Date ?
                                             StartDate.ToString("dd/MM") :
                                             $"{StartDate:dd/MM} - {EndDate:dd/MM}";

    }
}
