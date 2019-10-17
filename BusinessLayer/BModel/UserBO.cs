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
    public class UserBO:BussinessObject<Users>
    {
        IUnityContainer unityContainer;

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserBO(IMapper mapper, UnitOfWorkFactory<Users> unityOfWorkFactory, IUnityContainer unityContainer)
         : base(mapper, unityOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public UserBO GetUsersListById(int? id)
        {
            UserBO users;

            using (var unityOfWork = unitOfWorkFactory.Create())
            {
                users = unityOfWork.EntityRepository.GetAll().Where(x => x.Id == id).Select(item => mapper.Map<UserBO>(item)).FirstOrDefault();
            }
            return users;
        }

        public List<UserBO> GetUsersList()
        {
            List<UserBO> users = new List<UserBO>();

            using (var unityOfWork = unitOfWorkFactory.Create())
            {
                users = unityOfWork.EntityRepository.GetAll().Select(item => mapper.Map<UserBO>(item)).ToList();
            }
            return users;
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

        void Add(IUnitOfWork<Users> unitOfWork)
        {
            var user = mapper.Map<Users>(this);
            unitOfWork.EntityRepository.Add(user);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Users> unitOfWork)
        {
            var user = mapper.Map<Users>(this);
            unitOfWork.EntityRepository.Update(user);
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
