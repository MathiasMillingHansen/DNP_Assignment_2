using System.ComponentModel;

namespace Shared.DTOs.User;

[TypeConverter(typeof(OwnerDto))]
public class OwnerDtoTypeConverter
{
    public OwnerDtoTypeConverter(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public static implicit operator OwnerDto(OwnerDtoTypeConverter ownerDtoTypeConverter)
    {
        return new OwnerDto(ownerDtoTypeConverter.Username);
    }

    public static implicit operator OwnerDtoTypeConverter(OwnerDto ownerDto)
    {
        return new OwnerDtoTypeConverter(ownerDto.Username);
    }
}