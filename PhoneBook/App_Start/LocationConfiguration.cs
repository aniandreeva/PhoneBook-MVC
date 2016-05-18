using EntityFramework.BulkInsert.Extensions;
using Newtonsoft.Json;
using PhoneBook.Models;
using PhoneBook.Services.ModelsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;

namespace PhoneBook.App_Start
{
    public class LocationConfiguration
    {
        internal static void Configure()
        {
            List<Country> countries = new List<Country>();

            string jsonString = String.Empty;
            using (WebClient webClient = new WebClient())
            {
                jsonString = webClient.DownloadString("https://raw.githubusercontent.com/David-Haim/CountriesToCitiesJSON/master/countriesToCities.json");
            }

            Dictionary<string, string[]> locations = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString);

            Country country;
            foreach (var item in locations)
            {
                country = new Country();
                country.Name = item.Key;
                countries.Add(country);
            }


            // Insert Countries to DB
            CountriesServices countiesServices = new CountriesServices();
            countiesServices.SaveCollection(countries);

            List<City> cities = new List<City>();
            List<Country> allCountries = countiesServices.GetAll();

            foreach (var item in locations)
            {
                foreach (Country c in allCountries)
                {
                    if (c.Name==item.Key)
                    {
                        cities.AddRange(item.Value.Select(city => new City() { Name = city, CountryID = c.ID }));
                    }
                }
            }

            // Insert Cities to DB
            CitiesServices citiesServices = new CitiesServices();
            citiesServices.SaveCollection(cities);
        }
    }
}