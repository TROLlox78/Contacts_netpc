using Blazor.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Blazor.Pages;


public class ContractsBase : ComponentBase
{
    public bool showCreate = false;
    // holds expanded/contracted/editing state, I don't know how to do UI well :(
    public Dictionary<ContactDetailsDTO, ContactState> expandedContacts = new();
   
    public ContactCreateDTO newContact = new();
    public UserDTO userDTO = new();
    public ContactCreateDTO editing = new();
    public int editingId = -1;
    
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
            expandedContacts.Add(contact, new());
        }
    }
    public void ToggleCreateField()
    {
        showCreate = !showCreate;
    }
    public void ToggleExpanded(ContactDetailsDTO item)
    {
        expandedContacts[item].expanded = !expandedContacts[item].expanded;
    }
    public void ToggleEdit(KeyValuePair<ContactDetailsDTO, ContactState> item)
    {
        item.Value.editing = !item.Value.editing;
        item.Value.Set(item.Key);
    }

    public async void Edit(ContactCreateDTO item)
    {
        var response = await ContactService.UpdateContact(item.Id, item);
        if (response.IsSuccessStatusCode)
        {
            var Destroy = expandedContacts.FirstOrDefault(x=>x.Key.Id == item.Id).Key;
            expandedContacts.Remove(Destroy);

            UpdateContactUI(response);
        }
    }
    public async Task Delete(ContactDetailsDTO item)
    {
        var response = await ContactService.DeleteContact(item.Id);
        if (response.IsSuccessStatusCode)  
        expandedContacts.Remove(item);
    }
    public async void UpdateContactUI(HttpResponseMessage response)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        var contact = await ContactService.GetContact(responseBody);
        expandedContacts.Add(contact, new());
        StateHasChanged();
    }
    public async Task Create(ContactCreateDTO item)
    {
        var response = await ContactService.PostContact(item);
        if (response.IsSuccessStatusCode) // we ask for the item to put in
        {
            UpdateContactUI(response);
        }
    }
    public async Task Login()
    {
        await AuthHandler.HandleLogin(userDTO);
    }
    public async void Register()
    {
        await AuthHandler.HandleRegister(userDTO); // todo
    }
    public async Task checkAuth()
    {
        var r = await ContactService.GetAuth();
        Console.WriteLine(r);
    }
    public class ContactState
    {
        public bool expanded = false;
        public bool editing = false;
        public ContactCreateDTO editingModel = new();
        public void Set(ContactDetailsDTO item)
        {
            editingModel.Id = item.Id;
            editingModel.FirstName = item.FirstName;
            editingModel.LastName = item.LastName;
            editingModel.PhoneNumber = item.PhoneNumber;
            editingModel.DateOfBirth = item.DateOfBirth;
            editingModel.CategoryId = item.CategoryId;
            editingModel.SubCategory = item.SubCategory;
            editingModel.Email = item.Email;
        }
    }
}
