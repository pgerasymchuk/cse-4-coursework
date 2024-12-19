using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.Mime.MediaTypeNames;

namespace CycleShare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        private readonly IGalleryItemService _galleryItemService;
        private readonly IAddressService _addressService;
        private readonly IWebHostEnvironment _environment;

        public BikeController(IBikeService bikeService,
                              IGalleryItemService galleryItemService,
                              IAddressService addressService,
                              IWebHostEnvironment environment)
        {
            _bikeService = bikeService;
            _galleryItemService = galleryItemService;
            _addressService = addressService;
            _environment = environment;
        }

        // GET: api/bike
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDto>>> Get([FromQuery] BikeFilterParams filterParams)
        {
            var dtos = await _bikeService.GetAllAsync(filterParams);
            return Ok(dtos);
        }

        // GET: api/bike/my
        [HttpGet("/my")]
        public async Task<ActionResult<IEnumerable<BikeDto>>> GetByUserId()
        {
            try
            {
                int userId = GetUserIdFromToken();
                var dtos = await _bikeService.GetAllByUserAsync(userId);
                return Ok(dtos);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/bike/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeDto>> GetById(int id)
        {
            var dto = await _bikeService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/bike
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] BikeDto bikeDto,
                                            [FromForm] AddressDto addressDto,
                                            [FromForm] List<IFormFile> images)
        {
            try
            {
                int userId = GetUserIdFromToken();
                bikeDto.UserId = userId;
                addressDto.Id = 0;
                int bikeId = await _bikeService.AddAsync(bikeDto, addressDto);
                await _galleryItemService.SaveImagesAsync(bikeId, images);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/bike/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,
                                               [FromForm] BikeDto bikeDto,
                                               [FromForm] AddressDto addressDto,
                                               [FromForm] List<IFormFile> images)
        {
            if (id != bikeDto.Id)
            {
                return BadRequest();
            }
            var bike = await _bikeService.GetByIdAsync(id);
            int userId = GetUserIdFromToken();
            if (bike.UserId != userId)
            {
                return Unauthorized();
            }
            try
            {
                addressDto.Id = 0;
                await _bikeService.UpdateAsync(bikeDto, addressDto);
                await _galleryItemService.SaveImagesAsync(id, images);
                return NoContent();
            }
            catch
            {
                if ((await _bikeService.GetByIdAsync(id)) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        // DELETE api/bike/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if ((await _bikeService.GetByIdAsync(id)) == null)
            {
                return NotFound();
            }
            var bike = await _bikeService.GetByIdAsync(id);
            int userId = GetUserIdFromToken();
            if (bike.UserId != userId)
            {
                return Unauthorized();
            }

            await _bikeService.DeleteByIdAsync(id);
            return NoContent();
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return 0;
            }
            int userId = int.Parse(userIdClaim.Value);
            return userId;
        }
    }
}
