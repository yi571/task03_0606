using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using task03_0606.Models.Interface;

namespace task03_0606.Models.Repositiry
{
    public class OrderRepository : IOrderRepository  , IDisposable
    {
        protected FoodCourtDBEntities db
        {
            get;
            private set;
        }

        public void Create(Order instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Orders.Add(instance);
                this.SaveChanges();
            }
        }

        public void Delete(Order instance)
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


        public Order Get(int orderId)
        {
            return db.Orders.FirstOrDefault(x => x.orderId == orderId);
        }

        public IQueryable<Order> GetAll()
        {
            return db.Orders.OrderBy(x => x.orderId);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Update(Order instance)
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