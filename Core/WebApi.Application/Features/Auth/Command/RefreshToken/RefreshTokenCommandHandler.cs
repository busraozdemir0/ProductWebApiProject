using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;
using WebApi.Application.Features.Auth.Rules;
using WebApi.Application.Interfaces.AutoMapper;
using WebApi.Application.Interfaces.Tokens;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        public RefreshTokenCommandHandler(IMapper mapper, AuthRules authRules, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenService tokenService) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            string email = principal.FindFirstValue(ClaimTypes.Email); // Kisinin access token'i yardimiyla Email'ine ulasiyoruz.

            User? user = await userManager.FindByEmailAsync(email); // Email yardimiyla user 
            IList<string> roles = await userManager.GetRolesAsync(user); // User'in rolleri

            await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);  // RefreshToken suresine bakiliyor ve bu sure de dolmussa artik bu kisiyi giris yapmaya yonlendiriyoruz.

            JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
            string newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;  // user'in refresh token'ini yeni urettigimiz ile update ediyoruz.
            await userManager.UpdateAsync(user);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
