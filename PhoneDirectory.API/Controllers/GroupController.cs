using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Application.DTOs.Group;
using PhoneDirectory.Application.Services;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupDTO dto)
        {
            var newGroup = await _groupService.CreateGroupAsync(dto);
            return CreatedAtAction(nameof(GetGroup), new { id = newGroup.Id }, newGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGroupDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID uyuşmazlığı.");
            }

            var result = await _groupService.UpdateGroupAsync(dto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}