namespace EF3.SQLEntityModels
{
    public class Address
    {
        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public Address(string street, string zipCode, string city)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
        }
    }
}
