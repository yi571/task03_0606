using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.Interface
{
    public interface IOrderRepository : IDisposable
    {

        void Create(Order instance);

        void Update(Order instance);

        void Delete(Order instance);

        Order Get(int orderId);

        IQueryable<Order> GetAll();

        void SaveChanges();
    }
}