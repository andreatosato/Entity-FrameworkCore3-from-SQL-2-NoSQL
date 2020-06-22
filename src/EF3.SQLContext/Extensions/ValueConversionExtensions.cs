using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EF3.SQLContext.Extensions
{
    public static class ValueConversionExtensions
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions;

        static ValueConversionExtensions()
        {
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder, bool required = true) where T : class, new()
        {
            var converter = new ValueConverter<T, string>
            (
                v => JsonSerializer.Serialize(v, jsonSerializerOptions),
                v => JsonSerializer.Deserialize<T>(v, jsonSerializerOptions) ?? new T()
            );

            var comparer = new ValueComparer<T>
            (
                (l, r) => JsonSerializer.Serialize(l, jsonSerializerOptions) == JsonSerializer.Serialize(r, jsonSerializerOptions),
                v => v == null ? 0 : JsonSerializer.Serialize(v, jsonSerializerOptions).GetHashCode(),
                v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, jsonSerializerOptions), jsonSerializerOptions)
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("nvarchar(MAX)").IsRequired(required);

            return propertyBuilder;
        }
    }
}
