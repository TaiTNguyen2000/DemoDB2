using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoDB2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace DemoDB2.Controllers
{
    public class ProductController : Controller
    {

        DBSportStoreEntities database = new DBSportStoreEntities();
        

        // GET: Product
        public ActionResult Index()
        {
            return View(database.Products.ToList());
        }

        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }

        

        [HttpPost]
        public ActionResult Create(Product Pro)
        {
            try
            {
                if(Pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(Pro.UploadImage.FileName);
                    string extend = Path.GetExtension(Pro.UploadImage.FileName);
                    filename = filename + extend;
                    Pro.ImagePro = filename;
                    Pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Products.Add(Pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}