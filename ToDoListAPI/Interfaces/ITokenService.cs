using ToDoListAPI.Models;

namespace ToDoListAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
