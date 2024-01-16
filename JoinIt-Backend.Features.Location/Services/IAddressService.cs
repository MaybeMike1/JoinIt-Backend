using JoinIt_Backend.Features.Location.Models.Dtos;
using JoinIt_Backend.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Services
{
    public interface IAddressService
    {
        Task<AddressResponseDto> GetAddresses();

        Task<AddressResponseDto> GetAddressById(Guid guid);

        Task<AddressResponseDto> CreateNewAddress(AddressRequestDto addressRequestDto);

    }
    public class AddressService : IAddressService
    {
        private readonly DatabaseContext _databaseContext;

        public AddressService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AddressResponseDto> CreateNewAddress(AddressRequestDto addressRequestDto)
        {
            try
            {
                var zipCode = _databaseContext.Zips.FirstOrDefault(x => x.Guid == addressRequestDto.ZipGuid);

                if(zipCode is null)
                {
                    return new AddressResponseDto
                    {
                        StatusCode = 400,
                        Address = null,
                        Addresses = null,
                        Message = "Was not able to find Zip associated with request."
                    };
                }

                var address = AddressDto.MapToAddress(addressRequestDto, zipCode);
                await _databaseContext.Addresses.AddAsync(address);
                return new AddressResponseDto
                {
                    Address = AddressDto.MapToAddressDto(address, zipCode),
                    Addresses = _databaseContext.Addresses.Select(x => AddressDto.MapToAddressDto(x, x.Zip)).ToList(),
                    Message = "Successfully created new address",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                return new AddressResponseDto
                {
                    StatusCode = 500,
                    Address = null,
                    Addresses = new List<AddressDto>(),
                    Message = "Something went wrong on the server"
                };
            }
        }

        public async Task<AddressResponseDto> GetAddressById(Guid guid)
        {
            try
            {
                var address = await _databaseContext.Addresses.Include(x => x.Zip).FirstOrDefaultAsync(x => x.Guid == guid);
                var country = await _databaseContext.Countries.ToListAsync();
                if(address is null)
                {
                    return new AddressResponseDto
                    {
                        StatusCode = 500,
                        Address = null,
                        Addresses = new List<AddressDto>(),
                        Message = "Not able to find adress with Guid" + guid,
                    };
                }

                return new AddressResponseDto
                {
                    StatusCode = 500,
                    Address = AddressDto.MapToAddressDto(address, address.Zip),
                    Addresses = _databaseContext.Addresses.Select(x => AddressDto.MapToAddressDto(x, x.Zip)).ToList(),
                    Message = "Success found address with guid" + guid
                };
            }
            catch (Exception)
            {

                return new AddressResponseDto
                {
                    StatusCode = 500,
                    Address = null,
                    Addresses = new List<AddressDto>(),
                    Message = "Something went wrong on the server"
                };
            }
        }

        public async Task<AddressResponseDto> GetAddresses()
        {
            try
            {
                var addresses = await _databaseContext.Addresses.Include(x => x.Zip).ToListAsync();
                var country = await _databaseContext.Countries.ToListAsync();
                return new AddressResponseDto
                {
                    StatusCode = 200,
                    Address = null,
                    Addresses = addresses.Select(x => AddressDto.MapToAddressDto(x, x.Zip)).ToList(),
                    Message = "Found addresses with count of" + addresses.Count,
                };
            }
            catch (Exception)
            {
                return new AddressResponseDto
                {
                    StatusCode = 500,
                    Address = null,
                    Addresses = new List<AddressDto>(),
                    Message = "Something went wrong on the server"
                };
            }
        }
    }
}
