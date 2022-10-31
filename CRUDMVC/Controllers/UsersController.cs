using CRUDMVC.Data;
using CRUDMVC.Models;
using CRUDMVC.Models.UserModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CRUDMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDbContext userDbContext;

        public UsersController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }


        // READ: 
        // Afisez o lista cu datele din tabelul din baza de date (guid - id unic, name, email si date of birth)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await userDbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // CREATE: Fac un nou user si il introduc in baza de date
        //         Daca unul din campuri nu este completat suntem redirectionati pe pagina "ErrorPage"
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User()
                    {
                        Id = Guid.NewGuid(),
                        Name = userViewModel.Name,
                        Email = userViewModel.Email,
                        DateOfBirth = userViewModel.DateOfBirth
                    };

                    await userDbContext.Users.AddAsync(user);
                    await userDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return RedirectToAction("Add");
            }
            return await Task.Run(() => View("ErrorPage"));
        }


        // Aici imi afisez pe o pagina separata informatiile unui SINGUR user
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var userViewModel = new UpdateUserViewModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        DateOfBirth = user.DateOfBirth
                    };
                    return await Task.Run(() => View("View", userViewModel));
                }
            }
            catch (DataException)
            {
                return await Task.Run(() => View("ErrorPage"));
            }

            return await Task.Run(() => View("ErrorPage"));
        }

        // UPDATE: Schimb informatiile despre un user
        //         Daca unul din campuri nu este completat suntem redirectionati pe pagina "ErrorPage"
        [HttpPost]
        public async Task<IActionResult> View(UpdateUserViewModel model)
        {
            var user = await userDbContext.Users.FindAsync(model.Id);

            if (user == null)
                return await Task.Run(() => View("ErrorPage"));

            try
            {
                if (ModelState.IsValid)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.DateOfBirth = model.DateOfBirth;

                    await userDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return await Task.Run(() => View("ErrorPage"));
            }
            return await Task.Run(() => View("ErrorPage"));
        }

        // DELETE: Sterg un anumit user (toate informatiile) din db.
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel model)
        {
            var user = await userDbContext.Users.FindAsync(model.Id);

            if (user == null)
                return RedirectToAction("Index");

            userDbContext.Users.Remove(user);
            await userDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
