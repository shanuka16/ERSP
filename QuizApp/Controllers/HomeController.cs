using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList.Mvc;
using QuizAppML.Model;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        
        QuizAppEntities db = new QuizAppEntities();
        score sc = new score();//creating the object class

        int scoreExam = 0;

        [HttpGet]
            public ActionResult sregister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sregister(student svm, HttpPostedFileBase imgfile)
        {
            student s = new student();
            try
            {
                s.std_name = svm.std_name;
                s.std_password = svm.std_password;
                s.std_image = uploadimage(imgfile);
                s.std_contact = svm.std_contact;
                s.std_address = svm.std_address;
                s.std_lname = svm.std_lname;
                s.std_mail = svm.std_mail;
                db.students.Add(s);
                db.SaveChanges();
                return RedirectToAction("slogin");
            }
            catch (Exception)
            {
                ViewBag.msg = "Data could not be inserted...";
            }
            return View();
        }

        public string uploadimage(HttpPostedFileBase file)

        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("/Content/img/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("slogin");
        }
        public ActionResult tlogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult tlogin(tbl_admin a)
        {
            tbl_admin ad = db.tbl_admin.Where(x => x.ad_name == a.ad_name && x.ad_pass == a.ad_pass).SingleOrDefault();
            if (ad!=null)
            {
                Session["ad_id"] = ad.ad_id;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid User Name or Password";
            }
            return View();
        }
        public ActionResult slogin()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult slogin(student s )
        {
            student sv = db.students.Where(x => x.std_name == s.std_name && x.std_password == s.std_password).SingleOrDefault();
            sc.scoree[0] = sv.std_id.ToString();
            sc.scoree[1] = sv.std_name;

            if (sv==null)
            {
                ViewBag.msg = "Invalid User Name or Password";
            }
            else
            {
                
                Session["std_id"] = sv.std_id;
                return RedirectToAction("ExamDashboard");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ExamDashboard()
        {
             
            List<tbl_category> catLi = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = catLi;

            return View();
        }

        [HttpPost]
        public ActionResult ExamDashboard(int room)
        {
            List <tbl_category> list = db.tbl_category.ToList();
            foreach (var item in list)
            {
                if (item.cat_id==room)
                {
                    List<tbl_questions> li = db.tbl_questions.Where(x => x.q_fk_catid == item.cat_id).ToList();
                    Queue<tbl_questions> queue = new Queue<tbl_questions>();
                    foreach (tbl_questions a in li)
                    {
                        queue.Enqueue(a);
                    }

                    TempData["examid"] = item.cat_fk_ad_id;

                    TempData["fnalExmStdId"] = Convert.ToInt32(Session["ad_id"]);
                    TempData["examName"] = item.cat_name;
                    TempData["questions"] = queue;
                
                    TempData.Keep();
                    return RedirectToAction("StartQuiz");
                }
                else
                {
                    ViewBag.error = "No Room found...";
                }
            }
            return View();
        }

        public ActionResult StartQuiz()
        {
            if (Session["std_id"]==null)
            {
                return RedirectToAction("slogin");
            }
            tbl_questions q = null;
            if (TempData["questions"]!=null)
            {
                Queue<tbl_questions> qlist = (Queue<tbl_questions>)TempData["questions"];
                if (qlist.Count>0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();

                    TempData["questions"] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("EndExam");
                }
            }
            else
            {
                return RedirectToAction("ExamDashboard");
            }

            TempData.Keep();
            return View(q);
        }

        [HttpPost]
        public ActionResult StartQuiz(tbl_questions q)
        {
            string correctans = null;
            if (q.QA != null)
            {

                correctans = "1";
            }
            else if (q.QB != null)
            {
                correctans = "2";
            }
            else if (q.QC != null)
            {
                correctans = "3";
            }
            else if (q.QD != null)
            {
                correctans = "4";
            }
            /*
                if (correctans.Equals(q.QCorrectAns))
                {
                    scoreExam = scoreExam + 1;
                    //Session["totScore"] = scoreExam.ToString();
                }

                TempData.Keep();
                sc.scoree[2] = scoreExam.ToString();
                return RedirectToAction("StartQuiz");
             */

            if (correctans.Equals(q.QCorrectAns.ToString()))
            {
                //scoreExam = 3 ;
                //scoreExam = scoreExam + 1;
                //Session["exScore"] = scoreExam;
                int x = Convert.ToInt32(TempData["ExScores"]) ;
                TempData["ExScores"] = x + 1;
                TempData.Keep();
                //Session["totScore"] = scoreExam;
            }
            //scoreExam = 12;
            //TempData["ExScoresd"] = scoreExam;
            //Session["exScore"] = scoreExam;
            TempData.Keep();
            //sc.scoree[2] = TempData["ExScoresd"].ToString();
            return RedirectToAction("StartQuiz");
        }


        public ActionResult ViewAllQuestions(int?id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("tlogin");
            }

            if (id==null)
            {
                return RedirectToAction("Dashboard");
            }

            return View(db.tbl_questions.Where(x => x.q_fk_catid == id).ToList());

        }


        public ActionResult EditQuestions(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("tlogin");
            }

            if (id == null)
            {
                return RedirectToAction("Dashboard");
            }

            return View(db.tbl_questions.Where(x => x.q_id == id).ToList());

        }



        public ActionResult EndExam(tbl_questions q)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EndExam(tbl_setExam svm)
        {
            tbl_setExam s = new tbl_setExam();
            try
            {
                //s.exam_fk_stu = svm.exam_fk_stu;
                int stn_id = Convert.ToInt32(Session["std_id"].ToString());

                //s.exam_fk_stu = Convert.ToInt32(Session["std_id"].ToString());
                s.exam_fk_stu = stn_id;
                s.std_score = svm.std_score;
                s.exam_name = svm.exam_name;
                db.tbl_setExam.Add(s);
                db.SaveChanges();
                return RedirectToAction("ExamDashboard");
            }
            catch (Exception)
            {
                ViewBag.msg = "Exam data could not be inserted...";
            }
            return RedirectToAction("ExamDashboard");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add_Category()
        {
            // Session["ad_id"] = 2; //  remove it soon
            int ad_id = Convert.ToInt32(Session["ad_id"].ToString());
            List<tbl_category> catLi = db.tbl_category.Where(x=>x.cat_fk_ad_id==ad_id).OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = catLi;

            return View();
        }

        [HttpPost]
        public ActionResult Add_Category(tbl_category cat)
        {
            List<tbl_category> catLi = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = catLi;
            tbl_category c = new tbl_category();
            Random r = new Random();

            c.cat_name = cat.cat_name;
            c.cat_fk_ad_id = Convert.ToInt32(Session["ad_id"].ToString());
            c.cat_description = cat.cat_description.ToString();
            c.cat_encrytped_string = crypt.Encrypt(cat.cat_name.Trim() + r.Next().ToString(), true);

            db.tbl_category.Add(c);
            db.SaveChanges();

            return RedirectToAction("Add_Category");
           
        }
        [HttpGet]
        public ActionResult Add_Questions()
        {
            int s_id = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_ad_id == s_id).ToList();
            ViewBag.list= new SelectList(li, "cat_id", "cat_name");

            return View();
        }

        [HttpPost]
        public ActionResult Add_Questions(tbl_questions q)
        {
            int s_id = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_ad_id == s_id).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");

            tbl_questions qa = new tbl_questions();
            qa.q_text = q.q_text;
            qa.QA = q.QA;

            qa.QB = q.QB;
            qa.QC = q.QC;
            qa.QD = q.QD;
            qa.QCorrectAns = q.QCorrectAns;
            qa.q_description = q.q_description;

            qa.q_fk_catid = q.q_fk_catid;
            db.tbl_questions.Add(qa);
            db.SaveChanges();
            TempData["ms"]= "Question successfully Added";
            TempData.Keep();

            return RedirectToAction("Add_Questions");
        }

        [HttpGet]
        public ActionResult viewScores()
        {
            int std_id = Convert.ToInt32(Session["std_id"].ToString());
            List<tbl_setExam> exmLi = db.tbl_setExam.Where(x => x.exam_fk_stu == std_id).OrderByDescending(x => x.examId).ToList();
            ViewData["list"] = exmLi;

            return View();
        }

        public ActionResult Index()
        {
            if (Session["ad_id"]!=null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        [HttpGet]
        public ActionResult viewStudents()
        {
            List<student> stList = db.students.OrderByDescending(x => x.std_id).ToList();
            ViewData["list"] = stList;

            return View();
        }

        public ActionResult PredictHome()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Predict()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Predict(ModelInput input)
        {
            var predictions = ConsumeModel.Predict(input);
            ViewBag.Result = predictions;
            return View();
        }
        [HttpGet]
        public ActionResult PredictRes()
        {
            return View();
        }
        public ActionResult PredictRes(ModelInput input)
        {
            var predictions = ConsumeModel.Predict(input);
            ViewBag.Result = predictions;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Article()
        {
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}