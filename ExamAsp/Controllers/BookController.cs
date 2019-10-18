﻿using AutoMapper;
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

        public ActionResult Index()
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var bookList = bookBO.GetBooksList();
            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();

            ViewBag.Books = bookList.Select(x => mapper.Map<BookModel>(x)).ToList();
            ViewBag.Authors = authorList.Select(x => mapper.Map<AuthorModel>(x)).ToList();
            ViewBag.Genres = genreList.Select(x => mapper.Map<GenreModel>(x)).ToList();

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
        public ActionResult Edit(BookModel model, HttpPostedFileBase imageBook)
        {
            string str = "check";
            var bookBO = mapper.Map<BookBO>(model);
            byte[] imageData = null;
            if (imageBook != null)
            {
                using (var binaryReader = new BinaryReader(imageBook.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageBook.ContentLength);
                }
                bookBO.ImageData = imageData;
            }
            else
            {
                bookBO.ImageData = new byte[str.Length];
            }

            bookBO.Save();
            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

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