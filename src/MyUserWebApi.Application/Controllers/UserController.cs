using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyUserWebApi.Domain.Interfaces.Services.User;

namespace MyUserWebApi.Application.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode ((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}