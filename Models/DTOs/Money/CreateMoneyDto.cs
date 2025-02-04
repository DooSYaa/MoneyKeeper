using MoneyKeeper.Models.Enum;

namespace MoneyKeeper.Models.DTOs.Money
{
    public class CreateMoneyDto
    {
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public MoneyCategory MoneyCategory { get; set; }
        public DateTime Date { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
