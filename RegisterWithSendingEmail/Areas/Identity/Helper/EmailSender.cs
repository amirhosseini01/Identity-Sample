using System.Net;
using System.Net.Mail;

namespace Identity_Sample.Areas.Identity.Helper;

public static class EmailSender
{
    public static void SendFromGmail(SendEmailInput input)
    {
        var fromAddress = new MailAddress("from@gmail.com", "From Name");
        var toAddress = new MailAddress(input.ReciverEmail, input.ReciverName);
        const string fromPassword = "*fromPass";
        string subject = input.Subject;
        string body = input.Body;

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
                Body = body,
                IsBodyHtml = true
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
public record SendEmailInput(string ReciverEmail, string ReciverName, string Subject, string Body);