using MailKit.Net.Smtp; //[NOTE: for email]
using MimeKit;  //[NOTE: for email]
//using Microsoft.AspNetCore.Http;

using AccountingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;

namespace AccountingService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactSend(string subject, string contact, string name, string email, string message)
        {
            SendMail(subject,contact, name, email, message);
            return Redirect("Index");
        }
        
        public void SendMail(string subject,string contact, string name, string email, string messageBody)
        {
            // create email message
            string username = "testingsamplemail1530@gmail.com";
            string password = "fariha12345$";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, email));
            message.To.Add(new MailboxAddress("Fariha", "testingsamplemail1530@gmail.com"));
            message.Subject = subject;
            message.Body = new TextPart("plain") {
                Text = messageBody + Environment.NewLine + email + Environment.NewLine + contact,
            };
           
            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate(username, password);

                smtp.Send(message);
                smtp.Disconnect(true);
            };
            

        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
