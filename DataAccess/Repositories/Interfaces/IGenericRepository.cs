using Demo.DataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		void Add(TEntity entity);
		void Remove(TEntity entity);
		void Update(TEntity entity);
		IEnumerable<TEntity> GetAll(bool withTracking = false);
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity , bool>> Predicate);
		IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector);
		TEntity? GetById(int id);


	}
}
