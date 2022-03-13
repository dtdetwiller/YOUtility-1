using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOUtility.Areas.Identity.Data;
using YOUtility.Models;

namespace YOUtility.Data
{
    public class Tips
    {
        public String[] w_tips = {"Inspect your appliances. Keep an eye out for leaks and drips, as these " +
                "pile up quickly over time. Toilets, faucets and dishwashers are most likely to spring leaks.",
            "Upgrade to Energy Star appliances. Replacing your dishwasher and washing machine with their Energy " +
                "Star counterparts will allow you to cut back on both your water and energy usage. To " +
                "locate savings, tax credits and discounts in your area, use the Energy Star rebate finder.", "Take shorter " +
                "showers. Shaving off even one or two minutes can lead to impressive savings. If you’re feeling particularly " +
                "eco- or budget-friendly, you can try the “Navy shower,” in which you shut off the water while " +
                "soaping up and turning it back on to rinse off.", "Water your garden early in the day when it's " +
                "cooler! This will avoid excess evaporation. If you really want to cut down on watering, you can " +
                "use layers of mulch to protect the soil's moisture.", "Collect rain water. You can use a water " +
                "cistern or any large container to capture rainwater and use that water to hydrate your lawn and " +
                "garden."};
        public String[] g_tips = {"Keep your doors and windows closed as much as possible during the winter to keep" +
                "your home warm and cozy!", "Water heaters can be real gas hogs. Try using an insulation jacket" +
                "to help reduce its gas usage.", "Block your chimney in the winter when you are not using your fireplace" +
                "to keep warmth inside.", "Use the fireplace in your home whenever possible in the winter. This will " +
                "help keep the gas bill down. Plus, smores!", "Planting trees on the sunny side of your home is a " +
                "great way to create some shade, keep your house cooler, and keep the Earth's natural " +
                "carbon-cleaners in business."};
        public String[] e_tips = {"Unplug unused electronics. Standby power can account for 10% of an " +
                "average household's annual electricity use.", "Keep your oven closed. Every time you open your oven door," +
                " the internal temperature can drop 25 degrees. Then, your oven has to use more electricity to bring the " +
                "temperature back up. To save electricity, peek through the window and rely on your oven's light instead" +
                " of opening the door.", "Go solar. You may not be able to convert your whole home to solar power, " +
                "but there are a bunch of small ways you can incorporate it into your life.", "Try washing your " +
                "clothes at roughly 80 degrees Fahrenheit or below. According to the Energy Saving Trust, washing your clothes at lower temperatures can " +
                "save roughly 40% of the electricity used compared to using the highest temperature setting. " +
                "No worries, stain remover still works on lower settings.", "Next time a light bulb goes out, replace " +
                "it with an LED one! Incandescent lightbulbs waste 90% of the electricity used to power them, while " +
                "LED lights only waste roughly 50% of their electrical energy. They also last three times as long!"};

        // Returns three applicable water tips for current user.
        public List<string> water_tips(YOUtilityUser user)
        {
            List<String> tips = new List<String>();
            Random ran = new Random();
            if (user.garden && user.lawn)
            {
                tips.Add(w_tips[ran.Next(3)]);
                tips.Add(w_tips[3]);
                tips.Add(w_tips[4]);

            }
            else if (user.garden)
            {
                tips.Add(w_tips[2]);
                tips.Add(w_tips[0]);
                tips.Add(w_tips[3]);
            }
            else if (user.lawn)
            {
                tips.Add(w_tips[1]);
                tips.Add(w_tips[0]);
                tips.Add(w_tips[4]);
            }
            else
            {
                tips.Add(w_tips[0]);
                tips.Add(w_tips[1]);
                tips.Add(w_tips[2]);
            }
            return tips;
        }

        // Returns three applicable gas tips for current user.
        public List<string> gas_tips(YOUtilityUser user)
        {
            List<String> tips = new List<String>();
            Random ran = new Random();
            if (user.fireplace && user.trees)
            {
                tips.Add(g_tips[ran.Next(3)]);
                tips.Add(g_tips[3]);
                tips.Add(g_tips[4]);

            }
            else if (user.fireplace)
            {
                tips.Add(g_tips[2]);
                tips.Add(g_tips[0]);
                tips.Add(g_tips[3]);
            }
            else if (user.trees)
            {
                tips.Add(g_tips[1]);
                tips.Add(g_tips[0]);
                tips.Add(g_tips[4]);
            }
            else
            {
                tips.Add(g_tips[0]);
                tips.Add(g_tips[1]);
                tips.Add(g_tips[2]);
            }
            return tips;
        }

        // Returns three applicable electricity tips for current user.
        public List<string> electricity_tips(YOUtilityUser user)
        {
            List<String> tips = new List<String>();
            Random ran = new Random();
            if (user.washer && user.led)
            {
                tips.Add(e_tips[ran.Next(3)]);
                tips.Add(e_tips[3]);
                tips.Add(e_tips[4]);

            }
            else if (user.washer)
            {
                tips.Add(e_tips[2]);
                tips.Add(e_tips[0]);
                tips.Add(e_tips[3]);
            }
            else if (user.led)
            {
                tips.Add(e_tips[1]);
                tips.Add(e_tips[0]);
                tips.Add(e_tips[4]);
            }
            else
            {
                tips.Add(e_tips[0]);
                tips.Add(e_tips[1]);
                tips.Add(e_tips[2]);
            }
            return tips;
        }
    }
}

        /*
         * Returns list of newly accomplished achievements. User is updated, and if no new achievements
         * were made, then empty list is returned.
        public async Task<List<string>> check_basic(Bill bill, YOUtilityUser user)
        {
            if (user.Achievements == null || containsSeeds(user))
            {
                user.Achievements = "";
            }
            else if (check_earned_achievements(user))
            {
                return new List<string>();
            }   

            final_string = new StringBuilder(user.Achievements); // DB result
            List<string> earned = new List<string>(); // Program result

            List<int> user_bills_int = bill_id_parser(bill, user.BillIds);
            List<Bill> user_bills = get_bills(user_bills_int);
            if (!starter)
            {
                earned.Add(check_starter());
            }
            if (!warrior)
            {
                earned.Add(check_warrior(user_bills));
            }
            if (!blue)
            {
                earned.Add(check_blue(bill, user_bills));
            }
            if (!shock)
            {
                earned.Add(check_shock(bill, user_bills));
            }
            if (!gas)
            {
                earned.Add(check_gas(bill, user_bills));
            }
            user.Achievements = final_string.ToString();
            await _um.UpdateAsync(user);
            return earned.Where(x => x != "").ToList();
        }

        private string check_starter()
        {
            final_string.Append("Eco Starter ");
            return "Eco Starter";
        }

        private string check_warrior(List<Bill> user_bills)
        {
            if (user_bills.Count >= 10)
            {
                final_string.Append("Eco Warrior ");
                return "Eco Warrior";
            }
            return "";
        }

        private string check_blue(Bill bill, List<Bill> user_bills)
        {
            if (user_bills.Count == 0 || bill.utility != Bill.Type.water)
            {
                return "";
            }
            List<Bill> water = user_bills.Where(b => b.utility == Bill.Type.water).ToList();
            water.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
            if (water.Count == 0) { return ""; }
            if (bill.consumed < water[water.Count-1].consumed)
            {
                final_string.Append("Baby Blue ");
                return "Baby Blue";
            }
            return "";
        }

        private string check_shock(Bill bill, List<Bill> user_bills)
        {
            if (user_bills.Count == 0 || bill.utility != Bill.Type.electricity)
            {
                return "";
            }
            List<Bill> electricity = user_bills.Where(b => b.utility == Bill.Type.electricity).ToList();
            electricity.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
            if (electricity.Count == 0) { return ""; }
            if (bill.consumed < electricity[electricity.Count-1].consumed)
            {
                final_string.Append("Shock Jockey ");
                return "Shock Jockey";
            }
            return "";
        }

        private string check_gas(Bill bill, List<Bill> user_bills)
        {
            if (user_bills.Count == 0 || bill.utility != Bill.Type.gas)
            {
                return "";
            }
            List<Bill> gas = user_bills.Where(b => b.utility == Bill.Type.gas).ToList();
            gas.Sort((b1, b2) => DateTime.Compare(b1.endingPeriod, b2.endingPeriod));
            if (gas.Count == 0) { return ""; }
            if (bill.consumed < gas[gas.Count-1].consumed)
            {
                final_string.Append("Gas Mask ");
                return "Gas Mask";
            }
            return "";
        }

        // once scanner works, can be implemented
        public async Task<bool> check_tech(YOUtilityUser user, UserManager<YOUtilityUser> _um)
        {
            if (user.Achievements.Contains("Green Tech"))
            {
                return false;
            }
            else
            {
                user.Achievements = user.Achievements + "Green Tech ";
                await _um.UpdateAsync(user);
                return true;
            }
        }

        // Checks to see which achievements have already been earned. If they have all been earned,
        // returns true.
        public bool check_earned_achievements(YOUtilityUser user)
        {
            int count = 0;
            if (user.Achievements.Contains("Eco Starter"))
            {
                starter = true;
                count++;
            }
            if (user.Achievements.Contains("Eco Warrior"))
            {
                warrior = true;
                count++;
            }
            if (user.Achievements.Contains("Baby Blue"))
            {
                blue = true;
                count++;
            }
            if (user.Achievements.Contains("Shock Jockey"))
            {
                shock = true;
                count++;
            }
            if (user.Achievements.Contains("Gas Mask"))
            {
                gas = true;
                count++;
            }
            if (count == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<int> bill_id_parser(Bill newest_bill, string bill_ids)
        {
            List<int> ids = new List<int>();
            if (!bill_ids.Equals(""))
            {
                String[] bills = bill_ids.Split(' ');
                foreach (string id in bills)
                {
                    if (!id.Equals("") && !id.Equals(" "))
                    {
                        try
                        {
                            int temp = int.Parse(id);
                            if (temp == newest_bill.ID)
                            {
                                continue; // Only add bills that are not newest bill
                            }
                            ids.Add(temp);
                        }
                        catch (Exception e)
                        {
                            string s = e.Message;
                            continue;
                        }
                    }
                }
            }
            return ids;
        }

        private List<Bill> get_bills(List<int> ids)
        {
            List<Bill> bills = new List<Bill>();
            foreach (int id in ids)
            {
                Bill b = _context.Bill.Find(id);
                if (b!=null)
                {
                    bills.Add(b);
                }
            }
            return bills;
        }

        private bool containsSeeds(YOUtilityUser user)
        {
            string achievements = user.Achievements;
            if (achievements.Equals("Water Power!, Big saver!, 3 month user!"))
            {
                return true;
            }
            if (achievements.Equals("Conservationist of the year 2020!, Electricity Power User!"))
            {
                return true;
            }
            if (achievements.Equals("No achievements earned."))
            {
                return true;
            }
            return false;
        }

        public List<String> pub_check_earned(YOUtilityUser user)
        {
            List <String> result = new List<String>();
            if (user.Achievements == null)
            {
                return result;
            }
            if (user.Achievements.Contains("Eco Starter"))
            {
                result.Add(Eco_starter.Title);
                result.Add(Eco_starter.Description);
            }
            if (user.Achievements.Contains("Eco Warrior"))
            {
                result.Add(Eco_warrior.Title);
                result.Add(Eco_warrior.Description);
            }
            if (user.Achievements.Contains("Baby Blue"))
            {
                result.Add(Baby_blue.Title);
                result.Add(Baby_blue.Description);
            }
            if (user.Achievements.Contains("Shock Jockey"))
            {
                result.Add(Shock_jockey.Title);
                result.Add(Shock_jockey.Description);
            }
            if (user.Achievements.Contains("Gas Mask"))
            {
                result.Add(Gas_mask.Title);
                result.Add(Gas_mask.Description);
            }
            if (user.Achievements.Contains("Green Tech"))
            {
                result.Add(Green_tech.Title);
                result.Add(Green_tech.Description);
            }
            return result;
        }
    }
}
        */