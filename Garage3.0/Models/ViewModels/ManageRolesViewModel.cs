using Microsoft.AspNetCore.Identity;

namespace Garage3._0.Models.ViewModels
{
    internal class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> CurrentRoles { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
    }
}