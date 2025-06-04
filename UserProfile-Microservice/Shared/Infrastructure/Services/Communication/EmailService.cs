using Azure;
using Azure.Communication.Email;

namespace DittoBox.API.Shared.Infrastructure.Services.Communication
{
	public class EmailService : IEmailService
	{
		private readonly EmailClient _emailClient;

		public EmailService()
		{
			var emailConnectionString = Environment.GetEnvironmentVariable("EMAIL_CONNSTR");
			_emailClient = new EmailClient(emailConnectionString);
		}

		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var emailMessage = new EmailMessage(
				senderAddress: "DoNotReply@766ba3f8-fb03-4cc0-a24a-0c4a23c3e476.azurecomm.net",
				content: new EmailContent(subject)
				{
					PlainText = body
				},
				recipients: new EmailRecipients([new EmailAddress(to)]));


			await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
		}
	}
}