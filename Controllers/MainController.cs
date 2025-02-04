using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using MoneyKeeper.Data;
using MoneyKeeper.Migrations;
using MoneyKeeper.Models;
using MoneyKeeper.Models.DTOs.Account;
using MoneyKeeper.Models.DTOs.Money;
using MoneyKeeper.Models.Enum;
using Org.BouncyCastle.Utilities.Collections;

namespace MoneyKeeper.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        public MainController(ApplicationDbContext context, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateMoney()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMoney(Money money)    
        {
            
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            var newMoney = new Money
            {
                Id = Guid.NewGuid(),
                Price = money.Price,
                Description = string.Empty,
                MoneyCategory = money.MoneyCategory,
                Date = DateTime.Now,
                AppUserId = user.Id,
                AppUser = user
            };

            _context.Money.Add(newMoney);
            _context.SaveChanges();
            return RedirectToAction("Index", "Main");
        }
        [HttpGet]
        public IActionResult EditMoney(string id)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            var money = _context.Money.FirstOrDefault(m => m.Id.ToString() == id && m.AppUserId == user.Id);

            return View(money);
        }
        [HttpPost]
        public IActionResult EditMoney(string id, EditMoneyDto money)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            var moneyToEdit = _context.Money.FirstOrDefault(m => m.Id.ToString() == id && m.AppUserId == user.Id);

            if(moneyToEdit == null) return View(moneyToEdit);

            moneyToEdit.Price = money.Price;
            moneyToEdit.Description = string.Empty;
            moneyToEdit.MoneyCategory = money.MoneyCategory;
            moneyToEdit.AppUserId = user.Id;
            moneyToEdit.AppUser = user;

            _context.Update(moneyToEdit);
            _context.SaveChanges();
            return RedirectToAction("Money", "Main");
        }
        [HttpGet]
        public IActionResult Money(string mc, string date)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            var money = _context.Money.Where(m => m.AppUserId == user.Id);

            if (money == null) return View();

            var aggregatedData = money 
                .GroupBy(p => p.MoneyCategory) 
                .Select(g => new
                {
                    Category = g.Key.ToString(), 
                    Price = g.Sum(x => x.Price) 
                })
                .ToList();

            ViewBag.Categories = aggregatedData.Select(a => a.Category).ToList();
            ViewBag.Prices = aggregatedData.Select(a => a.Price).ToList();

            if (!string.IsNullOrEmpty(mc))
            {
                if (Enum.TryParse(typeof(MoneyCategory), mc, out var parsedCategory))
                {
                    var category = (MoneyCategory)parsedCategory;
                    money = money.Where(m => m.MoneyCategory == category);
                }
            }

            if (!string.IsNullOrEmpty(date))
            {
                if (DateTime.TryParse(date, out var parsedDate))
                {
                    money = money.Where(m => m.Date.Date == parsedDate.Date);
                    aggregatedData = money
                    .GroupBy(p => p.MoneyCategory)
                    .Select(g => new
                    {
                        Category = g.Key.ToString(),
                        Price = g.Sum(x => x.Price)
                    })
                    .ToList();

                    ViewBag.Categories = aggregatedData.Select(a => a.Category).ToList();
                    ViewBag.Prices = aggregatedData.Select(a => a.Price).ToList();
                }
            }

                return View(money);
        }
        public IActionResult DeleteMoney(string id)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            var moneyToDelete = _context.Money.FirstOrDefault(m => m.Id.ToString() == id && m.AppUserId == user.Id);

            if (moneyToDelete == null) return View();
            _context.Money.Remove(moneyToDelete);
            _context.SaveChanges();
            return RedirectToAction("Money", "Main");
        }

        [HttpGet]
        public IActionResult MoneyHistory(string Category, string date)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            Enum.TryParse(typeof(MoneyCategory), Category, out var parsedCategory);

            var history = _context.Money.Where(mc => mc.MoneyCategory == (MoneyCategory)parsedCategory && mc.AppUserId == user.Id);
            if (history == null)
                return View();

            if (!string.IsNullOrEmpty(date))
            {
                if (DateTime.TryParse(date, out var parsedDate))
                {
                    var content = history.Where(m => m.Date.Date == parsedDate.Date);

                    if (content.Count() == 0)
                    {
                        ViewBag.IsEmpty = true;
                        return View(history);
                    }

                    ViewBag.IsEmpty = false;
                    return View(content);
                }
            }
            return View(history);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }
    }
}
