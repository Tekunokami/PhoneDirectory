using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Application.DTOs.Contact;
using PhoneDirectory.Application.Services;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateContactDTO dto)
        {
            var newContact = await _contactService.CreateContactAsync(dto);
            return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, newContact);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID and DTO ID do not match.");

            var result = await _contactService.UpdateContactAsync(dto);
            if (!result)
                return NotFound();

            return Ok();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
