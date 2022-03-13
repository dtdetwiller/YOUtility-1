using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOUtility.Models;
using YOUtility.Data;


namespace YOUtility.Areas.Identity.Data
{
    public class SeedUsersDB
    {
        public static void Seed(
            UsersDB context,
            UserManager<YOUtilityUser> userManager)
        {
            //if (context.Database.EnsureCreated())
            //{
            //    return;
            //}

            if (userManager.Users.ToArray().Count() != 0)
              return;

            YOUtilityUser john_smith = new YOUtilityUser
            {
                Email = "john_smith@gmail.com",
                UserName = "john_smith",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "4 5 6",
                Address = "1044 E 400 S, Salt Lake City, UT",
                ProfilePic = "../../images/youtility_emblem.png",
                Biography = "My name is John Smith. My name might be generic, " +
                "but nothing about my desire to save the world is!",
                Achievements = "Water Power!, Big saver!, 3 month user!",

                Goals = "Save $300 on electricity this year., Reduce costs by 20% over the next 5 years.",
                JoinDate = DateTime.Parse("2019-09-01")
            };

            userManager.CreateAsync(john_smith, "123ABC!@#def").Wait();

            YOUtilityUser FarmerJoe = new YOUtilityUser
            {
                Email = "FarmerJoe@gmail.com",
                UserName = "FarmerJoe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "1 2 3",
                Address = "380 S 400 E, Salt Lake City, UT",
                ProfilePic = "../../images/youtility_emblem.png",
                Biography = "Howdy y'all, I'm Farmer Joe. Here to figure out how " +
                "to save money on watering my crops.",
                Achievements = "Conservationist of the year 2020!, Electricity Power User!",
                Goals = "Conservationist of the year 2021., Save 40% of watering costs in 2021.",
                JoinDate = new DateTime(2018, 11, 2, 4, 23, 28)

            };

            userManager.CreateAsync(FarmerJoe, "123ABC!@#def").Wait();

            YOUtilityUser waterhater = new YOUtilityUser
            {
                Email = "waterhater@gmail.com",
                UserName = "water_hater",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "232 University St E, Salt Lake City, UT",
                ProfilePic = "../../images/youtility_emblem.png",
                Biography = "I really hate water. I'm here" +
                "so that others can see how much I hate water using Youtility's" +
                "cool mapping tool.",
                Achievements = "No achievements earned.",
                Goals = "Speed up global warming by 20%.",
                JoinDate = DateTime.Parse("2015-10-10")

            };

            userManager.CreateAsync(waterhater, "123ABC!@#def").Wait();

            YOUtilityUser olduser = new YOUtilityUser
            {
                Email = "olduser@gmail.com",
                UserName = "olduser",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "1375 Presidents' Cir, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "I have been using youtility for a full year and cannot love it more!",
                Achievements = "No achievements earned.",
                Goals = "Decrease global warming by 20%",
                JoinDate = DateTime.Parse("2020-12-01")
            };

            userManager.CreateAsync(olduser, "123ABC!@#def").Wait();

            YOUtilityUser tom_brady = new YOUtilityUser
            {
                Email = "tom_brady@gmail.com",
                UserName = "tom_brady",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "220 S Banks Ct, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "My name is Tom Brady and I like football.",
                Achievements = "No achievements earned.",
                Goals = "Decrease pollution by 50%",
                JoinDate = DateTime.Parse("2010-12-01")
            };

            userManager.CreateAsync(tom_brady, "123ABC!@#def").Wait();

            YOUtilityUser slimreaper = new YOUtilityUser
            {
                Email = "slimreaper@gmail.com",
                UserName = "sl1mr34p3r",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "358 S Douglas St, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "My name is Kevin Durant.",
                Achievements = "No achievements earned.",
                Goals = "Play Basketball.",
                JoinDate = DateTime.Parse("2018-12-01")
            };

            userManager.CreateAsync(slimreaper, "123ABC!@#def").Wait();

            YOUtilityUser greekfreak = new YOUtilityUser
            {
                Email = "greekfreak@gmail.com",
                UserName = "GreekFreak",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "273 S 1300 E, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "I am Giannis",
                Achievements = "No achievements earned.",
                Goals = "Man we just out here livin.",
                JoinDate = DateTime.Parse("2011-11-11")
            };

            userManager.CreateAsync(greekfreak, "123ABC!@#def").Wait();

            YOUtilityUser aaron = new YOUtilityUser
            {
                Email = "aaron@gmail.com",
                UserName = "aaron",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "47 S 800 E, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "aaron",
                Achievements = "No achievements earned.",
                Goals = "aaron",
                JoinDate = DateTime.Parse("2011-11-11")
            };

            userManager.CreateAsync(aaron, "123ABC!@#def").Wait();

            YOUtilityUser matty_ice = new YOUtilityUser
            {
                Email = "matty_ice@gmail.com",
                UserName = "matty_ice",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "1371 E 2nd Ave, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "Super Bowl Champion.",
                Achievements = "No achievements earned.",
                Goals = "Win the next superbowl.",
                JoinDate = DateTime.Parse("2014-12-12")
            };

            userManager.CreateAsync(matty_ice, "123ABC!@#def").Wait();

            YOUtilityUser yoMama = new YOUtilityUser
            {
                Email = "yomama@gmail.com",
                UserName = "yomama",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "463 E 6th Ave, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "Yo mama so fat...",
                Achievements = "No achievements earned.",
                Goals = "Come up with better jokes.",
                JoinDate = DateTime.Parse("2016-08-13")
            };

            userManager.CreateAsync(yoMama, "123ABC!@#def").Wait();

            YOUtilityUser ponyboy = new YOUtilityUser
            {
                Email = "ponyboy@gmail.com",
                UserName = "ponyboy",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

                BillIds = "",
                Address = "463 E 6th Ave, Salt Lake City, UT",
                ProfilePic = "../../images/jim_p.png",
                Biography = "All I do is stay gold.",
                Achievements = "No achievements earned.",
                Goals = "Come up with better jokes.",
                JoinDate = DateTime.Parse("2016-08-13")
            };

            userManager.CreateAsync(ponyboy, "123ABC!@#def").Wait();
        }
    }
}