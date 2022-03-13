using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YOUtility.Areas.Identity.Data;
using YOUtility.Data;
using YOUtility.Models;

namespace YOUtility.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly YOUtilityContext _context;
        private readonly UserManager<YOUtilityUser> user_Manager;

        public BillsController(YOUtilityContext context, UserManager<YOUtilityUser> um)
        {
            _context = context;
            user_Manager = um;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bill.ToListAsync());
        }

        public async Task<IActionResult> UserIndex()
        {
            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);

            ViewBag.userID = user.Id;

            return View(all_bills(user).Result);
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }


        // GET: Bills/Create
        public IActionResult Create()
        {

            return View();
        }

        public async Task<List<Bill>> all_bills(YOUtilityUser user)
        {
            List<Bill> bills = new List<Bill>();
            if (user != null && user.BillIds != null)
            {
                string billids = user.BillIds.Trim();
                List<int> ids = new List<int>();
                if (!billids.Equals(""))
                {
                    String[] potential_ids = billids.Split(' ');
                    foreach (string id in potential_ids)
                    {
                        if (!id.Equals("") && !id.Equals(" "))
                        {
                            ids.Add(int.Parse(id));
                        }
                    }

                }
                foreach (int id in ids)
                {
                    Bill potential_bill = await _context.Bill.FindAsync(id);
                    if (potential_bill != null)
                    {
                        bills.Add(potential_bill);
                    }
                }
            }
            return bills;
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,utility,due,consumed,uploadTime,beginningPeriod,endingPeriod")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
                if (bill.uploadTime == DateTime.MinValue)
                {
                    bill.uploadTime = DateTime.Now;
                }
                _context.Add(bill);
                await _context.SaveChangesAsync();
                user.BillIds = user.BillIds + " " + bill.ID;
                await user_Manager.UpdateAsync(user);
                Achievements checker = new Achievements(_context, user_Manager);
                
                List<string> tempAchieve = await checker.check_basic(bill, user);
                if(!(tempAchieve.Count > 0))
                    ViewBag.newAchievements = null;
                else
                    ViewBag.newAchievements = String.Join(", ", tempAchieve);

                ViewBag.newBill = true;

                return View();
            }
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,utility,due,consumed,uploadTime,beginningPeriod,endingPeriod")] Bill bill)
        {
            if (id != bill.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserIndex));
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
            var bill = await _context.Bill.FindAsync(id);
            int billid = bill.ID;
            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();
            user.BillIds = user.BillIds.Replace(""+ billid, "").Trim();
            await user_Manager.UpdateAsync(user);
            return RedirectToAction(nameof(UserIndex));
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.ID == id);
        }

        // Gets a scanned pdf data and adds it to this users database.
        [HttpPost]
        public async Task<IActionResult> GetScannedPDFDataAsync(DateTime dateStart, DateTime dateEnd, double amount, double used, string type)
        {
            // The start date of the upload bill
            DateTime startDate = dateStart;
            // The end date of the uploaded bill
            DateTime endDate = dateEnd;
            // The amount the bill cost
            double due = amount;
            // The amount of utility consumed
            double consumed = used;
            // The type of utility the bill is for
            Bill.Type utility = Bill.Type.water;
            if (type == "electricity")
                utility = Bill.Type.electricity;
            else if (type == "gas")
                utility = Bill.Type.gas;

            // TO-DO add a new bill to the database and link it to the logged in user.
            YOUtilityUser user = user_Manager.GetUserAsync(HttpContext.User).Result;
            Bill bill = new Bill();
            bill.ID = 0;
            bill.due = due;
            bill.consumed = consumed;
            bill.endingPeriod = endDate;
            bill.beginningPeriod = startDate;
            bill.utility = utility;
            bill.uploadTime = DateTime.Now;
            _context.Add(bill);
            await _context.SaveChangesAsync();
            user.BillIds = user.BillIds + " " + bill.ID;
            await user_Manager.UpdateAsync(user);
            Achievements checker = new Achievements(_context, user_Manager);
            await checker.check_basic(bill, user);
            await checker.check_tech(user, user_Manager);
            return Ok(new
            {
                message = "Request successful."
            });
        }
    }
}
