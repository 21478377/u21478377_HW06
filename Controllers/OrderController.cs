using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using u21478377_HW06.Data;
using u21478377_HW06.ViewModels;

namespace u21478377_HW06.Controllers
{
    public class OrderController : Controller
    {
        private BikeStoresContext db = new BikeStoresContext();
        public IActionResult Index(pg=1)
        {
           
            List<ProductVM> products = new List<ProductVM>();

            const int pageSize = 10;

            //constant of page size
            if (pg < 1) pg = 1; //gets curent page, and prevents page from being 0, and negative nmber
            int Pagination = products.Count(); //finds no. entries in database table and returns number

            //declare variable
            var pager = new Pager(Pagination, pg, pageSize);

            int PaginationSkip = (pg - 1) * pageSize; //finds no. entries to skip when going to new page etc...

            var data = products.Skip(PaginationSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager; //assign page to viewbag

            // return View(products); 
            return View(data);
        }

        [HttpGet]
        public void Button_Click (object sender, EventArgs e)
        {
            List<ProductVM> products = new List<ProductVM>();

            DateTime date = Convert.ToDateTime(TxtFrom.Text);
            var result = from x in db.ProductVM where x.date <= OrderDate select x;
            
        }
    }
}
