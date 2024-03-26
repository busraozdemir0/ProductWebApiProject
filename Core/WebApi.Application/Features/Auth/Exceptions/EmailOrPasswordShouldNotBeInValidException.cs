using WebApi.Application.Bases;

namespace WebApi.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInValidException : BaseException
    {
        public EmailOrPasswordShouldNotBeInValidException() : base("Kullanıcı adı veya şifre yanlış.")
        {

        }
    }
}
