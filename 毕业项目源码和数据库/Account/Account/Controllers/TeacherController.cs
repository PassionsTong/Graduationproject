using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class TeacherController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Teacher
        //查询
        public ActionResult Index()
        {
            List<Teacher> teachers = db.Teacher.ToList();
            return View(teachers);
        }
        //添加
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return Content("<script>alert('请输入正确！');history.go(-1)</script>");
                throw;
            }
            
        }
        //修改
        public ActionResult Edit(int id)
        {
            ViewBag.tea = db.Teacher.Find(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //详情
        public ActionResult Details(int id)
        {
            ViewBag.tea = db.Teacher.Find(id);
            return View();
        }
        //删除
        public ActionResult Delete(int id)
        {
            Answer answer = db.Answer.Find(id);
            if (answer==null)
            {
                Teacher menu = db.Teacher.Find(id);
                db.Teacher.Remove(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("<script>alert('老师不可删');history.go(-1)</script>");
            }
            
        }
        //老师注册
        public ActionResult Createtea() 
        {
            return View();
        
        }
        [HttpPost]
        public ActionResult Createtea(Teacher teacher) 
        {
            try
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                return Content("<script>alert('请输入正确！');history.go(-1)</script>");
                throw;
            }
           
        }
    }
}