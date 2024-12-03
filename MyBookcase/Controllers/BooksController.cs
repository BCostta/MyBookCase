using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookcase.Data;
using MyBookcase.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MyBookcase.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            await dbContext.books.AddAsync(book);
            await dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Edit(int id)
        {
            var book = await dbContext.books.FindAsync(id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            var books = await dbContext.books.FindAsync(book.Id);
            if(books is not null)
            {
                books.Title = book.Title;
                books.Author = book.Author;
                books.yearsOfPublication = book.yearsOfPublication;
                books.Description = book.Description;
                books.categoryId = book.categoryId;
                books.quantityInStock = book.quantityInStock;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Books");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Book Book)
        {
            var book = await dbContext.books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == Book.Id);
            if(book is not null)
            {
                dbContext.books.Remove(book);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Books");
        }
    }
}
