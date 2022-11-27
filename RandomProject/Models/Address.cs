namespace RandomProject.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string StreetAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public string BuildingNumber { get; set; }
        public string MailBox { get; set; }
        public string Community { get; set; }
        public string ZipCode { get; set; }
        public string Zip { get; set; }
        public string Postcode { get; set; }
        public string TimeZone { get; set; }
        public string StreetSuffix { get; set; }
        public string CitySuffix { get; set; }
        public string CityPrefix { get; set; }
        public string State { get; set; }
        public string StateAbbr { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FullAddress { get; set; }
    }
}
