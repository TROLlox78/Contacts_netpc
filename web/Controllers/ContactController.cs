﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet("GetContact/{id:int}")]
    public async Task<ActionResult<ContactDetailsDTO>> GetContact(int id)
    {// this should be a join query to speed things up
        var contacts = await contactRepository.GetContacts();
        var contact = contacts.FirstOrDefault(x => x.Id == id);
        var categories = await contactRepository.GetCategories();
        if (contacts != null && categories != null && contact != null)
        {
            return Ok(DTOconverter.ConvertDTO(contact, categories));
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
        var response = await contactRepository.AddContact(newContact);
        if (response is null)
        {
            return Conflict("Email already exists");
        }
        else 
        {
            return Ok(response);
        }
        return StatusCode(500);
    }
    [Authorize]
    [HttpDelete("DeleteContact/{id}")]
    public async Task<ActionResult> DeleteContact(int id)
    {
        var result = await contactRepository.DeleteContact(id);
        if (result is NotFound) { return NotFound(); }
        return NoContent();

    }
    [Authorize]
    [HttpPut("UpdateContact/{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactCreateDTO updatedContact)
    {
        if (updatedContact == null || id != updatedContact.Id)
        {
            return BadRequest("Invalid data" );
        }
        var contact = DTOconverter.ConvertDTO(updatedContact);
        var response = await contactRepository.UpdateContact(id, contact);

        if (response is null)
        {
            return NotFound("Contact not found");
        }
        return Ok(response);
    }
}
