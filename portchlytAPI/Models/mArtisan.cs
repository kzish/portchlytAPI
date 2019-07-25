using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace portchlytAPI.Models
{
    /// <summary>
    /// this is the artisan ie the worker on the system
    /// </summary>
    public class mArtisan
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public DateTime dateRegistered { get; set; } = DateTime.Now;//the date this artisan registered in case we will need to clear the database
        public string image { get; set; } = "image";
        public string name { get; set; } = "";
        public string surname { get; set; } = "";
        public string password { get; set; } = "";
        public string mobile { get; set; } = "";//this is also the primary key
        public string email { get; set; } = "";
        public double hourlyRate { get; set; } = 0.0;
        public string streetAddress { get; set; } = "";
        public EmploymentType employmentType { get; set; } = EmploymentType.partTime;
        public string country { get; set; } = "";
        public string stateOrPorvince { get; set; } = "";
        public string cityOrTown { get; set; } = "";
        public string otp { get; set; }
        public List<string> skills { get; set; } = new List<string>();
        public List<Referee> referees { get; set; } = new List<Referee>();
        public List<artisanRating> artisanRating { get; set; } = new List<artisanRating>();
        public int numJobs { get; set; }//the number of jobs that i have done
        public int myrating { get; set; }
        public bool registered { get; set; }
        public bool busy { get; set; } = false;//is this artisan currently busy or not, you do not get another work untill you finish your current work
        public bool on_duty { get; set; } = true;//am i working or am i on leave? on duty
        public bool synced { get; set; }
        public Location location { get; set; } = new Location(9.0,7.0);//this is the current location of the artisan and is constantly updated
        public String app_id;//the app id of this artisan
        public double earnings_since_last_disbursement;//this is what this person has earned since his/her last dispursment
        public int getRating()//get my rating
        {
            try
            {
                var fiveRatings = artisanRating.Where(x => x.numStars == 5).Count();
                var fourRatings = artisanRating.Where(x => x.numStars == 4).Count();
                var threeRatings = artisanRating.Where(x => x.numStars == 3).Count();
                var twoRatings = artisanRating.Where(x => x.numStars == 2).Count();
                var oneRatings = artisanRating.Where(x => x.numStars == 1).Count();

                var rating = (
                               5 * fiveRatings +
                               4 * fourRatings +
                               3 * threeRatings +
                               2 * twoRatings +
                               1 * oneRatings
                              ) /
                              (
                              fiveRatings + fourRatings + threeRatings + twoRatings + oneRatings
                              );
                myrating = (int)rating;
                return (int)rating;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        public void hashPassword()
        {
            this.password = globals.getmd5(password);
        }
    }

    public enum EmploymentType
    {
        fullTime,
        partTime
    }

    public class Referee
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public string refname { get; set; }
        public string refemail { get; set; }
        public string refmobile { get; set; }

        //contructor
        public Referee()
        {
            refname = "";
            refmobile = "";
            refemail = "";
        }

    }


    public class artisanRating
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public int numStars { get; set; }//the number of stars given;
    }

    public class Location {
        public Location(double lat,double lon)
        {
            coordinates[0] = lat;
            coordinates[1] = lon;
        }
        public string type { get; set; } = "Point";
        public double[] coordinates { get; set; } = new double[2];//lat long
    }

}
