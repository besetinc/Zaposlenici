using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zaposlenici.Models;

namespace Zaposlenici.Controllers
{
    public class zaposlenikController : Controller
    {
        private readonly zaposlenikContext _context;

        public zaposlenikController(zaposlenikContext context)
        {
            _context = context;
        }

        // GET: zaposlenik
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var zapl = from zp in _context.zaposlenik
                       select zp;
            if(!String.IsNullOrEmpty(searchString))
            {
                zapl = zapl.Where(zp => zp.Ime.Contains(searchString));
            }
            return View(await zapl.AsNoTracking().ToListAsync());
        }

        // GET: zaposlenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.zaposlenik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // GET: zaposlenik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: zaposlenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,GodZaposlenja")] zaposlenik zaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaposlenik);
        }

        // GET: zaposlenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.zaposlenik.FindAsync(id);
            if (zaposlenik == null)
            {
                return NotFound();
            }
            return View(zaposlenik);
        }

        // POST: zaposlenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,GodZaposlenja")] zaposlenik zaposlenik)
        {
            if (id != zaposlenik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!zaposlenikExists(zaposlenik.Id))
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
            return View(zaposlenik);
        }

        // GET: zaposlenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.zaposlenik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // POST: zaposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.zaposlenik.FindAsync(id);
            _context.zaposlenik.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool zaposlenikExists(int id)
        {
            return _context.zaposlenik.Any(e => e.Id == id);
        }
    }
}
