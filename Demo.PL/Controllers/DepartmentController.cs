using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inheritance : DepartmentController is a Controller
        // Aggregation : DepartmentController has a DepartmentRepository
        // BadRequest(): 400
        // NotFound()  : 404
        //private IDepartmentRepository _departmentRepository; // Attribute from DepartmentRepository to Get All Methods DependnacnyInjection
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork) // ASK CLR to Create Object from Class DepartmentRepo
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        // /Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll(); // Get All Departments and save it in "departments"
            return View(departments);

            ///return View();
            ///return View(departments'Model'); Overload [Used]
            ///return View("ViewName");  Overload
            ///return View("ViewName" , departments 'Model'); Overload
        }

        [HttpGet] // Default
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            // l action da hytnfz lma a3ml submit ll form bta3t el create
            if (ModelState.IsValid) // Server Side Validation
            {
                await _unitOfWork.DepartmentRepository.Add(department);

                int count = await _unitOfWork.Complete();

                // 3.TempData

                if (count > 0)
                    TempData["Confirm"] = "Department Created Successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // /Department/Details/1
        
        //[HttpGet]
        public async Task<IActionResult> Details(int? id , string viewName = "Details") // id mmkn yege w mmkn myge4
        {
            if (id == null)
                return BadRequest(); // 400
            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department == null)
                return NotFound(); // 404

            return View(viewName , department);
            // viewName => kda lazm yegele request b Edit 3l4an yrg3 el View bta3 el Edit lw mga4 edit hyrg3 el Default Details
            
        }

        // /Department/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            return await Details(id, "Edit");

            ///if (id == null)                                          |
            ///    return BadRequest();                                 |
            ///var department = _departmentRepository.Get(id.Value);    |
            ///if (department == null)                                  |==> We Can Replace All this with Function Details [Doing Same]
            ///    return NotFound();                                   |
            ///return View(department);                                 |
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lly 3awz y3ml edit hy3ml through el website bssssssssssssssssssssssssssssss
        public async Task<IActionResult> Edit([FromRoute]int id ,Department department)
        {
            if (id != department.Id)
                return BadRequest(); // to prevent anyone to make changes in inspect page in browser

            if (ModelState.IsValid)
            {
                try // To Avoid Error
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception 
                    // 2. Show Freindly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id , Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Freindly Msg

                ModelState.AddModelError(string.Empty , ex.Message);
                return View(department);
            }
        }

    }
}
