using System.ComponentModel;
using System.Globalization;

namespace Shared.DTOs.User;

[TypeConverter(typeof(OwnerDtoConverter))]
public class OwnerDto
{
    public OwnerDto(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public void setUsername(string Username)
    {
        this.Username = Username;
    }
}

public class OwnerDtoConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string) return new OwnerDto((string)value);
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        if (destinationType == typeof(string) && value is OwnerDto)
        {
            var owner = (OwnerDto)value;
            return owner.Username;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}