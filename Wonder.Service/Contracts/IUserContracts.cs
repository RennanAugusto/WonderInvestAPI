using System.Collections.Generic;
using System.Threading.Tasks;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Service.Contracts
{
    public interface IUserContracts
    {
        public Task<ResultLoginDTO> GetToken(LoginDTO user);
        public  Task<DetailResultUser> Register(InputUserDto user);
        public IList<string> ValidationUser(InputUserDto user);

        public bool EmailValidation(string email);
        public IList<string> PasswordValidation(string password);
    }
}