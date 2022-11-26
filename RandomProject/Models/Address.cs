namespace RandomProject.Models
{
    public class Address
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string city { get; set; }
        public string street_name { get; set; }
        public string street_address { get; set; }
        public string secondary_address { get; set; }
        public string building_number { get; set; }
        public string mail_box { get; set; }
        public string community { get; set; }
        public string zip_code { get; set; }
        public string zip { get; set; }
        public string postcode { get; set; }
        public string time_zone { get; set; }
        public string street_suffix { get; set; }
        public string city_suffix { get; set; }
        public string city_prefix { get; set; }
        public string state { get; set; }
        public string state_abbr { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string full_address { get; set; }
    }
}
