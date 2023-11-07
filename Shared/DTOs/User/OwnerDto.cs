using System;
using System.ComponentModel;

namespace Shared.DTOs.User;

[TypeConverter(typeof(OwnerDtoConverter))]
public class OwnerDto
{
    public string Username { get; set; }

    public OwnerDto(string username)
    {
        Username = username;
    }
    
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

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        if (value is string)
        {
            return new OwnerDto((string)value);
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is OwnerDto)
        {
            OwnerDto owner = (OwnerDto)value;
            return owner.Username;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }


}