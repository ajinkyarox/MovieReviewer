using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MovieReviewer;

namespace MovieReviewer.Controllers
{
    public class logincredentialsController : Controller
    {
        private moviereviewerEntities1 db = new moviereviewerEntities1();

        // GET: logincredentials
        public ActionResult Index()
        {
            return View(db.logincredentials.ToList());
        }


        [System.Web.Mvc.HttpPost]
        public string login([FromBody] logincredential lcred)
        {
            dynamic jsonData = "";

            try
            {

      
                bool flag = db.logincredentials.Any(i => i.username == lcred.username && i.password==lcred.password);
                if (!flag)
                {
          
                    return "Invalid Username/Password";


                }

                db.logincredentials.Add(lcred);
                db.SaveChanges();
               
                return "Success";
            }
            catch (Exception e)
            {
                var msg = e.Message;
               
                return "Failure:"+msg;
            }
        }

        [System.Web.Mvc.HttpPost]
        public string PostLogin([FromBody] logincredential lcred)
        {
            dynamic jsonData = "";
            
            try
            {

                /*movy movy = new movy();
                 dynamic jsonData = JObject.Parse(requestbody.ToString());
                 movy.Name = jsonData.Name;
                 movy.Genre = jsonData.Genre;
               */
                bool flag = db.logincredentials.Any(i => i.username == lcred.username);
                if (flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Username Already exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }

                db.logincredentials.Add(lcred);
                db.SaveChanges();
                jsonData = @"{'status':'success','responseMessage':lcred}";
                return JsonConvert.SerializeObject(jsonData);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                /*dynamic*/
                jsonData = @"{  
'status':'failure',  
'responseMessage':msg 
}";
                return JsonConvert.SerializeObject(jsonData);
            }

        }

        // GET: logincredentials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logincredential logincredential = db.logincredentials.Find(id);
            if (logincredential == null)
            {
                return HttpNotFound();
            }
            return View(logincredential);
        }

        // GET: logincredentials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: logincredentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lid,username,password")] logincredential logincredential)
        {
            if (ModelState.IsValid)
            {
                db.logincredentials.Add(logincredential);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logincredential);
        }

        // GET: logincredentials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logincredential logincredential = db.logincredentials.Find(id);
            if (logincredential == null)
            {
                return HttpNotFound();
            }
            return View(logincredential);
        }

        // POST: logincredentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lid,username,password")] logincredential logincredential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logincredential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logincredential);
        }

        // GET: logincredentials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logincredential logincredential = db.logincredentials.Find(id);
            if (logincredential == null)
            {
                return HttpNotFound();
            }
            return View(logincredential);
        }

        // POST: logincredentials/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            logincredential logincredential = db.logincredentials.Find(id);
            db.logincredentials.Remove(logincredential);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
