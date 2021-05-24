using CollegeManagement.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Model;

namespace CollegeManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
  
        public class StudentRegistrationController : Controller
        {

            private readonly IUnitOfWork _unitOfWork;

            public StudentRegistrationController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public IActionResult Index()
            {
                return View();
            }
            public IActionResult Upsert(int? id)
            {

            StudentRegistration studentRegistration = new StudentRegistration();
                if (id == null)
                {
                    return View(studentRegistration);

                }

                studentRegistration = NewMethod(id);
                return studentRegistration == null ? NotFound() : View(studentRegistration);
            }
        private StudentRegistration NewMethod(int? id)
        {
            return _unitOfWork.Student.Get(id.GetValueOrDefault());
        }


        [HttpPost]
            [ValidateAntiForgeryToken]

            public IActionResult Upsert(StudentRegistration studentRegistration)
            {
                if (ModelState.IsValid)

                {
                    if (studentRegistration.Id == 0)
                    {
                        _unitOfWork.Student.Add(studentRegistration);

                    }
                    else
                    {
                        _unitOfWork.Student.Update(studentRegistration);

                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(studentRegistration);

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
                if (objFromDb == null)
                {
                    return Json(new { sucess = false, message = "Error while deleting" });

                }
            _unitOfWork.Student.Remove(objFromDb);

                return Json(new { success = true, message = "Delete Successful" });

            }





            #endregion


        }

    }

