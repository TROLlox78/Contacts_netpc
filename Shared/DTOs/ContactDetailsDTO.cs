namespace Shared.DTOs;

public class ContactDetailsDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int CategoryId { get; set; } // necessary for editing
    public string CategoryName { get; set; }
    public string? SubCategory { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
