using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace HospitalServerManager.Model.Controllers
{
	class SmtpMailSender
	{
		private MailAddress userEmail;
		SmtpClient emailClient = new SmtpClient("poczta.o2.pl", 587);
		public SmtpMailSender()
		{
			// TODO: Więcej interfejsów !
			emailClient.Credentials = new NetworkCredential("emailforapplication@o2.pl", "emailforapp");
			userEmail = new MailAddress("emailforapplication@o2.pl");
			emailClient.UseDefaultCredentials = false;
		}
		public SmtpMailSender(string userName, string password)
		{
			userEmail = new MailAddress(userName);
			emailClient.EnableSsl = true;
			emailClient.Credentials = new NetworkCredential(userName, password);
		}
		public async Task SendEmailAsync(string sendTo, string textBody, string subject)
		{
			// TODO: Dodać walidację !
			MailMessage mailMessage = new MailMessage(userEmail, new MailAddress(sendTo));
			mailMessage.Body = textBody;
			mailMessage.Subject = subject;
			emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			await SendEmailAsync(mailMessage);
		}
		public async Task SendEmailAsync(MailMessage completeMailMessage)
		{
			// TODO: Dodać walidację !
			emailClient.SendCompleted += EmailClient_SendCompleted;
			await emailClient.SendMailAsync(completeMailMessage);
		}

		private void EmailClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			// TODO: Dodać Cancell
			var token = e.UserState;
			if (e.Error != null)
			{
				new MessageDialog("ERRORS : " + e.Error.ToString());
			}
			else
			{
				new MessageDialog("EMAIL SEND : " + token.ToString());
			}
		}
	}
}
