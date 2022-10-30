using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using u21478377_HW06.Data;
using u21478377_HW06.Models;
using u21478377_HW06.ViewModels;

namespace u21478377_HW06.Controllers
{
    public class ReportController : Controller
    {
        private BikeStoresContext db = new BikeStoresContext();
        public async Task<ActionResult> Index(string sortOrder, string currentFilterTextbox, string textboxSearchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "Order" ? "order_desc" : "Order";
            ViewBag.SurnameSortParm = sortOrder == "Item" ? "item_desc" : "Item";
            ViewBag.SurnameSortParm = sortOrder == "Product" ? "product_desc" : "product";
            ViewBag.SurnameSortParm = sortOrder == "Category" ? "category_desc" : "Category";

            if (textboxSearchString != null)
            {
                page = 1;
            }
            else
            {
                textboxSearchString = currentFilterTextbox;
            }

            ViewBag.CurrentFilterTextbox = textboxSearchString;

            var ProdA = from ord in db.Categories
                        select ord;
            var OrdA = from pro in db.Products
                       select pro;
            var CarA = from ite in db.OrderItems
                       select ite;
            var OarA = from der in db.Orders
                       select der;

            OrdA = OrdA.Where(x => Convert.ToString(x.Category.CategoryName).Contains("Mountain Bikes"));
            OarA = OarA.Where(x => Convert.ToString(x.OrderStatus).Contains("sold"));

            var result = (from a in ProdA
                          join b in OrdA on a.CategoryId equals b.CategoryId
                          into randTable01
                          from b in randTable01.ToList()
                          join c in CarA on b.ProductId equals c.ProductId
                          into randTable02
                          from c in randTable02.ToList()
                          join d in OarA on c.OrderId equals d.OrderId
                          into randTable03
                          from d in randTable03.ToList()

                          select new ProductVM
                          {
                              ItemDetails = c,
                              ProductDetails = b,
                              CategoryDetails = a,
                              OrderDetails = d
                          });


            var success = db.ProductVM
                .Where(j => j.CategoryDetails.CategoryName == "Mountain bikes")
                .GroupBy(j => j.OrderDetails.OrderDate)
                .Select(group => new {
                    JobCompletion = group.Key,
                    Count = group.Count()
                });
            var countSuccess = success.Select(a => a.Count).ToArray();

            return new JsonResult(new { mySuccess = countSuccess });
        }
    }
}
