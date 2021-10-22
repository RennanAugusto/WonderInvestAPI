using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Data.Repository
{
    public class UserRepositoryImpl: IUserRepository
    {
        private readonly PostgreSqlContext _postgreSqlContext ;

        public UserRepositoryImpl(IServiceProvider provider)
        {
            this._postgreSqlContext = provider.GetService<PostgreSqlContext>();
        }
        
        public ApplicationUser GetByUserName(string pUserName)
        {
            var user = this._postgreSqlContext.Users.AsNoTracking()
                .Where(u => u.NormalizedUserName == pUserName.ToUpper()).First();
            
            return user;
        }
    }
}