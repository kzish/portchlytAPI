using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Newtonsoft.Json;
using portchlytAPI.Controllers;
using portchlytAPI.Models;
using Services;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace portchlytAPI
{
    public class Startup
    {
        string clientId;
        string[] subscriptions = { "porchlyt_mqtt_server" };//the items i am listening for
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


            //set up the mqtt
            globals.mqtt = new MqttClient(globals.MQTT_BROKER_ADDRESS);
            // register to message received 
            globals.mqtt.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            //create client id for mqtt
            clientId = "123Server"+Guid.NewGuid().ToString();
            //connect to the mqtt client
            globals.mqtt.Connect(clientId);
            //alert server is connected
            globals.mqtt.Publish(subscriptions[0],Encoding.UTF8.GetBytes("server connected "+ DateTime.Now));
            //subscribe to the topic "/server" with QoS 1 
            globals.mqtt.Subscribe(subscriptions, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            globals.mqtt.ConnectionClosed += Mqtt_ConnectionClosed;
            

            //init the mongodb index for the geo statial data
           // var keys = Builders<mArtisan>.IndexKeys.Ascending("Location");
            var keys = Builders<mArtisan>.IndexKeys.Geo2DSphere("location.coordinates");
            var indexOptions = new CreateIndexOptions {  };
            var model = new CreateIndexModel<mArtisan>(keys, indexOptions);
            globals.getDB().GetCollection<mArtisan>("mArtisan").Indexes.CreateOne(model);

            //run general updates
            var service_artisan_on_map_update = new artisan_on_map_update();

           

        }

        //handle reconnect
        private void Mqtt_ConnectionClosed(object sender, EventArgs e)
        {
            globals.mqtt.Connect(clientId);
            globals.mqtt.Subscribe(subscriptions, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

       
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //get the message from the sender
            //string msg = e.Message;Encoding.UTF8.GetBytes(e.Message);
            //handle message received, direct it to the necessary handler
            var msg = Encoding.ASCII.GetString(e.Message);//get the string representation of the info
            try
            {
                dynamic json = JsonConvert.DeserializeObject(msg);
                var msg_type = json.msg_type;//get the message type and send to the correct router, run the methods async
                if (msg_type == "artisan_location_update")Task.Run(() => { apiArtisanController.artisanLocationUpdate(msg); });
                    
                
                
                
                
                
                //globals.mqtt.Publish("test", e.Message, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }catch(Exception ex)
            {
                globals.mqtt.Publish("test", ASCIIEncoding.ASCII.GetBytes(ex.Message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
            });
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); // <= Add this for pagination
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //run general updates
            //services.AddSingleton<ITimer, artisan_on_map_update>();//add the timer service scheduler as a singleton
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("PorchLyt API v1.0.0");
            });
        }
    }
}

