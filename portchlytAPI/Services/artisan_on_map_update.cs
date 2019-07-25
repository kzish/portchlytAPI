using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using portchlytAPI.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Services
{


    //update the artisan icon on the maps of the client mobile apps
    public class artisan_on_map_update : ITimer
    {

        Timer timer = null;
        bool busy;

        //constructor
        public artisan_on_map_update()
        {
            var autoEvent = new AutoResetEvent(false);
            autoEvent.Set();
            timer = new Timer(RunTask, autoEvent, 3000, 3000);//3 sec timer
            
        }


        //task will send the message
        public void RunTask(object state)
        {
            if (busy)
            {
                return;//if im busy wait by not running this code
            }


            try
            {
                busy = true;//im busy now

                //fetch all artisans
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisans = artisan_col.Find(i => i._id != null).ToList();

                foreach (var artisan in artisans)
                {

                    dynamic json = new JObject();
                    json.type = "artisan_on_map_update";
                    json.artisan_app_id = artisan.app_id;
                    json.artisan_lat = artisan.location.coordinates[0];
                    json.artisan_lng = artisan.location.coordinates[1];
                    json.skill = String.Join(" ", artisan.skills);

                    Task.Run(()=>{
                        //this type of update goes to everyone
                        try
                        {
                            globals.mqtt.Publish("general_updates", Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json)), 0, false);
                        }
                        catch (Exception ex) {

                        }
                    });
                    

                }//foreach

                busy = false;//now im free
            }
            catch (Exception ex)
            {
                ///
            }
            finally
            {
                busy = false;
            }


        }//runtask







    }//sTimer

}//namespace
