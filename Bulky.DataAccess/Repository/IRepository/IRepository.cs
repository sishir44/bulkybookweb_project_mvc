using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        // T-Category
        IEnumerable<T> GetAll(string? includeProperties = null); //retrieve all entities of type T
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); //retrieve a single entity of type T based on a specified filter expressed as a lambda function.
        void Add(T entity); //add an entity of type T to the repository.
        void Remove(T entity); //remove a single entity of type T from the repository.
        void RemoveRange(IEnumerable<T> entity); //remove a collection of entities of type T from the repository.

    }
}
