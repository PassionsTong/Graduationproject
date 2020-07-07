using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class StudentController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Student

            //查询
        public ActionResult Index()
        {
            List<Student> stu = db.Student.ToList();
            return View(stu);
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
        //添加
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("<script>alert('请输入正确！');history.go(-1)</script>");
                throw;
            }

           
        }
        //学生注册
        public ActionResult Createstu()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Createstu(Student student)
        {
            try
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                return Content("<script>alert('请输入正确！');history.go(-1)</script>");
                throw;
            }
           
        }

        //详情
        public ActionResult Details(int id)
        {
            ViewBag.stu = db.Student.Find(id);
            return View();
        }
        //删除
        public ActionResult Delete(int id)
        {
            try
            {
                Student menu = db.Student.Find(id);
                db.Student.Remove(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("<script>alert('不可删除！');history.go(-1)</script>");
                throw;
            }
            
        }
        //在线考试
        public ActionResult Exam() 
        {
            List<Paper> papers = db.Paper.ToList();
            return View(papers);
        }
        

    }
}