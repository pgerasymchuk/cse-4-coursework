﻿namespace CycleShare.Server.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
