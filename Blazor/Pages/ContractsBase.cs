using Blazor.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Blazor.Pages
{
    
    public class ContractsBase : ComponentBase
    {
        public bool buttonTest = true;
        
        public UserDTO userDTO = new();

        [Inject]
        public IContactService ContactService { get; set; }
        [Inject]
        public AuthHandler AuthHandler { get; set; }
      
        public IEnumerable<ContactDetailsDTO> Contacts { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetContacts();
        }
        public async Task Login()
        {
            await AuthHandler.HandleLogin(userDTO);
        }
        public void Register()
        {
            AuthHandler.HandleRegister();
        }
        public async Task checkAuth()
        {
            var r = await ContactService.GetAuth();
            Console.WriteLine(r);
        }
    }

}
