using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db; //This is a private field that holds an instance of ApplicationDbContext. ApplicationDbContext is a class commonly used in Entity Framework to interact with the database.
        internal DbSet<T> dbSet; //This is an internal field of type DbSet<T>. A DbSet<T> represents the collection of all entities in the context
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); //initializes the _db field with the provided context and sets the dbSet field to the Set<T> associated with the provided context.
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        public void Add(T entity) //This method adds an entity of type T to the dbSet using the Add method provided by DbSet<T>.
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null) // This method takes a filter expressed as a lambda function and retrieves a single entity of type T from the dbSet based on the provided filter.
        {
            IQueryable<T> query = dbSet; 
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null) //retrieves all entities of type T from the dbSet
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in  includeProperties
                    .Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return query.ToList();
        }

        public void Remove(T entity) //removes a specific entity of type T from the dbSet
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity) //removes a range of entities of type T from the dbSet
        {
            dbSet.RemoveRange(entity);

        }
    }
}
