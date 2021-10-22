using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;

namespace Wonder.Domain.DomainServices
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public ApplicationUser GetUserByName(string pUserName)
        {
            return this._userRepo.GetByUserName(pUserName);
        }
    }
}