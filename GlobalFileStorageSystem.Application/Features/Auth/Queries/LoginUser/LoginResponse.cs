﻿namespace GlobalFileStorageSystem.Application.Features.Auth.Queries.LoginUser
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
