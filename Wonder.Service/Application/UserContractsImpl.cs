using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wonder.Domain.DomainServices;
using Wonder.Domain.Models;
using Wonder.Service.Contracts;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Service.Application
{
    public class UserContractsImpl: IUserContracts
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserService _userService;
        
        public UserContractsImpl(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInUser, UserService userService)
        {
            this._userManager = userManager;
            this._signInManager = signInUser;
            this._userService = userService;
        }

        public async Task<ResultLoginDTO> GetToken(LoginDTO user)
        {
            var result = new ResultLoginDTO();

            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                result.Detail = "Usuário e senha inválidos";
                result.Errors.Add("Usuário e senha inválidos");
                result.Success = false;
            }
            else
            {
                var resultSign =
                    await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, lockoutOnFailure: false);

                if (resultSign.Succeeded)
                {
                    var userApp = this._userService.GetUserByName(user.UserName);
                    result.Id = userApp.Id;
                    result.FullName = userApp.FullName;
                    result.Phone = userApp.PhoneNumber;
                    result.Email = userApp.Email;
                    result.Detail = "Login efetuado com sucesso";
                    result.Success = true;
                    result.Token = result.Token;
                    
                }
                else
                {
                    result.Detail = "Ocorreram erros ao efetuar login";
                    result.Success = false;
                    result.Errors.Add("Ocorreram erros ao efetuar login");

                }
            }

            return result;
        }
        public async  Task<DetailResultUser> Register(InputUserDto user)
        {
            var errorsList = ValidationUser(user);
            var result = new DetailResultUser();
            result.UserName = user.UserName;
            result.Email = user.Email;
            
            if (errorsList.Count > 0)
            {
                result.Errors = errorsList;
                result.Success = false;
                result.Detail = "Não foi possível registrar o usuário";
            }
            else
            {
                var userApp = new ApplicationUser();
                userApp.Email = user.Email;
                userApp.EmailConfirmed = true;
                userApp.UserName =  user.UserName;
                userApp.PhoneNumber = user.Phone;
                userApp.FullName = user.Name;
                var resultRegister = await  _userManager.CreateAsync(userApp, user.Password);
                
                
                if (resultRegister.Succeeded)
                {
                    result.Success = true;
                    result.Detail = "Usuario criado com sucesso";
                }
                else
                {
                    result.Success = false;
                    result.Detail = "Não foi possível registrar o usuário";
                    foreach (var error in resultRegister.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }
                }
            }

            return result;
        }

        public IList<string> ValidationUser(InputUserDto user)
        {
            var errors = new List<string>();
            if (!EmailValidation(user.Email))
                errors.Add(string.Format("Email inválido {0}", user.Email));

            var passwordIsValid = PasswordValidation(user.Password);

            if (passwordIsValid.Count > 0)
            {
                errors.AddRange(passwordIsValid);
            }

            return errors;
        }

        public bool EmailValidation(string email)
        {
            try {
                var mail = new System.Net.Mail.MailAddress(email);
                if (! string.IsNullOrWhiteSpace(mail.Address))
                    return true;
                
                return false;
            }
            catch {
                return false;
            }
        }

        public IList<string> PasswordValidation(string password)
        {
            var errors = new List<string>();
            int upperCount = 0, alphaNumericCount = 0, lowerCount = 0, especialCount = 0;

            if (password.Length < 8 )
                errors.Add("Senha não possui 8 caracteres mínimos");

            foreach (var letter in password)
            {
                if (char.IsUpper(letter))
                    upperCount++;
                if (char.IsLower(letter))
                    lowerCount++;
                if (char.IsLetterOrDigit(letter))
                    alphaNumericCount++;
                if (char.IsSymbol(letter))
                    especialCount++;
            }
            
            if (upperCount == 0)
                errors.Add("Senha não possui caracteres maiúsculos");
            if (lowerCount == 0)
                errors.Add("Senha não possui caracteres minúsculos");
            if (alphaNumericCount == 0)
                errors.Add("Senha não possui caracteres alpha numéricos");
            if (especialCount == 0)
                errors.Add("Senha não possui caracteres especiais");
            
            return errors;
        }
    }
}