var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var weatherDb = sql.AddDatabase("weatherDb");

builder.AddProject<Projects.WeatherDbBuilder>("weatherdbbuilder")
    .WithReference(weatherDb)
    .WaitFor(weatherDb);

builder.AddProject<Projects.Presentation_API>("presentation-api")
    .WithReference(weatherDb)
    .WaitFor(weatherDb);

builder.AddProject<Projects.Presentation_Server>("presentation-server")
    .WithReference(weatherDb)
    .WaitFor(weatherDb);

builder.Build().Run();
