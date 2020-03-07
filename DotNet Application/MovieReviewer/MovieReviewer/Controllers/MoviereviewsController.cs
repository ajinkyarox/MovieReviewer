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
    public class MoviereviewsController : Controller
    {
        private moviereviewerEntities1 db = new moviereviewerEntities1();

        public string GetAll([FromUri] int id)
        {

            return JsonConvert.SerializeObject(db.Moviereviews.Where(x=>x.Id==id));
        }

        [System.Web.Mvc.HttpDelete]
        public string DeleteReview([FromUri] int Rid)
        {


            dynamic jsonData = "";
           
            try
            {
                bool flag = db.Moviereviews.Any(i => i.Rid == Rid);
                if (!flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie does not exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }
                Moviereview movy = db.Moviereviews.FirstOrDefault(i => i.Rid == Rid);

                db.Moviereviews.Remove(movy);
                db.SaveChanges();
                jsonData = @"{'status':'success','responseMessage':movy}";
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

        [System.Web.Mvc.HttpPut]
        public string PutReview([FromBody] Moviereview movy)
        {
            dynamic jsonData = "";

            try
            {

                /*movy movy = new movy();
                 dynamic jsonData = JObject.Parse(requestbody.ToString());
                 movy.Name = jsonData.Name;
                 movy.Genre = jsonData.Genre;
               */
                bool flag = db.Moviereviews.Any(i => i.Rid == movy.Rid);
                Moviereview movie = db.Moviereviews.FirstOrDefault(i => i.Review == movy.Review);

                bool movieflag = true;
                if (movie != null)
                {
                    if (movie.Rid == movy.Rid)
                    {
                        movieflag = true;
                    }
                    else
                    {
                        movieflag = false;
                    }
                }
                if (!flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie does not exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }
                else if (!movieflag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie exists with different Id.'  
}";
                    return JsonConvert.SerializeObject(jsonData);
                }
                Moviereview temp = db.Moviereviews.FirstOrDefault(x => x.Rid == movy.Rid);
                temp.Review = movy.Review;
                
                db.SaveChanges();
                jsonData = @"{'status':'success','responseMessage':movy}";
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



        [System.Web.Mvc.HttpPost]
        public string PostReview([FromBody] Moviereview movy)
        {
            dynamic jsonData = "";
           
            try
            {
                bool flag = db.Moviereviews.Any(i => i.Review == movy.Review);
                if (flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie Already exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }

                db.Moviereviews.Add(movy);
                db.SaveChanges();
                jsonData = @"{'status':'success','responseMessage':movy}";
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


        // GET: Moviereviews
        public ActionResult Index()
        {
            return View(db.Moviereviews.ToList());
        }

        // GET: Moviereviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moviereview moviereview = db.Moviereviews.Find(id);
            if (moviereview == null)
            {
                return HttpNotFound();
            }
            return View(moviereview);
        }

        // GET: Moviereviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moviereviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rid,Id,Review")] Moviereview moviereview)
        {
            if (ModelState.IsValid)
            {
                db.Moviereviews.Add(moviereview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moviereview);
        }

        // GET: Moviereviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moviereview moviereview = db.Moviereviews.Find(id);
            if (moviereview == null)
            {
                return HttpNotFound();
            }
            return View(moviereview);
        }

        // POST: Moviereviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rid,Id,Review")] Moviereview moviereview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moviereview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviereview);
        }

        // GET: Moviereviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moviereview moviereview = db.Moviereviews.Find(id);
            if (moviereview == null)
            {
                return HttpNotFound();
            }
            return View(moviereview);
        }

        // POST: Moviereviews/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Moviereview moviereview = db.Moviereviews.Find(id);
            db.Moviereviews.Remove(moviereview);
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
