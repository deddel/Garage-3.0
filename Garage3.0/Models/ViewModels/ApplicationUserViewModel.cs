using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "First Name ")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        public string Roles{ get; set; }

    }
}
