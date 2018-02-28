using LinkShortener.Models.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAssignment4.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestAssignment4.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: Reviews/GetCompanyReviews/{"companyName":"SEARCH NAME HERE"}
        public JsonResult GetCompanyReviews(string id)
        {
            List<CompanyReview> result = null;
            try
            {
                LinkDatabase db = LinkDatabase.getInstance();
                JObject JsonObj = JObject.Parse(id);
                string companyName = JsonObj.GetValue("companyName").ToObject<string>();
                
                result = db.readReview(companyName);
            }
            catch(Exception e)
            {
                return Json(new { response = "operation failed" }, JsonRequestBehavior.AllowGet);
            }
            if (result == null || result.Count == 0)
            {
                return Json(new { response = "company does not exist" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /Reviews/SaveCompanyReview 
        [HttpPost]
        public JsonResult SaveCompanyReview(ReviewHolder data)
        {
            if(data == null)
            {
                return Json(new { response = "Error: Invalid body JSON" });
            }
            else if (!data.Review.CheckStars())
            {
                return Json(new { response = "Error: Stars must be a value between 0 and 5" });
            }
            else if (!data.Review.CheckTimeStamp())
            {
                return Json(new { response = "Error: Invalid UNIX Timestamp" });
            }
            else if (!data.Review.CheckCompanyName())
            {
                return Json(new { response = "Error: Empty Company Name Supplied" });
            }
            else if (!data.Review.CheckUsername())
            {
                return Json(new { response = "Error: Empty Username Supplied" });
            }
            else
            {
                try
                {
                    LinkDatabase db = LinkDatabase.getInstance();
                    db.SaveReview(data.Review);

                    return Json(new { response = "success" });

                } catch(Exception e)
                {
                    return Json(new { response = "exception thrown", exception = e.Message });
                }
            }
        }
        
    }
    public class ReviewHolder
    {
        public CompanyReview Review { get; set;}
    }
   
}

