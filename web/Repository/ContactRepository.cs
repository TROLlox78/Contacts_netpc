using Microsoft.EntityFrameworkCore;
using Web.Entities;
using Web.Data;

namespace Web.Repository;

public class ContactRepository : IContactRepository
{
    private readonly ContactsDbContext dbContext;

    // dependency injection 
    public ContactRepository(ContactsDbContext dbContext) 
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Contact>> GetContacts()
    {
        return await dbContext.Contacts.ToListAsync();
    }
    public async Task<IEnumerable<CategoryDict>> GetCategories()
    {
        return await dbContext.CategoryDict.ToListAsync();
    }
}
