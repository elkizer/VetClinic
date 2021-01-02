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
    public class ClientAnimalsController : Controller
    {
        private readonly ClinicContext _context;

        public ClientAnimalsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: ClientAnimals
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.ClientAnimals.Include(c => c.Client).Include(c => c.Species);
            return View(await clinicContext.ToListAsync());
        }

        // GET: ClientAnimals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAnimal = await _context.ClientAnimals
                .Include(c => c.Client)
                .Include(c => c.Species)
                .FirstOrDefaultAsync(m => m.ClientAnimalId == id);
            if (clientAnimal == null)
            {
                return NotFound();
            }

            return View(clientAnimal);
        }

        // GET: ClientAnimals/Create
        public IActionResult Create(long? personId)
        {
            if (personId != null)
            {
                ViewData["ClientId"] = new SelectList(_context.Clients.Where(c => c.PersonId == personId), "PersonId", "FullName");
            }
            else
            {
                ViewData["ClientId"] = new SelectList(_context.Clients, "PersonId", "FullName");
            }

            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name");
            return View();
        }

        // POST: ClientAnimals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientAnimalId,ClientId,SpeciesId,Name,BirthDate,Notes")] ClientAnimal clientAnimal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientAnimal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Clients", new { id = clientAnimal.ClientId }, null);
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "PersonId", "FullName", clientAnimal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", clientAnimal.SpeciesId);
            return View(clientAnimal);
        }

        // GET: ClientAnimals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAnimal = await _context.ClientAnimals.FindAsync(id);
            if (clientAnimal == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "PersonId", "FullName", clientAnimal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", clientAnimal.SpeciesId);
            return View(clientAnimal);
        }

        // POST: ClientAnimals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ClientAnimalId,ClientId,SpeciesId,Name,BirthDate,Notes")] ClientAnimal clientAnimal)
        {
            if (id != clientAnimal.ClientAnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientAnimalExists(clientAnimal.ClientAnimalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Clients", new { id = clientAnimal.ClientId }, null);
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "PersonId", "FullName", clientAnimal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", clientAnimal.SpeciesId);
            return View(clientAnimal);
        }

        // GET: ClientAnimals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAnimal = await _context.ClientAnimals
                .Include(c => c.Client)
                .Include(c => c.Species)
                .FirstOrDefaultAsync(m => m.ClientAnimalId == id);
            if (clientAnimal == null)
            {
                return NotFound();
            }

            return View(clientAnimal);
        }

        // POST: ClientAnimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var clientAnimal = await _context.ClientAnimals.FindAsync(id);
            _context.ClientAnimals.Remove(clientAnimal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = clientAnimal.ClientId }, null);
        }

        private bool ClientAnimalExists(long id)
        {
            return _context.ClientAnimals.Any(e => e.ClientAnimalId == id);
        }
    }
}
