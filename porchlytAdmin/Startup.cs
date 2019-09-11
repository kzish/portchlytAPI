using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace porchlytAdmin
{
    public class Startup
    {
        string clientId;
        string[] subscriptions = { "porchlyt_mqtt_server" };//the items i am listening for
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration = configuration;


            //set up the mqtt
            globals.mqtt = new MqttClient(globals.MQTT_BROKER_ADDRESS);
            // register to message received 
            globals.mqtt.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            //create client id for mqtt
            clientId = "123Server" + Guid.NewGuid().ToString();
            //connect to the mqtt client
            globals.mqtt.Connect(clientId);
            //subscribe to the topic "/server" with QoS 1 
            globals.mqtt.Subscribe(subscriptions, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            globals.mqtt.ConnectionClosed += Mqtt_ConnectionClosed;

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
                //globals.mqtt.Publish("test", e.Message, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
            catch (Exception ex)
            {
                globals.mqtt.Publish("test", ASCIIEncoding.ASCII.GetBytes(ex.Message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }

        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("PorchLyt admin v1.0.0");
            });
        }
    }
}
