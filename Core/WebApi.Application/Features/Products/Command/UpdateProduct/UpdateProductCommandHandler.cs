using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;
using WebApi.Application.Interfaces.AutoMapper;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : BaseHandler, IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) :
            base(mapper, unitOfWork, httpContextAccessor)
        {

        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            var map = mapper.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories= await unitOfWork.GetReadRepository<ProductCategory>()
                .GetAllAsync(x=>x.ProductId == product.Id);

            await unitOfWork.GetWriteRepository<ProductCategory>()  // Guncelleme isleminde kullanici kategorileri degistirebileceginden once ProductCategory tablosundan o urune ait categoryId'leri kaldırdık.
                .HardDeleteRangeAsync(productCategories);

            foreach (var categoryId in request.CategoryIds)  // Ardından request'den gelen Kategori id'lerini dongu ile donerek tabloya ekleme islemi yapiyoruz.
                await unitOfWork.GetWriteRepository<ProductCategory>()
                    .AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
