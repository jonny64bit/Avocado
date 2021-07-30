using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avocado.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseQuickCreateAndMigrate
{
    /// <summary>
    /// This is just a quick and dirty method of fast prototyping entity framework schemas and migrations.
    /// </summary>
    class Program
    {
        //Set sql box location
        const string Host = "localhost";

        //Set the database user account here
        const string DatabaseName = "Avocado";
        const string User = "Avocado";
        const string Password = "psceAV9rjPvaUc5H";

        //Set sa password for sql box
        const string PasswordSA = "SqlServer2019";

        static async Task Main()
        {
            await SetupDatabase();

            var services = new ServiceCollection();
            services.AddDbContext<DAL>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer($"Server={Host};Database={DatabaseName};User Id={User};Password={Password};MultipleActiveResultSets=True");
            });

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<DAL>();
            await context.Database.MigrateAsync();
            await Seed(context);
        }

        static async Task SetupDatabase()
        {
            using (var connection = new SqlConnection($"Server={Host};User Id=sa;Password={PasswordSA};MultipleActiveResultSets=True"))
            {
                var sb = new List<string>
                {
                    "USE [master]",
                    "ALTER DATABASE " + DatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    "DROP DATABASE IF EXISTS " + DatabaseName,
                    "CREATE DATABASE " + DatabaseName,
                    "ALTER DATABASE " + DatabaseName + " SET CONTAINMENT = PARTIAL",
                    "USE " + DatabaseName,
                    $"CREATE USER {User} WITH PASSWORD = '{Password}'",
                    "exec sp_addrolemember 'db_owner', '" + User + "';"
                };

                await connection.OpenAsync();
                foreach (var query in sb)
                {
                    try
                    {
                        var command = new SqlCommand(query, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
            }
        }

        static async Task Seed(DAL context)
        {
            //Instead of seeding here you could do this in database migrations.
            //Really if its only data for testing/development i like to keep it out of migrations.

            var rawLines = new List<string>
            {
                "2344,Tommy,Test",
                "2233,Barry,Test",
                "8766,Sally,Test",
                "2345,Jerry,Test",
                "2346,Ollie,Test",
                "2347,Tara,Test",
                "2348,Tammy,Test",
                "2349,Simon,Test",
                "2350,Colin,Test",
                "2351,Gladys,Test",
                "2352,Greg,Test",
                "2353,Tony,Test",
                "2355,Arthur,Test",
                "2356,Craig,Test",
                "6776,Laura,Test",
                "4534,JOSH,TEST",
                "1234,Freya,Test",
                "1239,Noddy,Test",
                "1240,Archie,Test",
                "1241,Lara,Test",
                "1242,Tim,Test",
                "1243,Graham,Test",
                "1244,Tony,Test",
                "1245,Neville,Test",
                "1246,Jo,Test",
                "1247,Jim,Test",
                "1248,Pam,Test"
            };

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Accounts] ON");

                foreach (var rawLine in rawLines)
                {
                    var segments = rawLine.Split(',');
                    await context.Accounts.AddAsync(new()
                    {
                        Id = int.Parse(segments[0]),
                        FirstName = segments[1],
                        LastName = segments[2]
                    });
                }

                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Accounts] OFF");

                await transaction.CommitAsync();
            }
        }
    }
}