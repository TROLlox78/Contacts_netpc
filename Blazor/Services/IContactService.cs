using Shared.DTOs;

namespace Blazor.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDetailsDTO>> GetContacts();
        Task<HttpResponseMessage> GetAuth();
    }
}
