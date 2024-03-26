using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;
using WebApi.Application.Features.Products.Rules;
using WebApi.Application.Interfaces.AutoMapper;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly ProductRules productRules;

        public CreateProductCommandHandler(ProductRules productRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) :
            base(mapper, unitOfWork, httpContextAccessor)
        {
            this.productRules = productRules;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            // Kendimiz ürün adı tekrar edemez gibi bir proje kuralı oluşturduk ve aynı ürün adı tekrar girilirse custom yazdigimiz exception firlatilmasini sagladik.
            await productRules.ProductTitleMustNotBeSame(products, request.Title);

            Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);

            await unitOfWork.GetWriteRepository<Product>().AddAsync(product);
            if (await unitOfWork.SaveAsync() > 0) // Eger basarili bir sekilde eklenirse kaydetme islemi sifirdan buyuk bir int deger donecek
            {
                // CategoryId birden fazla ama ayni Product'a ait. Bu sebepten buradaki product id'sini verip categoryId'sini +
                // ise foreach ile donerek her seferinde degistirip atiyoruz.

                foreach (var categoryId in request.CategoryIds) // Bir urunun birden fazla kategorisi oldugu icin request'deki CategoryIds alani uzerinde dongu kuruyoruz.
                    await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    });

                await unitOfWork.SaveAsync();
            }

            return Unit.Value; // Unit => Herhangi bir tip dondurmez. Void gibi calisir.
        }
    }
}
