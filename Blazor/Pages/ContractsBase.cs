using Blazor.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Blazor.Pages
{
    public class ContractsBase : ComponentBase
    {
        public bool buttonTest = true;
        [Inject]
        public IContactService ContactService { get; set; }

        public IEnumerable<ContactDetailsDTO> Contacts { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetContacts();
        }
    }
}
