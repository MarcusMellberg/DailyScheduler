using System;
using System.ComponentModel;
using System.Reflection;
using DailyScheduler.Enums;
using System.ComponentModel.DataAnnotations;

namespace DailyScheduler.Models
{
    public class User : BaseModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        [Required]
        public FamilyRole FamilyRole { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();

        public string FullNameWithRole => $"{FirstName} {LastName} ({FamilyRole.GetDescription()})";

    }
}

