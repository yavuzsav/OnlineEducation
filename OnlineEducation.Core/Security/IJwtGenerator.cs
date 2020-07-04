﻿using System.Threading.Tasks;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Core.Security
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
