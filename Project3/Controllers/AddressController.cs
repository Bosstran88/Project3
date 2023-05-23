using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Entity.Request;
using Project3.Models;
using Project3.Repositories;
using Project3.Services;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressService _addressService;
        IAddressRepo address;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> createOrUpdate([FromBody] AddAddressReq? menuReq)
        {
            return Ok(_addressService.createOrUpdate(menuReq));
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> getById(long Id)
        {
            return Ok(_addressService.getOne(Id));
        }

        [HttpDelete("deleteById/{menuId}")]
        public async Task<IActionResult> deleteById(long menuId)
        {
            return Ok(_addressService.deleteAddress(menuId));
        }

        [HttpGet("getList")]
        public async Task<IActionResult> getList()
        {
            return Ok(_addressService.getListAddress());

        }
    }
}
