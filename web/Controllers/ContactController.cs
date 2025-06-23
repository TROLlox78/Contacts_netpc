using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Web.Converters;
using Web.Entities;
using Web.Repository;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactSimpleViewDTO>>> GetContactsSimple()
        {
            var contacts = await contactRepository.GetContacts();
            if (contacts != null)
            {
                return Ok(DTOconverter.ConvertTSimpleDTO(contacts));

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDetailsDTO>>> GetContactsDetailed()
        {
            var contacts = await contactRepository.GetContacts();
            var categories= await contactRepository.GetCategories();
            if (contacts != null && categories != null)
            {
                return Ok(DTOconverter.ConvertToDetailedDTO(contacts, categories));
            }
            return NotFound();
        }
    }
}
