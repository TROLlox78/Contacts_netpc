using Shared.DTOs;
using Web.Entities;

namespace Web.Converters;

public class DTOconverter
{
    // while MS docs show a clean example where the DTO creation happens through its constructor,
    // we need a helper class because we have multiple projects in the solution
    public static IEnumerable<ContactDetailsDTO> ConvertToDetailedDTO(IEnumerable<Contact> contacts,
        IEnumerable<CategoryDict> categories )
    {
        return contacts.Select(contact => 
            new ContactDetailsDTO
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                CategoryName = categories.Where(x => contact.CategoryId==x.Id).First().Name,
                SubCategory = contact.SubCategory,
                PhoneNumber = contact.PhoneNumber,
                DateOfBirth = contact.DateOfBirth,
            }).ToList();
    }

    public static IEnumerable<ContactSimpleViewDTO> ConvertTSimpleDTO(IEnumerable<Contact> contacts)
    {
        return contacts.Select(contact =>
            new ContactSimpleViewDTO
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
            }).ToList();
    }
    public static IEnumerable<CategoryDTO> ConvertDTO(IEnumerable<CategoryDict> categories)
    {
        return categories.Select(category =>
            new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            }).ToList();
    }
    public static Contact ConvertDTO(ContactCreateDTO request)
    {
        return new Contact { 
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,  // TODO: hash the password
            CategoryId = request.CategoryId,
            SubCategory = request.SubCategory,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth
        };
    }
}
