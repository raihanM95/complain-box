using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplainBox.Models;
using Microsoft.AspNetCore.Http;

namespace ComplainBox.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            int id = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out id);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var appointments = _context.appointments.Where(c => c.UserId == id).ToList();

                return View(appointments);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // GET: Appointments/Index2
        public async Task<IActionResult> Index2()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var appDbContext = _context.appointments.Include(a => a.UserAccount);
                return View(await appDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                Appointment appointmentModel = new Appointment();
                
                int paId = 0;
                int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out paId);

                ViewData["UserId"] = appointmentModel.UserId = paId;
                string status = "Pending";

                appointmentModel.Status = status;
                return View(appointmentModel);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
            
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentTitle,AppointmentDescription,AppointmentDate,Status,UserId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.userAccounts, "UserId", "ConfirmPassword", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments
                .Include(a => a.UserAccount)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.appointments.FindAsync(id);
            _context.appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Accept/5
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            //Appointment appointment1 = new Appointment
            //{
            //    AppointmentId = appointment.AppointmentId
            //};
            
            return View(appointment);
        }

        // POST: Appointments/Accept/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id, [Bind("AppointmentId,AppointmentDate")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aAppointment = await _context.appointments.FindAsync(appointment.AppointmentId);
                    aAppointment.AppointmentDate = appointment.AppointmentDate;
                    aAppointment.Status = "Accepted";
                    _context.Update(aAppointment);
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

                return RedirectToAction(nameof(Index2));
            }
            
            return View(appointment);
        }

        // POST: Appointments/Reject/5
        public async Task<IActionResult> Reject(int id)
        {
            var appointment = await _context.appointments.FindAsync(id);
            appointment.Status = "Rejected";
            _context.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2));
        }

        private bool AppointmentExists(int id)
        {
            return _context.appointments.Any(e => e.AppointmentId == id);
        }
    }
}
