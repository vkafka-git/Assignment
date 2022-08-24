using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using APIControllers.Models;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
namespace APIControllers.Service
{
    
    public interface IFileProcessor
    {
        Person AddReservation(Person person);
    }

    public abstract class FileProcessor: IFileProcessor
    {
       public abstract Person  AddReservation(Person person);
        
    }

    public class JsonFileProcessor : FileProcessor
    {
        private IConfiguration configuration;

        public JsonFileProcessor(IConfiguration config)
        {
            configuration = config;
        }

        public override Person AddReservation(Person person)
        {
            //var filePath = @"Data/Persons.json";
            string filePath = configuration.GetSection("MySettings").GetSection("JsonFilePath").Value;
            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(filePath);
            // De-serialize to object or create new list
            var personList = JsonConvert.DeserializeObject<List<Person>>(jsonData)
                                  ?? new List<Person>();

            // Add any new employees
            personList.Add(new Person()
            {
                Firstname = person.Firstname,
                Lastname = person.Lastname
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(personList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            return person;
        }
    }
}
