using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo.PL.Controllers
{
    [Authorize] // Must be Login to show this Controller
    public class EmployeeController : Controller
    {
        //private IEmployeeRepository _employeeRepository;                              |
        // Attribute from EmployeeRepository to Get All Methods DependnacnyInjection    |--> m4 m7taghom 3l4an b2a 3ndy unitofwork
        //private readonly IDepartmentRepository _departmentRepository;                 |
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        #region IUnitOfWork
		// 1. lma 3mlt el UnitOfWork f kda h4el mn el CTOR [IEmployeeRepository , IDepartmentRepository]
        // 2. h3ml calling l IUnitOfWork hyb2a feeh el data dee w hwa lly hy3ml object mn Emp w Dept
	    #endregion        
        public EmployeeController(IUnitOfWork unitOfWork , IMapper mapper) // ASK CLR to Create Object from Class EmployeeRepo
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // /Employee/Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            #region ViewData | ViewBag | TempData
            /* Binding : Sending Extra Info From Action to Action View
     *      - One Way Binding => Send Information from Action to View
     *      - Send Data from Controller to View
     * --------------------------------------------------
     * 1. View Data : ASP.NET Framework 3.5
     *      - Dictionary Object
     *      - KeyValue Pair
     *          - ViewData["anything"] = "Message" as ValueType
     *                         Key         Value
     *      - Required Casting => a7dd noo3 el data [string | int | interface | etc]
     *      - Enforce Type Safety == Strongly Typed
     *      - ViewData Faster Than ViewBag
     * --------------------------------------------------
     * 2. ViewBag : ASP.NET Framework 4.0
     *      - Dynamic Property
     *      - Not Required Casting
     *      - Cannot Enforce Type Safety == Weekly Typed
     *      - ViewBag Slower Than ViewData
     * --------------------------------------------------
     * 3. TempData : 
     *      - Sending Message From Request to Request
     * --------------------------------------------------
     */

            #endregion
            ///ViewData["Message"] = "Using View Data";
            ///ViewBag.Msg = "Using View Bag";

            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork.EmployeeRepository.GetAll(); // Get All Employees and save it in "Employees"
            else
                employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);

            // Mapping
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmp);

            ///
            ///return View();
            ///return View(Employees'Model'); Overload [Used]
            ///return View("ViewName");  Overload
            ///return View("ViewName" , Employees 'Model'); Overload
        }

        [HttpGet] // Default
        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            // l action da hytnfz lma a3ml submit ll form bta3t el create
            if (ModelState.IsValid) // Server Side Validation
            {
                // Manual Mapping
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Address = employeeVM.Address,
                ///    Email = employeeVM.Email,
                ///    Salary = employeeVM.Salary,
                ///    Age = employeeVM.Age,
                ///    DepartmentId = employeeVM.DepartmentId,
                ///    IsActive = employeeVM.IsActive,
                ///    HireDate = employeeVM.HireDate,
                ///    PhoneNumber = employeeVM.PhoneNumber
                ///};
                ///

                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images"); // Image


                var mappedEmp = _mapper.Map<EmployeeViewModel , Employee>(employeeVM); 

                await _unitOfWork.EmployeeRepository.Add(mappedEmp);
                int count = await _unitOfWork.Complete();
                if (count > 0)
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }


        #region Details
        // /Employee/Details/1
        //[HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details") // id mmkn yege w mmkn myge4
        {
            if (id == null)
                return BadRequest(); // 400
            var Employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (Employee == null)
                return NotFound(); // 404

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);

            return View(viewName, mappedEmp);
            // viewName => kda lazm yegele request b Edit 3l4an yrg3 el View bta3 el Edit lw mga4 edit hyrg3 el Default Details

        }

        #endregion

        #region Edit
        // /Employee/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            return await Details(id, "Edit");

            ///if (id == null)                                  |
            ///    return BadRequest();                         |
            ///var Employee = _EmployeeRepository.Get(id.Value);|
            ///if (Employee == null)                            |==> We Can Replace All this with Function Details [Doing Same]
            ///    return NotFound();                           |
            ///return View(Employee);                           |
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lly 3awz y3ml edit hy3ml through el website bssssssssssssssssssssssssssssss
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest(); // to prevent anyone to make changes in inspect page in browser

            if (ModelState.IsValid)
            {
                try // To Avoid Error
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
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
            return View(employeeVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                int count = await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Freindly Msg

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        } 
        #endregion
    }
}
