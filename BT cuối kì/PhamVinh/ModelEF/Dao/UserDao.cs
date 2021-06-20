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
    public class UserDao
    {
        PhamVinhContext db = null;
        public UserDao()
        {
            db = new PhamVinhContext();
        }

        //them
        public long Insert(UserAccount entity)
        {
            db.UserAccounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //cap nhat
        public bool Update(UserAccount entity)
        {
            try
            {
                var user = db.UserAccounts.Find(entity.ID);
                user.UserName = entity.UserName;
                
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        //lay ra danh sach người dùng
        public IEnumerable<UserAccount> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserAccount> model = db.UserAccounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Status.Contains(searchString));
            }

            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        //tim và lay ID
        public UserAccount GetById(string userName)
        {
            return db.UserAccounts.SingleOrDefault(x => x.UserName == userName);
        }

        public UserAccount ViewDetail(int id)
        {
            return db.UserAccounts.Find(id);
        }


        //Xoa
        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccounts.Find(id);
                db.UserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //dang nhap
        public int Login(string userName, string passWord)
        {
            //kiểm tra giá trị trong bảng với gtri truyền vào
            var result = db.UserAccounts.SingleOrDefault(x => x.UserName == userName);
            //nếu có giá trị đó
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == "Blocked")
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }

            }
        }
    }
}
