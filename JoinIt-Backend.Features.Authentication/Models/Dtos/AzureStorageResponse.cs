using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class AzureStorageResponse
    {
        public string Message { get; set; } = string.Empty;

        public Uri? ImageUri { get; set; } = null;

    }
}
