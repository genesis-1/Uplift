using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using uplift.DataAccess.Data.Repository.IRepository;
using Uplift.DataAccess.Data;
using Uplift.Models;

namespace uplift.DataAccess.Repository
{
   public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Service service)
        {
            var objFromDb = _db.Services.FirstOrDefault(s => s.Id == service.Id);

            objFromDb.Name = service.Name;
            objFromDb.LongDesc = service.LongDesc;
            objFromDb.Price = service.Price;
            objFromDb.ImageUrl = service.ImageUrl;
            objFromDb.CategoryId = service.CategoryId;
            objFromDb.FrequencyId = service.FrequencyId;


            _db.SaveChanges();

        }
    }
}
