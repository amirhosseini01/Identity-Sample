using System.Net;
using System.Net.Mail;

namespace Identity_Sample.Areas.Identity.Helper;

public static class EmailSender
{
    public static void SendFromGmail()
    {
        var fromAddress = new MailAddress("from@gmail.com", "From Name");
        var toAddress = new MailAddress("to@example.com", "To Name");
        const string fromPassword = "fromPassword";
        const string subject = "Subject";
        const string body = "Body";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        try
        {
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
        catch (System.Exception)
        {
            //todo: log error here
            throw;
        }
    }
}