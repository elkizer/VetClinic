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
    public class AnimalsController : Controller
    {
        private readonly ClinicContext _context;

        public AnimalsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Animals.Include(a => a.Species);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Species)
                .Include(a => a.ClientAnimals)
                    .ThenInclude(ca => ca.Client)
                        .ThenInclude(ca => ca.PersonAddresses)
                .Include(a => a.ClientAnimals)
                    .ThenInclude(ca => ca.Client)
                        .ThenInclude(ca => ca.PersonPhones)
                .Include(a => a.ClientAnimals)
                    .ThenInclude(ca => ca.Client)
                        .ThenInclude(ca => ca.PersonEmails)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,SpeciesId,Name,BirthDate,Notes")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "SpeciesId", animal.SpeciesId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", animal.SpeciesId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("AnimalId,SpeciesId,Name,BirthDate,Notes")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "SpeciesId", animal.SpeciesId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var animal = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(long id)
        {
            return _context.Animals.Any(e => e.AnimalId == id);
        }
    }
}
