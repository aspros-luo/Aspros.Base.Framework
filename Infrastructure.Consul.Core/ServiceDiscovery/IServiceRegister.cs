﻿using System.Threading.Tasks;
using Infrastructure.Consul;

namespace Infrastructure.Consul.ServiceDiscovery
{
    public interface IServiceRegister
    {
        IConsulClient ConsulClient { get; set; }
        string ServiceAddress { get; }
        int ServicePort { get; }
        AgentServiceCheck AgentServiceCheck { get; set; }

        Task<bool> RegisterServiceAsync(string serviceName, string serviceId);
        Task<bool> RegisterServiceAsync(string serviceName);
    }
}
