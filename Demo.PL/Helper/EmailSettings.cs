using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email) 
        { 
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("hossammostafa1009@gmail.com", "oqzyddcmqdgjmikx"); // l mfrod nktb l data de f appSetting with Eng Maha
            client.Send("hossammostafa1009@gmail.com", email.To, email.Subject, email.Body);

        }

    }
}
