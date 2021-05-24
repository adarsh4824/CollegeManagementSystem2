using CollegeManagement.DataAccess.Repository;
using CollegeManagement.DataAccess.Repository.IRepository;
using CollegeManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Category1Controller : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public Category1Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int?  id)
        {

            Category1 category = new();
            if (id == null)
            {
                return View(category);

            }

            category = NewMethod(id);
            return category == null ? NotFound() : View(category);
        }

        private Category1 NewMethod(int? id)
        {
            return _unitOfWork.Category.Get(id.GetValueOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Category1 category)
        {
            if (ModelState.IsValid)

            {
                if(category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                   
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                   
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }






        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
           var abj = _unitOfWork.Student.GetAll();
            return Json(new { data = abj });


        }
        [HttpDelete]
        
        public IActionResult Delete(int id)
        {

            var objFromDb = _unitOfWork.Student.Get(id);
            if(objFromDb == null)
            {
                return Json(new { sucess = false, message = "Error while deleting" });

            }
            _unitOfWork.Student.Remove(objFromDb);

            return Json(new { success = true, message = "Delete Successful" });

        }





        #endregion


    }
}