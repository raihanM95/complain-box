using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComplainBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ComplainBox.Controllers
{
    public class ComplainsController : Controller
    {
        private readonly AppDbContext _context;
        // private readonly IHttpContextAccessor _httpContextAccessor;
        // private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ComplainsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Complains/Registration
        public IActionResult Registration()
        {
            UserAccount objLoginModel = new UserAccount();
            var roles = new List<SelectListItem>
            {
                new SelectListItem {Text = "-- Select --", Value = ""},
                new SelectListItem {Text = "Admin", Value = "Admin"},
                new SelectListItem {Text = "Student", Value = "Student"},
                new SelectListItem {Text = "Teacher", Value = "Teacher"}
            };

            ViewData["Roles"] = roles;

            return View(objLoginModel);
        }

        // POST: Complains/Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("UserId,Name,Email,Role,Password,ConfirmPassword")] UserAccount objLoginModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objLoginModel);
                await _context.SaveChangesAsync();

                ModelState.AddModelError("Success", "Successfuly Register!");

                return RedirectToAction(nameof(Login));
            }

            return View(objLoginModel);
        }

        // GET: Complains/Login
        public IActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();
            // UserAccount objLoginModel = new UserAccount();
            var roles = new List<SelectListItem>
            {
                new SelectListItem {Text = "-- Select --", Value = ""},
                new SelectListItem {Text = "Admin", Value = "Admin"},
                new SelectListItem {Text = "Student", Value = "Student"},
                new SelectListItem {Text = "Teacher", Value = "Teacher"}
            };

            ViewData["Roles"] = roles;

            if (HttpContext.Session.Get("UserName") == null)
            {
                return View(objLoginModel);
            }
            else
            {
                return RedirectToAction("Index", "Complains");
            }
        }

        // POST: Complains/Login
        [HttpPost]
        public IActionResult Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.userAccounts.Where(m => m.Email == objLoginModel.Email && m.Password == objLoginModel.Password && m.Role == objLoginModel.Role).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    // Session["HosName"] = userAccount.Name;
                    // Session["HosEmail"] = Doctor.Email;
                    if(user.Role == "Admin")
                        return RedirectToAction("ComplainView", "Complains");
                    else
                        return RedirectToAction("ComplainList", "Complains");
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid Email and Password");

                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login","Complains");
        }

        // GET: Complains
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "Complains");
            }

            string check = "Approved";
            var data = _context.complains.FromSql("Select * from complains where Status =@p0", check).ToList();

            return View(data);
        }

        // GET: Complains/ComplainList
        public IActionResult ComplainList()
        {
            //int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int id = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out id);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var complains = _context.complains.Where(c => c.UserId == id).ToList();
                
                return View(complains);
            }
            else
            {
                return RedirectToAction("Login", "Complains");
            }
        }

        // GET: Complains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.complains
                .Include(c => c.UserAccount)
                .FirstOrDefaultAsync(m => m.ComplainId == id);
            if (complain == null)
            {
                return NotFound();
            }

            return View(complain);
        }

        // GET: Complains/Create
        public IActionResult Create()
        {
            Complain complainModel = new Complain();
            //ViewData["UserId"] = new SelectList(_context.userAccounts, "UserId", "UserId");

            int paId = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out paId);

            ViewData["UserId"] = complainModel.UserId = paId;
            string status = "Pending";

            complainModel.Status = status;

            return View(complainModel);
        }

        // POST: Complains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplainId,Name,ComplainTitle,ComplainDescription,Status,UserId")] Complain complain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complain);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ComplainList));
            }
            ViewData["UserId"] = new SelectList(_context.userAccounts, "UserId", "UserId", complain.UserId);

            return View(complain);
        }

        // GET: Complains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.complains.FindAsync(id);
            if (complain == null)
            {
                return NotFound();
            }
            int paId = 0;
            int.TryParse(HttpContext.Session.GetInt32("UserId").ToString(), out paId);

            complain.UserId = paId;

            return View(complain);
        }

        // POST: Complains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplainId,Name,ComplainTitle,ComplainDescription,Status,UserId")] Complain complain)
        {
            if (id != complain.ComplainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplainExists(complain.ComplainId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(ComplainList));
            }
            ViewData["UserId"] = new SelectList(_context.userAccounts, "UserId", "UserId", complain.UserId);

            return View(complain);
        }

        // GET: Complains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.complains
                .Include(c => c.UserAccount)
                .FirstOrDefaultAsync(m => m.ComplainId == id);
            if (complain == null)
            {
                return NotFound();
            }

            return View(complain);
        }

        // POST: Complains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complain = await _context.complains.FindAsync(id);
            _context.complains.Remove(complain);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ComplainList));
        }

        public IActionResult ComplainView()
        {

            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "Complains");
            }
            
            string check = "Pending";
            var data = _context.complains.FromSql("SELECT * FROM complains WHERE Status =@p0", check).Include(c=> c.UserAccount).ToList();

            return View(data);
        }

        public IActionResult RejectedView()
        {
            string check = "Rejected";
            var data = _context.complains.FromSql("SELECT * FROM complains WHERE Status =@p0", check).Include(c => c.UserAccount).ToList();
            
            return View(data);
        }

        public IActionResult Approve(int? id)
        {
            var result = _context.complains.SingleOrDefault(b => b.ComplainId == id);
            if (result != null)
            {
                result.Status = "Approved";
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(ComplainView));
        }

        public IActionResult Rejected(int? id)
        {
            var result = _context.complains.SingleOrDefault(b => b.ComplainId == id);
            if (result != null)
            {
                result.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ComplainView));
        }

        private bool ComplainExists(int id)
        {
            return _context.complains.Any(e => e.ComplainId == id);
        }
    }
}
