namespace Shared.DTOs;

public class ContactSimpleViewDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ContactSimpleViewDTO() { }
    //public ContactSimpleViewDTO(Contact contact) { }
    // cannot use a constructor to convert because of circular dependencies between projects
}
