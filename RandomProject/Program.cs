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
        }
    }
}
