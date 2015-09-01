using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExperimentApplication.Classes
{
    public interface IRepository<T, TKey> where T : class
    {
        DbSet<T> DbSet { get; }
        int Count { get; }
        T CreateInstance();
        T Create(T TObject);
        void LoadReferences(T TObject, params Expression<Func<T, object>>[] navigationProperties);
        bool Delete(T TObject);
        bool Update(T TObject);
        IEnumerable<T> All();
        T GetById(TKey id);
        Task<T> GetByIdAsync(TKey id);
        void Attach(T t);
        void Dispose();
        void CreateList(List<T> objList);
        void DeleteList(List<T> objList);
        void SaveChanges();
        void RefreshEntity(T t);
        void LoadCollections(T TObject, params Expression<Func<T, ICollection<object>>>[] collectionProperties);
        long GetPrimaryKey(DbEntityEntry entry);
        string[] GetEntityKeyNames(DbEntityEntry entry);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken token);
        Task<IEnumerable<T>> AllAsync();
        IQueryable<T> GetQueryable();



    }
}
