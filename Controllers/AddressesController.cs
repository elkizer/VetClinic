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
    public class AddressesController : Controller
    {
        private readonly ClinicContext _context;

        public AddressesController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.PersonAddresses.Include(p => p.Person);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddresses
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonAddressId == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonAddressId,PersonId,AddressLine1,AddressLine2,City,State,PostalCode")] PersonAddress personAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FullName", personAddress.PersonId);
            return View(personAddress);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddresses.FindAsync(id);
            if (personAddress == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FullName", personAddress.PersonId);
            return View(personAddress);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PersonAddressId,PersonId,AddressLine1,AddressLine2,City,State,PostalCode")] PersonAddress personAddress)
        {
            if (id != personAddress.PersonAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonAddressExists(personAddress.PersonAddressId))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "FirstName", personAddress.PersonId);
            return View(personAddress);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddresses
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonAddressId == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var personAddress = await _context.PersonAddresses.FindAsync(id);
            _context.PersonAddresses.Remove(personAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonAddressExists(long id)
        {
            return _context.PersonAddresses.Any(e => e.PersonAddressId == id);
        }
    }
}
