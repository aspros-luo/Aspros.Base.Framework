using System;

namespace Infrastructure.Consul.Core.Configuration
{
    public class ConfigurationWatcher
    {
        public string CurrentValue { get; set; }
        public Action<string> CallBack { get; set; }
        public Action CallbackAllKeys { get; set; }
        public string KeyToWatch { get; set; }
    }
}
