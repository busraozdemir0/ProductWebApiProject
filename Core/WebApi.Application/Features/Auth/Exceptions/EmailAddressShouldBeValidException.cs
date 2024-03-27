using WebApi.Application.Bases;

namespace WebApi.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException : BaseException
    {
        public EmailAddressShouldBeValidException() : base("Böyle bir e-mail adresi bulunmamaktadır.") { }
    }
}
