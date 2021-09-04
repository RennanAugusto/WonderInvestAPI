using System;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Wonder.Infra.Data.Repository
{
    public class BaseRepositoryImpl<TEntity>: IBaseRepository<TEntity> where TEntity: Base
    {
        protected PostgreSqlContext _postgreSqlContext ;

        public BaseRepositoryImpl(PostgreSqlContext postgreSqlContext)
        {
            this._postgreSqlContext = postgreSqlContext;
        }

        public bool Insert(TEntity obj)
        {
            try
            {
                _postgreSqlContext.Set<TEntity>().Add(obj);
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Update(TEntity obj)
        {
            try
            {
                _postgreSqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _postgreSqlContext.Set<TEntity>().Remove(Select(id));
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public TEntity Select(int id)
        {
            return _postgreSqlContext.Set<TEntity>().Find(id);
        }

        public IList<TEntity> Select()
        {
           return _postgreSqlContext.Set<TEntity>().ToList();
        }
    }
}