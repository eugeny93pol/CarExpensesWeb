using CE.DataAccess.Dtos;

namespace CE.WebAPI.Options
{
    public interface IAuthOptions
    {
        string GenerateToken(UserDto user);
    }
}