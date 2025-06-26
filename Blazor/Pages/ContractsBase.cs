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
    public async Task Delete(ContactDetailsDTO item)
    {
        var response = await ContactService.DeleteContact(item.Id);
        if (response.IsSuccessStatusCode)
        expandedContacts.Remove(item);
    }
    public async Task Create(ContactCreateDTO item)
    {
        var response = await ContactService.PostContact(item);
        if (response.IsSuccessStatusCode) // we ask for the item to put in
        {
            Console.WriteLine(response);
            var count = expandedContacts.Count;
            string responseBody = await response.Content.ReadAsStringAsync();
            var contact = await ContactService.GetContact(responseBody);
            Console.WriteLine(responseBody);
            expandedContacts.Add(contact, false);
            Console.WriteLine("fetch {0} and {1}", count, expandedContacts.Count);
            StateHasChanged();
        }
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
