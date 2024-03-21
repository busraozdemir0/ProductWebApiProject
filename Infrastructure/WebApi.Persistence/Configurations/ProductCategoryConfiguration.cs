using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => new {x.ProductId, x.CategoryId});  // Bu iki alan icin primary key olusturduk.

            // ProductCategories icin bir tane ProductId olur
            builder.HasOne(p=>p.Product)
                .WithMany(pc=>pc.ProductCategories)
                .HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade); // Bir tane Product sildigimizde o Product'a ait tum baglantilar silicenek.

            builder.HasOne(c => c.Category)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
