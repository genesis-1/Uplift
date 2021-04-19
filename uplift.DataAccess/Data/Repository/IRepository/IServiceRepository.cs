using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using uplift.DataAccess.Repository.IRepository;
using Uplift.Models;

namespace uplift.DataAccess.Data.Repository.IRepository
{
    public interface IServiceRepository: IRepositroy<Service>
    {
        void Update(Service service);
    }
}
