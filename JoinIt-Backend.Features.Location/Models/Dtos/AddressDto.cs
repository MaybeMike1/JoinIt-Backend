using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class AddressDto
    {
        public Guid Guid { get; set; }
        public string StreetName { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public int? Floor { get; set; }

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;


        public static AddressDto MapToAddressDto(Address source, Zip zip)
        {
            return new AddressDto
            {
                Guid = source.Guid,
                PostalCode = zip.PostalCode,
                Country = zip.Country.Name,
                Floor = source.Floor,
                StreetName = source.StreetName,
                StreetNumber = source.StreetNumber,
            };
        }

        public static Address MapToAddress(AddressRequestDto source, Zip zip)
        {
            return new Address
            {
                StreetName = source.StreetName,
                Floor = source.Floor,
                StreetNumber = source.StreetNumber,
                Zip = zip,
            };
        }
    }
}
