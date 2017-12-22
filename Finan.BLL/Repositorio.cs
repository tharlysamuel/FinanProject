using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.BLL
{
    public class Repositorio<T> : IDisposable, IRepositorio<T> where T : class
    {
        public Repositorio()
        {

        }

        public void Insert(T item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Set<T>().Add(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }

        }

        public virtual void Update(T item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Entry<T>(item).State = EntityState.Modified;
                bd.SaveChanges();

            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao gravar:" + erro.Message);
            }
        }

        public virtual void Delete(T item)
        {
            try
            {
                Contexto bd = new Contexto();
                bd.Set<T>().Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }

        public virtual void Delete(params object[] keysValues)
        {
            try
            {
                Contexto bd = new Contexto();
                var item = bd.Set<T>().Find(keysValues);
                bd.Set<T>().Remove(item);
                bd.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new Exception("Falha ao excluir:" + erro.Message);
            }
        }

        public virtual T Get(params object[] keysValues)
        {
            Contexto bd = new Contexto();
            return bd.Set<T>().Find(keysValues);
        }

        public ICollection<T> GetWhere(Func<T, bool> predicate)
        {
            Contexto bd = new Contexto();
            return bd.Set<T>().Where(predicate).ToList();
        }

        public ICollection<T> GetAll()
        {
            try
            {
                Contexto bd = new Contexto();
                return (bd.Set(typeof(T)) as IQueryable<T>).ToList();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }

        public void Dispose()
        {

        }
    }
}
