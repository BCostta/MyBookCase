using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookcase.Data;
using MyBookcase.Models.Entities;

namespace MyBookcase.Controllers
{

    public class LoansController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LoansController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult IndexLoan()
        {
            ViewData["OcultarNav"] = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = new booksCategoryViewModel
            {
                books = await dbContext.books.ToListAsync(),
                categories = await dbContext.Categories.ToListAsync(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int Id, string Email)
        {
            User user = new User();

            user = await dbContext.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Email == Email);

            Loan loanView = new Loan();
            loanView.Id = Id;
            loanView.bookId = Id;
            loanView.loanDate = DateTime.Now;
            loanView.returnDate = loanView.loanDate.AddDays(30);
            loanView.Status = 1;

            await dbContext.Loans.AddAsync(loanView);
            await dbContext.SaveChangesAsync();
            return View();
        }
    }
}
