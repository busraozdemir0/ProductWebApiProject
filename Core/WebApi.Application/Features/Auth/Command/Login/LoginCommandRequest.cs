using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Application.Features.Auth.Command.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        [DefaultValue("deneme@gmail.com")]  // Swagger varsayilan olarak email icin bu degeri getirecek
        public string Email { get; set; }

        [DefaultValue("123456")]
        public string Password { get; set; }
    }
}
