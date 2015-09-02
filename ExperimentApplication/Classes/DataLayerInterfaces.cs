using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace ExperimentApplication.Classes
{
    public interface IBaseEntity
    {
        [NotMapped]
        long EntityId { get; }
    }

    /// <summary>
    /// Base implementation of the IRepository
    /// </summary>
    /// <typeparam name="T">The T object</typeparam>
    /// <typeparam name="TKey">The TKey object</typeparam>
    public abstract class BaseRepository<T, TKey> : IDisposable where T : class, IBaseEntity
    {
        private readonly DbContext _context;
        protected static Logger Logger = LogManager.GetCurrentClassLogger();

        protected BaseRepository(DbContext context)
        {
            _context = context;
        }

        protected BaseRepository()
        {

        }

        public virtual DbSet<T> DbSet => _context.Set<T>();

        public virtual T CreateInstance()
        {
            return DbSet.Create();
        }

        public virtual T Create(T TObject)
        {
            return DbSet.Add(TObject);
        }

        public virtual int Count => DbSet.Count();

        public void LoadReferences(T TObject, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                var entry = _context.Entry(TObject);
                var key = GetPrimaryKey(entry);

                if (entry.State == EntityState.Detached)
                {
                    var currentEntry = DbSet.Local.SingleOrDefault(e => e.EntityId == key);
                    if (currentEntry != null)
                    {
                        var attachedEntry = _context.Entry(currentEntry);
                        attachedEntry.CurrentValues.SetValues(TObject);
                    }
                    else
                        DbSet.Attach(TObject);
                }

                foreach (var navProp in navigationProperties)
                    entry.Reference(navProp).Load();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public virtual bool Delete(T TObject)
        {
            try
            {
                var entry = _context.Entry(TObject);
                var key = GetPrimaryKey(entry);

                if (entry.State == EntityState.Detached)
                {
                    var currentEntry = DbSet.Local.SingleOrDefault(e => e.EntityId == key);
                    if (currentEntry == null)
                    {
                        DbSet.Attach(TObject);
                        entry.State = EntityState.Deleted;
                    }
                    else
                        entry = _context.Entry(currentEntry);
                }

                entry.State = EntityState.Deleted;
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public virtual bool Update(T TObject)
        {
            try
            {
                var entry = _context.Entry(TObject);
                var key = GetPrimaryKey(entry);

                if (entry.State == EntityState.Detached)
                {
                    var currentEntry = DbSet.Local.SingleOrDefault(e => e.EntityId == key);
                    if (currentEntry != null)
                    {
                        var attachedEntry = _context.Entry(currentEntry);
                        attachedEntry.CurrentValues.SetValues(TObject);
                    }
                    else
                    {
                        DbSet.Attach(TObject);
                        entry.State = EntityState.Modified;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public virtual IEnumerable<T> All()
        {
            return DbSet.ToList();
        }

        public T GetById(TKey id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Attach(T t)
        {
            var entry = _context.Entry(t);
            var key = GetPrimaryKey(entry);
            var currentEntry = DbSet.Local.SingleOrDefault(e => e.EntityId == key);
            if (currentEntry == null)
                DbSet.Attach(t);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void CreateList(List<T> objList)
        {
            objList.ForEach(obj => _context.Entry(obj).State = EntityState.Added);
        }

        public void DeleteList(List<T> objList)
        {
            objList.ForEach(obj => Delete(obj));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void RefreshEntity(T t)
        {
            _context.Entry(t).Reload();
        }

        public void LoadCollections(T TObject, params Expression<Func<T, ICollection<object>>>[] collectionProperties)
        {
            //Sample Usage : _bucket.VendorRepository.LoadCollections(vendor, v => (ICollection<object>) v.Queries, v =>  (ICollection<object>) v.VendorContacts);
            try
            {
                var entry = _context.Entry(TObject);
                var key = GetPrimaryKey(entry);

                if (entry.State == EntityState.Detached)
                {
                    var currentEntry = DbSet.Local.SingleOrDefault(e => e.EntityId == key);
                    if (currentEntry != null)
                    {
                        var attachedEntry = _context.Entry(currentEntry);
                        attachedEntry.CurrentValues.SetValues(TObject);
                    }
                    else
                    {
                        DbSet.Attach(TObject);
                    }
                }

                foreach (var navProp in collectionProperties) entry.Collection(navProp).Load();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public long GetPrimaryKey(DbEntityEntry entry)
        {
            var myObject = entry.Entity;
            var property = myObject.GetType()
                     .GetProperties()
                     .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (property == null)
                return 0;

            return (long)property.GetValue(myObject, null);
        }

        public string[] GetEntityKeyNames(DbEntityEntry entry)
        {
            return entry.Entity.GetType()
                     .GetProperties()
                     .Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)))
                     .Select(s => s.Name).ToArray();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken token)
        {
            return _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return DbSet;
        }
    }
}