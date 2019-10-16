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


    }


}
