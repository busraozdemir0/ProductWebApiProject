using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces.RedisCache;

namespace WebApi.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>, ICacheableQuery
    {
        public string CacheKey => "GetAllProducts";

        public double CacheTime => 60; // On bellekte 60 dk tutulacagi anlamina gelir
        // 60 dk icerisinde tekrar GetAllProduct calisirsa cache'lenmis veriyi bize dondurecektir Bu sayede veritabani uzerinde islem yapmadan onbellekteki veriler bize geldiginden performans artisi olacaktir.
    }
}
