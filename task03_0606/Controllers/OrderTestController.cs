using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using task03_0606.Models.Interface;
using task03_0606.Models.Repositiry;

namespace task03_0606.Controllers
{
    public class OrderTestController : Controller
    {
        //private FoodCourtDBEntities db = new FoodCourtDBEntities();
        private IOrderRepository orderRepository;
        public OrderTestController()
        {
            this.orderRepository = new OrderRepository();
        }

        // GET: OrderTest
        public ActionResult Index()
        {
            var orders = this.orderRepository.GetAll().ToList();
            return View(orders.ToList());
        }

        // GET: OrderTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = this.orderRepository.Get(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: OrderTest/Create
        public ActionResult Create()
        {
            //ViewBag.phoneNum = new SelectList(db.UserInfoes, "phoneNum", "lastName");
            //ViewBag.tableId = new SelectList(db.TableListes, "tableId", "tableLocation");
            return View();
        }

        // POST: OrderTest/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "orderId,orderDate,orderState,phoneNum,tableId")] Order order)
        //{
        public ActionResult Create(Order order)
        {
            if (order != null && ModelState.IsValid)
            {
                //db.Orders.Add(order);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                this.orderRepository.Create(order);
                return RedirectToAction("Index");
            }
            else
            {
                return View(order);
            }


            //ViewBag.phoneNum = new SelectList(db.UserInfoes, "phoneNum", "lastName", order.phoneNum);
            //ViewBag.tableId = new SelectList(db.TableListes, "tableId", "tableLocation", order.tableId);
            //return View(order);
        }

        // GET: OrderTest/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.phoneNum = new SelectList(db.UserInfoes, "phoneNum", "lastName", order.phoneNum);
            //ViewBag.tableId = new SelectList(db.TableListes, "tableId", "tableLocation", order.tableId);
            //return View(order);

            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var order = this.orderRepository.Get(id.Value);
                return View(order);
            }
        }

        // POST: OrderTest/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "orderId,orderDate,orderState,phoneNum,tableId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.phoneNum = new SelectList(db.UserInfoes, "phoneNum", "lastName", order.phoneNum);
        //    ViewBag.tableId = new SelectList(db.TableListes, "tableId", "tableLocation", order.tableId);
        //    return View(order);
        public ActionResult Edit(Order order)
        {
            if (order != null && ModelState.IsValid)
            {
                this.orderRepository.Update(order);
                return View(order);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        // GET: OrderTest/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(order);

            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var order = this.orderRepository.Get(id.Value);
                return View(order);
            }
        }

        // POST: OrderTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Order order = db.Orders.Find(id);
            //db.Orders.Remove(order);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            try
            {
                var category = this.orderRepository.Get(id);
                this.orderRepository.Delete(category);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
