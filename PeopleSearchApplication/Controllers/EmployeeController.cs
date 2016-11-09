using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PeopleSearchApplication.DAL;
using PeopleSearchApplication.Models;
using PagedList;
using System.IO;
using PeopleSearchApplication.Interfaces;

namespace PeopleSearchApplication.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository _repository;

        public EmployeeController() : this(new EmployeeRepository())
        { }

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = _repository.GetAllEmployee();

            employees = employees.OrderBy(s => s.FirstName);
            int? page = null;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        //GET: Employee by name
        public ActionResult SearchEmployee(string sortOrder, string searchName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchName) && page.Equals(null))
                page = 1;

            ViewBag.CurrentFilter = searchName;

            var employees = _repository.GetAllEmployee();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.Trim();
                if(searchName.Contains(" "))
                {
                    var searchText1 = searchName.Split(' ')[0].ToLower();
                    var searchText2 = searchName.Split(' ')[1].ToLower();

                    employees = employees.Where(s => (s.LastName.ToLower().Equals(searchText1) && s.FirstName.ToLower().Equals(searchText2))
                    || (s.LastName.ToLower().Equals(searchText2) && s.FirstName.ToLower().Equals(searchText1)));

                }
                else
                    employees = employees.Where(s => s.LastName.ToLower().Contains(searchName.ToLower())
                                       || s.FirstName.ToLower().Contains(searchName.ToLower()));
            }

            switch (sortOrder)
            {
                case "lastname_asc":
                    employees = employees.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "firstname_asc":
                    employees = employees.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    employees = employees.OrderByDescending(s => s.FirstName);
                    break;
                case "age_asc":
                    employees = employees.OrderBy(s => s.Age);
                    break;
                case "age_desc":
                    employees = employees.OrderByDescending(s => s.Age);
                    break;
                default:
                    employees = employees.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(employees.ToPagedList(pageNumber, pageSize));
        }

        // Retrieve image from DB and show on view
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = _repository.GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _repository.GetEmployeeByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,Address,Age,Interests,Image")] Employee employee, HttpPostedFileBase FileName)
        {
            if (ModelState.IsValid)
            {
                if(FileName != null)
                    employee.Image = ConvertToBytes(FileName);

                _repository.CreateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

       // Convert image to byte to store in DB
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _repository.GetEmployeeByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,Address,Age,Interests,Image")] Employee employee, HttpPostedFileBase FileName)
        {
            if (ModelState.IsValid)
            {
                
                if(FileName != null)
                    employee.Image = ConvertToBytes(FileName);

                _repository.EditEmployee(employee);
                
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _repository.GetEmployeeByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

    }
}
