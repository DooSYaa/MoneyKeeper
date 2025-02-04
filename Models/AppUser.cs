using Microsoft.AspNetCore.Identity;

namespace MoneyKeeper.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Money> Money { get; set; }
    }
}
