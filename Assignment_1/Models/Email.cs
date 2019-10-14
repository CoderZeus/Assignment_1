using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations; //added eextra
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;

// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using SendGrid;
using SendGrid.Helpers.Mail;
//using System;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_1.Models
{
    public class Email
    {
        [DataType(DataType.EmailAddress), Display(Name = "To")]
        [Required]
        public string ToEmail { get; set; }
        [Display(Name = "Body")]
        [DataType(DataType.MultilineText)]
        public string EMailBody { get; set; }
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "CC")]
        public string EmailCC { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "BCC")]
        public string EmailBCC { get; set; }


        public static Message SendMessage(GmailService service, String userId, Message email)
        {
            try
            {
                return service.Users.Messages.Send(email, userId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }

    }    

namespace SendGrid
    {
        class SendGridClass
        {
            public  async Task<int> Execute()
            {
                //var apiKey = "SG.Pp-Le_UhRH2lWPv9wiZpXQ.XAmFCOpsKbPIIxnSY_fAVONnZJTvJkAterIH7NLCuQE_old";
                var apiKey = "SG.i8-HjOgQSRS5T1RT5RHD2Q.9FnY6CvXO0oWiCL2mkXY1IT2wxogUB9MUOFB_VHpORU_changed";
                //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("test@example.com", "Example User");
                var subject = "New Login from Device";
                var to = new EmailAddress("sharathkottalil@gmail.com", "Example User");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var bytes = File.ReadAllBytes("A.pdf");
                // var file = Convert.ToBase64String(bytes);
                var fileStream = File.OpenRead("D:\\Course\\FIT5032-C#\\Assignment\\Code\\Assignment_1\\Assignment_1\\App_Data\\A.pdf") ;
               
                 msg.AddAttachmentAsync("A.pdf", fileStream);
                
                    var response = await client.SendEmailAsync(msg);

                return 1;
            }
        }
    }


}
