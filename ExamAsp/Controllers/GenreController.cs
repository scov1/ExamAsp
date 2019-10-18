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
    public class GenreController : Controller
    {

        protected IMapper mapper;

        public GenreController(IMapper mapper)
        {
            this.mapper = mapper;
        }


        public ActionResult Index()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var genreList = genreBO.GetGenreList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreModel>(m)).ToList();

            return View();
        }

        public ActionResult Edit(int? id)
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var model = mapper.Map<GenreModel>(genreBO);


            if (id != null)
            {
                var genreList = genreBO.GetListGenreById(id);
                model = mapper.Map<GenreModel>(genreList);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GenreModel model)
        {
            var genreBO = mapper.Map<GenreBO>(model);
            genreBO.Save();

            return RedirectToActionPermanent("Index", "Genre");
        }

        public ActionResult Delete(int id)
        {
            var genre = DependencyResolver.Current.GetService<GenreBO>().GetListGenreById(id);
            genre.Delete(id);

            return RedirectToActionPermanent("Index", "Genre");
        }
    }
}