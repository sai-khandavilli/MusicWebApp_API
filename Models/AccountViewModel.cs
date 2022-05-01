using Microsoft.AspNetCore.Identity;

namespace MusicWebApp.Models
{
    public class AccountViewModel
    {
        public AccountViewModel(IdentityUser user)
        {
            UserName = user?.UserName ?? String.Empty;
            EmailId = user?.Email ?? String.Empty;
        }

        public string UserName { get; set; }

        public string EmailId { get; set; }
    }
}