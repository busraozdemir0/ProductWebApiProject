using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Features.Auth.Command.Login;
using WebApi.Application.Features.Auth.Command.RefreshToken;
using WebApi.Application.Features.Auth.Command.Register;
using WebApi.Application.Features.Auth.Command.Revoke;
using WebApi.Application.Features.Auth.Command.RevokeAll;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created); // Basarili kayit oldugunda status kodu 201 donecek
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request) // Kisi login oldugunda RefreshToken uretiliyor(belli bir sure boyunca bu token yardimiyla oturum acikta birakilabilir.)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request) // Kisi LogOut oldugunda RefreshToken'i null yapiliyor. Yani kullanici cikis yapinca vt'deki RefreshToken'da silinmis oluyor.
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAll() // Tum kullanicilarin RefreshToken'lari silinmesi icin (tum kullanicilar cikis yaptirilmis olunuyor.)
        {
            await mediator.Send(new RevokeAllCommandRequest());
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
