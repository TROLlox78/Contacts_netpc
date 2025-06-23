using Web.Entities;

namespace Web.Repository
{
    public interface IContactRepository
    {
        // returns Task to facilitate asynchronious code
        Task<IEnumerable<Contact>> GetContacts(); 
        Task<IEnumerable<CategoryDict>> GetCategories(); 


    }
}
