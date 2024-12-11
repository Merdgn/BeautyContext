using BeautiyCenter.Business.Abstract;
using BeautiyCenter.DataAccess.Abstract;
using BeautiyCenter.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Business.Concrete
{
    public class ServiceManager :IServiceService
    {
        private readonly IServiceDal _serviceDal;

        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public void TDelete(int id)
        {
            _serviceDal.Delete(id);
        }

        public List<Service> TGetAll()
        {
            return _serviceDal.GetAll();
        }

        public Service TGetById(int id)
        {
            return _serviceDal.GetById(id);
        }

        public void TInsert(Service entity)
        {
            _serviceDal.Insert(entity);
        }

        public void TUpdate(Service entity)
        {
            _serviceDal.Update(entity);
        }
    }
}
