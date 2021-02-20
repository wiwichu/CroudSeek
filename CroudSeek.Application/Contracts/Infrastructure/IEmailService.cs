using CroudSeek.Application.Models.Mail;
using System.Threading.Tasks;

namespace CroudSeek.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
