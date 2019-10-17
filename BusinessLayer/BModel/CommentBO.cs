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
    public class CommentBO:BussinessObject<Comments>
    {
        IUnityContainer unityContainer;

        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; }


        public CommentBO(IMapper mapper, UnitOfWorkFactory<Comments> unitOfWorkFactory, IUnityContainer unityContainer)
          : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public CommentBO GetListGenreById(int? id)
        {
            CommentBO comments;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                comments = unitOfWork.EntityRepository.GetAll().Where(x => x.Id == id).Select(item => mapper.Map<CommentBO>(item)).FirstOrDefault();

            }
            return comments;
        }

        public List<CommentBO> GetListGenre()
        {
            List<CommentBO> comments = new List<CommentBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                comments = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<CommentBO>(item)).ToList();
            }
            return comments;
        }

        public void Add(IUnitOfWork<Comments> unitOfWork)
        {
            var comment = mapper.Map<Comments>(this);
            unitOfWork.EntityRepository.Add(comment);
            unitOfWork.Save();
        }

        public void Update(IUnitOfWork<Comments> unitOfWork)
        {
            var comment = mapper.Map<Comments>(this);
            unitOfWork.EntityRepository.Update(comment);
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
