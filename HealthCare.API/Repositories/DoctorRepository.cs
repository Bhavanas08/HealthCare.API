using HealthCare.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.API.Repositories
{

    public class DoctorRepository : IRepository<Doctor>, IGetRepository<Doctor>
    {
        private readonly ApplicationDBContext _DbContext;

        public DoctorRepository(ApplicationDBContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task Create(Doctor obj)
        {
            _DbContext.Doctors.Add(obj);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<Doctor> Delete(int id)
        {
            var doctorDb = await _DbContext.Doctors.FindAsync(id);
            if (doctorDb != null)
            {
                _DbContext.Doctors.Remove(doctorDb);
                await _DbContext.SaveChangesAsync();
                return doctorDb;
            }
            return null;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _DbContext.Doctors.ToList();
        }

        public async Task<Doctor> GetById(int id)
        {
            var doctor = await _DbContext.Doctors.FindAsync(id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var doctorDb = await _DbContext.Doctors.FindAsync(id);
            if (doctorDb != null)
            {
                doctorDb.DoctorName = obj.DoctorName;
                doctorDb.Experience = obj.Experience;
          
                _DbContext.Doctors.Update(doctorDb);
                await _DbContext.SaveChangesAsync();
                return doctorDb;
            }
            return null;
        }
    }
}


