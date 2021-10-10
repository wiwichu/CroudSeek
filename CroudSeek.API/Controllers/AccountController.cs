using CroudSeek.API.Entities;
using CroudSeek.API.Services;
using CroudSeek.Application.Contracts.Identity;
using CroudSeek.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CroudSeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService, ICroudSeekRepository croudSeekRepository)
        {
            _croudSeekRepository = croudSeekRepository;
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            var response = await _authenticationService.RegisterAsync(request);
            var user = new User()
            {
                Name = request.UserName,
                Email = request.Email,
            };
            _croudSeekRepository.AddUser(user);
            if (_croudSeekRepository.Save())
            {
                return Ok(response);
            }
            throw new System.Exception($"Unable to register.");
        }
    }
}
