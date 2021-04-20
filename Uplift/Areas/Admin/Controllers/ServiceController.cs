using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using uplift.DataAccess.Repository.IRepository;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ServiceVM ServiceVM { get; set; }

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Upsert(int ? id)
        {
            ServiceVM serviceVM = new ServiceVM()
            {
                Service = new Models.Service(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown()

            };

            if(id != null)
            {
                serviceVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
            }

            return View(serviceVM);
        }

        #region Api calls

        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(ServiceVM.Service.Id == 0)
                {
                    // New service
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\services");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestreams);

                    }
                    ServiceVM.Service.ImageUrl = @"\images\services\" + fileName + extension;
                    _unitOfWork.Service.Add(ServiceVM.Service);
                }
                else
                {
                    //edit service
                    var serviceFromDb = _unitOfWork.Service.Get(ServiceVM.Service.Id);
                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var filestreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filestreams);

                        }
                        ServiceVM.Service.ImageUrl = @"\images\services\" + fileName + extension_new;


                    }
                    else
                    {
                        ServiceVM.Service.ImageUrl = serviceFromDb.ImageUrl;

                    }

                    _unitOfWork.Service.Update(ServiceVM.Service);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ServiceVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                ServiceVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown();
                return View(ServiceVM);
            }

        }

        #endregion
    }
}
