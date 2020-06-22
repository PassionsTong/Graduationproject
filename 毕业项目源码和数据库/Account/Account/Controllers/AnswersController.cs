using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class AnswersController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Answers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BeginAnswer( int PaperID,int?AnswerID)
        {
            if (AnswerID == null)
            {
                Answer answer = new Answer();
                answer.PaperID = PaperID;
                answer.StuID = (int)Session["LoginID"];
                answer.TeaID = 1;
                answer.AnswerScore = 0;
                answer.AnswerTime = DateTime.Now;
                answer.AnswerState = 0;
                db.Answer.Add(answer);
                db.SaveChanges();
                return RedirectToAction("AnswerDatail", new { id = answer.AnswerID });

            }
            else 
            {
                return RedirectToAction("AnswerDatail", new { id = AnswerID });
            
            }
           
        }
        public ActionResult AnswerDatail(int id) 
        {
            Answer answer = db.Answer.Include("Paper").Include("Student").First(a => a.AnswerID == id);
            if (answer==null)
            {
                return HttpNotFound();
            }
            ViewBag.answer = answer;
            return View(answer);
        
        }
        public ActionResult _Topic(int aid, int? sort) 
        {
            int cursort = sort == null ? 1 : (int)sort;
            Answer answer = db.Answer.Include("Paper").Include("Student").First(a => a.AnswerID == aid);
            if (answer==null) 
            {
                return HttpNotFound();
            }
            ViewBag.answer = answer;
            Selects topic = answer.Paper.Selects.ToList()[cursort - 1];
            ViewBag.topic = topic;
            ViewBag.sort = cursort;
            ViewBag.count = answer.Paper.Selects.ToList().Count;
            if (db.Detail.Where(d=>d.AnswerID==aid&&d.TopicID==topic.TopicID).Count()==0)
            {
                Detail detail = new Detail();
                detail.AnswerID = answer.AnswerID;
                detail.TopicID = topic.TopicID;
                detail.DetailAnswer = "";
                db.Detail.Add(detail);
                db.SaveChanges();
            }
            Detail curdetail = db.Detail.Include("Topic").Include("Answer").First(d => d.AnswerID == aid && d.TopicID == topic.TopicID);
            return PartialView(curdetail);
        }
        public ActionResult _AllDetailStu(int aid) 
        {
            ViewBag.Detail=db.Detail.Include("Topic").Include("Answer").Where(d => d.AnswerID == aid).ToList();
            return PartialView();

        }
        public ActionResult SubmitAnswer(Detail detail, int sort, int count) 
        {
            db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            if (sort < count)
            {
                return RedirectToAction("_Topic", new { aid = detail.AnswerID, sort = sort + 1 });

            }
            else 
            {
                return RedirectToAction("_AllDetailStu", new { aid = detail.AnswerID });
            
            }
            
        }
        public ActionResult Hand(int id) 
        {
            Answer answer = db.Answer.Find(id);
            answer.AnswerState = 1;
            answer.AnswerTime = DateTime.Now;
            db.Entry(answer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyAnswer");
        
        }
    }
}