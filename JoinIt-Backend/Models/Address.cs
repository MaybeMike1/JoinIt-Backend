namespace JoinIt_Backend.Models
{
    public class Address
    {
        //public int Id { get; set; }

        //public Guid Guid { get; set; }

        //public int ActivityNumber { get; set; }

        //public List<T> Attendants { get; set; } = new();

        //public string Name { get; set; } = string.Empty;

        public int Id { get; set; }

        public string StreetName { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public Zip Zip { get; set; } = new();

        public int? Floor { get; set; }
    }
}
