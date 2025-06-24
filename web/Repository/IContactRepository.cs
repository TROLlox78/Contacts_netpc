using Web.Entities;

namespace Web.Repository
{
    public interface IContactRepository
    {
        // returns Task to facilitate asynchronious code
        // TODO: test if it's better to return IResult everywhere
        Task<IEnumerable<Contact>> GetContacts(); 
        Task<IEnumerable<CategoryDict>> GetCategories(); 
        Task<IResult> AddContact(Contact contact);
        Task<IResult> DeleteContact(int contactId);
        Task<IResult> UpdateContact(int id, Contact contact);



    }
}
