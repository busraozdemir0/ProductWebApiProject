using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;
using WebApi.Application.Features.Auth.Exceptions;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Auth.Rules
{
    public class AuthRules:BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            // Eger kayit olmak isteyen kullanici zaten vt'de var ise custom olusturdugumuz hata donecek

            if (user is not null)
                throw new UserAlreadyExistException();

            return Task.CompletedTask;
        }

        public Task EmailOrPasswordShouldNotBeInValid(User? user, bool checkPassword)
        {
            // Login islemi icin Email ve Password vt'deki ile uyusuyor mu diye bakacak uymuyorsa hata firlatacak.
            
            if (user is null || !checkPassword)
                throw new EmailOrPasswordShouldNotBeInValidException();

            return Task.CompletedTask;

        }
    }
}
