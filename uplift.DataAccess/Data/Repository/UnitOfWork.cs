﻿using System;
using System.Collections.Generic;
using System.Text;
using uplift.DataAccess.Data.Repository;
using uplift.DataAccess.Data.Repository.IRepository;
using uplift.DataAccess.Repository.IRepository;
using Uplift.DataAccess.Data;

namespace uplift.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Frequency = new FrequencyRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IFrequencyRepository Frequency { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
