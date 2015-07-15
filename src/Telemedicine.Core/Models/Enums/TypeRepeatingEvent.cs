using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Telemedicine.Core.Models.Enums
{
    public enum TypeRepeatingEvent
    {
        [Display(Name = "каждый день")]
        Everyday,
        [Display(Name = "каждый будний день (с понедельника по пятницу)")]
        EveryBusinessDay,
        [Display(Name = "каждый понедельник, среду и пятницу")]
        EveryMondayWednesdayFriday,
        [Display(Name = "Каждый вторник и четверг")]
        EveryTuesdayThursday,
        [Display(Name = "каждую неделю")]
        EveryWeek,
        [Display(Name = "каждый месяц")]
        EveryMonth,
        [Display(Name = "каждый год")]
        EveryYear
    }
}