﻿using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhoneBook.Services
{
    public class EmailService
    {
        public static void SendEmail(User user, ControllerContext ctx)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("comfirm@phonebook.com");
            mail.To.Add(user.Email);
            mail.Subject = "Confirm your registration";

            string parameters = "?userID=" + user.ID + "&key=" + user.Password;

            var path = ctx.HttpContext.Request.Url.Host; 
            var port = ctx.HttpContext.Request.Url.Port;

            mail.Body = "Dear " + user.Username + ", click http://" + path + ":" + port + "/Account/Confirm" + parameters + " to confirm your account registration!";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("phonebook.pro@gmail.com", "phonebook");

            smtp.Send(mail);
        }
    }
}