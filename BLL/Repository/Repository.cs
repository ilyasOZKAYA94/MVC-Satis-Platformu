using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private SatiyorumContext _satiyorumContext; //veritabanı
        private DbSet<T> _dbSet; //tablo

        public Repository(SatiyorumContext context)
        {
            if (context != null)
            {
                _satiyorumContext = context;
                _dbSet = _satiyorumContext.Set<T>();
            }
        }


        public bool Add(T entity)
        {
            bool Sonuc = false;
            try
            {
                _dbSet.Add(entity);
                Sonuc = Convert.ToBoolean(_satiyorumContext.SaveChanges());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                //throw new Exception("Kayıt yapılamadı!");
            }
            return Sonuc;
        }        


        public bool Delete(T entity)
        {
            bool Sonuc = false;
            try
            {
                _dbSet.Remove(entity);
                Sonuc = Convert.ToBoolean(_satiyorumContext.SaveChanges());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
            }
            return Sonuc;
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
            {
                sorgu = sorgu.Where(filter);
            }
            if (orderby != null)
            {
                sorgu = orderby(sorgu);
            }
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.FirstOrDefault();
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
            {
                sorgu = sorgu.Where(filter);
            }
            if (orderby != null)
            {
                sorgu = orderby(sorgu);
            }
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.ToList();
        }

        public T GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

    }
}
