using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        public ApplicationUser GetByUserName(string pUserName);
    }
}