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
            try
            {
                // PART I

                Console.WriteLine("Type number between 5 and 20:");
                var userInput = ValidateUserInput();

                // connecting to api
                var randomAddressClient = InitHttpClient(new Uri("https://random-data-api.com/api/v2/"));
                var requestUri = $"addresses?size={userInput}";
                var responseAddresses = await randomAddressClient.GetAsync(requestUri);

                // pulling responses & displaying them to the screen
                responseAddresses.EnsureSuccessStatusCode();
                var addresses = await responseAddresses.Content.ReadFromJsonAsync<List<Address>>();
                DisplayAddresses(addresses);



                // PART II

                // connecting to api by country name
                var randomCountryClient = InitHttpClient(new Uri("https://restcountries.com/v3.1/name/"));
                var nameCountries = addresses.Select(x => x.Country);

                // creating list for countries to make sorting easier
                var allCountries = new List<Country>();

                // adding countries to list
                await InitAllCountries(randomCountryClient, nameCountries, allCountries);

                // sorting & displaying data
                var sortedCountries = allCountries.OrderByDescending(x => x.Population);
                DisplayData(sortedCountries);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayData(IOrderedEnumerable<Country> sortedCountries)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------");
            foreach (var sortedCountry in sortedCountries)
            {
                Console.WriteLine("Country: {0}\n", sortedCountry.Name.Common);
                if (sortedCountry.Capital?.Any() != true)
                {
                    Console.WriteLine("No capital!");
                }
                else
                {
                    Console.WriteLine("Capital: {0}", sortedCountry.Capital.First());
                }
                Console.WriteLine("Population: {0}", sortedCountry.Population);
                Console.WriteLine("Languages: {0}", string.Join(", ", sortedCountry.Languages.Select(x => x.Value)));
                Console.WriteLine("--------------------");
            }
            Console.WriteLine();
        }

        private static async Task InitAllCountries(HttpClient randomCountryClient, IEnumerable<string> nameCountries, List<Country> allCountries)
        {
            foreach (var nameCountry in nameCountries)
            {
                var responseCountry = await randomCountryClient.GetAsync(nameCountry);
                if (responseCountry.IsSuccessStatusCode)
                {

                    var countries = await responseCountry.Content.ReadFromJsonAsync<List<Country>>();
                    foreach (var country in countries)
                    {
                        allCountries.Add(country);
                    }
                }
                else
                {
                    Console.WriteLine(nameCountry);
                    Console.WriteLine("No information found!\n");
                }
            }
        }

        private static void DisplayAddresses(List<Address> addresses)
        {
            Console.WriteLine();
            foreach (var address in addresses)
            {
                Console.WriteLine(JToken.FromObject(address));
            }
        }

        private static HttpClient InitHttpClient(Uri uri)
        {
            return new HttpClient { BaseAddress = uri };
        }

        private static string ValidateUserInput()
        {
            // parsing user input to int and validating if its from 5 to 20
            string UserInput = "";
            string numberString = Console.ReadLine();
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

            return UserInput;
        }
    }
}
