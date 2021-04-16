using System;
using System.Collections.Generic;
using System.Text;

namespace uplift.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }

        void Save();
    }
}
