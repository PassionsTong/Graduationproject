using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class PaperController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Paper
        //查询
        public ActionResult Index()
        {
            List<Paper> papers = db.Paper.ToList();
            return View(papers);
        }
        //添加
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Paper paper)
        {

            db.Paper.Add(paper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //编辑
        public ActionResult Edit(int id)
        {
            ViewBag.pa = db.Paper.Find(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Paper paper)
        {
            db.Entry(paper).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ////详情
        //public ActionResult Details(int id)
        //{
        //    ViewBag.pa = db.Paper.Find(id);
        //    List<Topic> topics = db.Topic.ToList();
        //    return View(topics);
        //}
        //删除
        public ActionResult Delete(int id)
        {
            var selects = db.Selects.Where(p=>p.PaperID==id);
            if (selects == null)
            {
                Paper menu = db.Paper.Find(id);
                db.Paper.Remove(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                
                return Content("<script>alert('此试卷有题目不可删除');history.go(-1)</script>");
            }
            
        }
    }
}