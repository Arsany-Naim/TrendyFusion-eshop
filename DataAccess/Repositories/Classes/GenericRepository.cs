using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Classes
{
	public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		public void Add(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
		}
		public void Remove(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}

		public void Update(TEntity entity)
		{
			_dbContext.Set<TEntity>().Update(entity);
		}

		public IEnumerable<TEntity> GetAll(bool withTracking = false)
		{
			if (withTracking)
				return _dbContext.Set<TEntity>().ToList();
			else
				return _dbContext.Set<TEntity>().AsNoTracking().ToList();

		}

		public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
		{
			return _dbContext.Set<TEntity>()
							 .Where(e => e.IsDeleted != true)
							 .Select(Selector)
							 .ToList();
		}

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> Predicate)
		{
			return _dbContext.Set<TEntity>()
							 .Where(Predicate)
							 .ToList();
		}

		public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);


	
	}
}
