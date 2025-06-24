using Shared.DTOs;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient httpClient;

        public ContactService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ContactDetailsDTO>> GetContacts()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ContactDetailsDTO>>("api/contact/GetContactsDetailed");
        }
    }
}
