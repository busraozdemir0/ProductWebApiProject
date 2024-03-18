using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class Category:EntityBase
    {
        public Category()
        {
            
        }
        public Category(int parentId, string name, int priority)
        {
            ParentId = parentId;
            Name = name;
            Priority = priority;
        }
        public int ParentId { get; set; } // kesin girilmesi gereken bir alan oldugu icin required tanimladik
        public string Name { get; set; }
        public int Priority { get; set; }
        public ICollection<Detail> Details { get; set; } // Detail tablosu ile Bire cok iliski
        public ICollection<Product> Products { get; set; } // Product tablosu ile coka cok iliski => Migration olusturdugumuzda otomatik CategoryProduct adinda ortak tablo olusturur.
    }
}
