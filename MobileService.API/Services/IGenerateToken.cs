using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.API.Services
{
    public interface IGenerateToken
    {
        string GenerateTokenMethod(string userId);
    }
}
