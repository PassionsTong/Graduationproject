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
          
            ViewBag.s = db.Selects.Where(p => p.PaperID == id).OrderBy(p => p.Topic.TopicSort).ToList();
                int sum = 0;
                foreach (Selects item in ViewBag.s)
                {
                    sum += item.Topic.TopicScore;

                }
                TempData["Score"] = sum;
            
            
            return View(db.Selects.ToList());

        }
        //添加
        public ActionResult PaperIndex(int ids)
        {
            Session["Paper"] = ids;
            return View(db.Topic.ToList());

        }
        public ActionResult Paper(int id)
        {
            //Paper paper = Session["Paper"] as Paper;

            int kk =int.Parse(Session["Paper"].ToString());
            Selects selects = new Selects()
            {
                PaperID = kk,
                TopicID = id
            };
            db.Selects.Add(selects);
            db.SaveChanges();
            return RedirectToAction("Index", "Paper");
        }
        //删除试卷表考题
        public ActionResult Deletes(int id)
        {
            Selects selects = db.Selects.Find(id);
            db.Selects.Remove(selects);
            db.SaveChanges();
            return RedirectToAction("Index", "Paper");

        }

    }
}