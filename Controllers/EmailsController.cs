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
    public class EmailsController : Controller
    {
        private readonly ClinicContext _context;

        public EmailsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.PersonEmails.Include(p => p.Person);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personEmail = await _context.PersonEmails
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonEmailId == id);
            if (personEmail == null)
            {
                return NotFound();
            }

            return View(personEmail);
        }

        // GET: Emails/Create
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

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonEmailId,PersonId,EmailAddress")] PersonEmail personEmail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Clients", new { id = personEmail.PersonId }, null);
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personEmail.PersonId);
            return View(personEmail);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personEmail = await _context.PersonEmails.FindAsync(id);
            if (personEmail == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personEmail.PersonId);
            return View(personEmail);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PersonEmailId,PersonId,EmailAddress")] PersonEmail personEmail)
        {
            if (id != personEmail.PersonEmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonEmailExists(personEmail.PersonEmailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Clients", new { id = personEmail.PersonId }, null);
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personEmail.PersonId);
            return View(personEmail);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personEmail = await _context.PersonEmails
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonEmailId == id);
            if (personEmail == null)
            {
                return NotFound();
            }

            return View(personEmail);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var personEmail = await _context.PersonEmails.FindAsync(id);
            _context.PersonEmails.Remove(personEmail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = personEmail.PersonId }, null);
        }

        private bool PersonEmailExists(long id)
        {
            return _context.PersonEmails.Any(e => e.PersonEmailId == id);
        }
    }
}
