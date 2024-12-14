﻿using BeautiyCenter.DataAccess.Abstract;
using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.DataAccess.Repositories;
using BeautiyCenter.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.DataAccess.EntityFramework
{
    public class EfSalonDal : GenericRepository<Salon>, ISalonDal
    {
        public EfSalonDal(BeautyCenterContext context) : base(context)
        {
        }
    }
}