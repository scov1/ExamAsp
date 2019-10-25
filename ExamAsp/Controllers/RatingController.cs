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
    public class RatingController : Controller
    {
        protected IMapper mapper;

        public RatingController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var ratingBO = DependencyResolver.Current.GetService<RatingBO>();

            var ratingList = ratingBO.GetListRating();
            var bookList = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

            ViewBag.Ratings = ratingList.Select(x => mapper.Map<RatingModel>(x)).ToList();
            ViewBag.Books = bookList.Select(x => mapper.Map<BookModel>(x)).ToList();

            return View();
        }

        public ActionResult Edit(int? id)
        {
            var ratingBO = DependencyResolver.Current.GetService<RatingBO>();
            var books = DependencyResolver.Current.GetService<BookBO>();
            var model = mapper.Map<RatingModel>(ratingBO);

            if(id!=null)
            {
                var ratingList = ratingBO.GetListRatingById(id);
                model = mapper.Map<RatingModel>(ratingList);
            }

            ViewBag.Books = new SelectList(books.GetBooksList().Select(x => mapper.Map<BookModel>(x)).ToList(), "Id", "Title");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RatingModel model)
        {
            var ratingBO = mapper.Map<RatingBO>(model);
            ratingBO.Save();

            return RedirectToActionPermanent("Index", "Rating");
        }

        //[HttpPost]
        //public ActionResult VoteBook(int? id)
        //{

        //}

        public ActionResult Delete(int id)
        {
            var rating = DependencyResolver.Current.GetService<RatingBO>().GetListRatingById(id);
            rating.Delete(id);

            return RedirectToActionPermanent("Index", "Rating");
        }
    }
}