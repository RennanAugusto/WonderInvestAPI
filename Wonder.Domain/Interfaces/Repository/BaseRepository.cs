using System.Collections.Generic;
using FluentValidation;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        bool Insert(TEntity obj);

        bool Update(TEntity obj);

        bool Delete(int id);

        IList<TEntity> Select();

        TEntity Select(int id);
    }
}