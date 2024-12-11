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
    public class AppointmentManager : IAppointmentService
    {
        private readonly IAppointmentDal _appointmentDal;

        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void TDelete(int id)
        {
            _appointmentDal.Delete(id);
        }

        public List<Appointment> TGetAll()
        {
           return _appointmentDal.GetAll();
        }

        public Appointment TGetById(int id)
        {
            return _appointmentDal.GetById(id);
        }

        public void TInsert(Appointment entity)
        {
            _appointmentDal.Insert(entity);
        }

        public void TUpdate(Appointment entity)
        {
            _appointmentDal.Update(entity);
        }
    }
}

