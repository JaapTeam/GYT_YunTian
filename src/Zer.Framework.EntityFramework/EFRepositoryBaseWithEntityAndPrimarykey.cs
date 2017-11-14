using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Zer.Framework.Entities;
using Zer.Framework.Exception;
using Zer.Framework.Repository;

namespace Zer.Framework.EntityFramework
{
    public abstract class EfRepositoryBaseWithPrimaryKey<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly DbContext _dbContext;

        private DbSet<TEntity> Table { get; set; }

        protected EfRepositoryBaseWithPrimaryKey(DbContext dbContext)
        {
            this._dbContext = dbContext;
            Table = _dbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(TPrimaryKey id)
        {
            var entity = FirstOrDefault(CreateEqualityExpressionForId(id));

            if (entity == null)
            {
                throw new CustomException(
                    string.Format("{0} Entity is not found!", typeof(TEntity).Name),
                    new Dictionary<string, string>()
                    {
                        {typeof(TPrimaryKey).Name, id.ToString()}
                    });
            }

            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Table.OrderByDescending(x => x.Id);
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public virtual TEntity Insert(TEntity entity)
        {
            var result = Table.Add(entity);
            _dbContext.SaveChanges();
            return result;
        }

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> list)
        {
            foreach (var entity in list)
            {
                RepleaseWhitespaceString(entity);
            }
            var result = Table.AddRange(list);
            _dbContext.SaveChanges();
            return result;
        }

        private void RepleaseWhitespaceString(TEntity entity)
        {
            foreach (var propertyInfo in typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string)))
            {
                var value = propertyInfo.GetValue(entity);
                if (value == null) continue;
                value = value.ToString().Trim();
                propertyInfo.SetValue(entity, value);
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = GetById(id);
            updateAction(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Delete(TPrimaryKey id)
        {
            var entity = GetById(id);

            if (entity == null) return;

            Delete(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entityList = Table.Where(predicate).ToList();
            foreach (var entity in entityList)
            {
                AttachIfNot(entity);
                Table.Remove(entity);
            }
            _dbContext.SaveChanges();
        }

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate);
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LongCount(predicate);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

    }
}
