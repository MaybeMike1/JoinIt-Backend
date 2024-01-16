using JoinIt_Backend.Features.Location.Models.Dtos;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Services
{
    public interface ICountryService
    {
        Task<CountryResponseDto> GetCountryById(Guid id);

        Task<CountryResponseDto> GetAllCountries();
        
        Task<CountryResponseDto> GetCountryByName(string name);

        Task<CountryResponseDto> CreateNewCountry();

    }
    public class CountryService : ICountryService
    {
        private readonly DatabaseContext _databaseContext;

        public CountryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<CountryResponseDto> CreateNewCountry()
        {
            throw new NotImplementedException();
        }

        public async Task<CountryResponseDto> GetAllCountries()
        {
            try
            {
                var countries = await _databaseContext.Countries
                    .Include(x => x.ZipCodes)
                    .Select(x => new CountryDto { Country = x.Name, Id = x.Id, PostalCodes = x.ZipCodes.Select(x => x.PostalCode).ToList() })
                    .ToListAsync();

                return new CountryResponseDto
                {
                    Countries = countries,
                    Message = "Succesfully fetched all countries",
                    StatusCode = 200,
                    Country = null,
                };
            }
            catch (Exception e)
            {
                return new CountryResponseDto
                {
                    Country = null,
                    Message = "Failed to fetch countries" + e.StackTrace + e.Message,
                    Countries = new List<CountryDto>(),
                    StatusCode = 500,
                };
            }
        }

        public async Task<CountryResponseDto> GetCountryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CountryResponseDto> GetCountryByName(string name)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception e)
            {
                throw new NotImplementedException();
            }
        }
    }
}
