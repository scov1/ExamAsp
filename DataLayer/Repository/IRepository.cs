﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepository<T> where T :class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Delete(int id);
        void Update(T item);
        void Save();
    }
}
