using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyUserWebApi.Domain.Interfaces.Services.User;

namespace MyUserWebApi.Application.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode ((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}