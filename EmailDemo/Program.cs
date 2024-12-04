using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using System.Text;


var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com", 587)
{
    EnableSsl = true,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential("ahmad26.2.2002.jerjees@gmail.com", "My App Password")
}); 
    
var emailBodyTemplate = new StringBuilder();
emailBodyTemplate.AppendLine("Dear @Model.FirstName,");
emailBodyTemplate.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it!<p>");
emailBodyTemplate.AppendLine("-The JerCo Team");

Email.DefaultSender = sender;
Email.DefaultRenderer = new RazorRenderer();

var email = await Email
    .From("ahmad26.2.2002.jerjees@gmail.com")
    .To("dutchjer99@gmail.com", "Dutch")
    .Subject("Thanks!")
    .UsingTemplate(emailBodyTemplate.ToString(),new {FirstName = "Ahmad", ProductName = "test"})
    //.Body("Thanks for buying my product.")
    .SendAsync();

if (email.Successful)
{
    Console.WriteLine("Email sent successfully!");
}
else
{
    Console.WriteLine($"Failed to send email: {string.Join(", ", email.ErrorMessages)}");
}