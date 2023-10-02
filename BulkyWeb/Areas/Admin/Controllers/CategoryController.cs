using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() //List of Category
        {
            List<Category> objList = _unitOfWork.Category.GetAll().ToList();
            return View(objList);
        }

        [HttpGet] // CREATE
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] // CREATE
        public IActionResult Create(Category obj)
        {
            //Server side Custom Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Order and Name cannot be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet] // EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id); // 1-way
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id); // 2-way
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); // 3-way
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost] // EDIT
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet] // DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")] // DELETE
        public IActionResult DeletePOST(int? id)
        {
            Category? getValue = _unitOfWork.Category.Get(u => u.Id == id);
            if (getValue == null) { return NotFound(); }
            _unitOfWork.Category.Remove(getValue);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
