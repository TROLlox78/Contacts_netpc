using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Web.Converters;
using Web.Entities;
using Web.Repository;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactRepository contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        this.contactRepository = contactRepository;
    }
    [HttpGet("GetContactsSimple")] // if we decide an anoymous user may not see detailed information
    public async Task<ActionResult<IEnumerable<ContactSimpleViewDTO>>> GetContactsSimple()
    {
        var contacts = await contactRepository.GetContacts();
        if (contacts != null)
        {
            return Ok(DTOconverter.ConvertTSimpleDTO(contacts));
    
        }
        return NotFound();
    }
    [HttpGet("GetContactsDetailed")]
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
    [HttpGet("GetCategories")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await contactRepository.GetCategories();
        if ( categories != null)
        {
            return Ok( DTOconverter.ConvertDTO( categories));
        }
        return NotFound();
    }

    [Authorize]
    [HttpPost("CreateContact")]
    public async Task<ActionResult> CreateContact([FromBody]ContactCreateDTO request)
    {
        
        if (request== null) { return BadRequest("No data"); }
        var newContact = DTOconverter.ConvertDTO(request);
        var result = await contactRepository.AddContact(newContact);
        // if result shit = badrequest else ret ok
        
        return Ok("spko");
    }
}
