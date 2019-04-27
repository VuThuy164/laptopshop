using LaptopShop.Areas.Admin.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddUser(EditUserModel user)
        {
            if(ModelState.IsValid)
            {

                if(user.Password == user.RePassword)
                {
                    user _user = new user()
                    {
                        email = user.Email,
                        full_name = user.FullName,
                        password = user.Password,
                        RoleID = 2,
                        username = user.UserName,
                        created_at = DateTime.Now
                    };
                    var dao = new UserDao();
                    dao.AddUser(_user);
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không khớp");
                }

            }
            else
            {
                ModelState.AddModelError("", "dang ky khong dung");
            }
            return View();
        }
    }
}