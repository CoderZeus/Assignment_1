using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Assignment_1.Models;
using System.Data.Entity;

namespace Assignment_1.Controllers
{
    [Authorize(Roles = "Admin")]

    
    public class AdminController : Controller
    {
        // GET: Admin
        public ApplicationDbContext dbContext = ApplicationDbContext.Create(); //new ApplicationDbContext();
        public ActionResult AdminView()
        {
            //Assignment_1.Models.ApplicationUser applicationUser = applicationUser.Roles.ToList();

           
            var roles = dbContext.Roles.ToList();


            var Users = dbContext.Users.Include(u => u.Roles);
            var users = dbContext.Users.ToList();
            //Console.WriteLine(users.ToString());
            List<UserDetails> details = new List<UserDetails>();
            /*
            var adminUsers = from user in dbContext.AspNetUsers
                             join userRole in dbContext.AspNetUserRoles on user.Id equals userRole.UserId
                             join role in dbContext.AspNetRoles on userRole.RoleId equals role.Id
                             where role.Name == "Admin"
                             select user
                             */
            //var data = from user in users
            //         join role in roles on user.Id equals role.UserId
            /*
            foreach (var u in users)
            {
                foreach (var r in )
                {
                   
                }
            }
            
            return View(details);
            */
            return View(users);
        }
    }
}