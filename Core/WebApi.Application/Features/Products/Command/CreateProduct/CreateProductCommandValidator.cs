using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Application.Features.Products.Command.CreateProduct
{
    // CreateProduct icin validasyon islemi
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Başlık");  // Hata mesajini yazarken Title degil de Başlık yazmasi icin

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Açıklama");

            RuleFor(x => x.BrandId) // Dropdown'dan secildiginde marka id sifirdan kucukse hata verecek
                .GreaterThan(0)
                .WithName("Marka");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithName("Fiyat");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithName("İndirim Oranı");

            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .Must(categories=> categories.Any())  // id'ler icinde herhangi bir tanesi kesinlikle olmak zorunda oldugunu belirtiyor
                .WithName("Kategoriler");
        }
    }
}
