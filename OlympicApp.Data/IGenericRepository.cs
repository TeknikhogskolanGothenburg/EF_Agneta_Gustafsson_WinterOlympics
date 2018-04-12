using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OlympicApp.Data
{
    public interface IGenericRepository<T> where T : class
    {
        //jag ser inte riktigt helheten av detta än, men testar att implementera det som övning. //18-03-29.
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddAsync(T entity);
        void AddRange(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        void Save(T entity);
    }
}
