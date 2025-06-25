using Blazor.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Blazor.Pages;


public class ContractsBase : ComponentBase
{
    public bool showCreate = false;
    // holds expanded/contracted state, I don't know how to do UI well :(
    public Dictionary<ContactDetailsDTO, bool> expandedContacts = new();
   
    public ContactCreateDTO newContact = new();
    public UserDTO userDTO = new();
    
    [Inject]
    public IContactService ContactService { get; set; }
    [Inject]
    public AuthHandler AuthHandler { get; set; }
    public IEnumerable<ContactDetailsDTO> Contacts { get; set; }
    public IEnumerable<CategoryDTO> Categories { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Contacts = await ContactService.GetContacts();
        Categories = await ContactService.GetCategories();
        foreach (var contact in Contacts) {
            expandedContacts.Add(contact, false);
        }
    }
    public void ToggleCreateField()
    {
        showCreate = !showCreate;
    }
    public void ToggleExpanded(ContactDetailsDTO item)
    {
        expandedContacts[item] = !expandedContacts[item];
    }
    public void Edit(ContactDetailsDTO item)
    {
    }
    public void Delete(ContactDetailsDTO item)
    {
    }
    public void Create(ContactCreateDTO item)
    {
        ContactService.PostContact(item);
    }
    public async Task Login()
    {
        await AuthHandler.HandleLogin(userDTO);
    }
    public void Register()
    {
        AuthHandler.HandleRegister(); // todo
    }
    public async Task checkAuth()
    {
        var r = await ContactService.GetAuth();
        Console.WriteLine(r);
    }
}
