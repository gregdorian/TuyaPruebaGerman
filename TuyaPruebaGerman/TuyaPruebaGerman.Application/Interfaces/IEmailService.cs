using System.Threading.Tasks;
using TuyaPruebaGerman.Application.DTOs.Email;

namespace TuyaPruebaGerman.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}