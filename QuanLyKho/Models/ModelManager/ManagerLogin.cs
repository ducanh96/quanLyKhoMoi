using QuanLyKho.Models.ModelDB;
using QuanLyKho.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models.ModelManager
{
    public class ManagerLogin
    {
        #region Kiem tra ton tai tai khoan


        public bool IsUserName(string userName)
        {
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                var user = db.Table_User.Where(x => x.UserName == userName).ToList();
                if (user.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        #endregion

        #region Kiem tra ton tai tai khoan de dang ky

        public bool IsExitsUserToSignUp(string userName)
        {
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                bool kqua = db.Table_User.Where(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefault() != null;
                return kqua;
            }
        }

        #endregion

        #region Lấy mật khẩu


        public string GetPassword(string userName)
        {

            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {

                var pass = db.Table_User.Where(x => x.UserName == userName).Select(x => x.UserPassword).FirstOrDefault();
                if (pass != null)
                {
                    return pass;
                }
                else
                    return null;
            }
        }
        
        #endregion

        #region Thêm tài khoản

        public void AddUser(Table_User _user)
        {
            if (_user.UserName.Length > 0 && _user.UserPassword.Length > 0)
            {
                using (QuanLyKhoEntities db = new QuanLyKhoEntities())
                {

                    using (var dbTran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Table_User.Add(_user);
                            db.SaveChanges();
                            dbTran.Commit();

                        }
                        catch (Exception)
                        {
                            dbTran.Rollback();
                       }
                    }


                }
            }
        }

        #endregion

        #region Lấy profile người dùng

        public UserProfile GetUserProfile(int? id)
        {
            if(id == null)
            {
                return null; 
            }
            
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                return db.UserProfiles.Find(id);
            }
        }
        #endregion

        #region Lay id cua nguoi dung voi ten user
        public static int GetId(string userName)
        {
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                var user = db.Table_User.Where(x => x.UserName == userName).FirstOrDefault();
                if (user == null)
                {
                    return -1;
                }
                else
                {
                    return user.UserId;
                }

            }
        }
        #endregion

        #region Lay FullName
        public string GetName()
        {
            using (var db = new QuanLyKhoEntities())
            {
                int id = GetId(HttpContext.Current.User.Identity.Name);
                var name = db.UserProfiles.Find(id).Name;
                return name;
            }
                
        }
        #endregion

        #region Lấy danh sách người dùng
        public static List<User> GetListUser()
        {
            List<User> listUser = new List<User>();
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                
                foreach (var item in db.Table_User)
                {
                    User _user = new User
                    {
                        UserName = item.UserName,
                        Name = item.UserProfile.Name,
                        Role = item.UserRoles.FirstOrDefault().RoleId

                    };
                    listUser.Add(_user);
                   
                }
            }
            return listUser;
        }
        #endregion

    }
}