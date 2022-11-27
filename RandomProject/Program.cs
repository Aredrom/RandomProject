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
            Console.WriteLine("Type number between 5 and 20:");

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

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://random-data-api.com/api/v2/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string resource = "addresses";
            string sizeNumber = "?size=";

            var response = client.GetAsync($"{resource}{sizeNumber}{UserInput}");
            var result = response.Result;
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadFromJsonAsync<List<Address>>();
            Console.WriteLine();
            foreach (var address in content)
            {
                Console.WriteLine(JToken.FromObject(address));
            }
        }
    }
}
