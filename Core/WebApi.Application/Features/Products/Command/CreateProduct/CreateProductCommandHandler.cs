using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
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
        }
    }
}
