using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class PhonesController : Controller
    {
        private readonly ClinicContext _context;

        public PhonesController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.PersonPhones.Include(p => p.Person);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonPhoneId == id);
            if (personPhone == null)
            {
                return NotFound();
            }

            return View(personPhone);
        }

        // GET: Phones/Create
        public IActionResult Create(long? personId)
        {
            if (personId != null)
            {
                ViewData["PersonId"] = new SelectList(_context.Persons.Where(p => p.PersonId == personId), "PersonId", "FullName");
            }
            else
            {
                ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FullName");
            }

            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonPhoneId,PersonId,PhoneNumber")] PersonPhone personPhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Clients", new { id = personPhone.PersonId }, null);
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personPhone.PersonId);
            return View(personPhone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones.FindAsync(id);
            if (personPhone == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personPhone.PersonId);
            return View(personPhone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PersonPhoneId,PersonId,PhoneNumber")] PersonPhone personPhone)
        {
            if (id != personPhone.PersonPhoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personPhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonPhoneExists(personPhone.PersonPhoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Clients", new { id = personPhone.PersonId }, null);
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personPhone.PersonId);
            return View(personPhone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonPhoneId == id);
            if (personPhone == null)
            {
                return NotFound();
            }

            return View(personPhone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var personPhone = await _context.PersonPhones.FindAsync(id);
            _context.PersonPhones.Remove(personPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = personPhone.PersonId }, null);
        }

        private bool PersonPhoneExists(long id)
        {
            return _context.PersonPhones.Any(e => e.PersonPhoneId == id);
        }
    }
}
