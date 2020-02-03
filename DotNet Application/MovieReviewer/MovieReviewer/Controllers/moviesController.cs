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
using MovieReviewer.Cors;


namespace MovieReviewer.Controllers
{
    
    public class moviesController : Controller
    {
        private moviereviewerEntities1 db = new moviereviewerEntities1();


        public string Get()
        {
            movy movy = db.movies.Find(1);
            return "Hello from "+movy.Name;
        }

        [System.Web.Mvc.HttpPost]
        public string Post([FromBody] Newtonsoft.Json.Linq.JObject body)
        {
            

            return body.ToString();
        }
        
        [System.Web.Mvc.HttpPost]
        public string PostMovie([FromBody] movy movy)
        {
            dynamic jsonData = "";
            Console.WriteLine(movy.Name);
            try
            {

                /*movy movy = new movy();
                 dynamic jsonData = JObject.Parse(requestbody.ToString());
                 movy.Name = jsonData.Name;
                 movy.Genre = jsonData.Genre;
               */
                bool flag = db.movies.Any(i => i.Name == movy.Name);
                if (flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie Already exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }

                db.movies.Add(movy);
                db.SaveChanges();
                 jsonData = @"{'status':'success','responseMessage':movy}";
                return JsonConvert.SerializeObject(jsonData);
            }
            catch(Exception e)
            {
                var msg = e.Message;   
                /*dynamic*/ jsonData = @"{  
'status':'failure',  
'responseMessage':msg 
}";
                return JsonConvert.SerializeObject(jsonData);
            }
            
        }

        [System.Web.Mvc.HttpDelete]
        public string DeleteMovie([FromUri] int id)
        {


            dynamic jsonData = "";
            Console.WriteLine(id);
            try
            {
                bool flag = db.movies.Any(i => i.Id == id);
                if (!flag)
                {
                    jsonData = @"{  
'status':'failure',  
'responseMessage':'Movie does not exists'  
}";
                    return JsonConvert.SerializeObject(jsonData);


                }
                movy movy = db.movies.FirstOrDefault(i => i.Id == id);

                db.movies.Remove(movy);
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
        public string PutMovie([FromBody] movy movy)
        {
            dynamic jsonData = "";
            Console.WriteLine(movy.Name);
            try
            {

                /*movy movy = new movy();
                 dynamic jsonData = JObject.Parse(requestbody.ToString());
                 movy.Name = jsonData.Name;
                 movy.Genre = jsonData.Genre;
               */
                bool flag = db.movies.Any(i => i.Id == movy.Id);
                movy movie= db.movies.FirstOrDefault(i => i.Name == movy.Name);
               
                bool movieflag = true;
                if (movie != null)
                {
                    if (movie.Id == movy.Id)
                    {
                        movieflag = true;
                    }
                    else
                    {
                        movieflag = false;
                    }
                }
                if (!flag )
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
                movy temp=db.movies.FirstOrDefault(x => x.Id == movy.Id);
                temp.Name = movy.Name;
                temp.Genre = movy.Genre;
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

        public string GetAll()
        {

     return JsonConvert.SerializeObject(db.movies.ToList());
        }
        // GET: movies
        public ActionResult Index()
        {
            return View(db.movies.ToList());
        }

        // GET: movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movy movy = db.movies.Find(id);
            if (movy == null)
            {
                return HttpNotFound();
            }
            return View(movy);
        }

        // GET: movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Genre")] movy movy)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(movy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movy);
        }

        // GET: movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movy movy = db.movies.Find(id);
            if (movy == null)
            {
                return HttpNotFound();
            }
            return View(movy);
        }

        // POST: movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Genre")] movy movy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movy);
        }

        // GET: movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movy movy = db.movies.Find(id);
            if (movy == null)
            {
                return HttpNotFound();
            }
            return View(movy);
        }

        // POST: movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movy movy = db.movies.Find(id);
            db.movies.Remove(movy);
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
