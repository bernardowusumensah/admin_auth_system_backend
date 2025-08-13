using System;
using System.Collections.Generic;

namespace UserIdentity.Application.DTOs.Admin
{
    public class ServiceHealthDto
    {
        public string Service { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ServiceHealthResponse
    {
        public List<ServiceHealthDto> Services { get; set; } = new();
        public DateTime LastUpdated { get; set; }
    }

    public enum ServiceName
    {
        UserIdentity,
        Player,
        GameSettings,
        Orders
    }
}
