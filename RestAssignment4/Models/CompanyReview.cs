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
        public int Stars {get; set;   }
        public string TimeStamp { get; set; }

    // Check to see if review is valid
    public bool CheckStars()
        {
            if (this.Stars > 5 || this.Stars < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    
    // Return false if negative or entered timestamp can't be converted to double
    public bool CheckTimeStamp()
        {
            try
            {
                uint time = uint.Parse(this.TimeStamp);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

    // Return false if company name is empty
    public bool CheckCompanyName()
        {
            if (this.CompanyName == null || this.CompanyName.Length == 0)
                return false;
            return true;
        }

    // Return false if username is empty
    public bool CheckUsername()
        {
             if (this.UserName == null || this.UserName.Length == 0)
                return false;
            return true;
        
        }
    }
}