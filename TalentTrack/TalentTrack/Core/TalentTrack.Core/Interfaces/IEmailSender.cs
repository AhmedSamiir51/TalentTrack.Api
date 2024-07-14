namespace TalentTrack.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string EmailAddress, string subject, string Message);
    }
}
