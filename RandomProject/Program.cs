using RandomProject.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

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

                // base api address
                var client1 = new HttpClient();
                client1.BaseAddress = new Uri("https://restcountries.com/v3.1/name/");
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // selecting country from generated previous addresses
                var countries = content.Select(x => x.Country);
                Console.WriteLine();
                foreach (var country in countries)
                {
                    // connecting to api
                    var respone1 = client1.GetAsync(country);
                    var result1 = respone1.Result;

                    // validating if contry has informating and displaying proper message
                    if (result1.IsSuccessStatusCode)
                    {
                        Console.WriteLine(country + ":");

                        var content1 = await result1.Content.ReadFromJsonAsync<List<Country>>();
                        foreach (var showMe in content1)
                        {
                            if (showMe.Capital?.Any() != true)
                            {
                                Console.WriteLine("No capital!");
                            }
                            else
                            {
                                Console.WriteLine(showMe.Capital.First());
                            }                            
                            Console.WriteLine(showMe.Population);
                            Console.WriteLine(string.Join(", ", showMe.Languages.Select(x => x.Value)));
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(country);
                        Console.WriteLine("No information found!\n");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
