using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Cover> objCoverList = _unitOfWork.Cover.GetAll();
            return View(objCoverList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cover obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Cover.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverFromDbFirst = _unitOfWork.Cover.GetFirstOrDefault(u => u.Id == id);

            if (coverFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cover obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Cover.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var coverFromDbFirst = _unitOfWork.Cover.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Cover.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Cover.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
