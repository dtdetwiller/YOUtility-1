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
    public class Seeder
    {
        public static Bill[] create_Bill_Seed()
        {
            var bills = new Bill[]
           {
                new Bill{utility=Bill.Type.water, due=20, consumed=2640, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.gas, due=40, consumed=16, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.electricity, due=60, consumed=600, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.water, due=15, consumed=15, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.gas, due=60, consumed=3020, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.electricity, due=45, consumed=1000, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
           };
            return bills;
        }

        public static Bill[] create_olduser_seed()
        {
            var bills = new Bill[]
            {
                // three sequential months of gas bills
                new Bill{utility=Bill.Type.gas, due=20.05, consumed=12.4, uploadTime=DateTime.Parse("2021-8-22"), beginningPeriod=DateTime.Parse("2021-7-1"), endingPeriod=DateTime.Parse("2021-7-31")},
                new Bill{utility=Bill.Type.gas, due=40.63, consumed=16.6, uploadTime=DateTime.Parse("2021-9-24"), beginningPeriod=DateTime.Parse("2021-8-1"), endingPeriod=DateTime.Parse("2021-8-31")},
                new Bill{utility=Bill.Type.gas, due=60.02, consumed=18.3, uploadTime=DateTime.Parse("2021-10-25"), beginningPeriod=DateTime.Parse("2021-9-01"), endingPeriod=DateTime.Parse("2021-9-30")},

                
                // three spread out months of electricity bills
                new Bill{utility=Bill.Type.electricity, due=55.01, consumed=1200.53, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-1-01"), endingPeriod=DateTime.Parse("2021-1-31")},
                new Bill{utility=Bill.Type.electricity, due=50.80, consumed=1100.42, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-4-01"), endingPeriod=DateTime.Parse("2021-4-30")},
                new Bill{utility=Bill.Type.electricity, due=45.93, consumed=1000.33, uploadTime=DateTime.Parse("2021-11-22"), beginningPeriod=DateTime.Parse("2021-6-01"), endingPeriod=DateTime.Parse("2021-6-30")},

                // a year's worth of water bills
                new Bill{utility=Bill.Type.water, due=60.34, consumed=2640.87, uploadTime=DateTime.Parse("2020-2-2"), beginningPeriod=DateTime.Parse("2020-1-01"), endingPeriod=DateTime.Parse("2021-1-31")},
                new Bill{utility=Bill.Type.water, due=63.45, consumed=2700.34, uploadTime=DateTime.Parse("2020-3-3"), beginningPeriod=DateTime.Parse("2020-2-01"), endingPeriod=DateTime.Parse("2021-2-28")},
                new Bill{utility=Bill.Type.water, due=46.56, consumed=2300.44, uploadTime=DateTime.Parse("2020-4-4"), beginningPeriod=DateTime.Parse("2020-3-01"), endingPeriod=DateTime.Parse("2021-3-31")},
                new Bill{utility=Bill.Type.water, due=56.78, consumed=2450.69, uploadTime=DateTime.Parse("2020-5-5"), beginningPeriod=DateTime.Parse("2020-4-01"), endingPeriod=DateTime.Parse("2021-4-30")},
                new Bill{utility=Bill.Type.water, due=58.88, consumed=2489.77, uploadTime=DateTime.Parse("2020-6-3"), beginningPeriod=DateTime.Parse("2020-5-01"), endingPeriod=DateTime.Parse("2021-5-31")},
                new Bill{utility=Bill.Type.water, due=66.78, consumed=2798.33, uploadTime=DateTime.Parse("2020-7-2"), beginningPeriod=DateTime.Parse("2020-6-01"), endingPeriod=DateTime.Parse("2021-6-30")},
                new Bill{utility=Bill.Type.water, due=74.86, consumed=2934.21, uploadTime=DateTime.Parse("2020-9-3"), beginningPeriod=DateTime.Parse("2020-7-01"), endingPeriod=DateTime.Parse("2021-7-31")},
                new Bill{utility=Bill.Type.water, due=50.67, consumed=2379.33, uploadTime=DateTime.Parse("2020-9-3"), beginningPeriod=DateTime.Parse("2020-8-01"), endingPeriod=DateTime.Parse("2021-8-31")},
                new Bill{utility=Bill.Type.water, due=44.56, consumed=2298.87, uploadTime=DateTime.Parse("2020-10-1"), beginningPeriod=DateTime.Parse("2020-9-01"), endingPeriod=DateTime.Parse("2021-9-30")},
                new Bill{utility=Bill.Type.water, due=40.82, consumed=2270, uploadTime=DateTime.Parse("2020-11-4"), beginningPeriod=DateTime.Parse("2020-10-01"), endingPeriod=DateTime.Parse("2021-10-31")},
                new Bill{utility=Bill.Type.water, due=38.90, consumed=2130.70, uploadTime=DateTime.Parse("2020-12-6"), beginningPeriod=DateTime.Parse("2020-11-01"), endingPeriod=DateTime.Parse("2021-11-30")},
                new Bill{utility=Bill.Type.water, due=37.67, consumed=2012.45, uploadTime=DateTime.Parse("2021-1-3"), beginningPeriod=DateTime.Parse("2020-12-01"), endingPeriod=DateTime.Parse("2021-12-31")},
                };
            return bills;
        }

        // Updates all users if DB is dropped to maintain a clean BillID record.
        public static void updateUsers(YOUtilityContext context, UserManager<YOUtilityUser> um)
        {
            List<YOUtilityUser> users = um.Users.ToList();
            foreach (YOUtilityUser user in users) {
                if (user != null)
                {
                    StringBuilder result = new StringBuilder("");
                    string billids = user.BillIds.Trim();
                    if (!billids.Equals(""))
                    {
                        String[] string_ids = billids.Split(' ');
                        foreach (string id in string_ids)
                        {
                            if (!id.Equals("") && !id.Equals(" "))
                            {
                                try
                                {
                                    if (int.Parse(id) <= 6 && int.Parse(id) > 0)
                                    {
                                        result.Append(int.Parse(id) + " ");
                                    }
                                }
                                catch(Exception e)
                                {
                                    string m = e.Message;
                                }
                            }
                        }
                    }
                    user.BillIds = result.ToString();
                    um.UpdateAsync(user).Wait();
                }               
            }
        }



    }
}
