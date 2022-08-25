using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using APIControllers.Models;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

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
            string jsonData;
            
            string filePath = configuration.GetSection("MySettings").GetSection("JsonFilePath").Value;
            using (FileStream fstr = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                
                // Read existing json data
                StreamReader sr = new StreamReader(fstr);
                jsonData = sr.ReadToEnd();
                sr.Dispose();
            }

            
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

            using (FileStream fstr = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                StreamWriter sw = new StreamWriter(fstr);
                sw.Write(jsonData);
                sw.Flush();
                sw.Dispose();
            }

            return person;
        }
    }
}
