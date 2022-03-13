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
    public class Achievements
    {
        public struct Achievement
        {
            public int Num;
            public string Title;
            public string Description;

            public Achievement(int N, string t, string des)
            {
                Num = N;
                Title = t;
                Description = des;
            }
        }

        public YOUtilityContext _context;
        public UserManager<YOUtilityUser> _um;
        public StringBuilder final_string;
        public bool starter = false;
        public bool warrior = false;
        public bool tech = false;
        public bool blue = false;
        public bool shock = false;
        public bool gas = false;
        public Achievement Eco_starter = new Achievement(1, "Eco Starter", "Upload any utility bill!");
        public Achievement Eco_warrior = new Achievement(2, "Eco Warrior", "Upload more than 10 utility bills!");
        public Achievement Green_tech = new Achievement(3, "Green Tech", "Use the automatic upload.");
        public Achievement Baby_blue = new Achievement(4, "Baby Blue", "Save water from one month to the next.");
        public Achievement Shock_jockey = new Achievement(5, "Shock Jockey", "Save electricity from one month to the next.");
        public Achievement Gas_mask = new Achievement(6, "Gas Mask", "Save gas from one month to the next.");

        public Achievements(YOUtilityContext context, UserManager<YOUtilityUser> um)
        {
            _context = context;
            _um = um;
        }

        /*
         * Returns list of newly accomplished achievements. User is updated, and if no new achievements
         * were made, then empty list is returned.
         */
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