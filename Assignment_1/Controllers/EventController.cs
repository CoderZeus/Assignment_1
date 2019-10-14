using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Assignment_1.Models;
using System.IO;

namespace Assignment_1.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        // GET: Event
        public EventModel context = new EventModel();
        public ActionResult Index()
        {
            
            return View(context.EventList.ToList());
        }
       
        public ActionResult CreateEvent()
        {
            Event model = new Event();
            return View(model);
        }
        
        [HttpPost]
        
        public ActionResult CreateEvent(Event model)
        {
            model.Id = context.EventList.Count()+1;
            model.OwnerId = User.Identity.Name;
            
            context.EventList.Add(model);
            context.SaveChangesAsync();
            return RedirectToAction("Index", "Event");
            return View();
        }        
        public ActionResult DetailView(int id)
        {
            Event option = context.EventList.Find(id);
            if(option!=null)
            {
                return View(option);
            }
            return View();
        }

        public ActionResult UnauthorisedView()
        {
            return View();
        }


        public ActionResult EditEvent(int id)
        {
            Event option = context.EventList.Find(id);
            if (option != null)
            {
                return View(option);
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditEvent(Event ev)
        {
            
            if (ev != null)
            {
                if(ev.OwnerId== User.Identity.Name)
                {
                    // context.EventList.Add(ev);
                    context.Entry(ev).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChangesAsync();
                    return RedirectToAction("Index", "Event");
                    return View(ev);
                }
                else
                {
                    return RedirectToAction("UnauthorisedView", "Event");
                    return View();
                }
                
            }
            return View();
        }

        public ActionResult MapView()
        {
            
            return View();
        }

        public ActionResult Booking(int id)
        {
            Event option = context.EventList.Find(id);
            if (option != null)
            {
                if (option.SeatCount < option.MaxSeatCount)
                {
                    option.SeatCount = option.SeatCount + 1;
                    context.Entry(option).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChangesAsync();

                    CreatePdf(option);
                    return RedirectToAction("Index", "Event");
                    //return RedirectToAction("CreatePdf", "Event");
                }
            }
            return View();
        }
/*
        //[HttpPost]
        public void Booking(Event option)
        {
            //Event option = context.EventList.Find(id);
            if (option != null)
            {
                if(option.SeatCount< option.MaxSeatCount)
                {
                    option.SeatCount = option.SeatCount + 1;
                    context.Entry(option).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChangesAsync();

                    CreatePdf(option);
                    //return RedirectToAction("CreatePdf", "Event");
                }
               
               // return View(option);
            }
           // return View();
        }
*/
        public ActionResult CreatePdf(int id)
        {
            Event option = context.EventList.Find(id);
            if (option != null)
            {
                return View(option);
            }
            return View();
        }
        //[HttpPost]
        public void CreatePdf(Event ev)
        {
            
            context.Entry(ev).State = System.Data.Entity.EntityState.Modified;
            context.SaveChangesAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                Paragraph p = new Paragraph();
                p.Add("Booking confirmed for " + ev.EventName + "with ordernumber " + ev.SeatCount);

                document.Open();
                document.Add(p);
                document.Close();
                writer.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition",
                "attachment;filename="+ev.EventName+".pdf");
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }
            /*
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                PdfPen pen = new PdfPen(new PdfColor(0));
                
                PointF fpoint = new PointF(0, 0);
                //Draw the text
                graphics.DrawString("Hello World!!!", font, pen,0,0);

                MemoryStream stream = new MemoryStream();
                
                //document.Save(stream);
                document.Save("Output.pdf");
                

                // Open the document in browser after saving it
                // document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }
            */
            //return View();
        }
    }
}