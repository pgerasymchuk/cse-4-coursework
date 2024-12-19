using CycleShare.Server.Dtos;
using CycleShare.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CycleShare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        // GET: api/address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> Get()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(dtos);
        }

        // GET api/address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/address
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddressDto dto)
        {
            try
            {
                await _service.AddAsync(dto);
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // PUT api/address/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AddressDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateAsync(dto);
            }
            catch
            {
                if ((await _service.GetByIdAsync(id)) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE api/address/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if ((await _service.GetByIdAsync(id)) == null)
            {
                return NotFound();
            }
            await _service.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
