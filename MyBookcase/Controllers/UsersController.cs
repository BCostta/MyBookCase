using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookcase.Data;
using MyBookcase.Models.Entities;
using System.Linq;

namespace MyBookcase.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["OcultarNav"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            try
            {
                if (email is null || senha is null)
                {
                    ViewBag.Message = "Necessário que os campos sejam preenchidos";
                    return View();
                }
                else
                {

                    var user = await dbContext.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(u => u.Email == email && u.Password == senha);
                    if (user is null)
                    {
                        ViewBag.Message = "Usuário o senha inválidos";
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

            }
            catch(Exception ex)
            {
               return ViewBag("Erro", ex.Message);
            }

            return Ok();

        }

        [HttpGet]
        public IActionResult Add() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            user.dataRegister = DateTime.Now;
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await dbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var users = await dbContext.Users.FindAsync(id);
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var users = await dbContext.Users.FindAsync(user.Id);
            if (users is not null)
            {
                users.Name = user.Name;
                users.Email = user.Email;
                users.typeUser = user.typeUser;


                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == user.Id);
            if (users is not null)
            {
                dbContext.Users.Remove(users);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Users");
        }
    }
}
