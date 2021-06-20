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
    public class CategoryDao
    {
        PhamVinhContext db = null;
        public CategoryDao()
        {
            db = new PhamVinhContext();
        }

        //them
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        //lay ra danh sach sản phẩm
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        //tim và lay ID
        public Category GetById(string name)
        {
            return db.Categories.SingleOrDefault(x => x.Name == name);
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }


        //lay ra danh sach cua category de tao drop cho loai san pham
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }
    }
}
