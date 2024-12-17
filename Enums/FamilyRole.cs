using System.ComponentModel;

namespace DailyScheduler.Enums
{
    public enum FamilyRole
    {
        [Description("Pappa")]
        Dad,

        [Description("Mamma")]
        Mom,

        [Description("Barn")]
        Child,

        [Description("Far- och morförälder")]
        Grandparent,

        [Description("Övrig")]
        Other
    }
}
