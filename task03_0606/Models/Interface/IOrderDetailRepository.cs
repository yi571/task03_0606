using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.Interface
{
    public interface IOrderDetailRepository : IDisposable
    {
        void Create(OrderDetial instance);

        void Update(OrderDetial instance);

        void Delete(OrderDetial instance);

        OrderDetial Get(int orderId);

        IQueryable<OrderDetial> GetAll();

        void SaveChanges();

    }
}