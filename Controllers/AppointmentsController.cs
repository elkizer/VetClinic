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
    public class AppointmentsController : Controller
    {
        private readonly ClinicContext _context;

        public AppointmentsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["AppointmentDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["PetSortParm"] = sortOrder == "Pet" ? "pet_desc" : "Pet";
            ViewData["ClientSortParm"] = sortOrder == "Client" ? "client_desc" : "Client";
            ViewData["EmployeeSortParm"] = sortOrder == "Employee" ? "employee_desc" : "Employee";

            var appointments = from a in _context.Appointments.Include(a => a.Employee).Include(a => a.ClientAnimal).ThenInclude(a => a.Client)
                               select a;

            switch (sortOrder)
            {
                case "date_desc":
                    appointments = appointments.OrderByDescending(s => s.AppointmentDate);
                    break;
                case "pet_desc":
                    appointments = appointments.OrderByDescending(s => s.ClientAnimal.Name);
                    break;
                case "Pet":
                    appointments = appointments.OrderBy(s => s.ClientAnimal.Name);
                    break;
                case "client_desc":
                    appointments = appointments.OrderByDescending(s => s.ClientAnimal.Client.LastName);
                    break;
                case "Client":
                    appointments = appointments.OrderBy(s => s.ClientAnimal.Client.LastName);
                    break;
                case "employee_desc":
                    appointments = appointments.OrderByDescending(s => s.Employee.LastName);
                    break;
                case "Employee":
                    appointments = appointments.OrderBy(s => s.Employee.LastName);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.AppointmentDate);
                    break;
            }

            return View(await appointments.AsNoTracking().ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Employee)
                .Include(a => a.ClientAnimal)
                    .ThenInclude(a => a.Client)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ClientAnimalId"] = new SelectList(_context.ClientAnimals, "ClientAnimalId", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "PersonId", "FullName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,ClientAnimalId,EmployeeId,AppointmentDate,Reason")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientAnimalId"] = new SelectList(_context.ClientAnimals, "ClientAnimalId", "Name", appointment.ClientAnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "PersonId", "FullName", appointment.EmployeeId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ClientAnimalId"] = new SelectList(_context.ClientAnimals, "ClientAnimalId", "Name", appointment.ClientAnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "PersonId", "FullName", appointment.EmployeeId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("AppointmentId,ClientAnimalId,EmployeeId,AppointmentDate,Reason")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["ClientAnimalId"] = new SelectList(_context.ClientAnimals, "ClientAnimalId", "Name", appointment.ClientAnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "PersonId", "FullName", appointment.EmployeeId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.ClientAnimal)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
