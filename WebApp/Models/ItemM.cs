using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ItemM
    {

        [Required(ErrorMessage = "Project Name is requried!")]
        [Display(Name = "Project Name")]
        public string? ProjectName {  get; set; }

        [Required(ErrorMessage = "Executive Summary is requried!")]
        [Display(Name = "Executive Summary")]
        public string? ExecutiveSummary { get; set; }

        [Required(ErrorMessage = "Priority is requried!")]
        [Display(Name = "Priority")]
        public string? Priority { get; set; }

        [Required(ErrorMessage = "Project Type is requried!")]
        [Display(Name = "Project Type")]
        public string? ProjectType { get; set; }

        [Required(ErrorMessage = "Strategic Alignment is requried!")]
        [Display(Name = "Strategic Alignment")]
        public int StrategicAlignment { get; set;}

        [Required(ErrorMessage = "Customer Benefit is requried!")]
        [Display(Name = "Customer Benefit")]
        public int CustomerBenefit { get; set; }

        [Required(ErrorMessage = "Commercial Benefit is requried!")]
        [Display(Name = "Customer Benefit")]
        public int CommercialBenefit { get; set; }

        [Required(ErrorMessage = "Technical Benefit is requried!")]
        [Display(Name = "Technical Benefit")]
        public int TechnicalBenefit { get; set; }


        [Required(ErrorMessage = "Test Type is requried!")]
        [Display(Name = "Test Type")]
        public int? TestType { get; set; }


    }
}
