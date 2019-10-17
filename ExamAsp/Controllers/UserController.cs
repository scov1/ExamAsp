using AutoMapper;
using BusinessLayer.BModel;
using ExamAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamAsp.Controllers
{
    public class UserController : Controller
    {
        protected IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var userList = userBO.GetUsersList();
            ViewBag.Users = userList.Select(m => mapper.Map<UserModel>(m)).ToList();

            return View(ViewBag.Users);
        }


        public ActionResult Edit(int? id)
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var model = mapper.Map<UserModel>(userBO);
            if (id != null)
            {
                var userList = userBO.GetUsersListById(id);
                model = mapper.Map<UserModel>(userList);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            var userBO = mapper.Map<UserBO>(model);
            userBO.Save();

            return RedirectToActionPermanent("Index", "User");
        }


        public ActionResult Delete(int id)
        {
            var user = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(id);
            user.Delete(id);

            return RedirectToActionPermanent("Index", "User");
        }
    }
}