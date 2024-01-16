using JoinIt_Backend.Features.Authentication.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Services
{
    public interface IAzureStorageService
    {
        Task<AzureStorageResponse> UploadProfilePicture(Guid userId);

        Task<AzureStorageResponse> GetProfilePicture(Guid userId);

    }
    public class AzureStorageService : IAzureStorageService
    {

        public Task<AzureStorageResponse> GetProfilePicture(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<AzureStorageResponse> UploadProfilePicture(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
