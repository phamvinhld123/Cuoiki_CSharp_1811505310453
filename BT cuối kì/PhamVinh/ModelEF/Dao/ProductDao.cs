using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelEF.Model;
using PagedList.Mvc;
using System.Configuration;
using System.Data.Common;
using PagedList;
namespace ModelEF.Dao
{
    public class ProductDao
    {
        PhamVinhContext db = null;
        public ProductDao()
        {
            db = new PhamVinhContext();
        }

        //them sản phẩm
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //lay ra danh sach sản phẩm
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Status.Contains(searchString));
            }

            return model.OrderBy(x => x.Quantity).ThenByDescending(x=>x.UnitCost).ToPagedList(page, pageSize);
        }

        //tim và lay ID sản phẩm
        public Product GetById(string name)
        {
            return db.Products.SingleOrDefault(x => x.Name == name);
        }

        public List<Product>ListNewProducts(int unicost)
        {
            return db.Products.Where(x => x.UnitCost != null && x.Quantity >0).OrderByDescending(x => x.Quantity).Take(unicost).ToList();
        }

        //xem chi tiet
        public Product Find(long id)
        {
            return db.Products.Find(id);
        }

        //cap nhat
        public bool Update(Product entity)
        {
            try
            {
                var pro = db.Products.Find(entity.ID);
                pro.Name = entity.Name;
                pro.UnitCost = entity.UnitCost;
                pro.Quantity = entity.Quantity;
                pro.Image = entity.Image;
                pro.Description = entity.Description;
                pro.Status = entity.Status;
                pro.CategoryId = entity.CategoryId;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        //Lấy ra danh sách sản phẩm
        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }
    }
}
