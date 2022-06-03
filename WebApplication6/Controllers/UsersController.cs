using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using real.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace WebApplication6.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebApplication6Context _context;

        public UsersController(WebApplication6Context context)
        {
            _context = context;
            
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'WebApplication6Context.User'  is null.");
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Name == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,password,image")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = from u in _context.User
                        where u.Name == user.Name
                        select u;
                if (q.Count() > 0)
                {
                    ViewData["Error"] = "user already registered";
                }
                else
                {
                    user.Id = _context.User.Count()+1;
                    // var p = from u in _context.Contact
                    //         where u.Id == user.Id
                    //        select u;
                    //  user.contacts = (List<Contact>?)p;
                   
                    Contact c = new Contact { Name = "safwan", userid = user.Id,
                        Messages = new List<Message>() };
                    _context.Contact.Add(c);
                    
                    _context.Add(user);
                    
                    
                    await _context.SaveChangesAsync();
                     return  RedirectToAction("Login");
                }
                
            }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> usercontacts()
        {
            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);
            
            var qq = from u in _context.Contact
                     where u.userid == noname.First().Id
                     select u;
            List<real.Models.Contact> dd = qq.ToList();
            
            return View(dd);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Name,password,image")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = _context.User.Where(u => u.Name == user.Name && u.password == user.password);
                if (q.Any())
                {
                   
                    Signin(q.First());
                    var qq = nameof(Index);
                   return RedirectToAction("usercontacts", user);//contacs might be null
                 
                }
                else
                {
                    ViewData["Error"] = "wrong username or password";

                }
               
            } 
            return View(user);
        }
        private async void Signin(User account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Name),
            };

            
            var claimIdentity= new ClaimsIdentity(claims
                ,CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc=DataTimeOffset.UtcNow.AddMinutes(10)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authProperties);
        }
        public void Logout()
        {
            HttpContext.SignOutAsync();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,password,image")] User user)
        {
            if (id != user.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Name == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'WebApplication6Context.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                    foreach (var item in _context.Contact)
                    {
                        if (item.userid == user.Id)
                        {
                            _context.Contact.Remove(item);
                        }

                    }
                
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
          return (_context.User?.Any(e => e.Name == id)).GetValueOrDefault();
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "b")]
        public async Task<IActionResult> Create([Bind("Name")] Contact contact)
        {
            if (ModelState.IsValid)
            {

                var q = HttpContext.User;
                var claim = HttpContext.User.Claims.First();
                string emailAddress = claim.Value;

                var noname = _context.User.Where(u => u.Name == emailAddress);
                contact.userid = noname.First().Id;
               
                contact.Messages = new List<Message> () ;
             
                _context.Add(contact);
                await _context.SaveChangesAsync();
                Message m = new Message { sender = contact.Name, contactid = contact.Id  };
                _context.Message.Add(m);
                contact.Messages.Add(m);
                _context.Update(contact);
                await _context.SaveChangesAsync();
                
              
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
    }
}
