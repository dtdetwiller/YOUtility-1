using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOUtility.Areas.Identity.Data;
using YOUtility.Models;

/*
Author: Mateo Lopez
Partner: None
Date: 9/24/20
Course: CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Mateo Lopez - This work may not be copied for use in Academic Coursework.

I, Mateo Lopez, certify that I wrote this code from scratch and did not copy it in part or whole from
another source. Any references used in the completion of the assignment are cited in my README file.

File Contents
    This code below creates data for the database when it is first generated.
*/
namespace YOUtility.Data
{
    public class DBInitializer
    {
        public static void Initialize(YOUtilityContext context, UserManager<YOUtilityUser> um)
        {
            if (!context.Bill.Any())
            {
                var bills = Seeder.create_Bill_Seed();

                context.Bill.AddRange(bills);
                context.SaveChanges();
                Seeder.updateUsers(context, um);
            }

            
            try
            {
                YOUtilityUser olduser = um.FindByNameAsync("olduser").Result;
                if (olduser.BillIds.Equals(""))
                {
                    var bills = Seeder.create_olduser_seed();
                    context.Bill.AddRange(bills);
                    context.SaveChanges();
                    foreach (Bill b in bills)
                    {
                        olduser.BillIds = olduser.BillIds + " " + b.ID;
                    }
                    um.UpdateAsync(olduser).Wait();
                }
            }
            catch (Exception e)
            {
                String nothing = e.Message;
            }
        }
    }
}