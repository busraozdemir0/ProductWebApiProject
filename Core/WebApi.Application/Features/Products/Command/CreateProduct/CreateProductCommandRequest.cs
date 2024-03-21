using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandRequest:IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public IList<int> CategoryIds { get; set; }  // Bu senaryoda bir urune ait birden fazla kategori(alt kategori gibi orn. Elektronik-> Bilgisayar-> Laptop) olabildigi icin birden fazla CategoryId verilebilir.
    }
}
