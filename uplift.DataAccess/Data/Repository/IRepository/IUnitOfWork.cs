using System;
using System.Collections.Generic;
using System.Text;
using uplift.DataAccess.Data.Repository.IRepository;

namespace uplift.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }

        IServiceRepository Service { get; }

        void Save();
    }
}
