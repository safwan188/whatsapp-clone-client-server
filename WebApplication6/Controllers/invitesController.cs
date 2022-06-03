using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class invitesController : Controller
    {
        private readonly WebApplication6Context _context;

        public invitesController(WebApplication6Context context)
        {
            _context = context;
        }

        // GET: invites
        public async Task<IActionResult> Index()
        {

            return View(nameof(Index));
                         
        }

        // GET: invites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.invite == null)
            {
                return NotFound();
            }

            var invite = await _context.invite
                .FirstOrDefaultAsync(m => m.id == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }

        // GET: invites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: invites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,from,to,server")] invite invite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invite);
        }

        // GET: invites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.invite == null)
            {
                return NotFound();
            }

            var invite = await _context.invite.FindAsync(id);
            if (invite == null)
            {
                return NotFound();
            }
            return View(invite);
        }

        // POST: invites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,from,to,server")] invite invite)
        {
            if (id != invite.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!inviteExists(invite.id))
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
            return View(invite);
        }

        // GET: invites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.invite == null)
            {
                return NotFound();
            }

            var invite = await _context.invite
                .FirstOrDefaultAsync(m => m.id == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }

        // POST: invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.invite == null)
            {
                return Problem("Entity set 'WebApplication6Context.invite'  is null.");
            }
            var invite = await _context.invite.FindAsync(id);
            if (invite != null)
            {
                _context.invite.Remove(invite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool inviteExists(int id)
        {
          return (_context.invite?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
