using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using PaperStop.Application.Serialization.Options;
using PaperStop.Application.Serialization.Serializers;
using PaperStop.Domain.Contracts;
using PaperStop.Infrastructure.Extensions;

namespace PaperStop.Infrastructure.Configurations;

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
