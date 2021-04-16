﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uplift.DataAccess.Data.Repository.IRepository;
using uplift.DataAccess.Repository;
using Uplift.DataAccess.Data;
using Uplift.Models;

namespace uplift.DataAccess.Data.Repository
{
   public class FrequencyRepository: Repository<Frequency>,IFrequencyRepository
    {
        private readonly ApplicationDbContext _db;

        public FrequencyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {
            return _db.Frequencies.Select(i => new SelectListItem() { 
                Text = i.Name,
                Value = i.Id.ToString()
            });

        }

        public void Update(Frequency frequency)
        {
            var objFromDb = _db.Frequencies.FirstOrDefault(s => s.Id == frequency.Id);

            objFromDb.Name = frequency.Name;
            objFromDb.FrequencyCount = frequency.FrequencyCount;

            _db.SaveChanges();
        }
    }
}
