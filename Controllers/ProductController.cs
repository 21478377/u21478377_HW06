//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data;
//using System.Net;
//using System.Web;
using u21478377_HW06.Data;
using u21478377_HW06.Models;
using u21478377_HW06.ViewModels;
using PagedList.Mvc;
//using PagedList;
//using System.Web.Mvc;
//using ActionResult = System.Web.Mvc.ActionResult;
//using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
//using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
//using Controller = Microsoft.AspNetCore.Mvc.Controller;
using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace u21478377_HW06.Controllers
{
    public class ProductController : Controller
    {
        private BikeStoresContext db = new BikeStoresContext();

        public ActionResult Index(string sortOrder, string currentFilterTextbox, string textboxSearchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.SurnameSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.SurnameSortParm = sortOrder == "Brand" ? "brand_desc" : "Brand";
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

            var ProdA = from ord in db.Brands
                        select ord;
            var OrdA = from pro in db.Products
                       select pro;
            var CarA = from ite in db.Categories
                       select ite;

            if (!String.IsNullOrEmpty(textboxSearchString))
            {
                OrdA = OrdA.Where(x => Convert.ToString(x.ProductName).Contains(textboxSearchString));
            }

            var result = (from a in ProdA
                          join b in OrdA on a.BrandId equals b.BrandId
                          into randTable01
                          from b in randTable01.ToList()
                          join c in CarA on b.CategoryId equals c.CategoryId
                          into randTable02
                          from c in randTable02.ToList()

                          select new ProductVM
                          {
                              BrandDetails = a,
                              ProductDetails = b,
                              CategoryDetails = c
                          }); ;
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(s => s.ProductDetails.ProductName);
                    break; //this is probably not necessary

                case "Year":
                    result = result.OrderBy(s => s.ProductDetails.ModelYear);
                    break;
                case "Price":
                    result = result.OrderBy(s => s.ProductDetails.ListPrice);
                    break;
                case "Brand":
                    result = result.OrderBy(s => s.BrandDetails.BrandName);
                    break;
                case "Category":
                    result = result.OrderBy(s => s.CategoryDetails.CategoryName);
                    break;

                default:
                    result = result.OrderBy(s => s.ProductDetails.ProductName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        ////details view, must return as modal!!
        public ActionResult Details(string id)
        {
            //gets id and returns model to view
            Product products = db.Products.Where(x => Convert.ToString(x.ProductId) == id).FirstOrDefault();
            //return View(products) ajaz request call
            return PartialView("_Details");
        }

        //edit view, must be returned in MODAL!!
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Product products = db.Products.Where(y => Convert.ToString(y.ProductId) == id).FirstOrDefault();
            return PartialView("_Edit");
        }

        //receieves page received
        [HttpPost]
        public ActionResult Edit(Product products)
        {
            //attaches to model
            db.Attach(products);
            //save changes
            db.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //delete, return in modal!!
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Product products = db.Products.Where(y => Convert.ToString(y.ProductId) == id).FirstOrDefault();
            return PartialView("_Delete");
        }
        [HttpPost]
        public ActionResult Delete(Product products)
        {

            //attaches to model
            db.Attach(products);
            //save changes
            db.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //create
        [HttpGet]
        public ActionResult Create()
        {
            Product products = new Product();
            return PartialView("_Create");

        }
        [HttpPost]
        public ActionResult Create(Product products)
        {
            //retrieve last product id from database
            var productid = db.Products.Max(productid => productid.ProductId);
            long productNew;

            //create new product id
            Int64.TryParse(productid.ToString(), out productNew);

            if (productid > 0)
            {
                productNew = productid + 1; //increase by 1
                productid = (int)productNew;
            }


            //attaches to model, set id
            db.Attach(products);
            //save changes
            db.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

