using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using real.Models;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class Contacts2Controller : Controller
    {
        private readonly WebApplication6Context _context;

        public Contacts2Controller(WebApplication6Context context)
        {
            _context = context;
        }

        // GET: Contacts2
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var qq = from u in _context.Contact
                     where u.userid == noname.First().Id
                     select u;
            return Json(await qq.ToListAsync());
        }

        // GET: Contacts2/Details/5
        [HttpGet(":{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id && m.userid==noname.First().Id);
            if (contact == null)
            {
                return NotFound();
            }
           
            return Json(contact);
        }



        // POST: Contacts2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<IActionResult> Create([Bind("Name")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                var q = HttpContext.User;
                var claim = HttpContext.User.Claims.First();
                string emailAddress = claim.Value;
                var noname = _context.User.Where(u => u.Name == emailAddress);
                
                contact.userid = noname.First().Id;
                
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return Json(contact);
            }
            return View(contact);
        }
    

        // POST: Contacts2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut(":{id}")]

        public async Task<IActionResult> Edit(int id, [Bind("Name")] Contact contact)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var q = HttpContext.User;
                    var claim = HttpContext.User.Claims.First();
                    string emailAddress = claim.Value;
                    var noname = _context.User.Where(u => u.Name == emailAddress);
                    var contactt = await _context.Contact
               .FirstOrDefaultAsync(m => m.Id == id && m.userid==noname.First().Id);
                    contactt.Name = contact.Name;
                    _context.Update(contactt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest();
        }

        // GET: Contacts2/Delete/5
        

        // POST: Contacts2/Delete/5
        [HttpDelete(":{id}")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'WebApplication6Context.Contact'  is null.");
            }
            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var qq = from u in _context.Contact
                     where (u.userid == noname.First().Id && u.Id == id)
                     select u;
            var contact = qq.First();
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet(":{id}/messages")]
        public async Task<IActionResult> Messages(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id && m.userid == noname.First().Id);
            var qq = from u in _context.Message
                     where u.contactid == contact.Id
                     select u;
            if (contact == null)
            {
                return NotFound();
            }

            return Json(await qq.ToListAsync());
        }
        [HttpPost(":{id}/messages")]
        public async Task<IActionResult> newMessages(int? id,  [Bind("Name")] Message m)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id && m.userid == noname.First().Id);
            m.contactid = contact.Id;
            m.content = "fuck";
            _context.Message.AddAsync(m);
            await _context.SaveChangesAsync();
            if (contact == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet(":{id}/messages/:{id2}")]
            public async Task<IActionResult> Messagesfromcontact(int? id,int? id2)
            {
                if (id == null || _context.Contact == null)
                {
                    return NotFound();
                }

                var q = HttpContext.User;
                var claim = HttpContext.User.Claims.First();
                string emailAddress = claim.Value;

                var noname = _context.User.Where(u => u.Name == emailAddress);

                var contact = await _context.Contact
                    .FirstOrDefaultAsync(m => m.Id == id && m.userid == noname.First().Id);
                var qq = from u in _context.Message
                         where u.contactid == contact.Id && u.Id == id2
                         select u;

                if (contact == null)
                {
                    return NotFound();
                }

                return Json(await qq.ToListAsync());
            }
        [HttpPut(":{id}/messages/:{id2}")]
        public async Task<IActionResult> editMessagesfromcontact(int? id, [Bind("content")] Message m,int? id2)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }
            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id && m.userid == noname.First().Id);
            var qq = from u in _context.Message
                     where u.contactid == contact.Id && u.Id == id2
                     select u;
            qq.First().content = m.content;
            _context.Update(qq.First());
            await _context.SaveChangesAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete(":{id}/messages/:{id2}")]
        public async Task<IActionResult> delMessagesfromcontact(int? id, int? id2)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }
            var q = HttpContext.User;
            var claim = HttpContext.User.Claims.First();
            string emailAddress = claim.Value;

            var noname = _context.User.Where(u => u.Name == emailAddress);

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id && m.userid == noname.First().Id);
            var qq = from u in _context.Message
                     where u.contactid == contact.Id && u.Id == id2
                     select u;
            _context.Remove(qq.First());
            await _context.SaveChangesAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost("invitations")]
        public async Task<IActionResult> snewMessages( [Bind("from,to,server")] invite m )
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

           
            var noname = _context.User.Where(u => u.Name == m.from);
            var user = noname.First();
            _context.Contact.AddAsync(new Contact { userid = user.Id, Name = m.to });
            await _context.SaveChangesAsync();
            var noname2= _context.User.Where(u => u.Name == m.to);
            var user2 = noname2.First();
            _context.Contact.AddAsync(new Contact { userid = user2.Id, Name = m.from });
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("transfer")]
        public async Task<IActionResult> ssnewMessages([Bind("from,to,server")] invite m)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }


            var noname = _context.User.Where(u => u.Name == m.to);
            var user = noname.First();
            var contant = _context.Contact.Where(u => u.userid == user.Id);
            var contact = contant.First();
       
            Message q = new Message { contactid =contact.Id, sender = m.from, content = m.server };
            await _context.Message.AddAsync(q);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ContactExists(int id)
        {
          return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
