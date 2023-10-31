namespace JoinIt_Backend.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Zip> ZipCodes { get; set; } = new();
    }
}
