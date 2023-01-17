using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Boxers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class BoxerEntityTypeConfiguration : IEntityTypeConfiguration<Boxer>
{
    public void Configure(EntityTypeBuilder<Boxer> builder)
    {
        builder.Property(b => b.Name).IsRequired();
    }

}
