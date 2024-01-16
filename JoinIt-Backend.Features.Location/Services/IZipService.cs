using JoinIt_Backend.Features.Location.Models.Dtos;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Services
{
    public interface IZipService
    {
        Task<ZipResponseDto> GetZips();

        Task<ZipResponseDto> GetZipById(Guid id);

        Task<ZipResponseDto> GetZipsByCountry(string countryName);

        Task<ZipResponseDto> CreateNewZip();
    }

    public class ZipService : IZipService
    {

        private DatabaseContext _databaseContext;

        public ZipService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<ZipResponseDto> CreateNewZip()
        {
            throw new NotImplementedException();
        }

        public async Task<ZipResponseDto> GetZipById(Guid id)
        {
            try
            {
                var searchedZip = await _databaseContext.Zips.FirstOrDefaultAsync(x => x.Guid.Equals(id));
                if(searchedZip is null)
                {
                    throw new InvalidOperationException("Not found");
                }

                return new ZipResponseDto
                {
                    Message = "Succesfully fetch zips from data source.",
                    StatusCode = 200,
                    Zip = searchedZip,
                    Zips = new List<ZipDto>()
                };
            }
            catch(Exception e)
            {
                return new ZipResponseDto
                {
                    Message = "Failed to fetch data from data source.",
                    StatusCode = 500,
                    Zip = null,
                    Zips = new List<ZipDto>()
                };
            }
        }

        public async  Task<ZipResponseDto> GetZips()
        {
            try
            {
                var zips = await _databaseContext.Zips.Select(x => new ZipDto { Id = x.Guid, PostalCode = x.PostalCode}).ToListAsync();
                return new ZipResponseDto
                {
                    Message = "Succesfully fetch zips from data source.",
                    StatusCode = 200,
                    Zip = null,
                    Zips = zips
                };
            }catch(Exception e)
            {
                return new ZipResponseDto
                {
                    Message = "Failed to fetch data from data source.",
                    StatusCode = 500,
                    Zip = null,
                    Zips = new List<ZipDto>()
                };
            }
        }

        public async Task<ZipResponseDto> GetZipsByCountry(string countryName)
        {
            try
            {
                var searchedCountry = await _databaseContext.Countries.Include(x => x.ZipCodes).FirstOrDefaultAsync(x => x.Name.Equals(countryName));
                if(searchedCountry is null)
                {
                    throw new DirectoryNotFoundException();
                }

                var countryZips = searchedCountry.ZipCodes.Select(x => new ZipDto { Id = x.Guid, PostalCode = x.PostalCode }).ToList();

                return new ZipResponseDto
                {
                    Message = "Succesfully fetch zips from data source.",
                    StatusCode = 200,
                    Zip = null,
                    Zips = countryZips
                };
            }
            catch(Exception e)
            {
                return new ZipResponseDto
                {
                    Message = "Failed to fetch data from data source.",
                    StatusCode = 500,
                    Zip = null,
                    Zips = new List<ZipDto>()
                };
            }
        }
    }
}
