using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IUserService
    {
        User Register(User user);
        User Login(string email, string password);
    }
}
