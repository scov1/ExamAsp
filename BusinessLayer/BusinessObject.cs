using AutoMapper;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public abstract class BussinessObject<T> where T : class
    {
        protected IMapper mapper;
        protected UnitOfWorkFactory<T> unitOfWorkFactory;

        public BussinessObject(IMapper mapper, UnitOfWorkFactory<T> unitOfWorkFactory)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}
