﻿using Microsoft.Extensions.Configuration;

namespace Infrastructure.Consul.Core.Configuration
{
    public static class ConsulConfigurationExtensions
    {
        public static IConfigurationBuilder AddJsonConsul(this IConfigurationBuilder builder,
            IConfigurationRegister configurationRegister)
        {
            var consul = new ConsulConfigurationSource(configurationRegister);
            configurationRegister.UpdateKeyParser(new JsonKeyValueParser());
            builder.Add(consul);
            return builder;
        }
    }
}
