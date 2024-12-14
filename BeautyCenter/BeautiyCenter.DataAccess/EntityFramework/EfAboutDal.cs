using BeautiyCenter.DataAccess.Abstract;
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
    public class EfAboutDal : GenericRepository<About>, IAboutDal
    {
        public EfAboutDal(BeautyCenterContext context) : base(context)
        {
        }
    }
}
