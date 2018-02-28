using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAssignment4.Models
{
    public class CompanyReview
    {
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public int Stars { get; set; }
        public string TimeStamp { get; set; }
    }
}