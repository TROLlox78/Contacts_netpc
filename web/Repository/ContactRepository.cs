using Microsoft.EntityFrameworkCore;
using Web.Entities;
using Web.Data;

namespace Web.Repository;

public class ContactRepository : IContactRepository
{
    private readonly ContactsDbContext dbContext;

    // repo is server side
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
    public async Task<IResult> AddContact(Contact contact) {
        dbContext.Contacts.Add(contact); // TODO: CHECK UNIQUE EMAIL
        await dbContext.SaveChangesAsync();
        return TypedResults.Created(); // TODO: fill URI with contact id
    }

    public async Task<IResult> DeleteContact(int contactId)
    {
        var contact = await dbContext.Contacts.FindAsync(contactId);
        if (contact is null) { return TypedResults.NotFound(); }
        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public async Task<IResult> UpdateContact(int id, Contact newContact)
    {
        var oldContact = await dbContext.Contacts.FindAsync(id);
        if (oldContact is null) return TypedResults.NotFound();
        oldContact.FirstName   = newContact.FirstName;
        oldContact.LastName    = newContact.LastName;
        oldContact.Email       = newContact.Email;
        oldContact.Password    = newContact.Password;
        oldContact.CategoryId  = newContact.CategoryId;
        oldContact.SubCategory = newContact.SubCategory;
        oldContact.PhoneNumber = newContact.PhoneNumber;
        oldContact.DateOfBirth = newContact.DateOfBirth;
        await dbContext.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
