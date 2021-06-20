using ModelEF.Dao;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;
using PagedList;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        //Load ra trang sản phẩm:
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var prd = new ProductDao();
            var model = prd.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        //Yêu cầu hiện DropDownlist đã gọi từ viewbag productdao
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryId = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }


        //Thêm người sản phẩm, gọi hàm từ productdao
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var prd = new ProductDao();


                long id = prd.Insert(product);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thành công");
                }
            }
            return View("Index");
        }

        public ActionResult Detail(int id)
        {
            var ex= new ProductDao().Find(id);
            return View(ex);

        }

        public ActionResult Edit(int id)
        {
            var user = new ProductDao().ViewDetail(id);
            SetViewBag();
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
               
                var result = dao.Update(pro);
                if (result)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm  không thành công");
                }
            }
            return View("Index");
        }
    }
}