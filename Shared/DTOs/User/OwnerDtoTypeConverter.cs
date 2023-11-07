using System.ComponentModel;

namespace Shared.DTOs.User;

[TypeConverter(typeof(OwnerDto))]
public class OwnerDtoTypeConverter
{
    public static implicit operator OwnerDto(OwnerDtoTypeConverter ownerDtoTypeConverter)
    {
        return new(ownerDtoTypeConverter.Username);
    }

    public static implicit operator OwnerDtoTypeConverter(OwnerDto ownerDto)
    {
        return new(ownerDto.Username);
    }

    public string Username { get; set; }

    public OwnerDtoTypeConverter(string username)
    {
        Username = username;
    }    
}