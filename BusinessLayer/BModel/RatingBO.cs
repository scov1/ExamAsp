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
    public class RatingBO:BussinessObject<Rating>
    {
        public readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? Votes { get; set; }

        public RatingBO(IMapper mapper, UnitOfWorkFactory<Rating> unityOfWorkFactory, IUnityContainer unityContainer)
        : base(mapper, unityOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public RatingBO GetListRatingById(int? id)
        {
            RatingBO ratings;

            using (var unityOfWork = unitOfWorkFactory.Create())
            {
                ratings = unityOfWork.EntityRepository.GetAll().Where(x => x.Id == id).Select(item => mapper.Map<RatingBO>(item)).FirstOrDefault();
            }
            return ratings;
        }

        public List<RatingBO> GetListRating()
        {
            List<RatingBO> ratings = new List<RatingBO>();

            using (var unityOfWork = unitOfWorkFactory.Create())
            {
                ratings = unityOfWork.EntityRepository.GetAll().Select(item => mapper.Map<RatingBO>(item)).ToList();
            }
            return ratings;
        }


        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                    Update(unitOfWork);
                else
                    Add(unitOfWork);
            }
        }

        void Add(IUnitOfWork<Rating> unitOfWork)
        {
            var rating = mapper.Map<Rating>(this);
            unitOfWork.EntityRepository.Add(rating);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Rating> unitOfWork)
        {
            var rating = mapper.Map<Rating>(this);
            unitOfWork.EntityRepository.Update(rating);
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
