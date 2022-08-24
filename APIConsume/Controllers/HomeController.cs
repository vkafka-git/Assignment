using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //List<Reservation> reservationList = new List<Reservation>();
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync("https://localhost:44324/api/Reservation"))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
            //    }
            //}
            //return View(reservationList);
            return View();
        }

        public ViewResult GetPerson() => View();



        public ViewResult AddPerson() => View();

        [HttpPost]
        public async Task<IActionResult> AddPerson([Bind("Firstname,Lastname")] Person person)
        {
            Person receivedPerson = new Person();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44324/api/Reservation", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        receivedPerson = JsonConvert.DeserializeObject<Person>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
            
        }

       



        string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }
    }
}