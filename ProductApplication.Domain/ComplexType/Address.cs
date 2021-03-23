namespace ProductApplication.Domain.ComplexType
{
    public class Address
    {
        public string ZipCode { get; protected set; }
        public string Neighborhood { get; protected set; }
        public string Street { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string Number { get; protected set; }
        public string Complement { get; protected set; }

        protected Address() { }

        public Address(string zipCode, string neighborhood, string street, string city, string state, string number, string complement = null)
        {
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            Street = street;
            City = city;
            State = state;
            Number = number;
            Complement = complement;
        }
    }
}
