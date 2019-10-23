using AutoMapper;
using BusinessLayer.BModel;
using ExamAsp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamAsp.Controllers
{
    public class BookController : Controller
    {
        protected IMapper mapper;

        public BookController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index(string sort)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var bookList = bookBO.GetBooksList();
            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();

            if (Request.IsAjaxRequest())
            {
                if (sort == "Genre")
                {
                    var books = bookBO.GetBooksList().Select(x => mapper.Map<BookModel>(x)).ToList();
                    ViewBag.Books = books.OrderBy(y => y.GenreId);

                }
                else if (sort == "None")
                {
                    ViewBag.Books = bookList.Select(x => mapper.Map<BookModel>(x)).ToList();

                }
                ViewBag.Authors = authorList.Select(x => mapper.Map<AuthorModel>(x)).ToList();
                ViewBag.Genres = genreList.Select(x => mapper.Map<GenreModel>(x)).ToList();
                return View("PartialView/OrderPartialView");
            }

            else
            {
                ViewBag.Books = bookBO.GetBooksList().Select(x => mapper.Map<BookModel>(x)).ToList();
                ViewBag.Authors = authorList.Select(x => mapper.Map<AuthorModel>(x)).ToList();
                ViewBag.Genres = genreList.Select(x => mapper.Map<GenreModel>(x)).ToList();
            }
                return View();
        }


        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<BookModel>(bookBO);
            var genres = DependencyResolver.Current.GetService<GenreBO>();

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookModel>(bookBOList);
            }

            ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(x => mapper.Map<AuthorModel>(x)).ToList(), "Id", "LastName");
            ViewBag.Genres = new SelectList(genres.GetGenreList().Select(x => mapper.Map<GenreModel>(x)).ToList(), "Id", "Name");

         
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(BookModel model, HttpPostedFileBase upload)
        {
            string str = "check";
            var bookBO = mapper.Map<BookBO>(model);
           
            if (ModelState.IsValid && upload != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    imageData = binaryReader.ReadBytes(upload.ContentLength);
                }
                bookBO.ImageData = imageData;
            }
            else
            {
                bookBO.ImageData = new byte[str.Length];
            }
            //bookBO.GenreId = genre;
            //bookBO.AuthorId = author;
            bookBO.Save();

            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();

            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorModel>(m)).ToList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreModel>(m)).ToList();

            return RedirectToActionPermanent("Index", "Book");
        }


        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);

            return RedirectToActionPermanent("Index", "Book");
        }
    }
}