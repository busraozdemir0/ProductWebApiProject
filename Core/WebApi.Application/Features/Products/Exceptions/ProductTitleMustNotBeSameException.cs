using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;

namespace WebApi.Application.Features.Products.Exceptions
{
    // Urun adları ayni olamaz. Bunun için kendimiz bir proje kuralı oluşturduk. ve custom exception'ımızı yazdik.
    public class ProductTitleMustNotBeSameException:BaseExceptions
    {
        public ProductTitleMustNotBeSameException():base("Ürün başlığı zaten var")
        {
            
        }
    }
}
