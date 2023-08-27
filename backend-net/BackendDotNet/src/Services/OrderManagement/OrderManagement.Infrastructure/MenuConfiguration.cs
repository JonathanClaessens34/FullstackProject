using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class MenuConfiguration : IEntityTypeConfiguration<CocktailMenu>
    {
        public void Configure(EntityTypeBuilder<CocktailMenu> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}
