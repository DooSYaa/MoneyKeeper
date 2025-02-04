using MoneyKeeper.Models.Enum;

namespace MoneyKeeper.Models
{
    public class Money
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public MoneyCategory MoneyCategory { get; set; }
        public DateTime Date { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
