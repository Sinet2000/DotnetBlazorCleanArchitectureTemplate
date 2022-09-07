using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Serialization.Options;
using FPAAgentura.Application.Serialization.Serializers;
using FPAAgentura.Domain.Contracts;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace Infrastructure.Configurations;

public class EntityExtendedAttributeConfiguration : IEntityTypeConfiguration<IEntityExtendedAttribute>
{
    public void Configure(EntityTypeBuilder<IEntityExtendedAttribute> builder)
    {
        // This Converter will perform the conversion to and from Json to the desired type
        builder
            .Property(e => e.Json)
            .HasJsonConversion(
                new SystemTextJsonSerializer(
                    new OptionsWrapper<SystemTextJsonOptions>(new SystemTextJsonOptions())));
    }
}
