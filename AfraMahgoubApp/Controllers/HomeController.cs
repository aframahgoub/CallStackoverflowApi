using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AfraMahgoubApp.Models;
namespace AfraMahgoubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var questions = new AfraMahgoubApp.Models.questions();
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var Client = new HttpClient(handler))
            {
                Client.BaseAddress = new Uri("https://api.stackexchange.com/");
                //HTTP GET
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = Client.GetAsync("/2.2/questions?page=1&pagesize=50&order=desc&sort=hot&site=stackoverflow");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result; ;
                    questions = JsonConvert.DeserializeObject<AfraMahgoubApp.Models.questions>(readTask);
                    ViewData["questions"] = questions.items;

                }
            }
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }




        public ActionResult answers(int id)
        {
            var answeres = new AfraMahgoubApp.Models.answeres();
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var Client = new HttpClient(handler))
            {
                Client.BaseAddress = new Uri("https://api.stackexchange.com/");
                //HTTP GET
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = Client.GetAsync("/2.2/questions/" + id + "?&site=stackoverflow&filter=withbody");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result; ;
                    answeres = JsonConvert.DeserializeObject<AfraMahgoubApp.Models.answeres>(readTask);
                    ViewData["answeres"] = answeres.items;

                }
            }
            ViewBag.id = id;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }





}