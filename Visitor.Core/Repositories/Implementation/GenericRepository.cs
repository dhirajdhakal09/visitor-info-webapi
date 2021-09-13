﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Visitor.DatabaseMigration;

namespace Visitor.Core
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDatabaseContext _dbContext;

        public GenericRepository(AppDatabaseContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException("dbContext was not supplied");
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetQuerable()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            return this.GetQuerable().SingleOrDefault<T>(criteria);
        }

        public T GetFirst(Expression<Func<T, bool>> criteria)
        {
            return this.GetQuerable().FirstOrDefault<T>(criteria);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> criteria)
        {
            return this.GetQuerable().Where(criteria);
        }

        public IEnumerable<T> Get(Expression<Func<T, object>> orderBy, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return this.GetQuerable().OrderBy(orderBy).AsEnumerable();
            }
            else
            {
                return this.GetQuerable().OrderByDescending(orderBy).AsEnumerable();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return this.GetQuerable().Where(criteria).OrderBy(orderBy).AsEnumerable();
            }
            else
            {
                return this.GetQuerable().Where(criteria).OrderByDescending(orderBy).AsEnumerable();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, object>> orderBy, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return this.GetQuerable().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            else
            {
                return this.GetQuerable().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return this.GetQuerable().Where(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            else
            {
                return this.GetQuerable().Where(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

    
        public IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            
            return _dbSet.FromSqlRaw(query, parameters).AsEnumerable();
        }

        public int Count()
        {
            return this.GetQuerable().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return this.GetQuerable().Count(criteria);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            _dbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            // check if local is not null 
            if (entity != null)
            {
                // detach
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

    }
}