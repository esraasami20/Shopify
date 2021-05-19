using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shopify.Helper
{
    public class EmailHelper
    {
        static public bool SendEmail(string userEmail)
        {

            MailMessage mail = new MailMessage();

            mail.To.Add(userEmail);
            mail.From = new MailAddress("amr25111997@gmail.com");
            mail.Subject = "Confirmation";
            mail.IsBodyHtml = true;
            mail.Body = "<a href=http://www.google.com"+"?email=" + userEmail + ">click here to reset password</a>";

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("amr97ayman69@gmail.com", "mnia1997"); // Enter seders User name                                                                                                                     and password  
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
