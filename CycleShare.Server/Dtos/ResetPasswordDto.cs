﻿namespace CycleShare.Server.Dtos
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
