using Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class Repositories<T> : IRepository<T> where T : class
    {
        protected RepositoryContext RepositoryContext; 
        public Repositories(RepositoryContext repositoryContext) 
        { 
            RepositoryContext = repositoryContext; 
        }
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : 
            RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => 
            !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking() : 
            RepositoryContext.Set<T>().Where(expression);

        
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public void Add(T entity) => RepositoryContext.Set<T>().Add(entity);
    }
}
