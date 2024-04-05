using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RequestCalendarInfoViewModel
    {

        [Required(ErrorMessage = "Location is requried!")]
        [Display(Name = "Location")]
        public string? Location { get; set; }


        [Required(ErrorMessage = "Month is requried!")]
        [Display(Name = "Month")]
        public string? Month { get; set; }

    }
}
