
cd Infrastructure.Consul
dotnet pack --output ../release 
cd ../Infrastructure.Consul.Core
dotnet pack --output ../release 
cd ../Infrastructure.Domain.Core
dotnet pack --output ../release 
cd ../Infrastructure.Interfaces.Core
dotnet pack --output ../release 
cd ../Infrastructure.Ioc.Core
dotnet pack --output ../release 
cd ../release 
nuget push Infrastructure.Consul.0.0.2.nupkg 40F15705-8EE5-418F-9FD2-123094E212CB -source http://localhost:9001
nuget push Infrastructure.Consul.Core.1.0.2.nupkg 40F15705-8EE5-418F-9FD2-123094E212CB -source http://localhost:9001
nuget push Infrastructure.Domain.Core.1.0.5.nupkg 40F15705-8EE5-418F-9FD2-123094E212CB -source http://localhost:9001
nuget push Infrastructure.Interfaces.Core.1.0.2.nupkg 40F15705-8EE5-418F-9FD2-123094E212CB -source http://localhost:9001
nuget push Infrastructure.Ioc.Core.1.0.1.nupkg 40F15705-8EE5-418F-9FD2-123094E212CB -source http://localhost:9001