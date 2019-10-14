using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Assignment_1.Models.SendGrid;
using System.Threading.Tasks;

namespace Assignment_1.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult EmailNotify()
        {
            SendGridClass SendGridClass0bj =new SendGridClass();
            //Task t = Task.Factory.StartNew(SendGridClass0bj.Execute());
            SendGridClass0bj.Execute();
           // Exe
            //SendGridClass

            




            return View();
        }

       
    }
}