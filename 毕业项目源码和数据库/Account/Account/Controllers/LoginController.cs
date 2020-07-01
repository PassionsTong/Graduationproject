using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class LoginController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult StuLogin(Student student)
        {

            var name = db.Student.SingleOrDefault(p => p.StuLoginName == student.StuLoginName && p.StuLoginPwd == student.StuLoginPwd);
            Session["Sjh"] = name;
            if (name == null)
            {
                return Content("<script>alert('学生身份验证错误');history.go(-1)</script>");
            }
            else
            {
                Session["LoginID"] = name.StuID;
                return RedirectToAction("Index", "Home");

            }
            

        }
        public ActionResult Exit()
        {
            Session["Tjh"] = null;
            Session["Sjh"] = null;
            return RedirectToAction("Index");
        }
      

        [HttpPost]
        public ActionResult TeaLogin(  Teacher teacher)
        {
            var name1 = db.Teacher.SingleOrDefault(a => a.TeaLoginName == teacher.TeaLoginName && a.TeaLoginPwd == teacher.TeaLoginPwd);
            Session["Tjh"] = name1;
            if (name1 == null)
            {
                return Content("<script>alert('老师身份验证错误');history.go(-1)</script>");
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }


        }
        public ActionResult xiang() 
        {
            return View();
        }
        //修改
        public ActionResult Edit(int id)
        {
            ViewBag.stu = db.Student.Find(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}