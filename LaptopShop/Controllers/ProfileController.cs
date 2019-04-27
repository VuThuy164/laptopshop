using LaptopShop.Areas.Admin.Models;
using LaptopShop.Common;
using LaptopShop.Controllers;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopShop.Models
{
    public class ProfileController : BaseController
    {

        private EditUserModel GetUserLogin()
        {
            var userLogin = (user)Session["loginsession"];
            EditUserModel editUser = new EditUserModel()
            {
                id = userLogin.id,
                Email = userLogin.email,
                FullName = userLogin.full_name,
                UserName = userLogin.username
            };

            return editUser;
        }
        // GET: Admin/Profile
        [HttpGet]
        public ActionResult Index()
        {
            return View(GetUserLogin());
        }

        // GET: Admin/Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Profile/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Admin/Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            UserDao dao = new UserDao();
            var userEdit = dao.GetUserById(model.id);
            if(userEdit!=null)
            {
                if(model.Password!=model.RePassword)
                {
                    ModelState.AddModelError("", "Mật khẩu không khớp");
                    return View(GetUserLogin());
                }

                userEdit.email = model.Email;
                userEdit.full_name = model.FullName;
                userEdit.password = model.Password;
                userEdit.updated_at = DateTime.Now;

                var result = dao.UpdateUser(userEdit);
                if(!result)
                {
                    ModelState.AddModelError("err", "Update không thành công");
                    return View(GetUserLogin());
                }
                EditUserModel editUser = new EditUserModel()
                {
                    id = userEdit.id,
                    Email = userEdit.email,
                    FullName = userEdit.full_name,
                    UserName = userEdit.username
                };
                Session[loginsession] = userEdit;

                return RedirectToAction("Index");


            }
            else
            {
                ModelState.AddModelError("err", "User không tồn tại");
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/Profile/Edit/5
        
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
