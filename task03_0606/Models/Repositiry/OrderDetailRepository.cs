using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using task03_0606.Models.Interface;

namespace task03_0606.Models.Repositiry
{
    public class OrderDetailRepository : IOrderDetailRepository, IDisposable
    {

        protected FoodCourtDBEntities db
        {
            get;
            private set;
        }


        public void Create(OrderDetial instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.OrderDetials.Add(instance);
                this.SaveChanges();
            }
        }

        public void Delete(OrderDetial instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

       

        public OrderDetial Get(int orderId)
        {
            return db.OrderDetials.FirstOrDefault(x => x.orderId == orderId);
        }

        public IQueryable<OrderDetial> GetAll()
        {
            return db.OrderDetials.OrderBy(x => x.orderId);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Update(OrderDetial instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}