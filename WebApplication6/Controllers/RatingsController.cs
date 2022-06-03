using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class RatingsController : Controller
    {
        private RatingsService service;

        public RatingsController()
        {
            service = new RatingsService();
        }

        // GET: Page
        public async Task<IActionResult> Index()
        {
            return View(service.GetAll());
        }

        // GET: Page/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = service.GetById(id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Page/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Author,Rate,FeedBack")] Ratings ratings)
        {
            if (ModelState.IsValid)
            {
                service.Create(ratings.Author, ratings.Rate, ratings.FeedBack);
                return RedirectToAction(nameof(Index));
            }
            return View(ratings);
        }

        // GET: Ratings/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = service.GetById(id);
            if (ratings == null)
            {
                return NotFound();
            }
            return View(ratings);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Author,Rate,FeedBack")] Ratings ratings)
        {
            if (id != ratings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Edit(id, ratings.Rate, ratings.FeedBack);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(ratings);
        }

        // GET: Ratings/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = service.GetById(id);
            if (ratings == null)
            {
                return NotFound();
            }

            return View(ratings);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ratings = service.GetById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}