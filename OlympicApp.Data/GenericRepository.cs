using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OlympicApp.Data
{
    public abstract class GenericRepository<C, T> where T : class where C : DbContext, new() //missade först att skriva ",new()" och 
    {                                                                                       //fick då fel på rad 13. Kolla upp vad den gör.
        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual async void AddAsync(T entity)
        {
            await _entities.Set<T>().AddAsync(entity);
        }

        public virtual void AddRange(ICollection<T> entities)
        {
            _entities.Set<T>().AddRange(entities);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(ICollection<T> entities)
        {
            _entities.Set<T>().RemoveRange(entities);
        }

        public virtual ICollection<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).ToList<T>();
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Set<T>().Where(predicate).ToListAsync<T>();
        }

        public virtual ICollection<T> GetAll() // virtual = att man får override den = skriva egna GetAll metoder i repositoryt
        {

            return _entities.Set<T>().ToList();

        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            
            return await _entities.Set<T>().ToListAsync<T>();
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _entities.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(ICollection<T> entities)
        {
            _entities.Set<T>().UpdateRange(entities);
        }
    }
}
