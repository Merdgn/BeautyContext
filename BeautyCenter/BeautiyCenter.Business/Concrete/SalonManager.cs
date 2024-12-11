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
    public class SalonManager :ISalonService
    {
        private readonly ISalonDal _salonDal;

        public SalonManager(ISalonDal salonDal)
        {
            _salonDal = salonDal;
        }

        public void TDelete(int id)
        {
            _salonDal.Delete(id);
        }

        public List<Salon> TGetAll()
        {
            return _salonDal.GetAll();
        }

        public Salon TGetById(int id)
        {
            return _salonDal.GetById(id);
        }

        public void TInsert(Salon entity)
        {
            _salonDal.Insert(entity);
        }

        public void TUpdate(Salon entity)
        {
            _salonDal.Update(entity);
        }
    }
}
