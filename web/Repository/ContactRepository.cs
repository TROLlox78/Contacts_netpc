using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Data;
using Web.Entities;

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
    public async Task<string?> AddContact(Contact contact) {
        var contacts = await dbContext.Contacts.ToListAsync();
        if (contacts.FirstOrDefault(x => x.Email==contact.Email) != null)
        {
            return null;
        }
        var tracker = dbContext.Contacts.Add(contact);
        await dbContext.SaveChangesAsync();
        // return id of contact
        return $"/api/contact/GetContact/{tracker.Entity.Id}"; 
    }

    public async Task<IResult> DeleteContact(int contactId)
    {
        var contact = await dbContext.Contacts.FindAsync(contactId);
        if (contact is null) { return TypedResults.NotFound(); }
        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public async Task<string?> UpdateContact(int id, Contact newContact)
    {
        var oldContact = await dbContext.Contacts.FindAsync(id);
        if (oldContact is null) return null;
        oldContact.FirstName   = newContact.FirstName;
        oldContact.LastName    = newContact.LastName;
        oldContact.Email       = newContact.Email;
        oldContact.Password    = newContact.Password;
        oldContact.CategoryId  = newContact.CategoryId;
        oldContact.SubCategory = newContact.SubCategory;
        oldContact.PhoneNumber = newContact.PhoneNumber;
        oldContact.DateOfBirth = newContact.DateOfBirth;
        await dbContext.SaveChangesAsync();
        return $"/api/contact/GetContact/{id}";
    }

}
