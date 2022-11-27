﻿using RandomProject.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;

namespace RandomProject
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // PART I

            Console.WriteLine("Type number between 5 and 20:");

            try
            {
                string UserInput = "";
                string numberString = Console.ReadLine();

                // parsing user input to int and validating if its from 5 to 20
                int number = Int16.Parse(numberString);
                if (number >= 5 && number <= 20)
                {
                    UserInput = numberString;
                }
                else
                {
                    Console.WriteLine("Number must be in the range from 5 to 20!");
                    Environment.Exit(0);
                }

                // base api address
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://random-data-api.com/api/v2/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // variables for creating user defined request
                string resource = "addresses";
                string sizeNumber = "?size=";

                // connecting to api
                var response = client.GetAsync($"{resource}{sizeNumber}{UserInput}");
                var result = response.Result;
                result.EnsureSuccessStatusCode();

                // assigning given data to variable content and displaying on screen
                var content = await result.Content.ReadFromJsonAsync<List<Address>>();
                Console.WriteLine();
                foreach (var address in content)
                {
                    Console.WriteLine(JToken.FromObject(address));
                }



                // PART II


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
