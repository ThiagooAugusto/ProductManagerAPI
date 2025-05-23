﻿using Microsoft.AspNetCore.Identity;

namespace ProductManagerAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string ? RefreshToken {  get; set; }
        public DateTime RefreshTokenExpireTime {  get; set; }
    }
}
