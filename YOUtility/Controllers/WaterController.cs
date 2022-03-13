using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YOUtility.Areas.Identity.Data;
using YOUtility.Data;
using YOUtility.Models;

namespace YOUtility.Controllers
{
    [Authorize]
    public class WaterController : Controller
    {
        private readonly ILogger<WaterController> _logger;

        private readonly YOUtilityContext _context;
        private readonly UserManager<YOUtilityUser> user_Manager;


        public WaterController(ILogger<WaterController> logger, UserManager<YOUtilityUser> um, YOUtilityContext context)
        {
            user_Manager = um;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Timeline()
        {
            // Get all the water bills
            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
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
            List<Bill> waterbills = new List<Bill>();
            foreach (int id in ids)
            {
                Bill potential_bill = await _context.Bill.FindAsync(id);
                if (potential_bill != null && (potential_bill.utility == 0))
                {
                    waterbills.Add(potential_bill);
                }
            }

            return View(waterbills);
        }

        public async Task<IActionResult> Charts()
        {
            Bill[] sixBills = await last_two_bills();
            Bill thisMonthWater = sixBills[0];
            Bill lastMonthWater = sixBills[1];
            Bill thisMonthGas = sixBills[2];
            Bill lastMonthGas = sixBills[3];
            Bill thisMonthElectricity = sixBills[4];
            Bill lastMonthElectricity = sixBills[5];

            // Send the cost of each utility to the view
            if (thisMonthWater != null)
                ViewBag.waterCost = thisMonthWater.due;
            else
                ViewBag.waterCost = 0;

            if (thisMonthGas != null)
                ViewBag.gasCost = thisMonthGas.due;
            else
                ViewBag.gasCost = 0;

            if (thisMonthElectricity != null)
                ViewBag.electricityCost = thisMonthElectricity.due;
            else
                ViewBag.electricityCost = 0;

            // Send the last two bills to the view
            if (thisMonthWater != null)
                ViewBag.thisMonthUsed = thisMonthWater.consumed;
            else
                ViewBag.thisMonthUsed = 0;

            if (lastMonthWater != null)
                ViewBag.lastMonthUsed = lastMonthWater.consumed;
            else
                ViewBag.lastMonthUsed = 0;

            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
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

            List<Bill> waterbills = new List<Bill>();
            List<Bill> _gasBills = new List<Bill>();
            List<Bill> _electricityBills = new List<Bill>();

            foreach (int id in ids)
            {
                Bill potential_bill = await _context.Bill.FindAsync(id);
                // Gets all the water bills
                if (potential_bill != null && (potential_bill.utility == 0))
                {
                    waterbills.Add(potential_bill);
                }
                // Gets all the gas bills
                else if (potential_bill != null && (potential_bill.utility == Bill.Type.gas))
                {
                    _gasBills.Add(potential_bill);
                }
                // Gets all the electricity bills
                else if (potential_bill != null && (potential_bill.utility == Bill.Type.electricity))
                {
                    _electricityBills.Add(potential_bill);
                }
            }

            // Sends the gas and electricity bills to the view
            ViewBag.gasBills = _gasBills;
            ViewBag.electricityBills = _electricityBills;

            return View(waterbills);
        }

        public async Task<IActionResult> Tips()
        {
            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
            List<String> wtips = new List<String>();
            if (user != null)
            {
                Tips tips = new Tips();
                wtips = tips.water_tips(user);
            }

            ViewBag.tips = wtips;

            return View(wtips);
        }

        // Returns the bills from the previous two months. The array is exactly of size
        // 6. [0] corresponds to the previous month's bill for water, with [1] corresponding to the
        // one before. If a user does not have a valid bill for the previous two months, that index will
        // be null.
        public async Task<Bill[]> last_two_bills()
        {
            YOUtilityUser user = await user_Manager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                Bill[] result = new Bill[6];
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
                List<Bill> waterbills = new List<Bill>();
                List<Bill> gasbills = new List<Bill>();
                List<Bill> electricitybills = new List<Bill>();
                foreach (int id in ids)
                {
                    Bill potential_bill = await _context.Bill.FindAsync(id);
                    if (potential_bill != null)
                    {
                        if (potential_bill.utility == Bill.Type.water)
                            waterbills.Add(potential_bill);
                        if (potential_bill.utility == Bill.Type.gas)
                            gasbills.Add(potential_bill);
                        if (potential_bill.utility == Bill.Type.electricity)
                            electricitybills.Add(potential_bill);
                    }
                }
                waterbills.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
                gasbills.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
                electricitybills.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
                result = find_newest_bills(waterbills, result, Bill.Type.water);
                result = find_newest_bills(gasbills, result, Bill.Type.gas);
                result = find_newest_bills(electricitybills, result, Bill.Type.electricity);
                return result;
            }
            return new Bill[6];
        }

        // Helper method to find two newest bills.
        private Bill[] find_newest_bills(List<Bill> list, Bill[] result, Bill.Type utility)
        {
            int index;
            if (utility == Bill.Type.water)
            {
                index = 0;
            }
            else if (utility == Bill.Type.gas)
            {
                index = 2;
            }
            else { index = 4; }
            if (list.Count == 1)
            {
                if (list[0].beginningPeriod.Month == DateTime.Today.AddMonths(-1).Month)
                {
                    result[index] = list[0];
                }
            }
            else if (list.Count >= 2)
            {
                if (list[list.Count - 1].beginningPeriod.Month == DateTime.Today.AddMonths(-1).Month)
                {
                    result[index] = list[list.Count - 1];
                }
                if (list[list.Count - 2].beginningPeriod.Month == DateTime.Today.AddMonths(-2).Month)
                {
                    result[index + 1] = list[list.Count - 2];
                }
            }
            return result;
        }
    }
}
