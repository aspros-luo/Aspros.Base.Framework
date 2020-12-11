using Microsoft.Extensions.Configuration;

namespace Infrastructure.Consul.Core.Configuration
{
   public class ConsulConfigurationSource:IConfigurationSource
   {
       private readonly IConfigurationRegister _configurationRegister;

       public ConsulConfigurationSource(IConfigurationRegister configurationRegister)
       {
           _configurationRegister = configurationRegister;
       }

       public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
           return new ConsulConfigurationProvider(_configurationRegister);
        }

    }
}
