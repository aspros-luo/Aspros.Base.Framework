using Infrastructure.Consul;
using System.Collections.Generic;

namespace Infrastructure.Consul.Core.Configuration
{
    public interface IKeyParser
    {
        IEnumerable<KVPair> Parse(KVPair key);
    }
}
