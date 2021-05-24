using CollegeManagement.DataAccess.Repository;
using CollegeManagement.DataAccess.Repository.IRepository;
using CollegeManagement.Model;
using CollegeManagement.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverType CoverType { get; private set; }

        public   CoverTypeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {

            CoverType coverType = new();
            if (id == null)
            {
                return View(CoverType);
            }
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            CoverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            if(coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        private IActionResult NewMethod(CoverType coverType)
        {
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)

            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);
                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                                                                                                          
                   
                }
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);



                }
               
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);

        }






        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allObj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
                
            if (objFromDb == null)
            {
                return Json(new { sucess = false, message = "Error while deleting" });

            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
                                                                                                  
          
            return Json(new { success = true, message = "Delete Successful" });

        }





        #endregion


    }
}


