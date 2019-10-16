﻿using AutoMapper;
using DataLayer.Entities;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BusinessLayer.BModel
{
    public class BookBO:BussinessObject<Books>
    {
        public readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? GenreId { get; set; }
        public byte[] ImageData { get; set; }

    

        public BookBO(IMapper mapper,UnitOfWorkFactory<Books> unitOfWorkFactory,IUnityContainer unityContainer)
            :base(mapper,unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }


        public BookBO GetBooksListById(int? id)
        {
            BookBO books;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                books = unitOfWork.EntityRepository.GetAll().Where(x => x.Id == id).Select(item => mapper.Map<BookBO>(item)).FirstOrDefault();
            }
            return books;
        }

        public List<BookBO> GetBooksList()
        {
            List<BookBO> books = new List<BookBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                books = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<BookBO>(item)).ToList();
            }

            return books;
        }


        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                {
                    Update(unitOfWork);
                }
                else {
                    Add(unitOfWork);
                }
            }
        }

        public void Add(IUnitOfWork<Books> unitOfWork)
        {
            var book = mapper.Map<Books>(this);
            unitOfWork.EntityRepository.Add(book);
            unitOfWork.Save();
        }

        public void Update(IUnitOfWork<Books> unitOfWork)
        {
            var book = mapper.Map<Books>(this);
            unitOfWork.EntityRepository.Update(book);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.EntityRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
