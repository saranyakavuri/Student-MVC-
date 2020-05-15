using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Data.OleDb;
using WebApplication2.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        COReplacementOSBEntities obj = new COReplacementOSBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult smartlogin()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult smartlogin(FormCollection f)

        {
            string username = f["User"].ToString();
            string password = f["pwd"].ToString();
            if (FormsAuthentication.Authenticate(username, password))
            {
                Session["user"] = username;
                return RedirectToAction("AddStudent");
            }
            else
                return View();
        }




        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student_Test t)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter op = new ObjectParameter("id", 0);
                int r = obj.sp_student_test(t.name, t.address, t.@class, op);
                if (r > 0)
                {
                    Response.Write("<script>alert('student added succesfully')</script>");
                    
                      return RedirectToAction("viewstudent");
                    
                }
                else
                {
                    Response.Write("<script>alert('please enter valid details')</script>");
                    
                       // return RedirectToAction("AddStudent");
                    
                }
                    
               
            }
           
            return View();
        }
        public ActionResult viewstudent()
        {
            List<Student_Test> list1 = obj.Student_Test.ToList();
            if (list1 != null)
            {
                return View(list1);
            }
            else
            {
                ViewBag.Message = "list is empty";
                return View("Error");
            }



        }
        public ActionResult Editstudent(int id)
        {
            Student_Test objj = obj.Student_Test.Where(x => x.student_id == id).FirstOrDefault();
            if (objj != null)
                return View(objj);
            else
                return View("Error");
         
        }
        [HttpPost]
        public ActionResult Editstudent(Student_Test t)
        {
            int row = obj.sp_updatestudent(t.student_id, t.name, t.address, t.@class);
            if(row>0)
            {
                return RedirectToAction("viewstudent");
            }
            else
            return View();
        }
    }
}