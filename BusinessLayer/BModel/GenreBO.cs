using AutoMapper;
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
    public class GenreBO:BussinessObject<Genres>
    {
        public readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string Name { get; set; }


        public GenreBO(IMapper mapper,UnitOfWorkFactory<Genres> unitOfWorkFactory,IUnityContainer unityContainer)
            :base(mapper,unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public GenreBO GetListGenreById(int? id)
        {
            GenreBO genres;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genres = unitOfWork.EntityRepository.GetAll().Where(x => x.Id == id).Select(item => mapper.Map<GenreBO>(item)).FirstOrDefault();
             
            }
            return genres;
        }

        public List<GenreBO> GetListGenre()
        {
            List<GenreBO> genres = new List<GenreBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genres = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<GenreBO>(item)).ToList();
            }
            return genres;
        }

        public void Add(IUnitOfWork<Genres> unitOfWork)
        {
            var books = mapper.Map<Genres>(this);
            unitOfWork.EntityRepository.Add(books);
            unitOfWork.Save();
        }

        public void Update(IUnitOfWork<Genres> unitOfWork)
        {
            var books = mapper.Map<Genres>(this);
            unitOfWork.EntityRepository.Update(books);
            unitOfWork.Save();
        }

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                {
                    Update(unitOfWork);
                }
                else
                {
                    Add(unitOfWork);
                }
            }
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
