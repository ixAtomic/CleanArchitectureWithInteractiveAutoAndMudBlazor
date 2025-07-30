using Microsoft.Extensions.DependencyInjection;

// See https://aka.ms/new-console-template for more information
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

builder.AddSqlServerClient(connectionName: "weatherDb");

var serviceProvider = builder.Services.BuildServiceProvider();

Database.Upgrade(serviceProvider.GetRequiredService<SqlConnection>().ConnectionString);

builder.Build().Run();

public static class Database
{
    public static int Upgrade(string? _dbConnectionString)
    {
        EnsureDatabase.For.SqlDatabase(_dbConnectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(_dbConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            #if DEBUG
                        Console.ReadLine();
#endif
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}