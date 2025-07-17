using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveAsync(); 
            return Ok(contact);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Contact contact)
        {
            if (id != contact.Id)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _contactRepository.UpdateAsync(contact);
            if (!updated)
                return NotFound("Güncellenecek kişi bulunamadı.");

            await _contactRepository.SaveAsync(); // <-- Burası da
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _contactRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound("Silinecek kişi bulunamadı.");

            await _contactRepository.SaveAsync(); // <-- Ve burası!
            return NoContent();
        }

    }
}
