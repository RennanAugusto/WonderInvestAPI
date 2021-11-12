using System.Collections.Generic;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public class IWalletRepo: IBaseRepository<Wallet>
    {
        public bool Insert(Wallet obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Wallet obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Wallet> Select()
        {
            throw new System.NotImplementedException();
        }

        public Wallet Select(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}