namespace RandomProject.Models
{
    public class Country
    {
        public List<string> Capital { get; set; }
        public int Population { get; set; }
        public Dictionary<string,string> Languages { get; set; }
        public Name Name { get; set; }
    }
}
