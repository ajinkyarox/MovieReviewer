﻿using System;
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
using MovieReviewer.Others;
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
        public String GetScore([FromUri] int id)
        {

            List<reviewscore> mv = db.reviewscores.Where(x => x.movieid == id).ToList();
            List<int> scores =new List<int>();
            for(int i = 0; i < mv.Count; i++)
            {
                scores.Insert(i,(int)mv[i].score);
            }
            Kmeans km = new Kmeans();
            String score = km.kmeansscore(scores);
            return score;
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
                db.reviewscores.RemoveRange(db.reviewscores.Where(c => c.Rid == Rid));
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



        [System.Web.Mvc.HttpGet]
        public string GetReviewScore([FromUri] int id)
        {
           List<reviewscore> scores = db.reviewscores.ToList();

            return null;
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
                int movieid = movy.Id;
                Constants ct = new Constants();
                int pScore = 0;
                int nScore = 0;
                List<string> charsInReview =new List<string>(movy.Review.Split(' '));
                foreach(string st in ct.positiveWords)
                {
                    
                        if (movy.Review.ToLower().Contains(st))
                        {
                            pScore += 1;
                        }
                    
                }

                foreach (string st in ct.negativeWords)
                {
                    
                        if (movy.Review.ToLower().Contains(st))
                        {
                            nScore += 1;
                        }
                    
                }
              
                foreach (string stemp in ct.pnList)
                {
                    if (movy.Review.ToLower().Contains(stemp))
                    {
                        nScore -= 1;
                    }
                }
                foreach (string stemp in ct.nnList)
                {
                    if (movy.Review.ToLower().Contains(stemp))
                    {
                        pScore -= 1;
                    }
                }
                db.Moviereviews.Add(movy);
                reviewscore rs = new reviewscore();
                
                db.SaveChanges();
                Moviereview mr = db.Moviereviews.Where(i => i.Review == movy.Review).FirstOrDefault();
                rs.Rid = mr.Rid;
                rs.movieid = movieid;
                int totalScore = pScore-nScore;
                rs.score = totalScore;
                db.reviewscores.Add(rs);
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
