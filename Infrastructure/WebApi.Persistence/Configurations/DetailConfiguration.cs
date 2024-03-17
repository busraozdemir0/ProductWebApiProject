using Bogus;
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
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            // Bogus kutuphanesi yardimiyla fake veriler olusturalim
            Faker faker = new("tr");

            Detail detail1 = new()
            {
                Id = 1,
                Title = faker.Lorem.Sentence(1), // Lorem ipsumdan 1 kelime aldik
                Description = faker.Lorem.Sentence(5), // Lorem ipsumdan 5 kelime aldik
                CategoryId = 1, // Elektronik kategorisini temsil edecek
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            Detail detail2 = new()
            {
                Id = 2,
                Title = faker.Lorem.Sentence(2), 
                Description = faker.Lorem.Sentence(5), 
                CategoryId = 3, // Bilgisayar kategorisini temsil edecek
                CreatedDate = DateTime.Now,
                IsDeleted = true
            };

            Detail detail3 = new()
            {
                Id = 3,
                Title = faker.Lorem.Sentence(1), 
                Description = faker.Lorem.Sentence(5), 
                CategoryId = 4, // Kadın kategorisini temsil edecek
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            builder.HasData(detail1,detail2,detail3);
        }
    }
}
