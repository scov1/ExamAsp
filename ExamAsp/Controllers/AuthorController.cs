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
    public class AuthorController : Controller
    {
        protected IMapper mapper;
        public AuthorController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var authorList = authorBO.GetAuthorsList();
            ViewBag.Authors = authorList.Select(x => mapper.Map<AuthorModel>(x)).ToList();

            //ViewBag.Authors = authorList.Select(item => mapper.Map<AuthorModel>(item)).ToList();
       

            return View();
        }


        public ActionResult Edit(int? id)
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<AuthorModel>(authorBO);

            if (id != null)
            {
                var authorBOList = authorBO.GetAuthorsListById(id);
                model = mapper.Map<AuthorModel>(authorBOList);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(AuthorModel model)
        {
            var authorBO = mapper.Map<AuthorBO>(model);
            if (ModelState.IsValid)
            {
                authorBO.Save();
                return RedirectToActionPermanent("Index", "Author");
            }
            else return View(model);
        }


        public ActionResult Delete(int id)
        {
            var author = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsListById(id);
            author.Delete(id);

            return RedirectToActionPermanent("Index", "Author");
        }

    }
}