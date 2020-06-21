using Account.Models;
using System.Linq;
using System.Web.Mvc;
namespace Account.Controllers
{
    public class TopicsController : Controller
    {
        AccountEntities db = new AccountEntities();
        // GET: Topics
        public ActionResult Index()
        {

            return View(db.Topic.ToList());
        }
        //添加
        public ActionResult Create()
        {
            ViewBag.TopicTypes = new SelectList(new[] {
                new SelectListItem{
                    Value="1",Text="单选"

                },new SelectListItem{
                    Value="2",Text="判断"

                },new SelectListItem{
                    Value="3",Text="问答"

                }
            }, "Value", "Text");
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,TopicExplain,TopicScore,TopicType,TopicA,TopicB,TopicC,TopicD,TopicSort,TopicAnswer")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topic.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(topic);
        }
        //删除
        public ActionResult Delete(int id)
        {
            Topic topic = db.Topic.Find(id);
            db.Topic.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //修改
        public ActionResult Edit(int id)
        {
            ViewBag.top = db.Topic.Find(id);
            return View();

        }
        [HttpPost]
        public ActionResult Edit(Topic top)
        {
            db.Entry(top).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //添加考题
        public ActionResult Creates(int id) 
        {
            ViewBag.pa= db.Paper.Find(id);
            return View();

        }
            
    }
}