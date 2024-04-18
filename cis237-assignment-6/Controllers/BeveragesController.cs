using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using cis237_assignment_6.Models;

namespace cis237_assignment_6.Controllers
{    
    public class BeveragesController : Controller
    {
        private readonly BeverageContext _context;

        public BeveragesController(BeverageContext context)
        {
            _context = context;
        }

        // GET: Beverages
        public async Task<IActionResult> Index()
        {
            // Setup a variable to hold the beverages data.
            DbSet<Beverage> beveragesToFilter = _context.Beverages;

            // Setup some strings to hold the data that might be
            // in the session. If there is nothing in the session
            // we can still use these variables as a default.
            string filterNameString = "";
            string filterPackString = "";
            string filterMinPriceString = "";
            string filterMaxPriceString = "";

            // Define a min and max for the price;
            decimal minPriceDecimal = _context.Beverages.Min(beverage => beverage.Price);
            decimal maxPriceDecimal = _context.Beverages.Max(beverage => beverage.Price);

            // Check to see if there is a value in the session,
            // and if there is, assign it to the filter variable
            if (!String.IsNullOrWhiteSpace(HttpContext.Session.GetString("session_name")))
            {
                filterNameString = HttpContext.Session.GetString("session_name");
            }

            //return View(await _context.Beverages.ToListAsync());
        }

        // GET: Beverages/Details/5        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Beverages == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beverage == null)
            {
                return NotFound();
            }

            return View(beverage);
        }

        // GET: Beverages/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Pack,Price,Active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beverage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beverage);
        }

        // GET: Beverages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Beverages == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Pack,Price,Active")] Beverage beverage)
        {
            if (id != beverage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beverage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeverageExists(beverage.Id))
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
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Beverages == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beverage == null)
            {
                return NotFound();
            }

            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Beverages == null)
            {
                return Problem("Entity set 'BeverageContext.Beverages'  is null.");
            }
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage != null)
            {
                _context.Beverages.Remove(beverage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeverageExists(string id)
        {
          return _context.Beverages.Any(e => e.Id == id);
        }
    }
}
