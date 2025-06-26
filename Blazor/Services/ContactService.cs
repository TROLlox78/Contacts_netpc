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

        public async Task<HttpResponseMessage> GetAuth()
        {
            return await httpClient.GetAsync("api/auth");
        }

        public async Task<IEnumerable<ContactDetailsDTO>> GetContacts()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ContactDetailsDTO>>("api/contact/GetContactsDetailed");
        }
        public async Task<ContactDetailsDTO?> GetContact(int id)
        {
            return await httpClient.GetFromJsonAsync<ContactDetailsDTO>($"api/contact/GetContact/{id}");
        }
        public async Task<ContactDetailsDTO?> GetContact(string location)
        {
            return await httpClient.GetFromJsonAsync<ContactDetailsDTO>(location);
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("api/contact/GetCategories");
        }
        public async Task<HttpResponseMessage> UpdateContact(int id, ContactCreateDTO contact)
        {
            return await httpClient.PutAsJsonAsync($"api/contact/UpdateContact/{id}", contact);
        }
        public async Task<HttpResponseMessage> DeleteContact(int id)
        {
            return await httpClient.DeleteAsync($"api/contact/DeleteContact/{id}");
        }
        public async Task<HttpResponseMessage> PostContact(ContactCreateDTO contact)
        {
            return await httpClient.PostAsJsonAsync<ContactCreateDTO>("api/contact/CreateContact", contact);
        }
    }
}
