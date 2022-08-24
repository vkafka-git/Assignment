using System;
using System.Collections.Generic;
using APIControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using APIControllers.Service;

namespace APIControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        
        private IFileProcessor fileProcessor;
        private IWebHostEnvironment webHostEnvironment;

        public ReservationController(IWebHostEnvironment environment,IFileProcessor filePro)
        {
            webHostEnvironment = environment;
            fileProcessor = filePro;
        }

        [HttpPost]
        public Person Post([FromBody] Person res) =>
         fileProcessor.AddReservation(new Person
         {
             Firstname = res.Firstname,
             Lastname = res.Lastname
         });

        

        

       
        bool Authenticate()
        {
            var allowedKeys = new[] { "Secret@123", "Secret#12", "SecretABC" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowedKeys where t == key select t).Count();
            return count == 0 ? false : true;
        }

       
    }
}