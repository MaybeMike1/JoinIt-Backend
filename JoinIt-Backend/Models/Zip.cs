namespace JoinIt_Backend.Models
{
    public class Zip
    {
        public int Id { get; set; }

        public string PostalCode { get; set; } = string.Empty;

        public Country Country { get; set; } = new();

        public List<Address> Addresses { get; set; } = new();

    }
}
