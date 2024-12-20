var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Presentation_API>("presentation-api");

builder.AddProject<Projects.Presentation_Server>("presentation-server");

builder.Build().Run();
