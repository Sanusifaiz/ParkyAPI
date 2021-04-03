using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository

    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalParkDto nationalPark)
        {
            _db.NationalPark.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalParkDto nationalPark)
        {
            _db.NationalPark.Remove(nationalPark);
            return Save();
        }

        public NationalParkDto GetNationalPark(int nationalParkId)
        {
           return _db.NationalPark.FirstOrDefault(a => a.Id == nationalParkId);

            

        }

        public ICollection<NationalParkDto> GetNationalParks()
        {
            return _db.NationalPark.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalPark.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
           
            return value;
        }

        public bool NationalParkExists(int id)
        {
            bool value = _db.NationalPark.Any(a => a.Id == id);

            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalParkDto nationalPark)
        {
            _db.NationalPark.Update(nationalPark);
            return Save();
        }

      
    }

    
}
