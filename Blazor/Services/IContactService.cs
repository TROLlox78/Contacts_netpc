using Shared.DTOs;

namespace Blazor.Services;

public interface IContactService
{
    Task<IEnumerable<ContactDetailsDTO>> GetContacts();
    Task<HttpResponseMessage> GetAuth();
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<ContactDetailsDTO?> GetContact(int id);
    Task<ContactDetailsDTO?> GetContact(string location);
    Task<HttpResponseMessage> PostContact(ContactCreateDTO contact);
    Task<HttpResponseMessage> DeleteContact(int id);
    Task<HttpResponseMessage> UpdateContact(int id, ContactCreateDTO contact);
}
